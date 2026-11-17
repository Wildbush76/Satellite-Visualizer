using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace AFSPC.AstroStds.Models.DisplayModel
{
   [Serializable]
   public class ObjectDisplay
   {
      //[Browsable(false)]
      public long ID { get; set; } // satKey, senKey, obs group id

      public Color Color { get; set; }

      public string Label { get; set; }

      public int SymbolSize { get; set; }

      private static string[] myColorNames;
      private static int currColorIdx = 0;
      private static readonly int DefaultSymbolSize = 5;
      public bool Selected { get; set; }


      public ObjectDisplay()
      {
         this.ID = 0;
         this.Color = GetNextColor(); 
         this.Label = " ";
         this.SymbolSize = DefaultSymbolSize;
      }

      public ObjectDisplay(long id)
      {
         this.ID = id;
         this.Color = GetNextColor();
         this.Label = " ";
         this.SymbolSize = DefaultSymbolSize;
      }



      public ObjectDisplay(long id, Color color, string label)
      {
         this.ID = id;
         this.Color = color;
         this.Label = label;
         this.SymbolSize = DefaultSymbolSize;
      }


      public ObjectDisplay(long id, Color color, string label, int symbolSize)
      {
         this.ID = id;
         this.Color = color;
         this.Label = label;
         this.SymbolSize = symbolSize;
      }

      public ObjectDisplay(long id, Color color, string label, int symbolSize, bool selected)
      {
         this.ID = id;
         this.Color = color;
         this.Label = label;
         this.SymbolSize = symbolSize;
         this.Selected = selected;
      }
      
      public ObjectDisplay(long id, string label)
      {
         this.ID = id;
         this.Color = GetNextColor();
         this.Label = label;
      }



      public ObjectDisplay(ObjectDisplay objDisp)
      {
         this.ID = objDisp.ID;
         this.Color = objDisp.Color;
         this.Label = objDisp.Label;
         this.SymbolSize = objDisp.SymbolSize;
      }


      // Return a satellite list in the object display array
      public static List<long> ToSatKeyArray(List<ObjectDisplay> objDispArr)
      {
         List<long> satKeyArray = (from objDisp in objDispArr select objDisp.ID).ToList<long>();
         return satKeyArray;
      }


      // Return a sensor list in the object display array
      public static List<long> ToSenKeyArray(List<ObjectDisplay> objDispArr)
      {
         List<long> senKeyArray = (from objDisp in objDispArr select objDisp.ID).ToList<long>();
         return senKeyArray;
      }

      // Return a sensor list in the object display array
      public static List<long> ToObsKeyArray(List<ObjectDisplay> objDispArr)
      {
         return ToSatKeyArray(objDispArr);
      }

      public static string[] MyColorNames
      {
         get
         {
            if(myColorNames == null)
               myColorNames = Enum.GetNames(typeof(CustomColor));

            return myColorNames;
         }
      }

      public static void ResetColorIdx()
      {         
         currColorIdx = 0;
      }

      private static Color GetNextColor()
      {
         if (currColorIdx >= MyColorNames.Length)
            currColorIdx = 0;

         return Color.FromName(MyColorNames[currColorIdx++]);
      }

      // List of predifined colors
      public enum CustomColor
      {

         Blue = 1,
         Red = 2,
         Green = 3,
         Orange = 4,
         Cyan = 5,
         Fuchsia = 6,
         Yellow = 7,
         Brown = 8,
         Navy = 9,
         IndianRed = 10,
         ForestGreen = 11,
         BlueViolet = 12,
         Chocolate = 13,
         DodgerBlue = 14,
         Indigo = 15,
         Lime = 16,
         Magenta = 17,
         Maroon = 18,
         Turquoise = 19,
         Gold = 20,
         Plum = 21,
         Purple = 22,
         RoyalBlue = 23,
         SaddleBrown = 24,
         Teal = 25,
         Tomato = 26,

      }

      // Extract data (deserialize) from file
      public static List<ObjectDisplay> DeserializeObjectDisplay(string dataFile)
      {
         try
         {
            //Opens file "data.xml" and deserializes the object from it.
            Stream stream = File.Open(dataFile, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();

            List<ObjectDisplay> objDispList = (List<ObjectDisplay>)formatter.Deserialize(stream);
            if(stream != null)
            {
               stream.Close();
            }
            

            return objDispList;
         }
         catch
         {
            return null;
         }
      }

      // Save (serialize) data to file
      public static void SerializeObjectDisplay(string dataFile, List<ObjectDisplay> objDispList)
      {
         if (objDispList == null || objDispList.Count == 0)
            return;

         //Opens a file and serializes the object into it in binary format.
         Stream stream = File.Open(dataFile, FileMode.Create);
         BinaryFormatter formatter = new BinaryFormatter();

         formatter.Serialize(stream, objDispList);
         if (stream != null)
         {
            stream.Close();
         }
      }

      static public ObjectDisplay GetObjDispItem(List<ObjectDisplay> objDispList, long inputID)
      {
         foreach (ObjectDisplay objDisp in objDispList)
         {
            if (objDisp.ID == inputID)
               return objDisp;
         }

         return new ObjectDisplay();
      }

      static public List<ObjectDisplay> GetObjDispItems(List<ObjectDisplay> objDispList, List<long> inputIDs)
      {
         var objDisp = (from anObjDisp in objDispList where inputIDs.Contains(anObjDisp.ID) select anObjDisp).ToList<ObjectDisplay>();

         return objDisp;
      }

      static public ObjectDisplay GetObjDispItem(List<ObjectDisplay> objDispList, int inputID)
      {
         foreach (ObjectDisplay objDisp in objDispList)
         {
            if (objDisp.ID == inputID)
               return objDisp;
         }

         return new ObjectDisplay();
      }

      static public List<ObjectDisplay> GetObjDispItems(List<ObjectDisplay> objDispList, List<int> inputIDs)
      {
         var objDisp = (from anObjDisp in objDispList where inputIDs.Contains((int)anObjDisp.ID) select anObjDisp).ToList<ObjectDisplay>();

         return objDisp;
      }

   }
}
