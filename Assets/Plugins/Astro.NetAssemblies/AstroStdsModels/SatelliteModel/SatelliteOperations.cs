using System.Collections.Generic;
using System.Linq;
using AFSPC.AstroStds.Models.DisplayModel;
using AFSPC.AstroStds.Wrappers;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   public delegate void SatListChangedHandler();
   public delegate void SatDispDataChangedHandler();
   public delegate void SatSelectChangedHandler();
   public delegate void ShowSatelliteInfoFormHandler(); //Event handler to show floating window of satellite info
   public delegate void ShowGetSatHandler();
   public delegate void SatIDHandler(long satKey); //Event raised when the selected satellite changes



   public static class SatelliteOperations
   {
      public static event SatListChangedHandler SatListChanged;   // Even raised when an obs file was added/deleted
      public static event SatDispDataChangedHandler SatDispDataChanged;   // Even raised when satellite display data changed
      public static event SatSelectChangedHandler SatSelectChanged;   // Even raised when satellite selection changed (select/deselect/reverse/...)
      public static event ShowSatelliteInfoFormHandler ShowSatelliteInfoForm;
      public static event ShowGetSatHandler ShowGetSat;
      public static event SatIDHandler SatIDChanged;


      /// <summary>
      /// The list of satellites that are being displayed and stored in memory 
      /// </summary>
      public static List<SatelliteDisplay> SatDispList = new List<SatelliteDisplay>();

      /// <summary>
      /// Remove all loaded satellites from memory
      /// </summary>
      public static void RemoveAllSats()
      {
         SatStateWrapper.SatStateRemoveAllSats();
         SatDispList.Clear();
         if (SatListChanged != null)
            SatListChanged();
      }

      /// <summary>
      /// This is to update the SatDispList after a satellite has been added successfully to the DLL by 
      /// manual method from a GUI
      /// </summary>
      public static void UpdateSatDispList()
      {
         // Make sure the satellite has been added successfully to the DLL
         if (SatDispList.Count < SatStateWrapper.SatStateGetCount())
         {
            List<long> prevSatKeys = SatKeysInSatDispList();
            AddNewSatsToList(prevSatKeys);
         }
         if (SatListChanged != null)
            SatListChanged();

      }


      /// <summary>
      /// Load satellites from the input file to memory
      /// </summary>
      /// <param name="file">file contains satellite data</param>
      /// <param name="loadSatType">satellite type</param>
      public static void AddSatsFrSingleFile(string file, LoadSatType loadSatType) //AddAnObsFile(string obsFileName)
      {
         int errCode = 0;

         // Count number of satellites currently loaded 
         int prevTotalSats = SatStateWrapper.SatStateGetCount();
         List<long> prevSatKeys = SatKeysInSatDispList();


         if (loadSatType == LoadSatType.AllTypes)
         {
            if (!LoadAllSatTypes(file))
               return;
         }
         else
         {
            switch (loadSatType)
            {
               case LoadSatType.TLEs:
                  errCode = TleWrapper.TleLoadFile(file);
                  break;
               case LoadSatType.VCMs:
                  errCode = VcmWrapper.VcmLoadFile(file);
                  break;
               case LoadSatType.SPVECs:
                  errCode = SpVecWrapper.SpVecLoadFile(file);
                  break;
               case LoadSatType.ExtEphems:
                  errCode = ExtEphemWrapper.ExtEphLoadFile(file);
                  break;
               case LoadSatType.XMLs:
                  errCode = XmlEphemReader.ReadFile(file);
                  break;
            }

            InfoMessages.SaveInfoMessage();

            if (errCode != 0)
            {
               ErrorMessages.SaveErrorMessage();
               return;
            }
         }

         // make sure there are valid new satellites loaded
         if (errCode == 0 && SatStateWrapper.SatStateGetCount() > prevTotalSats)
         {
            AddNewSatsToList(prevSatKeys);
         }
      }

      private static bool LoadAllSatTypes(string file)
      {
         int errCode;
         int totalLoaded = 0;

         errCode = TleWrapper.TleLoadFile(file);
         InfoMessages.SaveInfoMessage();
         if (errCode != 0)
            ErrorMessages.SaveErrorMessage();
         else
            totalLoaded++;


         errCode = VcmWrapper.VcmLoadFile(file);
         InfoMessages.SaveInfoMessage();
         if (errCode != 0)
            ErrorMessages.SaveErrorMessage();
         else
            totalLoaded++;


         errCode = SpVecWrapper.SpVecLoadFile(file);
         InfoMessages.SaveInfoMessage();
         if (errCode != 0)
            ErrorMessages.SaveErrorMessage();
         else
            totalLoaded++;


         errCode = ExtEphemWrapper.ExtEphLoadFile(file);
         InfoMessages.SaveInfoMessage();
         if (errCode != 0)
            ErrorMessages.SaveErrorMessage();
         else
            totalLoaded++;

         if (totalLoaded == 0)
            return false;
         else
            return true;
      }

      public static void AddSatsFrSingleFile(string file, int XF_TASK) //AddAnObsFile(string obsFileName)
      {
         int errCode = 0;

         // Count number of satellites currently loaded 
         int prevTotalSats = SatStateWrapper.SatStateGetCount();
         List<long> prevSatKeys = SatKeysInSatDispList();


         errCode = SatStateWrapper.SatStateLoadFile(file, XF_TASK);
         InfoMessages.SaveInfoMessage();

         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            return;
         }

         // make sure there are valid new satellites loaded
         if (errCode == 0 && SatStateWrapper.SatStateGetCount() > prevTotalSats)
         {
            AddNewSatsToList(prevSatKeys);
         }
      }

      /// <summary>
      /// New satellites have been loaded to the DLL, now need to add those new sats to the SatDispList as well
      /// </summary>
      /// <param name="prevSatKeys">previous satKeys</param>
      private static void AddNewSatsToList(List<long> prevSatKeys)
      {
         List<long> newSatKeys;

         if (prevSatKeys == null || prevSatKeys.Count == 0)
            newSatKeys = SatKeysInDLLs();
         else
            newSatKeys = SatKeysInDLLs().Except(prevSatKeys).ToList<long>();

         // only add satellites that are not in the prevSatKeys
         foreach (long satKey in newSatKeys)
         {
            SatDispList.Add(new SatelliteDisplay(satKey));
         }

         if (SatListChanged != null)
            SatListChanged();
      }

      /// <summary>
      ///  Update satellite's display data in the grid after data has been updated/modified
      /// </summary>
      /// <param name="currObjDisp"></param>
      public static void UpdateSatDispData(ObjectDisplay currObjDisp)
      {
         for (int i = 0; i < SatDispList.Count(); i++)
         {
            if (SatDispList[i].ID == currObjDisp.ID)
            {
               // update the old data with the new changes
               SatDispList[i] = new SatelliteDisplay(currObjDisp.ID);


               // retrieve the parameters previously saved in color, label, symbolsize
               SatDispList[i].Color = currObjDisp.Color;
               SatDispList[i].Label = currObjDisp.Label;
               SatDispList[i].SymbolSize = currObjDisp.SymbolSize;
               break;
            }
         }
      }

      /// <summary>
      /// Update the satellite display list with what was saved before
      /// </summary>
      /// <param name="persistedSatDispList">saved satellite display list</param>
      public static void UsePersistedData(List<ObjectDisplay> persistedSatDispList)
      {
         if (persistedSatDispList != null && persistedSatDispList.Count > 0 &&
             SatDispList != null && SatDispList.Count > 0)
         {
            for (int i = 0; i < SatDispList.Count(); i++)
            {
               foreach (ObjectDisplay persistedSatDisp in persistedSatDispList)
               {
                  if (SatDispList[i].ID == persistedSatDisp.ID)
                  {
                     SatDispList[i].Color = persistedSatDisp.Color;
                     SatDispList[i].Label = persistedSatDisp.Label;
                     SatDispList[i].SymbolSize = persistedSatDisp.SymbolSize;
                     break;
                  }
               }
            }

            if (SatDispDataChanged != null)
               SatDispDataChanged();
         }
      }

      /// <summary>
      /// Remove a list of selected satellites from memory and from the current satDispList
      /// </summary>
      /// <param name="selSatKeys"></param>
      public static void RemoveSatDisplays(List<long> selSatKeys)
      {
         // make sure the list isn't empty
         if (selSatKeys == null || selSatKeys.Count == 0)
            return;

         // find the right SatelliteDisplay objects from the input selSatDisps
         var delSatDispList = (from satKey in selSatKeys
                               join sat2 in SatDispList on satKey equals sat2.ID
                               select sat2).ToArray<SatelliteDisplay>();

         foreach (SatelliteDisplay satDisp in delSatDispList)
         {
            // first remove the sat from the DLL
            int errCode = SatStateWrapper.SatStateRemoveSat(satDisp.ID);

            //... then remove it from the current SatDispList
            if (errCode == 0)
               SatDispList.Remove(satDisp);
         }

         if (SatListChanged != null)
            SatListChanged();
      }


      /// <summary>
      /// Return all the current satKeys loaded in the dlls
      /// </summary>
      /// <returns>a list of satKeys currently loaded in the AS DLLs</returns>
      public static List<long> SatKeysInDLLs()
      {
         int totalSats = SatStateWrapper.SatStateGetCount();
         if (totalSats == 0)
            return new List<long>(); // don't return null

         long[] currSatKeys = new long[totalSats];
         SatStateWrapper.SatStateGetLoaded((int)DllMainWrapper.KeyOrder.Quick, currSatKeys);
         return currSatKeys.ToList();
      }

      /// <summary>
      /// Return all the current satKeys loaded in the dlls in the specified order
      /// </summary>
      /// <param name="order"></param>
      /// <returns>a list of satKeys currently loaded in the AS DLLs</returns>
      public static List<long> SatKeysInDLLs(int order)
      {
         int totalSats = SatStateWrapper.SatStateGetCount();
         if (totalSats == 0)
            return new List<long>(); // don't return null

         long[] currSatKeys = new long[totalSats];
         SatStateWrapper.SatStateGetLoaded(order, currSatKeys);
         return currSatKeys.ToList();
      }

      /// <summary>
      /// Return all the current satKeys contains in this SatDispList
      /// </summary>
      /// <returns></returns>
      public static List<long> SatKeysInSatDispList()
      {
         var satKeys = (from satObjDisp in SatDispList select satObjDisp.ID).ToList<long>();

         return satKeys;
      }

      /// <summary>
      /// Return a list of satellite instances from the currently loaded satellites in the dlls
      /// </summary>
      /// <returns>list of sattelite instances</returns>
      public static List<Satellite> GetCurrSatInstanceList()
      {
         var satInstanceList = (from satObjDisp in SatDispList select satObjDisp.Sat).ToList<Satellite>();

         return satInstanceList;
      }

      /// <summary>
      /// Return the ObjectDisplay associated with the input satKey
      /// </summary>
      /// <param name="satKey"></param>
      /// <returns></returns>
      static public ObjectDisplay SatKeyToObjDisp(long satKey)
      {
         ObjectDisplay objDisp = SatDispList.First(s => s.ID == satKey);

         return objDisp;
      }

      /// <summary>
      /// Return all the ObjectDisplays associated with the input satKeys
      /// </summary>
      /// <param name="satKeys"></param>
      /// <returns>list of ObjectDisplays</returns>
      static public List<ObjectDisplay> SatKeysToObjDispList(List<long> satKeys)
      {
         var objDispList = (from satObjDisp in SatDispList where satKeys.Contains(satObjDisp.ID) select (ObjectDisplay)satObjDisp).ToList<ObjectDisplay>();

         return objDispList;
      }


      /// <summary>
      /// Return all the ObjectDisplays associated with the input satKeys
      /// </summary>
      /// <param name="satKeys"></param>
      /// <returns>list of ObjectDisplays</returns>
      static public List<ObjectDisplay> ObjDispList()
      {
         List<ObjectDisplay> objDispList = SatDispList.ConvertAll(satDisp => new ObjectDisplay(satDisp));

         return objDispList;
      }

      /// <summary>
      /// Refresh the grid view everytime a cell data is changed in the SpaceObjectCtrl
      /// </summary>
      public static void RaiseSatDispDataChanged()
      {
         if (SatDispDataChanged != null)
            SatDispDataChanged();
      }


      /// <summary>
      /// Raise event when user select/deselect/reverse... selected satellite list in the SpaceObjectCtrl
      /// </summary>
      public static void RaiseSatSelectChanged()
      {
         if (SatSelectChanged != null)
            SatSelectChanged();
      }
      /// <summary>
      /// Raise the ShowSatelliteInfo event so the ShowSatelliteInfo form is displayed
      /// </summary>
      public static void OpenSatelliteInfoWindow()
      {
         if (ShowSatelliteInfoForm != null)
            ShowSatelliteInfoForm();
      }

      /// <summary>
      /// Raise the ShowSatelliteInfo event so the ShowSatelliteInfo form is displayed
      /// </summary>
      public static void ShowGetSatForm()
      {
         if (ShowGetSat != null)
            ShowGetSat();
      }

      /// <summary>
      /// Raise the SatIDChanged event
      /// </summary>
      /// <param name="satKey">new satKey</param>
      public static void RaiseSatIDChanged(long satKey)
      {
         if (SatIDChanged != null)
            SatIDChanged(satKey);
      }
   }
}
