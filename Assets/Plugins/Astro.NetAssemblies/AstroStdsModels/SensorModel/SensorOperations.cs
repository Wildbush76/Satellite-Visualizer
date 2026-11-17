// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 05-19-2015
//
// Last Modified By : nguyend
// Last Modified On : 05-18-2015
// ***********************************************************************
// <copyright file="SensorOperations.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using AFSPC.AstroStds.Models.DisplayModel;
using AFSPC.AstroStds.Wrappers;

/// <summary>
/// The SensorModel namespace.
/// </summary>
namespace AFSPC.AstroStds.Models.SensorModel
{
   /// <summary>
   /// Delegate SenListChangedHandler
   /// </summary>
   public delegate void SenListChangedHandler();
   /// <summary>
   /// Delegate SenDispDataChangedHandler
   /// </summary>
   public delegate void SenDispDataChangedHandler();
   public delegate void SenSelectChangedHandler();
   /// <summary>
   /// Delegate ShowSensorInfoFormHandler
   /// </summary>
   public delegate void ShowSensorInfoFormHandler(); //Event handler to show floating window of satellite info
   /// <summary>
   /// Delegate SenIDHandler
   /// </summary>
   /// <param name="senKey">The sen key.</param>
   public delegate void SenIDHandler(long senKey); //Event raised when the selected sensor changes


   /// <summary>
   /// Class SensorOperations.
   /// </summary>
   public static class SensorOperations
   {
      /// <summary>
      /// Occurs when [sen list changed].
      /// </summary>
      public static event SenListChangedHandler SenListChanged;   
      /// <summary>
      /// Occurs when [sen disp data changed].
      /// </summary>
      public static event SenDispDataChangedHandler SenDispDataChanged;
      public static event SenSelectChangedHandler SenSelectChanged;
      /// <summary>
      /// Occurs when [show sensor information form].
      /// </summary>
      public static event ShowSensorInfoFormHandler ShowSensorInfoForm;
      /// <summary>
      /// Occurs when [sen identifier changed].
      /// </summary>
      public static event SenIDHandler SenIDChanged;



      /// <summary>
      /// The list of sensors that are being displayed and stored in memory
      /// </summary>
      public static List<SensorDisplay> SenDispList = new List<SensorDisplay>();

      /// <summary>
      /// Remove all loaded sensors from memory
      /// </summary>
      public static void RemoveAllSens()
      {
         SensorWrapper.SensorRemoveAll();
         SenDispList.Clear();
         if (SenListChanged != null)
            SenListChanged();
      }

      /// <summary>
      /// This is to update the SenDispList after a sensor has been added successfully to the DLL by
      /// manual method from a GUI
      /// </summary>
      public static void UpdateSenDispList()
      {
         if(SenDispList.Count < SensorWrapper.SensorGetCount())
         {
            List<long> prevSenKeys = SenKeysInSenDispList();
            AddNewSensToList(prevSenKeys);

         }
      }


      /// <summary>
      /// Load sensors from the input file to memory
      /// </summary>
      /// <param name="file">file contains sensor data</param>
      public static void AddSensFrSingleFile(string file) 
      {
         int errCode = 0;
         // Count number of sensors currently loaded 
         int prevTotalSens = SensorWrapper.SensorGetCount();
         List<long> prevSenKeys = SenKeysInSenDispList();

         errCode = SensorWrapper.SensorLoadFile(file);
         InfoMessages.SaveInfoMessage();
         if (errCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            return;
         }

         // make sure there are valid new sensors loaded
         if (errCode == 0 && SensorWrapper.SensorGetCount() > prevTotalSens)
         {
            AddNewSensToList(prevSenKeys);
         }
      }


      /// <summary>
      /// New sensors have been loaded to the DLL, now need to add those new sens to the SenDispList as well
      /// </summary>
      /// <param name="prevSenKeys">previous senKeys</param>
      private static void AddNewSensToList(List<long> prevSenKeys)
      {
         List<long> newSenKeys;

         if (prevSenKeys == null)
            newSenKeys = SenKeysInDLL();
         else
            newSenKeys = SenKeysInDLL().Except(prevSenKeys).ToList<long>();

         // only add sensors that are not in the prevSenKeys
         foreach (long senKey in newSenKeys)
         {
            SenDispList.Add(new SensorDisplay(senKey));
         }

         if (SenListChanged != null)
            SenListChanged();
      }

      /// <summary>
      /// Update the sensor display list with what was saved before
      /// </summary>
      /// <param name="persistedSenDispList">saved sensor display list</param>
      public static void UsePersistedData(List<ObjectDisplay> persistedSenDispList)
      {
         if (persistedSenDispList != null && persistedSenDispList.Count > 0 &&
             SenDispList != null && SenDispList.Count > 0)
         {
            for (int i = 0; i < SenDispList.Count(); i++)
            {
               foreach (ObjectDisplay persistedSenDisp in persistedSenDispList)
               {
                  if (SenDispList[i].ID == persistedSenDisp.ID)
                  {
                     SenDispList[i].Color = persistedSenDisp.Color;
                     SenDispList[i].Label = persistedSenDisp.Label;
                     SenDispList[i].SymbolSize = persistedSenDisp.SymbolSize;
                     break;
                  }
               }
            }

            if (SenDispDataChanged != null)
               SenDispDataChanged();
         }
      }

