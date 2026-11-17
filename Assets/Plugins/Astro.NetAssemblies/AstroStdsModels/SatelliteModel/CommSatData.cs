using System;
using System.Windows.Forms;
using AFSPC.AstroStds.Models.SatelliteModel;
using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Utilities;
using System.ComponentModel;
using System.Collections.Generic;
using static AFSPC.AstroStds.Models.SatelliteModel.Satellite;
using static AFSPC.AstroStds.Wrappers.DllMainWrapper;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class CommSatData
   {
      // satellite's number
      public int SatNum { get; set; }

      // satellite's international designator
      public string SatName { get; set; }

      // satellite's orbital elset type 
      public DllMainWrapper.EltTypeEnum EltType { get; set; }


      // satellite's revolution number
      public int RevNum { get; set; }

      // satellite's epoch in days since 1950, UTC
      public DaysSince50UTC EpochDs50UTC { get; set; }


      // satellite's either SGP4 bStar (1/er) or SP bTerm (m2/kg)
      public double BField { get; set; }


      // satellite's element number
      public int ElsetNum { get; set; }

      // satellite's inclination (deg)                         
      public double Inclination_deg { get; set; }

      // satellite's right ascension of the asending node (deg) 
      public double RAAN_deg { get; set; }

      // satellite's eccentricity (unitless)
      public double Eccentricity { get; set; }

      // satellite's argument of perigee (deg) 
      public double Omega_deg { get; set; }

      // satellite's mean anomaly (deg)
      public double MnAnom_deg { get; set; }

      // satellite's mean motion (revs/day)
      public double MnMotion_rpd { get; set; }

      // satellite's period (min)
      public double Period_min { get; set; }

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

      public CommSatData()
      {

      }


      public CommSatData(long satKey)
      {
         int errCode;
         int satNum, ephType, revNum, elsetNum;
         double epochDs50UTC, bField, incli, node, eccen, omega, mnAnomaly, mnMotion, period, perigeeHt, apogeeHt, perigee, apogee, A;
         byte[] satName = new byte[8];

         errCode = SatStateWrapper.SatStateGetSatDataAll(satKey, out satNum, satName, out ephType, out revNum,
                                                                    out epochDs50UTC, out bField, out elsetNum, out incli, out node,
                                                                    out eccen, out omega, out mnAnomaly, out mnMotion, out period,
                                                                    out perigeeHt, out apogeeHt, out perigee, out apogee, out A);

         if (errCode != 0)
            throw new KeyNotFoundException("SpVecWrapper.SpVecDataToArray", satKey);

         this.SatNum          = satNum;
         this.SatName         = Utilities.Utility.BytesToString(satName).Trim('\0').Trim();
         this.EltType         = (EltTypeEnum)ephType;
         this.RevNum          = revNum;
         this.EpochDs50UTC    = epochDs50UTC;
         this.BField          = bField;
         this.ElsetNum        = elsetNum;
         this.Inclination_deg = incli;
         this.RAAN_deg        = node;
         this.Eccentricity    = eccen;
         this.Omega_deg       = omega;
         this.MnAnom_deg      = mnAnomaly;
         this.MnMotion_rpd    = mnMotion;
         this.Period_min      = period;
         this.PerigeeHt_km    = perigeeHt;
         this.ApogeeHt_km     = apogeeHt;
         this.Perigee_km      = perigee;
         this.Apogee_km       = apogee;
         this.A_km            = A;

      }
   }
}
