using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class SensorWrapper
   {
      public enum SenViewTypeEnum
      {
         [Description("2- Bounded-cone tracker")]  // idx 0 - conical
         BoundedCone = '2',
         [Description("3- Conventional tracker")]  // idx 1
         Conventional = '3',
         [Description("4- Optical tracker")] // idx 2
         Optical = '4',
         [Description("7- Constant Azimuth Fan")] // idx 3
         Fan = '7',
         [Description("9- Orbiting sensor")]  // idx 4
         Orbiting = '9',
         [Description("R- Space fence SF_FOR")]   // idx 5
         SF_FOR = 'R',
         [Description("V- Space fence penetration")] // idx 6
         SF_FOV = 'V'
      };



      //public static char[] SenViewTypeChar = new char[] { '2', '3', '4', '9', 'R', 'V' };

      public enum SenObsTypeEnum
      {

         [Description("0- Range-rate only")]
         ObType0 = '0',
         [Description("1- Az, El")]
         ObType1 = '1',
         [Description("2- Az, El, Rng")]
         ObType2 = '2',
         [Description("3- Az, El, Rng, Rng Rate")]
         ObType3 = '3',
         [Description("4- 3 + Az Rate + El Rate")]
         ObType4 = '4',
         [Description("5- RA, DEC")]
         ObType5 = '5',
         [Description("6- Range only")]
         ObType6 = '6',
         [Description("7- Rotating EFG vector")]
         ObType7 = '7',
         [Description("8- Az, El, sen rotate EFG")]
         ObType8 = '8',
         [Description("9- RA, DEC, sen rotate EFG")]
         ObType9 = '9',
         [Description("V- ECI position/velocity vec")]
         ObType10 = 'V',
         [Description("P- ECI position vec")]
         ObType11 = 'P',
         [Description("M- 8 + Rng")]
         ObType18 = 'M',
         [Description("O- 9 + Rng")]
         ObType19 = 'O'
      };


      public enum SenOptVisFlgEnum
      {
         [Description("0- Accept All Passes (ground optical only)")]
         OptVisFlg0 = 0,
         [Description("1- Optical Visible Passes (optical sen)")]
         OptVisFlg1 = 1,
         [Description("2- 1 with Solar Aspect Angle < Max")]
         OptVisFlg2 = 2,
         [Description("3- Sen In Dark/No Restrctn on Sat (IR sen)")]
         OptVisFlg3 = 3
      };


      public enum SenPlntryRstrtnEnum
      {
         [Description("0- All (sun, moon, earth)")]
         PlntryRstrtn0,
         [Description("1- Earth Only")]
         PlntryRstrtn1,
         [Description("2- Moon Only")]
         PlntryRstrtn2,
         [Description("3- Sun Only")]
         PlntryRstrtn3,
         [Description("4- Sun, Moon Only")]
         PlntryRstrtn4,
         [Description("5- None")]
         PlntryRstrtn5
      };

      public enum SenBoreVecEnum
      {
         [Description("No Limits")]
         BoreVec0 = ' ',
         [Description("D- Down")]
         BoreVec1 = 'D',
         [Description("U- Up")]
         BoreVec2 = 'U',
         [Description("B- Back")]
         BoreVec3 = 'B',
         [Description("A- Ahead")]
         BoreVec4 = 'A',
         [Description("R- Right")]
         BoreVec5 = 'R',
         [Description("L- Left")]
         BoreVec6 = 'L',
      };

      // Returns all currently loaded senKeys
      public static long[] GetCurrLoadedSenKeys()
      {
         int numSens = SensorGetCount();
         if (numSens > 0)
         {
            long[] senKeys = new long[numSens];
            SensorGetLoaded(2, senKeys);
            return senKeys;
         }
         else
         {
            return null;
         }
      }

      public static long[] SenKeysFrFile(string inputFile)
      {
         long[] prevSenKeys = GetCurrLoadedSenKeys();
         long[] newSenKeys;

         SensorLoadFile(inputFile);
         if (prevSenKeys == null)
            newSenKeys = GetCurrLoadedSenKeys();
         else
            newSenKeys = GetCurrLoadedSenKeys().Except(prevSenKeys).ToArray<long>();

         return newSenKeys;
      }
   }
}
