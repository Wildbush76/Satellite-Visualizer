using AFSPC.AstroStds.Models.SatelliteModel;
using AFSPC.AstroStds.Models.SensorModel;
using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Models.ObsModel;
using System;

namespace AFSPC.AstroStds.Models
{
   [Serializable]
   public class LoadAstroStdData
   {
      public static void LoadEnvAndTime(string fileName)
      {
         int errCode; 

         // Load environmental data from the input file
         errCode = EnvConstWrapper.EnvLoadFile(fileName);
         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            return;
         }
         // Load timing constants from the input file
         errCode = TimeFuncWrapper.TConLoadFile(fileName);
         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            return;
         }
      }

      // Load satellites and other parameters (propagator control)
      public static void LoadSatsAndCtrl(string fileName)
      {
         SatelliteOperations.AddSatsFrSingleFile(fileName, SatStateWrapper.XF_TASK_BOTH);
      }


      // Load sensor data
      public static void LoadSensors(string fileName)
      {
         SensorOperations.AddSensFrSingleFile(fileName);
      }


      public static void LoadObservations(string fileName)
      {
         int errCode;

         //Load observation data from the input file
         errCode = ObsWrapper.ObsLoadFile(fileName);
         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            return;
         }
      }


      public static void RemoveAllLoadedData()
      {
         SatelliteOperations.RemoveAllSats();
         SensorOperations.RemoveAllSens();
         ObsOperations.RemoveAllObs();
         SatStateWrapper.SatStateReset();
         TimeFuncWrapper.TConRemoveAll();
         EnvConstWrapper.EnvSetFkIdx(5);
         EnvConstWrapper.EnvSetGeoIdx(EnvConstWrapper.XF_GEOMOD_WGS72);
         EnvConstWrapper.EnvSetEarthShape(1); // use oblate earth
      }
   }
}
