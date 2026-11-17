using System;
using System.Text;
using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Utilities;
using System.ComponentModel;
using AFSPC.AstroStds.Models.SatelliteModel;
using AFSPC.AstroStds.Models.DcModel;
using AFSPC.AstroStds.Models.ElsetModel;
using AFSPC.AstroStds.Models.SensorModel;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows.Forms;
using System.Security.Permissions;

namespace AFSPC.AstroStds.Models.ObsModel
{
   [Serializable]
   public class Obs
   {
      [Browsable(false)]
      public long ObsKey { get; set; }             // Observation ID key

      public char SecClass { get; set; }           // Obs classification
      public int SatNum { get; set; }             // "CaveTag" Satellite number as determined by 1SPCS  
      public int SenNum { get; set; }              // Sensor number (or Sensor number #1 in tdoa/fdoa obs)
      public int SenNum2 { get; set; }              // Sensor number #2 in tdoa/fdoa obs
      [Browsable(false)]
      public DaysSince50UTC ObsTimeDs50UTC { get; set; }   // Obs time in days since 1950 UTC

      public string DDD_ddd { get; set; }
      public string YYDDDHHMMSS { get; set; }
      public string DTGForm2 { get; set; }

      public double Elevation_deg { get; set; }        // Elevation (deg)
      public double Declination_deg { get; set; }      // Declination (deg)
      public double Azimuth_deg { get; set; }          // Azimuth_deg (deg)
      public double RA_deg { get; set; }               // Right ascension

      [Browsable(false)]
      public double ElOrDec { get; set; }
      [Browsable(false)]
      public double AzOrRA { get; set; }

      public double Range_km { get; set; }            // Slant range (km)
      public double RangeRate_kmps { get; set; }        // Slant range rate (km/sec)
      public double ElRate_degps { get; set; }           // Elevation rate (deg/sec)
      public double AzRate_degps { get; set; }           // Azimuth rate (deg/sec)
      public double DecRate_degps { get; set; }           // Declincation rate (deg/sec)
      public double RARate_degps { get; set; }           // RA rate (deg/sec)

      public double RangeAccel_kmps2 { get; set; }       // Range acceleration (km/sec^2)
      public char ObsType { get; set; }            // Observation type
      public int ObsTypeInt { get; set;}           // Observation type in integer
      public int TrackInd { get; set; }            // Track position indicator
      public int ASTAT { get; set; }               // Association status asssigned by SPADOC
      public int SiteTag { get; set; }             // The satellite number assigned by reporting sensor as it came into CMAS
      public int SpadocTag { get; set; }           // The last satellite number assigned by SPADOC
      public SatelliteEFG EFGPos {get; set;}
      public SatelliteVelocity EFGVel { get; set; } // For TTY ob type 7 only
      public SatellitePosition XYZPos { get; set; }

      public bool AbApplied { get; set; }    //Aberration was applied to original observation

      [Browsable(false)]
      public string B3Card { get; set; }

      [Browsable(false)]
      public string TTYCard { get; set; }

      public int YrOfEqnx { get; set; }

      [Browsable(false)]
      public double[] Pos = new double[3];
      [Browsable(false)]
      public double[] Vel = new double[3];

      
      [Browsable(false)]
      public int ErrCode { get; set; }


      public double DeltaRange_m { get; set; }
      public double DeltaRangeSigma_m { get; set; }
      public double DeltaRangeRate_mps { get; set; }
      public double DeltaRangeRateSigma_mps { get; set; }
      public double Delay1_m { get; set; }
      public double Delay2_m { get; set; }
      public long Freq_Hz { get; set; }
      public DaysSince50UTC ProcTimeDs50UTC { get; set; }   // process time in days since 1950 UTC
      public double SNR { get; set; }

      public Obs()
      {

      }

      /// <summary>
      /// Create an obs instance using the loaded data associated with the obsKey from the obs dll
      /// </summary>
      /// <param name="obsKey">obs key</param>
      public Obs(long obsKey)
      {
         double[] xa_obs = new double[64];

         ErrCode = ObsWrapper.ObsDataToArray(obsKey, xa_obs);
         if (ErrCode != 0)
            return;

         PopulateDataFrArray(xa_obs);
         this.ObsKey = obsKey;
      }

