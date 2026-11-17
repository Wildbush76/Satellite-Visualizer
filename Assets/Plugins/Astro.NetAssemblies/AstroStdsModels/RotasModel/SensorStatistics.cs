using AFSPC.AstroStds.Models.RotasModel;
using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Wrappers;
using LinqStatistics;
using System.Collections.Generic;
using System.Linq;
using AFSPC.AstroStds.Models.SensorModel;
using AFSPC.AstroStds.Models.Properties;
using System.Security.Cryptography;
using System;

namespace AFSPC.AstroStds.Models.RotasModel
{
   [Serializable]
   public class SensorStatistics
   {
      public SensorStatistics()
      {
      }
      public SensorStatistics(int senNum, List<Residuals> allResids, double startTimeDS50UTC, double endTimeDS50UTC, int minNumResidsToCalcBS)
      {
         //Filter the residuals to be within the start/end times, and the correct sensor number
         List<Residuals> resids = allResids.Where(x => x.ObsTimeDs50UTC < endTimeDS50UTC && x.ObsTimeDs50UTC > startTimeDS50UTC && senNum == x.SenNum).ToList<Residuals>();

         if (resids.Count < minNumResidsToCalcBS)
         {
            return;
         }

         this.StartTimeDS50UTC = startTimeDS50UTC;
         this.EndTimeDS50UTC = endTimeDS50UTC;

         // Input is all the residuals for a sensor that should be used to create a Sensor stats record
         this.SenNum = senNum;
         Sensor sensor = new Sensor(senNum);
         this.ObsType = ObsWrapper.ObsTypeIToC(resids.FirstOrDefault().ObsType);

         byte[] senDesc = new byte[512];
         SensorWrapper.SensorGetLocField(senNum, SensorWrapper.XF_SENLOC_DESC, senDesc);
         this.SenName = Utility.BytesToStringRTrim(senDesc);
         // Find all the residuals for the sensor and add to their corresponding lists in order to later calculate sigma and bias

         // Calculate RightAscension & Declination statistics
         this.RA_deg = new Stats(resids.Select(x => x.RADelta_deg).ToList());
         this.Dec_deg = new Stats(resids.Select(x => x.DecDelta_deg).ToList());

         // Calculate Azimuth/Elevation/Range statistics            
         this.Az_deg = new Stats(resids.Select(x => x.AzDelta_deg).ToList());
         this.El_deg = new Stats(resids.Select(x => x.ElDelta_deg).ToList());
         this.Range_Km = new Stats(resids.Select(x => x.RangeDelta_km).ToList());
         this.RgRt_kmps = new Stats(resids.Select(x => x.RngRtDelta_kmps).ToList());

         this.Tdoa_ns = new Stats(resids.Select(x => x.TdoaDelta_ns).ToList());
         this.Fdoa_Hz = new Stats(resids.Select(x => x.FdoaDelta_Hz).ToList());

         // Calculate time statistics
         this.TimeBias = new Stats(resids.Select(x => x.DeltaT_min).ToList());

         // Find number of obs used
         this.NumObs = resids.Count();

         // Find the number of sats tracked
         List<int> distinctSats = resids.Select(x => x.SatNum).Distinct().ToList();
         this.NumSats = distinctSats.Count;

         // find the time of the first and last observation used
         this.FirstObTimeDS50UTC = resids.Min(x => x.ObsTimeDs50UTC.Ds50UTC);
         this.LastObTimeDS50UTC = resids.Max(x => x.ObsTimeDs50UTC.Ds50UTC);
         this.Span = resids.Range(x => x.ObsTimeDs50UTC.Ds50UTC);
      }


      public int SenNum { get; set; }

      public Stats RA_deg { get; set; }

      public Stats Dec_deg { get; set; }

      public Stats Az_deg { get; set; }

      public Stats El_deg { get; set; }

      public Stats Tdoa_ns { get; set; }

      public Stats Fdoa_Hz { get; set; }
      public Stats Range_Km { get; set; }
      public Stats RgRt_kmps { get; set; }

      public int NumObs { get; set; }

      public int OrigNumObs { get; set; }

      public int NumSats { get; set; }

      public double FirstObTimeDS50UTC { get; set; }

      public double LastObTimeDS50UTC { get; set; }

      public double StartTimeDS50UTC { get; set; }

      public double EndTimeDS50UTC { get; set; }

      public double Span { get; set; }

      public Stats TimeBias { get; set; }

      public char ObsType { get; set; }

