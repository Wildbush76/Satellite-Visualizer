using AFSPC.AstroStds.Models.SatelliteModel;
using AFSPC.AstroStds.Models.SatStateModel;
using AFSPC.AstroStds.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFSPC.AstroStds.Models
{
   [Serializable]
   public class SpPropOut
   {
      public int ErrCode { get; set; }
      public DaysSince50UTC Ds50UTC { get; set; }
      public double Mse { get; set; }
      public double Ds50UT1 { get; set; }
      public double Ds50TAI { get; set; }
      public double Ds50ET { get; set; }
      public int RevNum { get; set; }
      public int Nterms { get; set; }
      public bool IsSpectr { get; set; }
      public bool HasCovMtx { get; set; }

      public int CovType { get; set; }
      public double[][] CovMtx { get; set; }

      public SatellitePosition PosJ2K { get; set; }
      public SatelliteVelocity VelJ2K { get; set; }
      public SatellitePosition PosECI { get; set; }
      public SatelliteVelocity VelECI { get; set; }
      public SatelliteLLH LLH { get; set; }
      public SatelliteAcceleration AccJ2K { get; set; }
      public SatelliteAcceleration AccECI { get; set; }

      public SpPropOut()
      {
      }

      private void PoPulateSpOut(double[] xa_spOut)
      {
         this.Ds50UTC = xa_spOut[SpPropWrapper.XA_SPOUT_UTC];
         this.Ds50TAI = xa_spOut[SpPropWrapper.XA_SPOUT_TAI];
         this.Ds50UT1 = xa_spOut[SpPropWrapper.XA_SPOUT_UT1];
         this.Ds50ET  = xa_spOut[SpPropWrapper.XA_SPOUT_ET];
         this.Mse     = xa_spOut[SpPropWrapper.XA_SPOUT_MSE];
         this.RevNum  = (int)xa_spOut[SpPropWrapper.XA_SPOUT_REVNUM];
         this.Nterms  = (int)xa_spOut[SpPropWrapper.XA_SPOUT_NTERMS];
         this.IsSpectr = (int)xa_spOut[SpPropWrapper.XA_SPOUT_ISSPECTR] == 1;
         this.HasCovMtx = (int)xa_spOut[SpPropWrapper.XA_SPOUT_HASCOVMTX] == 1;
         this.CovType = (int)xa_spOut[SpPropWrapper.XA_SPOUT_COVTYPE];

         int i = SpPropWrapper.XA_SPOUT_COVMTX;
         this.CovMtx = new double[6][];
         for (int row = 0; row < 6; row++)
         {
            this.CovMtx[row] = new double[6];
            for (int col = 0; col < 6; col++)
            {
               this.CovMtx[row][col] = xa_spOut[i];
               i++;
            }
         }

         double[] arr3 = new double[3];
         Array.Copy(xa_spOut, SpPropWrapper.XA_SPOUT_J2KPOSX, arr3, 0, 3);
         this.PosJ2K = new SatellitePosition(arr3);
         Array.Copy(xa_spOut, SpPropWrapper.XA_SPOUT_J2KVELX, arr3, 0, 3);
         this.VelJ2K = new SatelliteVelocity(arr3);
         Array.Copy(xa_spOut, SpPropWrapper.XA_SPOUT_ECIPOSX, arr3, 0, 3);
         this.PosECI = new SatellitePosition(arr3);
         Array.Copy(xa_spOut, SpPropWrapper.XA_SPOUT_ECIVELX, arr3, 0, 3);
         this.VelECI = new SatelliteVelocity(arr3);
         Array.Copy(xa_spOut, SpPropWrapper.XA_SPOUT_LAT, arr3, 0, 3);
         this.LLH = new SatelliteLLH(arr3);
         Array.Copy(xa_spOut, SpPropWrapper.XA_SPOUT_J2KACCX, arr3, 0, 3);
         this.AccJ2K = new SatelliteAcceleration(arr3);
         Array.Copy(xa_spOut, SpPropWrapper.XA_SPOUT_ECIACCX, arr3, 0, 3);
         this.AccECI = new SatelliteAcceleration(arr3);
      }

      public SpPropOut(long satKey, int timeType, double time, int covType = 1) //SpPropWrapper.XF_COVMTX_UVW_DATE)
      {
         double[] xa_spOut = new double[128];

         this.ErrCode = SpPropWrapper.SpPropAll(satKey, timeType, time, covType, xa_spOut);
         if (this.ErrCode != 0) return;

         PoPulateSpOut(xa_spOut);
      }
   }
}