      /// <summary>
      /// Create an obs instance using the data parsed from the provided B3/1-lineTTY/CSV card. Note that the obs data won't be loaded to the obs dll
      /// </summary>
      /// <param name="b3Card">a B3-formated obs card</param>
      public Obs(string b3Card, bool convertObs = true)
      {
         // parse obs data without coverting it to TEME of Date
         if(!convertObs)
         {
            double[] xa_obs = new double[64];
            ObsWrapper.ObsParse(b3Card, "", xa_obs);
            PopulateDataFrArray(xa_obs);
            return;
         }

         char secClass, obsType;
         int satNum, senNum, trackInd, astat, siteTag, spadocTag;
         double obsTimeDs50UTC, elOrDec, azOrRA, range, rangeRate, elRate, azRate, rangeAccel;
         double[] pos = new double[3];

         // parse obs data and converting it to TEME of Date if neccessary
         ErrCode = ObsWrapper.ObsB3Parse
             (b3Card, out secClass, out satNum, out senNum, out obsTimeDs50UTC, out elOrDec,
             out azOrRA, out range, out rangeRate, out elRate, out azRate, out rangeAccel,
             out obsType, out  trackInd, out astat, out siteTag, out spadocTag, pos);

         if (ErrCode != 0)
            return;


         this.ObsKey = 0;
         this.SecClass = secClass;
         this.SatNum = satNum;
         this.SenNum = senNum;
         this.ObsTimeDs50UTC = obsTimeDs50UTC;
         this.DDD_ddd = ObsTimeDs50UTC.Dtg17Str.Substring(5); // TimeFuncWrapper.UTCToDtg17Str(ObsTimeDs50UTC).Substring(5);
         this.YYDDDHHMMSS = ObsTimeDs50UTC.Dtg23Str; // TimeFuncWrapper.UTCToDtg23Str(ObsTimeDs50UTC);
         this.DTGForm2 = ObsTimeDs50UTC.ToDTGForm2();
         this.ElOrDec = elOrDec;
         this.AzOrRA = azOrRA;
         this.Pos = pos;
         this.YrOfEqnx = 0; // the ObsB3Parse routine will convert whatever frame to TEME of Date
         this.ObsType = obsType;

         if (obsType == '1' || obsType == '2' || obsType == '3' || obsType == '4' || obsType == '8' || obsType == 'M')
         {
            this.Elevation_deg = elOrDec;
            this.Azimuth_deg = azOrRA;
            this.Declination_deg = 0;
            this.RA_deg = 0;
         }
         else if (obsType == '5' || obsType == '9' || obsType == 'O')
         {
            this.Elevation_deg = 0;
            this.Azimuth_deg = 0;
            this.Declination_deg = elOrDec;
            this.RA_deg = azOrRA;
         }
         else
         {
            this.Elevation_deg = 0;
            this.Azimuth_deg = 0;
            this.Declination_deg = 0;
            this.RA_deg = 0;
         }

         this.Range_km = range;
         this.RangeRate_kmps = rangeRate;
         this.ElRate_degps = elRate;
         this.AzRate_degps = azRate;
         this.RangeAccel_kmps2 = rangeAccel;
         this.ObsTypeInt = (int)Char.GetNumericValue(obsType);
         this.TrackInd = trackInd;
         this.ASTAT = astat;
         this.SiteTag = siteTag;
         this.SpadocTag = spadocTag;

         if (obsType == '8' || obsType == '9' || obsType == 'M' || obsType == 'O')
            this.EFGPos = new SatelliteEFG(pos);
         else if (obsType == 'V' || obsType == 'P')
            this.XYZPos = new SatellitePosition(pos);

         this.B3Card = ToB3String();
         this.TTYCard = ToTTYString();
      }


      /// <summary>
      /// Create an obs instance using the data parsed from TTY (one or two line). Note that the obs data won't be loaded to the obs dll
      /// </summary>
      /// <param name="ttyLine1">Line #1 of TTY obs</param>
      /// <param name="ttyLine2">Line #2 of TTY obs type 4, 7, 8, 9</param>
      public Obs(string ttyLine1, string ttyLine2)
      {
         // parse obs data without coverting it to TEME of Date
         double[] xa_obs = new double[64];
         ObsWrapper.ObsParse(ttyLine1, ttyLine2, xa_obs);
         PopulateDataFrArray(xa_obs);
      }

      /// <summary>
      /// Create an obs instance using the data provided in the input obs array. Note that the obs data won't be loaded to the obs dll
      /// </summary>
      /// <param name="xa_obs">array contains obs data</param>
      public Obs(double[] xa_obs)
      {
         PopulateDataFrArray(xa_obs);
      }

