using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFSPC.AstroStds.Wrappers;
using System.ComponentModel;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class SatelliteParameters
   {
      public enum SatelliteCategory
      {
         [Description("Synchronous")]
         SATCAT_SYNCHRONOUS = 1,
         [Description("Deep space (not synchronous)")]
         SATCAT_DEEPSPACE = 2,
         [Description("Decaying (perigee height below 575 km)")]
         SATCAT_DECAYING = 3,
         [Description("Routine (everything else)")]
         SATCAT_ROUTINE = 4,
      };


      // satellite's epoch in days since 1950, UTC
      public DaysSince50UTC EpochDs50UTC { get; set; }

      // satellite's mean anomaly (deg)
      public double MnAnom_deg { get; set; }

      // satellite's right ascension of the asending node (deg) 
      public double RAAN_deg { get; set; }

      // satellite's argument of perigee (deg) 
      public double Omega_deg { get; set; }

      // satellite's period (min)
      public double Period_min { get; set; }

      // satellite's eccentricity (unitless)
      public double Eccentricity { get; set; }

      // satellite's inclination (deg)                         
      public double Inclination_deg { get; set; }

      // satellite's mean motion (revs/day)
      public double MnMotion_rpd { get; set; }

      // satellite's either SGP4 bStar (1/er) or SP bTerm (m2/kg)
      public double BField { get; set; }

      // satellite's perigee height (km) 
      public double PerigeeHt_km { get; set; }

      // satellite's apogee height (km) 
      public double ApogeeHt_km { get; set; }

      // satellite's perigee (km)
      public double Perigee_km { get; set; }

      // satellite's apogee (km)
      public double Apogee_km { get; set; }

      // satellite's semi major axis (km)
      public double A_km { get; set; }

      // satellite's category (Synchronous, Deep space, Decaying, Routine)
      public SatelliteCategory SatCat { get; set; }

      // satellite's center of mass offset (m)
      public double CMOffset_m { get; set; }

      // satellite's longitude east subpoint (deg) - only for synchronous satellite
      public double LonESubPt { get; set; }

      // satellite's longitude drift rate (deg East/day) - only for synchronous satellite
      public double LonDriffRate_dpd { get; set; }

      // satellite's omega rate of change (deg/day)
      public double OmegaDot_dpd { get; set; }

      // satellite's nodal precession rate (deg/day)
      public double RAANDot_dpd { get; set; }

      // satellite's nodal period (min)
      public double NodalPeriod_min { get; set; }

      // satellite's nodal crossing time prior to its epoch (ds50UTC)
      public double NodalXTime { get; set; }

      // satellite is GEO: 0=no, 1=yes    
      public bool IsGeoSat { get; set; }

      // satellite's relative energy - only for GOBS
      public double RelEnergy { get; set; }

      // satellite's number
      public int SatNum { get; set; }

      // satellite's orbital elset type 
      public DllMainWrapper.EltTypeEnum EltType { get; set; }


      // satellite's propagation type - see DllMainWrapper.PROPTYPE_xxx for available values
      public int PropType { get; set; }

      // satellite's element number
      public int ElsetNum { get; set; }


      public SatelliteParameters()
      {
      }

      public SatelliteParameters(double[] xa_satparm)
      {
         PopulateSatParmArray(xa_satparm);
      }

      private void PopulateSatParmArray(double[] xa_satparm)
      {
         this.EpochDs50UTC = new DaysSince50UTC(xa_satparm[ElOpsWrapper.XA_SATPARM_EPOCHUTC]);
         this.MnAnom_deg = xa_satparm[ElOpsWrapper.XA_SATPARM_MNANOM];
         this.RAAN_deg = xa_satparm[ElOpsWrapper.XA_SATPARM_NODE];
         this.Omega_deg = xa_satparm[ElOpsWrapper.XA_SATPARM_OMEGA];
         this.Period_min = xa_satparm[ElOpsWrapper.XA_SATPARM_PERIOD];
         this.Eccentricity = xa_satparm[ElOpsWrapper.XA_SATPARM_ECCEN];
         this.Inclination_deg = xa_satparm[ElOpsWrapper.XA_SATPARM_INCLI];
         this.MnMotion_rpd = xa_satparm[ElOpsWrapper.XA_SATPARM_MNMOTION];
         this.BField = xa_satparm[ElOpsWrapper.XA_SATPARM_BFIELD];
         this.PerigeeHt_km = xa_satparm[ElOpsWrapper.XA_SATPARM_PERIGEEHT];
         this.ApogeeHt_km = xa_satparm[ElOpsWrapper.XA_SATPARM_APOGEEHT];
         this.Perigee_km = xa_satparm[ElOpsWrapper.XA_SATPARM_PERIGEE];
         this.Apogee_km = xa_satparm[ElOpsWrapper.XA_SATPARM_APOGEE];
         this.A_km = xa_satparm[ElOpsWrapper.XA_SATPARM_A];
         this.SatCat = (SatelliteCategory)(int)xa_satparm[ElOpsWrapper.XA_SATPARM_SATCAT];
         this.CMOffset_m = xa_satparm[ElOpsWrapper.XA_SATPARM_CMOFFSET];
         this.LonESubPt = xa_satparm[ElOpsWrapper.XA_SATPARM_LONE];
         this.LonDriffRate_dpd = xa_satparm[ElOpsWrapper.XA_SATPARM_DRIFT];
         this.OmegaDot_dpd = xa_satparm[ElOpsWrapper.XA_SATPARM_OMEGADOT];
         this.RAANDot_dpd = xa_satparm[ElOpsWrapper.XA_SATPARM_RADOT];
         this.NodalPeriod_min = xa_satparm[ElOpsWrapper.XA_SATPARM_NODALPRD];
         this.NodalXTime = xa_satparm[ElOpsWrapper.XA_SATPARM_NODALX];
         this.IsGeoSat = (int)xa_satparm[ElOpsWrapper.XA_SATPARM_ISGEO] == 1;
         this.RelEnergy = xa_satparm[ElOpsWrapper.XA_SATPARM_RELENERGY];
         this.SatNum = (int)xa_satparm[ElOpsWrapper.XA_SATPARM_SATNUM];
         this.EltType = (DllMainWrapper.EltTypeEnum)xa_satparm[ElOpsWrapper.XA_SATPARM_OET];
         this.PropType = (int)xa_satparm[ElOpsWrapper.XA_SATPARM_PROPTYPE];
         this.ElsetNum = (int)xa_satparm[ElOpsWrapper.XA_SATPARM_ELSETNUM];
      }

      public SatelliteParameters(long satKey)
      {
         double[] xa_satparm = new double[32];

         int errCode = ElOpsWrapper.GetSatParameters(satKey, xa_satparm);
         if (errCode != 0)
            throw new KeyNotFoundException("ElOpsWrapper.GetSatParameters", satKey);
         PopulateSatParmArray(xa_satparm);
      }
   }
}

