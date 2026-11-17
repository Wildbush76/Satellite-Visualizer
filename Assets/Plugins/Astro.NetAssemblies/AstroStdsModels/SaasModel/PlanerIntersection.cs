using AFSPC.AstroStds.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFSPC.AstroStds.Models.SaasModel
{
   [Serializable]
   public class PlanerIntersection
   {
      /// <summary>
      /// time of intersection in days since 1950 UTC
      /// </summary>
      public DaysSince50UTC Ds50UTC { get; set; }

      /// <summary>
      /// satellite inclination(deg)
      /// </summary>
      public double Inclination_deg { get; set; }

      /// <summary>
      /// satellite latitude(deg) at the time of the intersection
      /// </summary>
      public double Latitude_deg { get; set; }

      /// <summary>
      /// satellite longitude(deg) at the time of the intersection
      /// </summary>
      public double Longitude_deg { get; set; }

      /// <summary>
      /// satellite height(km) at the time of the intersection
      /// </summary>
      public double Height_km { get; set; }

      /// <summary>
      /// time of flight (min) since launch time
      /// </summary>
      public double Tof_min { get; set; }

      /// <summary>
      /// coplaner angle (deg)
      /// </summary>
      public double CoplanarAngle_deg { get; set; }

      public PlanerIntersection()
      {

      }

      public PlanerIntersection(double[] xa_intxn)
      {
         this.Ds50UTC = new DaysSince50UTC(xa_intxn[SaasWrapper.XA_INTXN_DS50UTC]);
         this.Inclination_deg = xa_intxn[SaasWrapper.XA_INTXN_INCLI];
         this.Latitude_deg = xa_intxn[SaasWrapper.XA_INTXN_LAT];
         this.Longitude_deg = xa_intxn[SaasWrapper.XA_INTXN_LON];
         this.Height_km = xa_intxn[SaasWrapper.XA_INTXN_HEIGHT];
         this.Tof_min = xa_intxn[SaasWrapper.XA_INTXN_TOF];
         this.CoplanarAngle_deg = xa_intxn[SaasWrapper.XA_INTXN_COPLANG];
      }

   }
}
