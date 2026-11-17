
using AFSPC.AstroStds.Models.SatelliteModel;
using AFSPC.AstroStds.Wrappers;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   public static class BlueSerivce
   {
      public const double EARTHROTATIONRATE = 360.98564735; // deg/day

      public enum BlueActionEnum
      {
         update,
         rotate
      }

      /// <summary>
      /// Add new elset using BLUE (updated epoch and node)
      /// </summary>
      /// <param name="satKey">satKey of the elset that needs to be updated</param>
      /// <param name="launchDs50UTC">launch time in days since 1950, UTC</param>
      /// <returns>true: added successfully; false: failed</returns>
      public static bool AddNewSatellite(long satKey, DaysSince50UTC launchDs50UTC, BlueActionEnum action)
      {
         Satellite sat = null;
         if (action == BlueActionEnum.update)
            sat = UpdateSatToLaunchTime(satKey, launchDs50UTC);
         else if (action == BlueActionEnum.rotate)
            sat = RotateSatToNewEpoch(satKey, launchDs50UTC);
         if (sat != null && sat.ErrCode == 0)
         {
            satKey = sat.AddToDll();
            if (satKey > 0)
               return true; // succeeded 
            else
            {
               ErrorMessages.SaveErrorMessage();
               return false; // failed to add new sat
            }
         }

         return false;
      }


      private static Satellite UpdateSatToLaunchTime(long satKey, DaysSince50UTC launchDs50UTC)
      {
         // pulling satellite data from the dll
         Satellite sat = new Satellite(satKey);
         if (sat.ErrCode != 0)
            return sat;

         // update satellite data to the new launch time
         double node = sat.ClassElts.RAAN_deg;
         double epochDayEnd = sat.EpochDs50UTC.DayOfYearInt + 1;
         double remainder = epochDayEnd - sat.EpochDs50UTC.DayOfYearReal;
         DaysSince50UTC newEpoch = launchDs50UTC - remainder;

         if (newEpoch <= sat.EpochDs50UTC)
         {
            string newErrMessage = string.Format("{0} {1}: Resulting epoch is before the input epoch. Try adjusting the launch time to be later ", sat.SatNum, sat.EpochDs50UTC.Dtg15Str);
            ErrorMessages.SaveErrorMessage(newErrMessage);
            sat.ErrCode = 1;
            return sat;
         }

         double rotationSinceLaunch = (newEpoch.Ds50UTC - sat.EpochDs50UTC.Ds50UTC) * EARTHROTATIONRATE;
         double newRightAscension = (node + rotationSinceLaunch) % 360.0;

         // update new epoch time
         sat.EpochDs50UTC = newEpoch;

         // update node
         sat.ClassElts.RAAN_deg = newRightAscension;

         return sat;
      }

      private static Satellite RotateSatToNewEpoch(long satKey, DaysSince50UTC newEpoch)
      {
         // pulling satellite data from the dll
         Satellite sat = new Satellite(satKey);
         if (sat.ErrCode != 0)
            return sat;

         // update satellite data to the new launch time
         double node = sat.ClassElts.RAAN_deg;

   
         /*
         double cosI = Math.Cos(sat.Inclination_deg * Constants.DEG2RAD);
         double A = sat.A_km / 6378.135;
         double eccen = sat.Eccentricity;
   
   
         // nodal precession rate rad/day
         //double raDot = -(9.96*cosI) /(((1.-eccen*eccen)**2)*A**3.5) 
         double raDot = -(9.96*cosI) /(Math.Pow(1.0 - eccen * eccen, 2) * Math.Pow(A, 3.5));
 
         double rotationSinceOriginalTLE = (newEpoch - sat.EpochDs50UTC) * (EARTHROTATIONRATE + raDot);
         */

         double rotationSinceOriginalTLE = (newEpoch - sat.EpochDs50UTC) * EARTHROTATIONRATE;
         
          
         double newRightAscension = (node + rotationSinceOriginalTLE) % 360.0;

         // update new epoch time
         sat.EpochDs50UTC = newEpoch;

         // update node
         sat.ClassElts.RAAN_deg = newRightAscension;

         return sat;
      }
   }
}