      private void PopulateDataFrArray(double[] xa_obs)
      {
         char obsType;
         double[] pos = new double[3];


         this.SecClass =  DllMainWrapper.SecClassIntToChar((int)xa_obs[ObsWrapper.XA_OBS_SECCLASS]);
         this.SatNum = (int)xa_obs[ObsWrapper.XA_OBS_SATNUM];
         this.SenNum = (int)xa_obs[ObsWrapper.XA_OBS_SENNUM];
         this.ObsTimeDs50UTC = xa_obs[ObsWrapper.XA_OBS_DS50UTC];
         this.DDD_ddd = ObsTimeDs50UTC.Dtg17Str.Substring(5); // TimeFuncWrapper.UTCToDtg17Str(ObsTimeDs50UTC).Substring(5);
         this.YYDDDHHMMSS = ObsTimeDs50UTC.Dtg23Str; // TimeFuncWrapper.UTCToDtg23Str(ObsTimeDs50UTC);
         this.DTGForm2 = ObsTimeDs50UTC.ToDTGForm2();

         obsType = ObsWrapper.ObsTypeIToC((int)xa_obs[ObsWrapper.XA_OBS_OBSTYPE]);
         this.ObsType = obsType;

         if (obsType == 'R') //RF obs
         {
            //this.SenNum  = (int)xa_obs[ObsWrapper.XA_OBS_SENNUM1];
            this.SenNum2 = (int)xa_obs[ObsWrapper.XA_OBS_SENNUM2];
            this.DeltaRange_m = xa_obs[ObsWrapper.XA_OBS_DELRNG];
            this.DeltaRangeSigma_m = xa_obs[ObsWrapper.XA_OBS_DELRNGSIGM];
            this.DeltaRangeRate_mps = xa_obs[ObsWrapper.XA_OBS_DELRRATE];
            this.DeltaRangeRateSigma_mps = xa_obs[ObsWrapper.XA_OBS_DELRRATESIGM];
            this.Delay1_m = xa_obs[ObsWrapper.XA_OBS_DELAY1];
            this.Delay2_m = xa_obs[ObsWrapper.XA_OBS_DELAY2];
            this.Freq_Hz = (long)xa_obs[ObsWrapper.XA_OBS_FREQ];
            this.ProcTimeDs50UTC = xa_obs[ObsWrapper.XA_OBS_PROCTIME];
            this.SNR = xa_obs[ObsWrapper.XA_OBS_SNR];
            this.ObsTypeInt = (int)xa_obs[ObsWrapper.XA_OBS_OBSTYPE];
            return;
         }



         this.ElOrDec = xa_obs[ObsWrapper.XA_OBS_ELORDEC]; ;
         this.AzOrRA = xa_obs[ObsWrapper.XA_OBS_AZORRA]; ;
         this.YrOfEqnx = (int)xa_obs[ObsWrapper.XA_OBS_YROFEQNX];

         if (obsType == '1' || obsType == '2' || obsType == '3' || obsType == '4' || obsType == '8' || obsType == 'M')
         {
            this.Elevation_deg = xa_obs[ObsWrapper.XA_OBS_ELORDEC];
            this.Azimuth_deg = xa_obs[ObsWrapper.XA_OBS_AZORRA];
            this.Declination_deg = 0;
            this.RA_deg = 0;
         }
         else if (obsType == '5' || obsType == '9' || obsType == 'O')
         {
            this.Elevation_deg = 0;
            this.Azimuth_deg = 0;
            this.Declination_deg = xa_obs[ObsWrapper.XA_OBS_ELORDEC];
            this.RA_deg = xa_obs[ObsWrapper.XA_OBS_AZORRA];
         }
         else
         {
            this.Elevation_deg = 0;
            this.Azimuth_deg = 0;
            this.Declination_deg = 0;
            this.RA_deg = 0;
         }

         this.Range_km = xa_obs[ObsWrapper.XA_OBS_RANGE];
         this.RangeRate_kmps = xa_obs[ObsWrapper.XA_OBS_RANGERATE];
         this.ElRate_degps = xa_obs[ObsWrapper.XA_OBS_ELRATE];
         this.AzRate_degps = xa_obs[ObsWrapper.XA_OBS_AZRATE];
         this.RangeAccel_kmps2 = xa_obs[ObsWrapper.XA_OBS_RANGEACCEL];
         this.ObsTypeInt = ObsWrapper.ObsTypeCToI(obsType); 
         this.TrackInd = (int)xa_obs[ObsWrapper.XA_OBS_TRACKIND];
         this.ASTAT = (int)xa_obs[ObsWrapper.XA_OBS_ASTAT];
         this.SiteTag = (int)xa_obs[ObsWrapper.XA_OBS_SITETAG];
         this.SpadocTag = (int)xa_obs[ObsWrapper.XA_OBS_SPADOCTAG];
         this.Pos = new double[] { xa_obs[ObsWrapper.XA_OBS_POSX], xa_obs[ObsWrapper.XA_OBS_POSY], xa_obs[ObsWrapper.XA_OBS_POSZ] };
         this.Vel = new double[] { xa_obs[ObsWrapper.XA_OBS_VELX], xa_obs[ObsWrapper.XA_OBS_VELY], xa_obs[ObsWrapper.XA_OBS_VELZ] };
         this.EFGPos = new SatelliteEFG();
         this.XYZPos = new SatellitePosition();


         if (obsType == '7' || obsType == '8' || obsType == '9' || obsType == 'M' || obsType == 'O' || obsType == '4')
            this.EFGPos = new SatelliteEFG(this.Pos);
         else if (obsType == 'V' || obsType == 'P')
            this.XYZPos = new SatellitePosition(this.Pos);

         if (obsType == '7' || obsType == '4')
            this.EFGVel = new SatelliteVelocity(this.Vel);

         this.B3Card = ToB3String(xa_obs);
         this.TTYCard = ToTTYString(xa_obs);
      }



