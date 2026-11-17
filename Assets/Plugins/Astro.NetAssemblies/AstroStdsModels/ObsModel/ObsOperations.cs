using System;
using System.Collections.Generic;
using System.Linq;
using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Models.RotasModel;
using AFSPC.AstroStds.Models.DisplayModel;
using System.IO;
using AFSPC.AstroStds.Models.SensorModel;

namespace AFSPC.AstroStds.Models.ObsModel
{
   public delegate void ObsFileListChangedHandler();
   public delegate void ObsFileDataChangedHandler();
   public delegate void ShowGetObsHandler(List<long> obsKeys);


   public static class ObsOperations
   {
      public static event ObsFileListChangedHandler ObsFileListChanged;   // Even raised when an obs file was added/deleted
      public static event ObsFileDataChangedHandler ObsFileDataChanged;   // Even raised when obs file display data changed
      public static event ShowGetObsHandler ShowGetObs;
      private static List<string> unloadedObsFiles = new List<string>(); // list of fail-to-load obs files due to dependent sensor files aren't loaded first (for RF obs)


      private static int gid = 0;

      /// <summary>
      /// 
      /// </summary>
      public static List<ObsFileDisplay> ObsFileDispList = new List<ObsFileDisplay>();

      /// <summary>
      /// Dictionary of file name and the associated file/group ID
      /// </summary>
      public static Dictionary<string, int> DicObsFileGID = new Dictionary<string, int>();


      /// <summary>
      /// All the obsKeys currently loaded in the ObsDispList
      /// </summary>
      public static List<long> ObsKeysInObsDispList = new List<long>();

      /// <summary>
      /// All current obs intances populated from the current obsKeys
      /// </summary>
      public static List<Obs> CurrObsInstanceList = new List<Obs>();

      /// <summary>
      /// Remove all loaded obs from memory
      /// </summary>
      public static void RemoveAllObs()
      {
         ObsWrapper.ObsRemoveAll();
         ObsFileDispList.Clear();
         DicObsFileGID.Clear();
         ObsKeysInObsDispList.Clear();
         CurrObsInstanceList.Clear();
         gid = 1;
         if (ObsFileListChanged != null)
            ObsFileListChanged();
      }


      /// <summary>
      /// Load a new obs file to memory
      /// </summary>
      /// <param name="obsFileName">file contains observation data</param>
      public static int AddObsFrSingleFile(string obsFileName)
      {
         int errCode = 0;

         // remove current obs in this file before reloading the same file
         if (DicObsFileGID != null && DicObsFileGID.Keys.Contains(obsFileName))
         {
            if (!ObsOperations.RemoveObsFromFile(obsFileName))
            {
               return 1;
            }
         }

         // Count number of obsellites currently loaded 
         int prevTotalObs = ObsWrapper.ObsGetCount();
         List<long> prevObsKeys = ObsKeysInDLL();

         // new obs file added
         if (DicObsFileGID != null)
         {
            gid++; // auto increment the gid everytime a new obs file is loaded
            errCode = ObsWrapper.ObsLoadFileGID(obsFileName, gid);
            InfoMessages.SaveInfoMessage();

            if (errCode != 0 && ObsWrapper.ObsGetCount() == prevTotalObs) // no new obs were added and having problem with input obs file (still ok if error != 0 but some obs were loaded)
            {
               ErrorMessages.SaveErrorMessage();
               unloadedObsFiles.Add(obsFileName);
               return errCode;
            }

            // make sure there is valid obs loaded
            // still add the file to the list even if there's some kind of errors (or dependent sensor files aren't loaded prior loading RF obs)
            if (ObsWrapper.ObsGetCountGID(gid) > 0)
            {
               AddNewObsToList(prevObsKeys, obsFileName);
            }
         }

         return errCode;
      }


