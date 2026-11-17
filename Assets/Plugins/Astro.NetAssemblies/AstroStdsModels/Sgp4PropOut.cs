using AFSPC.AstroStds.Models.SatelliteModel;
using AFSPC.AstroStds.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFSPC.AstroStds.Models
{
   [Serializable]
   public class Sgp4PropOut
   {
      public int ErrCode { get; set; }
      public DaysSince50UTC Ds50UTC { get; set; }
      public double Mse { get; set; }
      public SatellitePosition PosECI { get; set; }
      public SatelliteVelocity VelECI { get; set; }
      public SatelliteLLH LLH { get; set; }
      public int RevNum { get; set; }
      public double NodalPeriod_min { get; set; }
      public double Apogee_km { get; set; }
      public double Perigee_km { get; set; }
      public double Mn_A_km { get; set; }
      public double Mn_Eccentricity { get; set; }
      public double Mn_Inclination_deg { get; set; }
      public double Mn_MnAnom_deg { get; set; }
      public double Mn_Node_deg { get; set; }
      public double Mn_Omega_deg { get; set; }
      public double Osc_A_km { get; set; }
      public double Osc_Eccentricity { get; set; }
      public double Osc_Inclination_deg { get; set; }
      public double Osc_OscAnom_deg { get; set; }
      public double Osc_Node_deg { get; set; }
      public double Osc_Omega_deg { get; set; }



      public Sgp4PropOut(long satKey, double ds50UTC)
      {
         double[] xa_Sgp4Out = new double[64];
         ErrCode = Sgp4PropWrapper.Sgp4PropAll(satKey, Sgp4PropWrapper.SGP4_TIMETYPE_DS50UTC, ds50UTC, xa_Sgp4Out);
         if (ErrCode != 0) return;

         this.Ds50UTC = new DaysSince50UTC(xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_DS50UTC]);
         this.Mse = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_MSE];
         this.PosECI = new SatellitePosition(new double[] { xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_POSX], xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_POSY], xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_POSZ] });
         this.VelECI = new SatelliteVelocity(new double[] { xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_VELX], xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_VELY], xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_VELZ] });
         this.LLH = new SatelliteLLH(new double[] { xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_LAT], xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_LON], xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_HEIGHT] });
         this.RevNum = (int)xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_REVNUM];
         this.NodalPeriod_min = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_NODALPER];
         this.Apogee_km = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_APOGEE];
         this.Perigee_km = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_PERIGEE];
         this.Mn_A_km = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_MN_A];
         this.Mn_Eccentricity = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_MN_E];
         this.Mn_Inclination_deg = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_MN_INCLI];
         this.Mn_MnAnom_deg = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_MN_MA];
         this.Mn_Node_deg = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_MN_NODE];
         this.Mn_Omega_deg = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_MN_OMEGA];
         this.Osc_A_km = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_OSC_A];
         this.Osc_Eccentricity = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_OSC_E];
         this.Osc_Inclination_deg = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_OSC_INCLI];
         this.Osc_OscAnom_deg = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_OSC_MA];
         this.Osc_Node_deg = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_OSC_NODE];
         this.Osc_Omega_deg = xa_Sgp4Out[Sgp4PropWrapper.XA_SGP4OUT_OSC_OMEGA];

      }
   }
}

