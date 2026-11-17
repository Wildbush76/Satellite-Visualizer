using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace AFSPC.AstroStds.Utilities
{
   public delegate int GetKey32FieldFnPtr(int keyInt, int idx, byte[] retValStr);
   public delegate int GetKey64FieldFnPtr(long keyLong, int idx, byte[] retValStr);

   public delegate void GetFieldFnPtr(int idx, byte[] retValStr);


   public static class Utility
   {
      /// <summary>
      /// path to the AstroStd dlls - contains only a single folder - this is only for .NET loading dlls (not Fortran)
      /// search path can be set using these methods: ASTROSTDDLLPATH (compiled time), and SetASLibPath (run time)
      /// </summary>

#if DEBUG
      //this is for your local path
      public static string ASTROSTDDLLPATH = @"C:\dev\asdevenv\Lib_x64\Debug";
      //public static string ASTROSTDDLLPATH = @"C:\Users\jaszkowiake\Documents\repo\Lib_Win32\Debug";
#elif RELEASE
      public static string ASTROSTDDLLPATH = @"C:\dev\asdevenv\Lib_x64\Release";
      // public static string ASTROSTDDLLPATH = @".\"; // for SPToEGP
#else
      public static string ASTROSTDDLLPATH = ""; // This one is for UnitTests which uses "Final" option which then uses PATH env to specify AS library path
#endif


      private static int GETSETSTRLEN = 512;
      private static bool isInit = true;



      //// static constructor to set up the AS library path 
      //static Utility()
      //{
      //   //Init();
      //}

      public static void Init()
      {
         if (isInit) return;
         isInit = true;
      }






      /// <summary>
      /// Converts a byte array to string
      /// </summary>
      /// <param name="sBytes">An array of bytes</param>
      /// <returns>The resulting string</returns>
      public static string BytesToString(byte[] sBytes)
      {
         return new string(System.Text.Encoding.ASCII.GetChars(sBytes));
      }


      /// <summary>
      /// Converts a byte array to string and then remove trailing white-space characters
      /// </summary>
      /// <param name="sBytes">array of bytes</param>
      /// <returns>the resulting string</returns>
      public static string BytesToStringRTrim(byte[] sBytes)
      {
         return new string(System.Text.Encoding.ASCII.GetChars(sBytes)).TrimEnd();
      }

      /// <summary>
      /// Converts a byte array to string and then trim the string (remove all leading and trailing white-space characters)
      /// </summary>
      /// <param name="sBytes">array of bytes</param>
      /// <returns>the resulting string</returns>
      public static string BytesToStringLRTrim(byte[] sBytes)
      {
         return new string(System.Text.Encoding.ASCII.GetChars(sBytes)).Trim();
      }

      /// <summary>
      /// Converts a byte array to integer
      /// </summary>
      /// <param name="sBytes">array of bytes</param>
      /// <returns>the resulting integer</returns>
      public static int BytesToInt(byte[] sBytes)
      {
         int intVal;

         int.TryParse(BytesToString(sBytes), out intVal);

         return intVal;
      }


      /// <summary>
      /// Converts a byte array to double
      /// </summary>
      /// <param name="sBytes">array of bytes</param>
      /// <returns>the resulting double</returns>
      public static double BytesToDouble(byte[] sBytes)
      {
         double doubleVal;

         double.TryParse(BytesToString(sBytes), out doubleVal);

         return doubleVal;
      }


      /// <summary>
      /// Opens a file for writing and check for error
      /// </summary>
      /// <param name="fileName">The input file name</param>
      /// <returns>A stream writer object</returns>
      public static StreamWriter FileOpen(string fileName)
      {
         FileStream fs;
         StreamWriter sw = null;

         try
         {
            fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fs);
         }
         catch
         {
            //MessageBox.Show("Error opening file " + fileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.WriteLine("Error opening file " + fileName, "File IO Error");
            return null;
         }

         return sw;
      }

      /// <summary>
      /// Opens a file for appending
      /// </summary>
      /// <param name="fileName">The input file name</param>
      /// <returns>A stream writer object</returns>
      public static StreamWriter FileAppend(string fileName)
      {
         FileStream fs;
         StreamWriter sw = null;

         // File contains all obs/els test cases
         try
         {
            fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fs);
         }
         catch
         {
            //MessageBox.Show("Error opening file " + fileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.WriteLine("Error opening file " + fileName, "File IO Error");
            System.Environment.Exit(1);
         }

         return sw;
      }

      /// <summary>
      /// Opens a file for reading
      /// </summary>
      /// <param name="fileName">The input file name</param>
      /// <returns>A stream reader object</returns>
      public static StreamReader FileRead(string fileName)
      {
         return FileRead(fileName, true);
      }

      /// <summary>
      /// Opens a file for reading
      /// </summary>
      /// <param name="fileName">The input file name</param>
      /// <param name="showError">An option whether or not to show a message box when an error occurs</param>
      /// <returns>A stream reader object</returns>
      public static StreamReader FileRead(string fileName, bool showError)
      {
         FileStream fs;
         StreamReader sr = null;

         // File contains all obs/els test cases
         try
         {
            fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            sr = new StreamReader(fs);
         }
         catch (Exception)
         {
            if (showError)
            {
               //MessageBox.Show("Error opening file " + fileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               return null;
            }
         }

         return sr;
      }

      /// <summary>
      /// Opens a file for reading
      /// </summary>
      /// <param name="fileName">The input file name</param>
      /// <returns>A stream writer object</returns>
      public static StreamWriter FileOpenGUI(string fileName)
      {
         FileStream fs;
         StreamWriter sw = null;

         try
         {
            fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fs);
         }
         catch
         {
            //MessageBox.Show("Error opening file " + fileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
         }

         return sw;
      }


      /// <summary>
      /// Opens a file using the specified mode
      /// </summary>
      /// <param name="fileName">The input file name</param>
      /// <param name="mode">File access mode</param>
      /// <returns>A stream writer object</returns>
      public static StreamWriter FileOpenGUI(string fileName, FileMode mode)
      {
         FileStream fs;
         StreamWriter sw = null;

         try
         {
            fs = new FileStream(fileName, mode, FileAccess.Write);
            sw = new StreamWriter(fs);
         }
         catch
         {
            //MessageBox.Show("Error opening file " + fileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
         }

         return sw;
      }

      /// <summary>
      /// Converts minutes since satellite's epoch to ds50UTC
      /// </summary>
      /// <param name="epochDs50UTC">the satellite's epoch time in ds50UTC</param>
      /// <param name="mse">number of minutes since the satellite's epoch</param>
      /// <returns>days since 1950, UTC</returns>
      public static double MseToDs50UTC(double epochDs50UTC, double mse)
      {
         return epochDs50UTC + (mse / 1440.0);
      }

      /// <summary>
      /// Returns a double for the requested function/field index
      /// </summary>
      /// <param name="fnPtr">function name</param>
      /// <param name="keyLong">32-bit integer key</param>
      /// <param name="idx">field index</param>
      /// <returns>the resulting double value</returns>
      public static double GetKey32Double(GetKey32FieldFnPtr fnPtr, int keyInt, int idx)
      {
         int errCode;
         byte[] retValStr = new byte[GETSETSTRLEN];

         errCode = fnPtr(keyInt, idx, retValStr);
         if (errCode != 0)
            return 0.0;
         else
            return BytesToDouble(retValStr);
      }


      /// <summary>
      /// Returns an integer for the requested function/field index
      /// </summary>
      /// <param name="fnPtr">function name</param>
      /// <param name="keyLong">32-bit integer key</param>
      /// <param name="idx">field index</param>
      /// <returns>the resulting integer value</returns>
      public static int GetKey32Int(GetKey32FieldFnPtr fnPtr, int keyInt, int idx)
      {
         int errCode;
         byte[] retValStr = new byte[GETSETSTRLEN];

         errCode = fnPtr(keyInt, idx, retValStr);
         if (errCode != 0)
            return 0;
         else
            return BytesToInt(retValStr);
      }

      /// <summary>
      /// Returns a character for the requested function/field index
      /// </summary>
      /// <param name="fnPtr">function name</param>
      /// <param name="keyLong">32-bit integer key</param>
      /// <param name="idx">field index</param>
      /// <returns>the resulting character value</returns>
      public static char GetKey32Char(GetKey32FieldFnPtr fnPtr, int keyInt, int idx)
      {
         int errCode;
         byte[] retValStr = new byte[GETSETSTRLEN];

         errCode = fnPtr(keyInt, idx, retValStr);
         if (errCode != 0)
            return ' ';
         else
            return (char)retValStr[0];
      }

      /// <summary>
      /// Returns a string for the requested function/field index
      /// </summary>
      /// <param name="fnPtr">function name</param>
      /// <param name="keyLong">32-bit integer key</param>
      /// <param name="idx">field index</param>
      /// <returns>the resulting string</returns>
      public static string GetKey32String(GetKey32FieldFnPtr fnPtr, int keyInt, int idx)
      {
         int errCode;
         byte[] retValStr = new byte[GETSETSTRLEN];

         errCode = fnPtr(keyInt, idx, retValStr);
         if (errCode != 0)
            return "";
         else
            return BytesToStringLRTrim(retValStr);
      }

      /// <summary>
      /// Returns a double for the requested function/field index
      /// </summary>
      /// <param name="fnPtr">function name</param>
      /// <param name="keyLong">64-bit integer key</param>
      /// <param name="idx">field index</param>
      /// <returns>the resulting double value</returns>
      public static double GetKey64Double(GetKey64FieldFnPtr fnPtr, long keyLong, int idx)
      {
         int errCode;
         byte[] retValStr = new byte[GETSETSTRLEN];

         errCode = fnPtr(keyLong, idx, retValStr);
         if (errCode != 0)
            return 0.0;
         else
         {
            double temp = BytesToDouble(retValStr);
            return BytesToDouble(retValStr);
         }
      }

      /// <summary>
      /// Returns an integer for the requested function/field index
      /// </summary>
      /// <param name="fnPtr">function name</param>
      /// <param name="keyLong">64-bit integer key</param>
      /// <param name="idx">field index</param>
      /// <returns>the resulting integer value</returns>
      public static int GetKey64Int(GetKey64FieldFnPtr fnPtr, long keyLong, int idx)
      {
         int errCode;
         byte[] retValStr = new byte[GETSETSTRLEN];

         errCode = fnPtr(keyLong, idx, retValStr);
         if (errCode != 0)
            return 0;
         else
            return BytesToInt(retValStr);
      }

      /// <summary>
      /// Returns a character for the requested function/field index
      /// </summary>
      /// <param name="fnPtr">function name</param>
      /// <param name="keyLong">64-bit integer key</param>
      /// <param name="idx">field index</param>
      /// <returns>the resulting character value</returns>
      public static char GetKey64Char(GetKey64FieldFnPtr fnPtr, long keyLong, int idx)
      {
         int errCode;
         byte[] retValStr = new byte[GETSETSTRLEN];

         errCode = fnPtr(keyLong, idx, retValStr);
         if (errCode != 0)
            return ' ';
         else
            return (char)retValStr[0];
      }


      /// <summary>
      /// Returns a string value obtained from calling the input function/field index
      /// </summary>
      /// <param name="fnPtr">function name</param>
      /// <param name="keyLong">64-bit integer key</param>
      /// <param name="idx">field index</param>
      /// <returns>the resulting string</returns>
      public static string GetKey64String(GetKey64FieldFnPtr fnPtr, long keyLong, int idx)
      {
         int errCode;
         byte[] retValStr = new byte[GETSETSTRLEN];

         errCode = fnPtr(keyLong, idx, retValStr);
         if (errCode != 0)
            return "";
         else
            return BytesToStringLRTrim(retValStr);
      }


      /// <summary>
      /// Returns a double value obtained from calling the input function/field index
      /// </summary>
      /// <param name="fnPtr">function name</param>
      /// <param name="idx">field index</param>
      /// <returns>the resulting double value</returns>
      public static double GetDouble(GetFieldFnPtr fnPtr, int idx)
      {
         byte[] retValStr = new byte[GETSETSTRLEN];

         fnPtr(idx, retValStr);

         return BytesToDouble(retValStr);
      }

      /// <summary>
      /// Returns an integer value obtained from calling the input function/field index
      /// </summary>
      /// <param name="fnPtr">function name</param>
      /// <param name="idx">field index</param>
      /// <returns>the resulting integer value</returns>
      public static int GetInt(GetFieldFnPtr fnPtr, int idx)
      {
         byte[] retValStr = new byte[GETSETSTRLEN];

         fnPtr(idx, retValStr);

         return BytesToInt(retValStr);
      }


      /// <summary>
      /// Returns a character value obtained from calling the input function/field index
      /// </summary>
      /// <param name="fnPtr">function name</param>
      /// <param name="idx">field index</param>
      /// <returns>the resulting character value</returns>
      public static char GetChar(GetFieldFnPtr fnPtr, int idx)
      {
         byte[] retValStr = new byte[GETSETSTRLEN];

         fnPtr(idx, retValStr);

         return (char)retValStr[0];
      }


      /// <summary>
      /// Returns a string obtained from calling the input function/field index
      /// </summary>
      /// <param name="fnPtr">function name</param>
      /// <param name="idx">field index</param>
      /// <returns>The resulting string</returns>
      public static string GetString(GetFieldFnPtr fnPtr, int idx)
      {
         byte[] retValStr = new byte[GETSETSTRLEN];

         fnPtr(idx, retValStr);

         return BytesToStringLRTrim(retValStr);
      }

      /// <summary>
      /// Updates the timing constants file in the shared folder
      /// </summary>
      /// <param name="fullTContFile">The current main timing constants file</param>
      /// <param name="partTContFile">The new partial timing constants that needs to be added to the main file</param>
      public static void UpdateTimingConstants(string fullTContFile, string partTContFile)
      {
         if (File.GetLastWriteTime(fullTContFile) < File.GetLastWriteTime(partTContFile))
         {

            string[] fullTConStrings = null;
            try
            {
               fullTConStrings = File.ReadAllLines(fullTContFile);
            }
            catch
            {
               //MessageBox.Show("Error reading from file \"" + fullTContFile + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               return;
            }

            string[] partTConStrings = null;
            try
            {
               partTConStrings = File.ReadAllLines(partTContFile);
            }
            catch
            {
               //MessageBox.Show("Error reading from file \"" + partTContFile + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               return;
            }

            List<string> mergedTConStrings = new List<string>();

            foreach (string currStr in fullTConStrings)
            {
               if (currStr.Substring(0, 20) == partTConStrings[0].Substring(0, 20))
                  break;
               mergedTConStrings.Add(currStr);
            }

            mergedTConStrings.AddRange(partTConStrings);
            File.WriteAllLines(fullTContFile, mergedTConStrings.ToArray());
         }
      }

      /// <summary>
      /// Converts VCM's concatenated string to multi-line
      /// </summary>
      /// <param name="vcmBytes">VCM concatenated string</param>
      /// <returns>multi-line VCM in a string list</returns>
      public static List<string> VcmBytesToStrList(byte[] vcmBytes)
      {
         List<string> vcmStrList = new List<string>();
         string vcmString = Utility.BytesToString(vcmBytes);

         foreach (string token in vcmString.Split(new char[] { '<' }, StringSplitOptions.RemoveEmptyEntries))
         {
            vcmStrList.Add("<" + token.TrimEnd());
         }

         return vcmStrList;
      }

      /// <summary>
      /// Determine if the input value is small enough to be displayed as 0.000 instead of -0.000 
      /// </summary>
      /// <param name="inValue">input value</param>
      /// <param name="decPlaces">number of decimal places</param>
      /// <returns></returns>
      public static double NonNegativeZero(double inValue, int decPlaces)
      {
         if (inValue <= 0 && (-inValue * Math.Pow(10.0, decPlaces + 1) < 5.0))
            return 0;
         else
            return inValue;
      }


      /// <summary>
      /// Convert decimal degrees to degrees, minutes, and seconds
      /// </summary>
      /// <param name="decDeg"></param>
      /// <param name="deg"></param>
      /// <param name="mm"></param>
      /// <param name="ss"></param>
      public static void DecToDegMMSS(double inDecDeg, out int deg, out int mm, out double ss)
      {
         double decDeg = Math.Abs(inDecDeg);

         deg = (int)(decDeg);
         mm = (int)((decDeg - deg) * 60.0);
         ss = (decDeg - deg - mm / 60.0) * 3600.0;

         if (inDecDeg < 0)
            deg = -deg;
      }

      /// <summary>
      /// Convert decimal degrees to hours, minutes, and seconds
      /// </summary>
      /// <param name="decDeg">input decimal degree</param>
      /// <param name="hh">output hours</param>
      /// <param name="mm">output minutes</param>
      /// <param name="sss">output seconds</param>
      public static void DecToHHMMSSS(double decDeg, out int hh, out int mm, out double sss)
      {
         int imin, ihr;
         double degree, fHH, fMM;

         degree = decDeg % 360.0; // in case deg > 360
         if (degree < 0)
            degree += 360.0;

         fHH = degree / 15.0;
         hh = (int)fHH;
         fMM = 60.0 * (fHH - hh) + 1.0e-12;
         mm = (int)fMM;

         sss = (fMM - mm) * 60.0;

         //DegtoHHMMSSS_Str uses a rounding f'n, catch borderline cases
         if (sss >= 59.95)
         {
            sss = 0.0;
            imin = 1;
            ihr = (int)((mm + imin) / 60.0);
            mm = (mm + imin) % 60;
            hh = (hh + ihr) % 24;
         }
      }

      /// <summary>
      /// Convert decimal degrees to hours, minutes, and seconds
      /// </summary>
      /// <param name="decDeg">input decimal degree</param>
      /// <param name="hh">output hours</param>
      /// <param name="mm">output minutes</param>
      /// <param name="sss">output seconds</param>
      public static string DecToHHMMSSS_Str(double decDeg)
      {
         int hh;
         int mm;
         double sss;
         int ihr, imin, isec;

         DecToHHMMSSS(decDeg, out hh, out mm, out sss);

         isec = (int)Math.Round(sss * 10.0); //number of 1 / 10th of seconds

         if (isec >= 600)
         {
            imin = 1;
            isec = 0;
         }
         else
         {
            imin = 0;
         }

         ihr = (int)((mm + imin) / 60.0);
         mm = (mm + imin) % 60;
         hh = (hh + ihr) % 24;

         string hhmmss_str = string.Format("{0:D2}{1:D2}{2:D3}", hh, mm, isec);
         return hhmmss_str;
      }

      /// <summary>
      /// Returns magnitude of a vector
      /// </summary>
      /// <param name="vec"></param>
      /// <returns>magnitude of the input vector</returns>
      public static double VecMag(double[] vec)
      {
         return Math.Sqrt(vec[0] * vec[0] + vec[1] * vec[1] + vec[2] * vec[2]);
      }

      /// <summary>
      /// Returns unit vector of a vector
      /// </summary>
      /// <param name="vec"></param>
      /// <returns>unit vector</returns>
      public static double[] UnitVec(double[] vec)
      {
         double vecMag = VecMag(vec);
         return new double[] { vec[0] / vecMag, vec[1] / vecMag, vec[2] / vecMag };
      }

      /// <summary>
      /// Multiplies a vector by a scalar 
      /// </summary>
      /// <param name="vec"></param>
      /// <param name="scalar"></param>
      /// <returns>resulting vector</returns>
      public static double[] VecScalarMul(double scalar, double[] vec)
      {
         return new double[] { vec[0] * scalar, vec[1] * scalar, vec[2] * scalar };
      }

      /// <summary>
      /// Computes dot product of two vectors
      /// </summary>
      /// <param name="vec1"></param>
      /// <param name="vec2"></param>
      /// <returns>dot product of two vector</returns>
      public static double DotProduct(double[] vec1, double[] vec2)
      {
         return vec1[0] * vec2[0] + vec1[1] * vec2[1] + vec1[2] * vec2[2];
      }


      /// <summary>
      /// Computes angle between two vectors (0-180)
      /// </summary>
      /// <param name="vec1"></param>
      /// <param name="vec2"></param>
      /// <returns>angle in degrees</returns>
      public static double AngleBetween(double[] vec1, double[] vec2)
      {
         return Math.Acos(DotProduct(vec1, vec2) / (VecMag(vec1) * VecMag(vec2))) * (180 / Math.PI);
      }

      /// <summary>
      /// Computes counter clockwise angle from vector vec1 to vector vec2 (0-360)
      /// </summary>
      /// <param name="vec1"></param>
      /// <param name="vec2"></param>
      /// <returns>angle in degrees</returns>
      public static double AngleBetween0And360(double[] vec1, double[] vec2)
      {
         double angle = Utility.AngleBetween(vec1, vec2);

         // using the sign of the vector cross product (plus or minus in the z direction which normal to chase and target position vectors) 
         // to determine if the angle is more or less than 180 degrees
         bool lessThan180 = Utility.CrossProduct(vec1, vec2)[2] >= 0;

         if (!lessThan180)
            angle = 360.0 - angle;

         return angle;

      }


      /// <summary>
      /// Computes cross product of two vectors
      /// </summary>
      /// <param name="vec1"></param>
      /// <param name="vec2"></param>
      /// <returns>cross product</returns>
      public static double[] CrossProduct(double[] vec1, double[] vec2)
      {
         double x, y, z;

         x = vec1[1] * vec2[2] - vec2[1] * vec1[2];
         y = vec1[2] * vec2[0] - vec1[0] * vec2[2];
         z = vec1[0] * vec2[1] - vec1[1] * vec2[0];


         return new double[] { x, y, z };
      }

      public static double[] VecAdd(double[] vec1, double[] vec2)
      {
         return new double[] { vec1[0] + vec2[0], vec1[1] + vec2[1], vec1[2] + vec2[2] };
      }

      public static double[] VecSubtract(double[] vec1, double[] vec2)
      {
         return new double[] { vec1[0] - vec2[0], vec1[1] - vec2[1], vec1[2] - vec2[2] };
      }


      public static double[] PlaneXingVec(double[] pos1, double[] vel1, double[] pos2, double[] vel2)
      {
         double[] normPlan1 = CrossProduct(pos1, vel1);
         double[] normPlan2 = CrossProduct(pos2, vel2);
         return CrossProduct(normPlan1, normPlan2);
      }

      public static double DistBtw2Pts(double[] pos1, double[] pos2)
      {
         double dx = pos1[0] - pos2[0];
         double dy = pos1[1] - pos2[1];
         double dz = pos1[2] - pos2[2];

         return Math.Sqrt(dx * dx + dy * dy + dz * dz);
      }

      public static double[] ComputeWVector(double incli, double node)
      {
         double[] wVec = new double[3];
         const double DEG2RAD = Math.PI / 180;


         wVec[0] = Math.Sin(incli * DEG2RAD) * Math.Sin(node * DEG2RAD);
         wVec[1] = -Math.Sin(incli * DEG2RAD) * Math.Cos(node * DEG2RAD);
         wVec[2] = Math.Cos(incli * DEG2RAD);

         return wVec;
      }

      public static double Modulo(double x, double y)
      {
         double result = x % y;
         if ((result < 0 && y > 0) || (result > 0 && y < 0))
         {
            result += y;
         }
         return result;
      }
   }
}