      /// <summary>
      /// Add new obs to this repository
      /// </summary>
      /// <param name="prevObsKeys"></param>
      /// <param name="obsFileName"></param>
      private static void AddNewObsToList(List<long> prevObsKeys, string obsFileName)
      {
         List<long> newObsKeys;
         bool newOneAdded = false;

         if (prevObsKeys == null)
            newObsKeys = ObsKeysInDLL();
         else
            newObsKeys = ObsKeysInDLL().Except(prevObsKeys).ToList<long>();

         foreach (long obsKey in newObsKeys)
         {
            Obs newObs = new Obs(obsKey);
            if (newObs.ErrCode == 0)
            {
               CurrObsInstanceList.Add(newObs);
               ObsKeysInObsDispList.Add(obsKey);
               newOneAdded = true;
            }
         }

         if (newOneAdded)
         {
            DicObsFileGID.Add(obsFileName, gid);
            ObsFileDispList.Add(new ObsFileDisplay(gid, obsFileName));

            // TODO : need to raise this even only for the last added file
            if (ObsFileListChanged != null)
               ObsFileListChanged();
         }
      }


      /// <summary>
      /// Update the sensor display list with what was saved before
      /// </summary>
      /// <param name="persistedObsDispList">saved sensor display list</param>
      public static void UsePersistedData(List<ObjectDisplay> persistedObsDispList)
      {
         if (persistedObsDispList != null && persistedObsDispList.Count > 0 &&
             ObsFileDispList != null && ObsFileDispList.Count > 0)
         {
            for (int i = 0; i < ObsFileDispList.Count(); i++)
            {
               foreach (ObjectDisplay persistedSenDisp in persistedObsDispList)
               {
                  if (ObsFileDispList[i].ID == persistedSenDisp.ID)
                  {
                     ObsFileDispList[i].Color = persistedSenDisp.Color;
                     break;
                  }
               }
            }

            if (ObsFileDataChanged != null)
               ObsFileDataChanged();
         }
      }

      // Load obsellites from the list of input file names
      private static void LoadMultipleObsFiles(string[] fileNames)
      {
         foreach (String file in fileNames)
         {
            foreach (string obsFile in GetObsFileNamesInFile(file))
               ObsOperations.AddObsFrSingleFile(obsFile);
         }
      }

      /// <summary>
      /// Load observations from the main input file which usually has multiple OBSFIL = obsFileName entries 
      /// </summary>
      /// <param name="fileName"></param>
      public static void LoadObsFrMainFile(string fileName)
      {
         LoadMultipleObsFiles(GetObsFileNamesInFile(fileName).ToArray());
      }

      /// <summary>
      /// Return direct obs file name or extract the name from the entry OBSFIL=
      /// </summary>
      /// <param name="fileName">the input string</param>
      /// <returns>the resulting obs file name</returns>
      private static List<string> GetObsFileNamesInFile(string fileName)
      {
         List<string> fileNames;
         StreamReader mainFile;

         mainFile = Utility.FileRead(fileName);
         fileNames = new List<string>();

         string card = "";

         while (card != null)
         {
            card = mainFile.ReadLine();
            if (card != null && card.Length > 6 && card.Substring(0, 6).ToUpper() == "OBSFIL")
            {
               string extractedFileName = card.Substring(card.IndexOf('=') + 1);
               extractedFileName = extractedFileName.Trim();

               fileNames.Add(extractedFileName);
            }
         }

         if (mainFile != null)
         {
            mainFile.Close();
         }

         // No obs file entries found in this file. The file itself contains obs
         if (fileNames.Count == 0)
            fileNames.Add(fileName);

         return fileNames;
      }


