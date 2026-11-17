using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;

namespace AFSPC.AstroStds.Models
{
   [Serializable]
   public class SatCat
   {
      [Browsable(false)]
      public int ErrCode { get; set; }

      public int SatNo { get; set; }
      public string CommonName { get; set; }
      public string IntID { get; set; }
      public string Owner { get; set; }
      
      public string LaunchSite { get; set; }
      //public string LaunchDate { get; set; }
      //public string ReEntryDate { get; set; }
      public DateTime LaunchDate { get; set; }
      public DateTime ReEntryDate { get; set; }

      public double Period { get; set; }
      public double Inclination { get; set; }
      public double Apogee { get; set; }
      public double Perigee { get; set; }
      public int CommentCode { get; set; }
      public double RCSValue { get; set; }
      public string RCSSource { get; set; }
      public string OrbitInfo { get; set; }

      public SatCat(string card)
      {
         ErrCode = 1;
         double tempDouble;
         int tempInt;
         DateTime tempDateTime;

         try
         {
            int.TryParse(card.Substring(12, 5), out tempInt);
            SatNo = tempInt;
            CommonName = card.Substring(18, 24);
            IntID = card.Substring(0, 11);
            Owner = card.Substring(43, 3);
            
            DateTime.TryParse(card.Substring(49, 9), out tempDateTime);
            //LaunchDate = tempDateTime.ToString("MM/dd/yyyy");
            LaunchDate = tempDateTime;

            LaunchSite = card.Substring(60, 5);

            DateTime.TryParse(card.Substring(67, 9), out tempDateTime);
            ReEntryDate = tempDateTime;
            //ReEntryDate = tempDateTime.ToString("MM/dd/yyyy");
            //if (card.Substring(82, 4) == "#.##")
            if(System.Text.RegularExpressions.Regex.Match(card.Substring(82, 4), @"\d.\d\d").Success)
            {
               double.TryParse(card.Substring(77, 9), out tempDouble);
               Period = tempDouble;
               double.TryParse(card.Substring(89, 6), out tempDouble);
               Inclination = tempDouble;
               double.TryParse(card.Substring(96, 7), out tempDouble);
               Apogee = tempDouble;
               double.TryParse(card.Substring(104, 7), out tempDouble);
               Perigee = tempDouble;
               int.TryParse(card.Substring(113, 1), out tempInt);
               CommentCode = tempInt;
               double.TryParse(card.Substring(115, 9), out tempDouble);
               RCSValue = tempDouble;
               RCSSource = card.Substring(125, 3);
            }
            else
            {
               OrbitInfo = card.Substring(77, 51);
            }
            ErrCode = 0;
         }
         catch
         {
            ErrCode = 1;

         }
      }

      public static List<SatCat> PopulateSatCat(string satCatFile)
      {
         List<SatCat> satCatList = new List<SatCat>();

         if (!File.Exists(satCatFile))
            return null;

         string[] satCatCards =  null;
         try
         {
            satCatCards = File.ReadAllLines(satCatFile);
         }
         catch
         {
            //MessageBox.Show("Error reading from file \"" + satCatFile + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
         }

         if (satCatCards == null || satCatCards.Length == 0)
            return null;

         foreach (string card in satCatCards)
         {
            SatCat satCat = new SatCat(card);
            if (satCat.ErrCode == 0)
               satCatList.Add(satCat);

         }

         return satCatList;

      }

   }
}