      public Obs(char secClass, int satNum, int senNum, double obsTimeDs50UTC, double elOrDec,
             double azOrRA, double range, double rangeRate, double elRate, double azRate, double rangeAccel,
             char obsType, int  trackInd, int astat, int siteTag, int spadocTag, double[] pos)
      {
         this.SecClass = secClass;
         this.SatNum = satNum;
         this.SenNum = senNum;
         this.ObsTimeDs50UTC = obsTimeDs50UTC;
         this.ElOrDec = elOrDec;
         this.AzOrRA = azOrRA;
         this.YrOfEqnx = 0;

         if (obsType == '1' || obsType == '2' || obsType == '3' || obsType == '4' || obsType == '8' || obsType == 'M')
         {
            this.Elevation_deg = elOrDec;
            this.Azimuth_deg = azOrRA;
            this.Declination_deg = 0;
            this.RA_deg = 0;
         }
         else if (obsType == '5' || obsType == '9' || obsType == 'O')
         {
            this.Elevation_deg = 0;
            this.Azimuth_deg = 0;
            this.Declination_deg = elOrDec;
            this.RA_deg = azOrRA;
         }
         else
         {
            this.Elevation_deg = 0;
            this.Azimuth_deg = 0;
            this.Declination_deg = 0;
            this.RA_deg = 0;
         }

         this.Range_km = range;
         this.RangeRate_kmps = rangeRate;
         this.ElRate_degps = elRate;
         this.AzRate_degps = azRate;
         this.RangeAccel_kmps2 = rangeAccel;
         this.ObsType = obsType;
         this.ObsTypeInt = (int)Char.GetNumericValue(obsType);
         this.TrackInd = trackInd;
         this.ASTAT = astat;
         this.SiteTag = siteTag;
         this.SpadocTag = spadocTag;

         if (obsType == '8' || obsType == '9' || obsType == 'M' || obsType == 'O')
            this.EFGPos = new SatelliteEFG(pos);
         else if (obsType == 'V' || obsType == 'P')
            this.XYZPos = new SatellitePosition(pos);

         this.B3Card = ToB3String();
         this.TTYCard = ToTTYString();
         
      }


      public bool UpdateObsDataToDll()
      {
         return true;
      }

      /// <summary>
      /// Return the time in ds50UTC extracted from the input B3 card
      /// </summary>
      /// <param name="b3Card">a B3-formated obs card</param>
      /// <returns></returns>
      public static double GetB3ObsTimeDs50UTC(string b3Card)
      {
         StringBuilder dtg = new StringBuilder(15);
         dtg.Append(b3Card.Substring(9, 11));
         dtg.Append(".");
         dtg.Append(b3Card.Substring(20, 3));

         return TimeFuncWrapper.DTGToUTC(dtg.ToString());
      }