      /// <summary>
      /// Remove a list of selected sensors from memory and from the current satDispList
      /// </summary>
      /// <param name="selSenKeys">The sel sen keys.</param>
      public static void RemoveSensorDisplays(List<long> selSenKeys)
      {
         // make sure the list isn't empty
         if (selSenKeys == null || selSenKeys.Count == 0)
            return;

         // find the right SensorDisplay objects from the input selSenDisps
         var delSenDispList = (from senKey in selSenKeys
                               join sen2 in SenDispList on senKey equals sen2.ID
                               select sen2).ToArray<SensorDisplay>();

         foreach (SensorDisplay senDisp in delSenDispList)
         {
            // first remove the sen from the DLL
            int errCode = SensorWrapper.SensorRemove((int)senDisp.ID);

            //... then remove it from the current SenDispList
            if (errCode == 0)
               SenDispList.Remove(senDisp);
         }

         if (SenListChanged != null)
            SenListChanged();

      }

      /// <summary>
      /// Return all the current senKeys loaded in the sensor dll
      /// </summary>
      /// <returns>List&lt;System.Int32&gt;.</returns>
      public static List<long> SenKeysInDLL()
      {
         int totalSens = SensorWrapper.SensorGetCount();
         if (totalSens == 0)
            return new List<long>(); // don't return null if none exists

         long[] currSenKeys = new long[totalSens];
         SensorWrapper.SensorGetLoaded((int)DllMainWrapper.KeyOrder.Quick, currSenKeys);
         return currSenKeys.ToList();
      }

      /// <summary>
      /// Return all the current satKeys loaded in the dlls
      /// </summary>
      /// <returns>List&lt;System.Int32&gt;.</returns>
      public static List<long> SenKeysInSenDispList()
      {
         var senKeys = (from senObjDisp in SenDispList select senObjDisp.ID).ToList<long>();

         return senKeys;
      }


      /// <summary>
      /// Return a list of sensor instances from the currently loaded sensors in the dlls
      /// </summary>
      /// <returns>list of sattelite instances</returns>
      public static List<Sensor> GetCurrSenInstanceList()
      {
         var senInstanceList = (from senObjDisp in SenDispList select senObjDisp.Sen).ToList<Sensor>();

         return senInstanceList;
      }


      /// <summary>
      /// Return the ObjectDisplay associated with the input senKey
      /// </summary>
      /// <param name="senKey">The sen key.</param>
      /// <returns>ObjectDisplay.</returns>
      static public ObjectDisplay SenKeyToObjDisp(long senKey)
      {
         ObjectDisplay objDisp = SenDispList.First(s => s.ID == senKey);

         return objDisp;
      }

      /// <summary>
      /// Return all the ObjectDisplays associated with the input senKeys
      /// </summary>
      /// <param name="senKeys">The sen keys.</param>
      /// <returns>list of ObjectDisplays</returns>
      static public List<ObjectDisplay> SenKeysToObjDispList(List<long> senKeys)
      {
         var objDispList = (from senObjDisp in SenDispList where senKeys.Contains(senObjDisp.ID) select (ObjectDisplay)senObjDisp).ToList<ObjectDisplay>();

         return objDispList;
      }

      ///// <summary>
      ///// Return all the ObjectDisplays associated with the input senKeys
      ///// </summary>
      ///// <param name="senKeys">The sen keys.</param>
      ///// <returns>list of ObjectDisplays</returns>
      //static public List<ObjectDisplay> SenKeysToObjDispList(List<long> senKeys)
      //{
      //   var objDispList = (from senObjDisp in SenDispList where senKeys.Contains(senObjDisp.ID) select (ObjectDisplay)senObjDisp).ToList<ObjectDisplay>();

      //   return objDispList;
      //}

      /// <summary>
      /// Refresh the grid view everytime a cell data is changed in the SpaceObjectCtrl
      /// </summary>
      public static void RaiseSenDispDataChanged()
      {
         if (SenDispDataChanged != null)
            SenDispDataChanged();
      }


      /// <summary>
      /// Raise event when user select/deselect/reverse... selected sensor list in the SpaceObjectCtr
      /// </summary>
      public static void RaiseSenSelectChanged()
      {
         if (SenSelectChanged != null)
            SenSelectChanged();
      }
      /// <summary>
      /// Raise the ShowSensorInfo event so the ShowSatelliteInfo form is displayed
      /// </summary>
      public static void OpenSensorInfoWindow()
      {
         if (ShowSensorInfoForm != null)
            ShowSensorInfoForm();
      }

      /// <summary>
      /// Raise the SenIDChanged event
      /// </summary>
      /// <param name="senKey">new senKey</param>
      public static void RaiseSenIDChanged(long senKey)
      {
         if (SenIDChanged != null)
            SenIDChanged(senKey);
      }

   }
}