      /// <summary>
      /// Remove all obs that were loaded from the specified file
      /// </summary>
      /// <param name="obsFileName">obs file name</param>
      /// <returns>true: successful; false: failed to remove obs from file</returns>
      public static bool RemoveObsFromFile(string obsFileName)
      {
         int errCode;

         if (!DicObsFileGID.Keys.Contains(obsFileName))
            return true;


         //List<long> selObsKeys = ObsKeysInGID(DicObsFileGID[obsFileName]);
         //if (selObsKeys == null || selObsKeys.Count == 0)
         //   return true;
         //// Remove obsKeys and obs instances
         //// find the right ObselliteDisplay objects from the input selObsDisps
         //var delObsList = (from obsKey in selObsKeys
         //                        join obs2 in CurrObsInstanceList on obsKey equals obs2.ObsKey
         //                        select obs2).ToArray<Obs>();

         //foreach (Obs obs in delObsList)
         //{
         //   // first remove the obs from the DLL
         //   errCode = ObsWrapper.ObsRemove(obs.ObsKey);

         //   //... then remove it from the current CurrObsInstanceList
         //   if (errCode == 0)
         //      CurrObsInstanceList.Remove(obs);
         //}

         errCode = ObsWrapper.ObsRemoveGID(DicObsFileGID[obsFileName]);
         if (errCode == 0)
         {
            DicObsFileGID.Remove(obsFileName);
            foreach (ObsFileDisplay obsFileDisp in ObsFileDispList)
            {
               if (obsFileDisp.Label == obsFileName)
               {
                  ObsFileDispList.Remove(obsFileDisp);
                  if (ObsFileListChanged != null)
                     ObsFileListChanged();
                  break;
               }
            }
            CurrObsInstanceList = GetCurrObsInstanceList();
            ObsKeysInObsDispList = ObsKeysInDLL();

            return true;
         }
         else
            return false;
      }


      /// <summary>
      /// Remove all obs that were loaded by the specified group id
      /// </summary>
      /// <param name="gid">groupd id of the obs file</param>
      /// <returns>true: successful; false: failed to remove obs from file</returns>
      public static bool RemoveObsFromFile(int gid)
      {
         string deletedFile = null;

         foreach (string fileName in DicObsFileGID.Keys)
         {
            if (DicObsFileGID[fileName] == gid)
            {
               deletedFile = fileName;
               break;
            }
         }

         if (deletedFile != null)
            return RemoveObsFromFile(deletedFile);
         else
            return false;
      }



      /// <summary>
      /// Return all the current obsKeys loaded in the obs dll
      /// </summary>
      /// <returns></returns>
      public static List<long> ObsKeysInDLL()
      {
         long[] currObsKeys = new long[ObsWrapper.ObsGetCount()];
         ObsWrapper.ObsGetLoaded((int)DllMainWrapper.KeyOrder.Quick, currObsKeys);
         return currObsKeys.ToList();
      }


      /// <summary>
      /// Return all the current obsKeys loaded in the obs dll in the specified sort order
      /// </summary>
      /// <param name="sortOrder"></param>
      /// <returns></returns>
      public static List<long> ObsKeysInDLL(int sortOrder)
      {
         long[] currObsKeys = new long[ObsWrapper.ObsGetCount()];
         ObsWrapper.ObsGetLoaded(sortOrder, currObsKeys);
         return currObsKeys.ToList();
      }

      /// <summary>
      /// Return all the obsKeys belong to the specified group id
      /// </summary>
      /// <param name="gid"></param>
      /// <returns></returns>
      public static List<long> ObsKeysInGID(int gid)
      {
         long[] currObsKeys = new long[ObsWrapper.ObsGetCountGID(gid)];
         ObsWrapper.ObsGetLoadedGID(gid, (int)DllMainWrapper.KeyOrder.Quick, currObsKeys);
         return currObsKeys.ToList();
      }

      /// <summary>
      /// Return all the obsKeys belong to the specified group id in the order specified in the sortOrder
      /// </summary>
      /// <param name="gid"></param>
      /// <returns></returns>
      public static List<long> ObsKeysInGID(int gid, int sortOrder)
      {
         long[] currObsKeys = new long[ObsWrapper.ObsGetCountGID(gid)];
         ObsWrapper.ObsGetLoadedGID(gid, sortOrder, currObsKeys);
         return currObsKeys.ToList();
      }