      public double RejFactor { get; set; }

      public string SenName { get; set; }

      public static List<SensorStatistics> CalcSensorBS(List<Residuals> allResids)
      {
         double[] rejFactorArray = new double[0];
         if (allResids.Count > 0)
         {
            double calibStartTime = allResids.Min(x => x.ObsTimeDs50UTC.Ds50UTC);
            double calibEndTime = allResids.Max(x => x.ObsTimeDs50UTC.Ds50UTC);
            double calibrationSpan = calibEndTime - calibStartTime;
            return CalcSensorBS(rejFactorArray, allResids, calibStartTime, calibEndTime, calibrationSpan, 1);
         }
         return new List<SensorStatistics>();

      }

      public static List<SensorStatistics> CalcSensorBS(double[] rejFactorArray, List<Residuals> allResids, double calibStartTime, double calibEndTime, double calibrationSpan, int minResToCalcBS)
      {
         List<SensorStatistics> fullSenStatsList = new List<SensorStatistics>();
         SensorStatistics mySenStat;
         List<Residuals> currResids;
         List<Residuals> senResids;

         int[] senKeys = allResids.Select(x => x.SenNum).Distinct<int>().ToArray<int>();

         foreach (int senKey in senKeys)
         {
            double endTime = calibEndTime;
            double startTime;
            int origNumObs;
            senResids = allResids.Where(x => x.SenNum == senKey).ToList<Residuals>();

            // This while loop uses the calibration span to create increments between the start and end times 
            while (endTime > calibStartTime)
            {
               if (calibStartTime > endTime - calibrationSpan)
               {
                  startTime = calibStartTime;
               }
               else
               {
                  startTime = endTime - calibrationSpan;
               }

               currResids = senResids.Where(x => x.ObsTimeDs50UTC <= endTime && x.ObsTimeDs50UTC > startTime).ToList<Residuals>(); ;
               origNumObs = currResids.Count();

               if (origNumObs >= minResToCalcBS)
               {

                  mySenStat = new SensorStatistics(senKey, currResids, startTime, endTime, minResToCalcBS);
                  mySenStat.OrigNumObs = origNumObs;
                  mySenStat.RejFactor = 0.0;
                  fullSenStatsList.Add(mySenStat);

                  //	for (int i = 0; i < rejFactorArray.Length; i++)
                  //	{
                  //		if (currResids.Count >= minResToCalcBS)
                  //		{
                  //			rf = rejFactorArray[i];

                  //			// Set min and max values to determine which residuals to include in calculations
                  //			raMax = rf * mySenStat.RaSigma + mySenStat.RaBias;
                  //			raMin = mySenStat.RaBias - (rf * mySenStat.RaSigma);
                  //			decMax = mySenStat.DecBias + (rf * mySenStat.DecSigma);
                  //			decMin = mySenStat.DecBias - (rf * mySenStat.DecSigma);

                  //			List<Residuals> cullResids = new List<Residuals>();

                  //			// For each residual, remove if it is outside the min/max values
                  //			foreach (Residuals currResid in currResids)
                  //			{
                  //				if (currResid.RADelta_deg > raMax || currResid.DecDelta_deg > decMax || currResid.RADelta_deg < raMin || currResid.DecDelta_deg < decMin)
                  //				{
                  //					// We have already rejected twice in the Calculate Residual class so this would be our i + 3 rejection test (since i is zero indexed).
                  //					//ObsCollection.UpdateResidualRejection(currResid.Id, true, i + 3);
                  //					cullResids.Add(currResid);
                  //				}
                  //			}

                  //			foreach (Residuals cull in cullResids)
                  //			{
                  //				currResids.Remove(cull);
                  //			}

                  //			if (i == rejFactorArray.Length - 1)
                  //			{
                  //				validCalib++;
                  //			}

                  //			mySenStat = new SensorStatistics(senKey, currResids, startTime, endTime, minResToCalcBS);
                  //			mySenStat.RejFactor = rf;

                  //			if (mySenStat.NumObs >= minResToCalcBS)
                  //			{
                  //				mySenStat.OrigNumObs = origNumObs;
                  //				mySenStat.EndTimeDS50UTC = endTime;
                  //				mySenStat.StartTimeDS50UTC = startTime;
                  //				fullSenStatsList.Add(mySenStat);
                  //			}

                  //		}
                  //	}
               }

               endTime = startTime;
            }

            senResids.Clear();
         }

         return fullSenStatsList;
      }
   }
}