using System;
using System.Collections.Generic;
using AFSPC.AstroStds.Models.SatelliteModel;

namespace AFSPC.AstroStds.Models.SuperEveModel
{
   [Serializable]
   public class SuperEveYAxis
   {
      public double DeltaPos { get; set; }
      public double DeltaT { get; set; }
      public double DeltaP_Radial { get; set; }
      public double DeltaP_Intrack { get; set; }
      public double DeltaP_Crosstrack { get; set; }
      public double DeltaVel { get; set; }
      public double DeltaV_Radial { get; set; }
      public double DeltaV_Intrack { get; set; }
      public double DeltaV_Crosstrack { get; set; }
      public double DeltaBeta { get; set; }
      public double DeltaHeight { get; set; }
      public double DeltaAngMom { get; set; }
      public double MhlnbsUVW { get; set; }
      public double MhlnbsHTB { get; set; }

      public SuperEveYAxis(EphemDeltas deltas) // long refSatKey, long predSatKey, double currDs50UTC, bool isInertialUVW)
      {
         DeltaPos = deltas.Pos_km;
         DeltaT = deltas.Time_sec;
         DeltaP_Radial = deltas.PosRadial_km;
         DeltaP_Intrack = deltas.PosIntrack_km;
         DeltaP_Crosstrack = deltas.PosCrossTrack_km;
         DeltaVel = deltas.Vel_kmps;
         DeltaV_Radial = deltas.VelRadial_kmps;
         DeltaV_Intrack = deltas.VelIntrack_kmps;
         DeltaV_Crosstrack = deltas.VelCrossTrack_kmps;
         DeltaBeta = deltas.Beta_deg;
         DeltaHeight = deltas.Height_km;
         DeltaAngMom = deltas.AngMom_deg;
         MhlnbsUVW = deltas.MhlnbsUVW;
         MhlnbsHTB = deltas.MhlnbsHTB;
      }

      public enum YAxisSelection
      {
         DeltaPos,
         DeltaT,
         DeltaP_Radial,
         DeltaP_Intrack,
         DeltaP_Crosstrack,
         DeltaVel,
         DeltaV_Radial,
         DeltaV_Intrack,
         DeltaV_Crosstrack,
         DeltaBeta,
         DeltaHeight,
         DeltaAngMom,
         MhlnbsUVW,
         MhlnbsHTB

      }


      public static List<double> GetYValues(YAxisSelection axisSelection, List<SuperEveYAxis> yAxisValuesList)
      {
         List<double> yValues = new List<double>(yAxisValuesList.Count);
         foreach (SuperEveYAxis eveAxisValue in yAxisValuesList)
         {
            if (axisSelection == YAxisSelection.DeltaPos)
               yValues.Add(eveAxisValue.DeltaPos);
            else if (axisSelection == YAxisSelection.DeltaT)
               yValues.Add(eveAxisValue.DeltaT);
            else if (axisSelection == YAxisSelection.DeltaP_Radial)
               yValues.Add(eveAxisValue.DeltaP_Radial);
            else if (axisSelection == YAxisSelection.DeltaP_Intrack)
               yValues.Add(eveAxisValue.DeltaP_Intrack);
            else if (axisSelection == YAxisSelection.DeltaP_Crosstrack)
               yValues.Add(eveAxisValue.DeltaP_Crosstrack);
            else if (axisSelection == YAxisSelection.DeltaP_Radial)
               yValues.Add(eveAxisValue.DeltaP_Radial);
            else if (axisSelection == YAxisSelection.DeltaVel)
               yValues.Add(eveAxisValue.DeltaVel);
            else if (axisSelection == YAxisSelection.DeltaV_Radial)
               yValues.Add(eveAxisValue.DeltaV_Radial);
            else if (axisSelection == YAxisSelection.DeltaV_Intrack)
               yValues.Add(eveAxisValue.DeltaV_Intrack);
            else if (axisSelection == YAxisSelection.DeltaV_Crosstrack)
               yValues.Add(eveAxisValue.DeltaV_Crosstrack);
            else if (axisSelection == YAxisSelection.DeltaBeta)
               yValues.Add(eveAxisValue.DeltaBeta);
            else if (axisSelection == YAxisSelection.DeltaHeight)
               yValues.Add(eveAxisValue.DeltaHeight);
            else if (axisSelection == YAxisSelection.DeltaAngMom)
               yValues.Add(eveAxisValue.DeltaAngMom);
            else if (axisSelection == YAxisSelection.MhlnbsUVW)
               yValues.Add(eveAxisValue.MhlnbsUVW);
            else if (axisSelection == YAxisSelection.MhlnbsHTB)
               yValues.Add(eveAxisValue.MhlnbsHTB);
            else
               yValues = null;
         }

         return yValues;
      }
   }
}
