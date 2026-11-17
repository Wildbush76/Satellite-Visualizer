using System;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class AofWrapper
   {
      public static void PrintEntryExit(double[] xa_aofArea, long satKey, int numOfPens, double[] xa_entExitTimes)
      {
         bool printLook = (xa_aofArea[AofWrapper.XA_AOFAREA_TYPE] == AofWrapper.AOF_AREATYPE_CIRCLE || xa_aofArea[AofWrapper.XA_AOFAREA_TYPE] == AofWrapper.AOF_AREATYPE_POINT);
         for (int i = 0; i < numOfPens; i++)
         {
            Console.WriteLine("Entry = {0}", TimeFuncWrapper.UTCToDtg18Str(TimeFuncWrapper.TAIToUTC(xa_entExitTimes[i * 2])));
            if (printLook)
            {
               double[] xa_look = new double[8];
               double[] llh = new double[3];
               llh[0] = xa_aofArea[AofWrapper.XA_AOFAREA_ELEM1];
               llh[1] = xa_aofArea[AofWrapper.XA_AOFAREA_ELEM2];
               llh[2] = xa_aofArea[AofWrapper.XA_AOFAREA_ELEM3];
               AofWrapper.AofGetLook(xa_entExitTimes[i * 2], llh, satKey, xa_look);
               Console.WriteLine("Az = {0}, El = {1}, Rng = {2}", xa_look[LamodWrapper.XA_LOOK_AZIM], xa_look[LamodWrapper.XA_LOOK_ELEV], xa_look[LamodWrapper.XA_LOOK_RNG]);
            }
            Console.WriteLine("Exit  = {0}", TimeFuncWrapper.UTCToDtg18Str(TimeFuncWrapper.TAIToUTC(xa_entExitTimes[i * 2 + 1])));
            if (printLook)
            {
               double[] xa_look = new double[8];
               double[] llh = new double[3];
               llh[0] = xa_aofArea[AofWrapper.XA_AOFAREA_ELEM1];
               llh[1] = xa_aofArea[AofWrapper.XA_AOFAREA_ELEM2];
               llh[2] = xa_aofArea[AofWrapper.XA_AOFAREA_ELEM3];
               AofWrapper.AofGetLook(xa_entExitTimes[i * 2 + 1], llh, satKey, xa_look);
               Console.WriteLine("Az = {0}, El = {1}, Rng = {2}", xa_look[LamodWrapper.XA_LOOK_AZIM], xa_look[LamodWrapper.XA_LOOK_ELEV], xa_look[LamodWrapper.XA_LOOK_RNG]);
            }
         }
      }
      public static void PrintEntryExit(int numOfPens, double[] xa_eeDat)
      {
         for (int i = 0; i < numOfPens; i++)
         {
            Console.WriteLine("Entry = {0}", TimeFuncWrapper.UTCToDtg18Str(TimeFuncWrapper.TAIToUTC(xa_eeDat[i * 2])));
            Console.WriteLine("Exit  = {0}", TimeFuncWrapper.UTCToDtg18Str(TimeFuncWrapper.TAIToUTC(xa_eeDat[i * 2 + 1])));
         }
      }
   }
}