      /// <summary>
      /// Return a list of obs objects instantiated from all the obskey currently loaded in the dll
      /// </summary>
      /// <returns></returns>
      public static List<Obs> GetCurrObsInstanceList()
      {
         List<Obs> obsList = new List<Obs>();
         List<long> obsKeys = ObsKeysInDLL();
         foreach (long currObsKey in obsKeys)
         {
            Obs obs = new Obs(currObsKey);
            if (obs.ErrCode == 0)
            {
               obsList.Add(obs);
            }
         }

         return obsList;
      }

      /// <summary>
      /// Return a list of obs objects instantiated from the currently loaded obs in the specified group id in the dll
      /// </summary>
      /// <param name="gid">group id</param>
      /// <returns>list of obs objects instantiaged from the obsKeys in the gid</returns>
      public static List<Obs> GetCurrObsInstanceList(int gid)
      {
         List<Obs> obsList = new List<Obs>();
         List<long> obsKeys = ObsKeysInGID(gid);
         foreach (long currObsKey in obsKeys)
         {
            Obs obs = new Obs(currObsKey);
            if (obs.ErrCode == 0)
            {
               obsList.Add(obs);

            }
         }

         return obsList;
      }


      /// <summary>
      /// Return the ObjectDisplay associated with the input id
      /// </summary>
      /// <param name="satKey"></param>
      /// <returns></returns>
      static public ObjectDisplay GIDToObjDisp(long gid)
      {
         ObjectDisplay objDisp = ObsFileDispList.First(s => s.ID == gid);

         return objDisp;
      }


      // Return all obs that match the sensor keys (sensor numbers) in the senKeys list
      public static List<long> ReturnObsFromSenKeysDate(List<long> senKeys, double startTime, double stopTime)
      {
         List<long> currObsKeys = ObsKeysInDLL();
         List<long> selObsList = new List<long>();

         List<int> senNums = new List<int>();
         foreach (long senKey in senKeys)
         {
            senNums.Add(SensorWrapper.SenNumOf(senKey));
         }

         foreach (long obsKey in currObsKeys)
         {
            Obs currObs = new Obs(obsKey);
            if (senNums.Contains(currObs.SenNum))
            {
               if (currObs.ObsTimeDs50UTC.Ds50UTC >= startTime && currObs.ObsTimeDs50UTC.Ds50UTC <= stopTime)
                  selObsList.Add(obsKey);
            }
         }
         if (selObsList != null)
            return selObsList;
         else
            return new List<long>();
      }


      // Return all obs that match the sensor keys (sensor numbers) in the senKeys list
      public static List<Obs> ReturnObsFromSenKeysDate(List<Obs> obs, List<long> senKeys, double startTime, double stopTime)
      {
         List<int> senNums = new List<int>();
         foreach (long senKey in senKeys)
         {
            senNums.Add(SensorWrapper.SenNumOf(senKey));
         }

         List<Obs> selObsList = (from anObs in obs
                                 where senNums.Contains(anObs.SenNum)
                                 select anObs).ToList<Obs>();
         return selObsList;
      }




      // Return a dictionary of sensor number and all obsKeys that contains that sensor number

      public static Dictionary<int, List<long>> DicSenNumObsKeyList()
      {
         List<long> currObsKeys = ObsKeysInDLL();
         return DicSenNumObsKeyList(currObsKeys);
      }


      // Return a dictionary of sensor number and all obsKeys that contains that sensor number
      public static Dictionary<int, List<long>> DicSenNumObsKeyList(List<long> currObsKeys)
      {
         Dictionary<int, List<long>> dicSenNumObsKeyList = new Dictionary<int, List<long>>();

         foreach (long currObsKey in currObsKeys)
         {
            int senNum = ObsWrapper.SenNumFrObsKey(currObsKey);
            if (!dicSenNumObsKeyList.ContainsKey(senNum))
               dicSenNumObsKeyList.Add(senNum, new List<long>());
            dicSenNumObsKeyList[senNum].Add(currObsKey);
         }
         return dicSenNumObsKeyList;
      }

