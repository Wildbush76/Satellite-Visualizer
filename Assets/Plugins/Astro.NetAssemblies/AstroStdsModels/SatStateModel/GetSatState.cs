using AFSPC.AstroStds.Wrappers;
using System;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class GetSatState
   {
      public static void InitSat(long satKey)
      {
         int errCode = SatStateWrapper.SatStateInitSat(satKey);
         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            return;
         }
      }

      public static void Mse(long satKey, double mse, SatelliteState satState)
      {
         double ds50UTC;
         double[] pos = new double[3];
         double[] vel = new double[3];
         double[] llh = new double[3];
         int revNum;

         int errCode = SatStateWrapper.SatStateMse(satKey, mse, out ds50UTC, out revNum, pos, vel, llh);
         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            satState = null;
            return;
         }

         satState.SatKey = satKey;
         satState.Ds50UTC = ds50UTC;
         satState.Mse = mse;
         satState.RevNum = revNum;
         satState.PosDate = new SatellitePosition(pos);
         satState.VelDate = new SatelliteVelocity(vel);
         satState.LLH = new SatelliteLLH(llh);
      }

      public static void Ds50UTC(long satKey, double ds50UTC, SatelliteState satState)
      {
         double mse;
         double[] pos = new double[3];
         double[] vel = new double[3];
         double[] llh = new double[3];
         int revNum;

         int errCode = SatStateWrapper.SatStateDs50UTC(satKey, ds50UTC, out mse, out revNum, pos, vel, llh);
         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            satState = null;
            return;
         }

         satState.SatKey = satKey;
         satState.Ds50UTC = ds50UTC;
         satState.Mse = mse;
         satState.RevNum = revNum;
         satState.PosDate = new SatellitePosition(pos);
         satState.VelDate = new SatelliteVelocity(vel);
         satState.LLH = new SatelliteLLH(llh);
      }

      public static SatellitePosition Position(long satKey, double ds50UTC)
      {
         double mse;
         double[] pos = new double[3];
         double[] vel = new double[3];
         double[] llh = new double[3];
         int revNum;

         int errCode = SatStateWrapper.SatStateDs50UTC(satKey, ds50UTC, out mse, out revNum, pos, vel, llh);
         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            return null;
         }

         return new SatellitePosition(pos);
      }

      public static SatelliteLLH LatLonHeight(long satKey, double ds50UTC)
      {
         double mse;
         double[] pos = new double[3];
         double[] vel = new double[3];
         double[] llh = new double[3];
         int revNum;

         int errCode = SatStateWrapper.SatStateDs50UTC(satKey, ds50UTC, out mse, out revNum, pos, vel, llh);
         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            return null;
         }

         return new SatelliteLLH(llh);
      }
   }
}
