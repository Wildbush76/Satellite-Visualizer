using AFSPC.AstroStds.Wrappers;
using System;

namespace AFSPC.AstroStds.Models.ElCompModel
{
   [Serializable]
   public class ElCompResults
   {
      public int PriSatNum { get; set; }
      public int SecSatNum { get; set; }  
      public double Inclination_deg { get; set; }
      public double Node_deg { get; set; }
      public double Eccentricity { get; set; }
      public double Omega_deg { get; set; }
      public double MnAnom_deg { get; set; }
      public double MnMotion_revspd { get; set; }
      public double EpochDs50UTC { get; set; }
      public double PerigeeHt_km { get; set; }
      public double Period_min { get; set; }
      public double TNode_min { get; set; }
      public double LongE_deg { get; set; }
      public double AngMom_deg { get; set; }
      public string ASTAT { get; set; }

      public ElCompResults()
      {
      }

      private void PopulateElComp(double[] xa_elcomp)
      {
         this.Inclination_deg = xa_elcomp[ElCompWrapper.XA_ELCOM_INCLI];
         this.Node_deg = xa_elcomp[ElCompWrapper.XA_ELCOM_NODE];
         this.Eccentricity = xa_elcomp[ElCompWrapper.XA_ELCOM_E];
         this.Omega_deg = xa_elcomp[ElCompWrapper.XA_ELCOM_OMEGA];
         this.MnAnom_deg = xa_elcomp[ElCompWrapper.XA_ELCOM_MNANOM];
         this.MnMotion_revspd = xa_elcomp[ElCompWrapper.XA_ELCOM_MNMOTION];
         this.EpochDs50UTC = xa_elcomp[ElCompWrapper.XA_ELCOM_EPOCH];

         this.PerigeeHt_km = xa_elcomp[ElCompWrapper.XA_ELCOM_PHT];
         this.Period_min = xa_elcomp[ElCompWrapper.XA_ELCOM_PERIOD];
         this.TNode_min = xa_elcomp[ElCompWrapper.XA_ELCOM_TNODE];
         this.LongE_deg = xa_elcomp[ElCompWrapper.XA_ELCOM_LONGE];
         this.AngMom_deg = xa_elcomp[ElCompWrapper.XA_ELCOM_WDOT];
      }

      public ElCompResults(double[] xa_elcomp)
      {
         PopulateElComp(xa_elcomp);
      }

      public ElCompResults(long priSatKey, long secSatKey, double[] xa_elcomp, int astat)
      {
         PopulateElComp(xa_elcomp);
         this.PriSatNum = ElOpsWrapper.SatNumOf(priSatKey);
         this.SecSatNum = ElOpsWrapper.SatNumOf(secSatKey);
         if (astat == ElCompWrapper.ELCOM_ASTAT_CLOSE)
            this.ASTAT = "CLOSE";
         else if (astat == ElCompWrapper.ELCOM_ASTAT_FULL)
            this.ASTAT = "FULL";
         else if (astat == ElCompWrapper.ELCOM_ASTAT_MAYBE)
            this.ASTAT = "MAYBE";
         else if(astat == ElCompWrapper.ELCOM_ASTAT_NONE)
            this.ASTAT = "NONE";
      }
   }
}