      // Return a dictionary of sensor number and all obsKeys that contains that sensor number
      public static Dictionary<int, List<long>> DicSenNumObsKeyList(List<Obs> obs)
      {
         // Extract all unique sen numbers
         var senNums = (from anObs in obs select anObs.SenNum).Distinct();
         Dictionary<int, List<long>> dicSenNumObsKeyList = new Dictionary<int, List<long>>();
         foreach (int senNum in senNums)
         {
            dicSenNumObsKeyList.Add(senNum, (from anObs in obs where senNum == anObs.SenNum select anObs.ObsKey).ToList<long>());
         }

         return dicSenNumObsKeyList;
      }

      // Return a dictionary of sensor number and its description
      public static Dictionary<long, string> DicSenNumObsKeyList(List<Residuals> allResxp)
      {
         var combSenNums = (from resid in allResxp orderby SenNumsToCombSenNum(resid.SenNum, resid.SenNum2) select SenNumsToCombSenNum(resid.SenNum, resid.SenNum2)).Distinct();
         Dictionary<long, string> dicSenNumMenuDesc = new Dictionary<long, string>();
         foreach (long senNumComb in combSenNums)
         {
            int senNum1, senNum2;
            senNum1 = (int)(senNumComb / 1e6);
            senNum2 = (int)(senNumComb % 1e6);

            List<long> matchedObsKeys = (from resid in allResxp
                                         where (resid.SenNum == senNum1 && (resid.SenNum2 == 0 || resid.SenNum2 == senNum2))
                                         select resid.ObsKey).ToList<long>();
            if (matchedObsKeys.Count != 0)
            {
               dicSenNumMenuDesc.Add(senNumComb, CombSenToDescs(senNumComb, matchedObsKeys.Count));
            }
         }

         return dicSenNumMenuDesc;
      }

      // Combine sensor number 1 and sensor number 2 to a combination sensor number
      public static long SenNumsToCombSenNum(int senNum1, int senNum2)
      {
         return senNum1 * 1000000L + senNum2;
      }

      // Extract sensor number 1 and sensor number 2 from a combination sensor number
      public static void CombSenToSenNums(long combSenNum, out int senNum1, out int senNum2)
      {
         senNum1 = (int)(combSenNum / 1000000);
         senNum2 = (int)(combSenNum % 1000000);
      }

      // Return sensor description based on a single sensor or a combination of two sensors (RF obs)
      public static string CombSenToDescs(long combSenNum, int count)
      {
         int senNum1, senNum2;
         CombSenToSenNums(combSenNum, out senNum1, out senNum2);

         if (senNum2 == 0)
         {
            Sensor sensor1 = new Sensor(senNum1);
            return string.Format("{0:D3} {1} - {2}", senNum1, sensor1.SenDesc.Trim(), count);
         }
         else
         {
            return string.Format("{0,6}/{1,6} {2}", senNum1, senNum2, count);
         }
      }


      // Return a dictionary of obsellite number and all obsKeys that contains that obsellite number
      public static Dictionary<int, List<long>> DicSatNumObsKeyList(List<long> currObsKeys)
      {
         Dictionary<int, List<long>> dicObsPerSat = new Dictionary<int, List<long>>();

         foreach (long currObsKey in currObsKeys)
         {
            int obsNum = ObsWrapper.SatNumFrObsKey(currObsKey);

            if (!dicObsPerSat.ContainsKey(obsNum))
               dicObsPerSat.Add(obsNum, new List<long>());
            dicObsPerSat[obsNum].Add(currObsKey);
         }
         return dicObsPerSat;
      }


