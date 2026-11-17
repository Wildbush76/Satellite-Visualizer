using System;
using System.Collections.Generic;
using AFSPC.AstroStds.Wrappers;
using System.IO;
using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Models.SatelliteModel;

namespace AFSPC.AstroStds.Models
{
   [Serializable]
   public class SaveAstroStdData
   {

      // Save current Environment and Timing Constants settings to a file
      public static void SaveEnvAndTime(string fileName)
      { 
         int errCode;

         // Save EnvConst data
         errCode = EnvConstWrapper.EnvSaveFile(fileName, DllMainWrapper.IOSAVE_APPEND, DllMainWrapper.SAVE_CARDFORM);
         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            return;
         }

         // Save Timing Constants data
         
         errCode = TimeFuncWrapper.TConSaveFile(fileName, DllMainWrapper.IOSAVE_APPEND, DllMainWrapper.SAVE_CARDFORM);
         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            return;
         }
         
      }

      public static void SaveAllSatsToFile(string fileName)
      {
         int errCode;

         // Save EnvConst data
         errCode = SatStateWrapper.SatStateSaveFile(fileName, DllMainWrapper.IOSAVE_APPEND, DllMainWrapper.SAVE_CARDFORM, SatStateWrapper.XF_TASK_BOTH);
         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            return;
         }
      }

      // Save a list of satellites to a file
      public static void SaveSatsToFile(StreamWriter swOutFile, List<long> satKeys)
      {
         int errCode;

         if (satKeys == null || satKeys.Count == 0 || swOutFile == null)
            return;

         foreach (long satKey in satKeys)
         {
            int orbEltType = SatStateService.GetOrbEltType(satKey);

            if (orbEltType == DllMainWrapper.ELTTYPE_TLE_SGP  || orbEltType == DllMainWrapper.ELTTYPE_TLE_SGP4 ||
                orbEltType == DllMainWrapper.ELTTYPE_TLE_PPT3 || orbEltType == DllMainWrapper.ELTTYPE_TLE_XP   || 
                orbEltType == DllMainWrapper.ELTTYPE_TLE_SP)
            {
               byte[] line1 = new byte[DllMainWrapper.GETSETSTRLEN];
               byte[] line2 = new byte[DllMainWrapper.GETSETSTRLEN];
               errCode = TleWrapper.TleGetLines(satKey, line1, line2);
               if (errCode != 0)
               {
                  ErrorMessages.SaveErrorMessage();
                  return;
               }
               swOutFile.WriteLine("{0}\n{1}", Utility.BytesToString(line1).Substring(0, 80), Utility.BytesToString(line2).Substring(0, 80));
            }
            else if (orbEltType == DllMainWrapper.ELTTYPE_SPVEC_B1P)
            {
               byte[] line1 = new byte[DllMainWrapper.GETSETSTRLEN];
               byte[] line2 = new byte[DllMainWrapper.GETSETSTRLEN];

               errCode = SpVecWrapper.SpVecGetLines(satKey, line1, line2);
               if (errCode != 0)
               {
                  ErrorMessages.SaveErrorMessage();
                  return;
               }
               swOutFile.WriteLine("{0}\n{1}", Utility.BytesToString(line1).Substring(0, 80), Utility.BytesToString(line2).Substring(0, 80));
            }
            else if (orbEltType == DllMainWrapper.ELTTYPE_VCM)
            {
               byte[] vcmLines = new byte[4000];
               errCode = VcmWrapper.VcmGetLines(satKey, vcmLines);
               if (errCode != 0)
               {
                  ErrorMessages.SaveErrorMessage();
                  return;
               }
               string vcmString = Utility.BytesToString(vcmLines);
               foreach (string token in vcmString.Split(new char[] { '<' }, StringSplitOptions.RemoveEmptyEntries))
               {
                  swOutFile.WriteLine("<" + token);
               }
            }
            else if (orbEltType == DllMainWrapper.ELTTYPE_EXTEPH)
            {
               byte[] extEphLine = new byte[DllMainWrapper.GETSETSTRLEN];
               errCode = ExtEphemWrapper.ExtEphGetLine(satKey, extEphLine);
               if (errCode != 0)
               {
                  ErrorMessages.SaveErrorMessage();
                  return;
               }
               swOutFile.WriteLine(Utility.BytesToString(extEphLine));

            }
         }
      }


      // Save a list of sensors to a file
      public static void SaveSensToFile(StreamWriter swOutFile, List<long> senKeys)
      {
         if (senKeys == null || senKeys.Count == 0 || swOutFile == null)
            return;

         int errCode = 0;
         byte[] sCard = new byte[DllMainWrapper.GETSETSTRLEN];
         byte[] l1Card = new byte[DllMainWrapper.GETSETSTRLEN];
         byte[] l2Card = new byte[DllMainWrapper.GETSETSTRLEN];


         foreach (long senKey in senKeys)
         {
            errCode = SensorWrapper.SensorGetLines(senKey, sCard, l1Card, l2Card);
            if (errCode != 0)
            {
               ErrorMessages.SaveErrorMessage();
               return;
            }
            swOutFile.WriteLine("{0}\n{1}\n{2}",
               Utility.BytesToStringRTrim(sCard),
               Utility.BytesToStringRTrim(l1Card),
               Utility.BytesToStringRTrim(l2Card));
         }
      }


      // Save a list of osbservation files to file
      public static void SaveObsFilesToFile(StreamWriter swOutFile, List<string> obsFiles)
      {
         if (obsFiles == null || obsFiles.Count == 0 || swOutFile == null)
            return;

         foreach (string obsFile in obsFiles)
         {
            swOutFile.WriteLine("OBSFILE = {0}", obsFile);
         }
      }

      public static string RemoveIllegalFileNameChars(string fileString)
      {
         fileString = fileString.Replace("/", "").Replace(":", "").Replace("\\", "").Replace("?", "").Replace("<", "").Replace(">", "").Replace("|", "");
         fileString = fileString.Replace("(", "").Replace(")", "");
         

         return fileString;
      }
   }
}