      public string ToB3String()
      {
         byte[] b3Card = new byte[512];

         char obsType = this.ObsType;

         if (obsType == '1' || obsType == '2' || obsType == '3' || obsType == '4' || obsType == '8' || obsType == 'M')
         {
            ObsWrapper.ObsFieldsToB3Card(this.SecClass, this.SatNum, this.SenNum, this.ObsTimeDs50UTC.Ds50UTC, this.Elevation_deg, this.Azimuth_deg, this.Range_km, this.RangeRate_kmps,
                             this.ElRate_degps, this.AzRate_degps, this.RangeAccel_kmps2, this.ObsType, this.TrackInd, this.ASTAT, this.SiteTag, this.SpadocTag, this.Pos, b3Card);

         }
         else if (obsType == '5' || obsType == '9' || obsType == 'O')
         {
            ObsWrapper.ObsFieldsToB3Card(this.SecClass, this.SatNum, this.SenNum, this.ObsTimeDs50UTC.Ds50UTC, this.Declination_deg, this.RA_deg, this.Range_km, this.RangeRate_kmps,
                             this.ElRate_degps, this.AzRate_degps, this.RangeAccel_kmps2, this.ObsType, this.TrackInd, this.ASTAT, this.SiteTag, this.SpadocTag, this.Pos, b3Card);
         }

         return Utility.BytesToStringLRTrim(b3Card);
      }

      public string ToB3String(double[] xa_obs)
      {
         byte[] b3Card = new byte[512];
         byte[] emptyLine = new byte[512];
         ObsWrapper.ObsArrToLines(xa_obs, ObsWrapper.OBSFORM_B3, b3Card, emptyLine);

         return Utility.BytesToStringLRTrim(b3Card);
      }

      public string ToTTYString()
      {
         byte[] ttyCard1 = new byte[512];
         byte[] ttyCard2 = new byte[512];
         ObsWrapper.ObsFieldsToTTYCard(this.SecClass, this.SatNum, this.SenNum, this.ObsTimeDs50UTC.Ds50UTC, this.ElOrDec, this.AzOrRA, this.Range_km, this.RangeRate_kmps, 
                                      this.ElRate_degps, this.AzRate_degps, this.RangeAccel_kmps2, this.ObsType, this.Pos, ttyCard1, ttyCard2);
         return Utility.BytesToStringLRTrim(ttyCard1) + (ttyCard2[0] == ' ' ? "" : ("\r\n" + Utility.BytesToStringLRTrim(ttyCard2)));
      }

      public string ToTTYString(double[] xa_obs)
      {
         byte[] ttyCard1 = new byte[512];
         byte[] ttyCard2 = new byte[512];
         ObsWrapper.ObsArrToLines(xa_obs, ObsWrapper.OBSFORM_TTY, ttyCard1, ttyCard2);
         return Utility.BytesToStringLRTrim(ttyCard1) + (ttyCard2[0] == ' ' ? "" : ("\r\n" + Utility.BytesToStringLRTrim(ttyCard2)));
      }

      public string ToRFString(double[] xa_obs)
		{
         byte[] rfCard = new byte[512];
         byte[] emptyLine = new byte[512];
         ObsWrapper.ObsArrToLines(xa_obs, ObsWrapper.OBSFORM_B3, rfCard, emptyLine);
         return Utility.BytesToStringLRTrim(rfCard);
      }
      // Convert obstype character symbol to obstype integer representation

      public long LoadToDll()
      {
         double[] extArr = new double[128];

         char obsType = this.ObsType;

         if (obsType == '1' || obsType == '2' || obsType == '3' || obsType == '4' || obsType == '8' || obsType == 'M')
         {
            this.ElOrDec = this.Elevation_deg;
            this.AzOrRA = this.Azimuth_deg;
         }
         else if (obsType == '5' || obsType == '9' || obsType == 'O')
         {
            this.ElOrDec = this.Declination_deg;
            this.AzOrRA = this.RA_deg;
         }
         else
         {
            this.ElOrDec = 0;
            this.AzOrRA = 0;
         }

         long obsKey = ObsWrapper.ObsAddFrFields(this.SecClass, this.SiteTag, this.SenNum, this.ObsTimeDs50UTC.Ds50UTC, this.ElOrDec, this.AzOrRA, this.Range_km, this.RangeRate_kmps,
                                      this.ElRate_degps, this.AzRate_degps, this.RangeAccel_kmps2, this.ObsType, this.TrackInd, this.ASTAT, this.SiteTag, this.SpadocTag, this.Pos, this.Vel, extArr);

         return obsKey;
      }

      public string ToHHMMSSS()
      {
         return Utility.DecToHHMMSSS_Str(this.RA_deg);
      }
   }
}