      public static Dictionary<long, List<long>> DicSatKeyObsKeyList(List<Residuals> allResxp)
      {
         // Extract all unique obskeys
         var obsKeys = (from resid in allResxp orderby resid.SatKey select resid.SatKey).Distinct();

         Dictionary<long, List<long>> dicObsPerSat = new Dictionary<long, List<long>>();
         foreach (long obsKey in obsKeys)
         {
            dicObsPerSat.Add(obsKey, (from resid in allResxp where obsKey == resid.SatKey select resid.ObsKey).ToList<long>());
         }

         return dicObsPerSat;
      }



      // Return number of obs that contain the specified sensor number
      public static int ReturnNumObsFromSenNum(int senNum)
      {
         List<long> currObsKeys = ObsKeysInDLL();
         return ReturnNumObsFromSenNum(senNum, currObsKeys);
      }


      // Return number of obs from the specified obsKeys that contain the specified sensor number
      public static int ReturnNumObsFromSenNum(int senNum, List<long> currObsKeys)
      {

         int numObs = 0;

         foreach (long currObsKey in currObsKeys)
         {
            int obsSenNum = ObsWrapper.SenNumFrObsKey(currObsKey);
            if (obsSenNum == senNum)
               numObs = numObs + 1;
         }
         return numObs;
      }


      // Return a dictionary of fileName and obsKeys that were in both currObsKeys and loaded obsKeys in that file
      public static Dictionary<string, List<long>> ReturnObsFromFile(List<long> currObsKeys)
      {
         Dictionary<string, List<long>> dicSelObsFromFile = new Dictionary<string, List<long>>();

         foreach (string fileName in DicObsFileGID.Keys)
         {
            List<long> obsKeysInGID = ObsKeysInGID(DicObsFileGID[fileName]);
            var matchedObsKeys = currObsKeys.Intersect(obsKeysInGID).ToList<long>();

            /*
            var matchedObsKeys = (from cObsKey in currObsKeys
                                  join aObsKey in obsKeysInGID on cObsKey equals aObsKey
                                  select cObsKey).ToList<long>();
             * */

            if (matchedObsKeys.Count > 0)
               dicSelObsFromFile.Add(fileName, matchedObsKeys);
         }

         return dicSelObsFromFile;

      }

      public static Dictionary<int, List<long>> DicSenNumobsKeyList()
      {
         throw new NotImplementedException();
      }

      /// <summary>
      /// Refresh the grid view everytime a cell data is changed in the SpaceObjectCtrl
      /// </summary>
      public static void RaiseObsFileDataChanged()
      {
         if (ObsFileDataChanged != null)
            ObsFileDataChanged();
      }

      /// <summary>
      /// Raise the ShowSatelliteInfo event so the ShowSatelliteInfo form is displayed
      /// </summary>
      public static void ShowGetObsForm(List<long> obsKeys)
      {
         if (ShowGetObs != null)
            ShowGetObs(obsKeys);
      }



      /// <summary>
      /// Return all the ObjectDisplays associated with the input obs files
      /// </summary>
      /// <param name="obsFiles">input obs files</param>
      /// <returns>list of ObjectDisplays</returns>
      static public List<ObjectDisplay> ObsFilesToObjDispList(List<string> obsFiles)
      {
         var objDispList = (from obsObjDisp in ObsFileDispList where obsFiles.Contains(obsObjDisp.Label) select (ObjectDisplay)obsObjDisp).ToList<ObjectDisplay>();

         return objDispList;
      }


      /// <summary>
      /// Try to reload obs files that were failed to load due to depedent sensor files aren't loaded first (for RF obs) since sensor file
      /// </summary>
      public static void ReloadUnloadedObsFiles()
      {
         if (unloadedObsFiles.Count > 0)
         {
            LoadMultipleObsFiles(unloadedObsFiles.ToArray());
            unloadedObsFiles.Clear();
         }
      }
   }
}

