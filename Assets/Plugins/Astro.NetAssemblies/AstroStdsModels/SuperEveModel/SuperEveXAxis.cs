using System;
using System.Collections.Generic;
using AFSPC.AstroStds.Models.SatelliteModel;
using AFSPC.AstroStds.Wrappers;

namespace AFSPC.AstroStds.Models.SuperEveModel
{
   [Serializable]
   public class SuperEveXAxis
   {
      public double Ds50UTC { get; set; }
      public double XDate { get; set; }
      public double TrueAnomaly { get; set; }
      public double ArgOfLatitude { get; set; }
      public double Mse { get; set; }

      public SuperEveXAxis(EphemDeltas deltas)
      {
         XDate = deltas.XDate;
         TrueAnomaly = deltas.TrueAnomaly_deg;
         ArgOfLatitude = deltas.ArgOfLatitude_deg;
         Mse = deltas.Mse;
         Ds50UTC = deltas.EphemTimeDs50UTC;
      }

      public SuperEveXAxis(double ds50UTC, double[] refPos, double[] refVel, double xDate, double mse)
      {
         Ds50UTC = ds50UTC;

         // X-Axis data fields
         // Convert to Keplerian elements
         double[] refKep = new double[6];
         AstroFuncWrapper.PosVelToKep(refPos, refVel, refKep);

         TrueAnomaly = AstroFuncWrapper.CompTrueAnomaly(refKep) % 360;
         ArgOfLatitude = (TrueAnomaly + refKep[AstroFuncWrapper.XA_KEP_OMEGA]) % 360;
         XDate = xDate;
         Mse = mse;
      }


      public enum XAxisSelection
      {
         TrueAnomaly_deg,
         ArgOfLatitude_deg,
         XDate,
         Mse,
         Ds50UTC,
      }

      public static List<double> GetXValues(XAxisSelection axisSelection, List<SuperEveXAxis> xAxisValuesList)
      {
         List<double> xValues = new List<double>(xAxisValuesList.Count);
         foreach (SuperEveXAxis eveAxisValue in xAxisValuesList)
         {
            if (axisSelection == XAxisSelection.TrueAnomaly_deg)
               xValues.Add(eveAxisValue.TrueAnomaly);
            else if (axisSelection == XAxisSelection.ArgOfLatitude_deg)
               xValues.Add(eveAxisValue.ArgOfLatitude);
            else if (axisSelection == XAxisSelection.XDate)
               xValues.Add(eveAxisValue.XDate);
            else if (axisSelection == XAxisSelection.Mse)
               xValues.Add(eveAxisValue.Mse);
            else if (axisSelection == XAxisSelection.Ds50UTC)
               xValues.Add(eveAxisValue.Ds50UTC);
            else
               return null;
         }

         return xValues;
      }
   }
}
