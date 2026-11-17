using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace AFSPC.AstroStds.Wrappers
{
   public static partial class ExtEphemWrapper
   {
      public enum InputCoordSystems
      {
         [Description("ECI")]
         COORD_ECI = 1,
         [Description("J2K")]
         COORD_J2K = 2,
         [Description("EFG")]
         COORD_EFG = 3,
         [Description("ECR")] 
         COORD_ECR = 4,
         [Description("LLH")]
         COORD_LLH = 5,
         [Description("SEN")]
         COORD_SEN = 6,

         [Description("ECIFP")]
         COORD_ECIFP = 11,
         [Description("J2KFP")]
         COORD_J2KFP = 12,
         [Description("EFGFP")]
         COORD_EFGFP = 13,
         [Description("ECRFP")]
         COORD_ECRFP = 14,
         [Description("LLHOV")]
         COORD_LLHOV = 15,
         [Description("SENOV")]
         COORD_SENOV = 16,

         [Description("HCSRL")]
         COORD_HCSRL = 17,
         [Description("WPTRL")]
         COORD_WPTRL = 18,
         [Description("HCSGC")]
         COORD_HCSGC = 19,
         [Description("WPTGC")]
         COORD_WPTGC = 20,

      };

      public static void PrintVecEphHeader(StreamWriter sw, string keStr, string erStr, string epochStr, string orbEltType, string EOL)
      {
         sw.Write(" {0} {1} {2}{3,14}SP {4}{5}", keStr, erStr, epochStr, " ", orbEltType, EOL);
      }



      public static void PrintSimObsHeader(StreamWriter sw, string keStr, string erStr, string epochStr, string EOL)
      {
         sw.Write(" {0} {1} {2}{3,19}Z{4}", keStr, erStr, epochStr, " ", EOL);
      }

      public static void PrintITCHeader(StreamWriter sw, int satNum, string keStr, string erStr, string epochStr, string EOL)
      {
         sw.Write(" {0:D5} {1} {2} {3} {4,43}SP{5}", satNum, keStr, erStr, epochStr, " ", EOL);
      }


      public static void PrintOCMHeader(StreamWriter sw, int satNum, string createTimeStr, string epochStr, string EOL)
      {
         sw.Write("[CCSDS_OCM_VERS           = 3.0" + EOL);
         sw.Write("CREATION_DATE            = {0}", createTimeStr + EOL);
         sw.Write("ORIGINATOR               = " + EOL + EOL);
         sw.Write("META_START" + EOL);
         sw.Write("OBJECT_DESIGNATOR        = {0}", satNum + EOL);
         sw.Write("TIME_SYSTEM              = UTC" + EOL);
         sw.Write("EPOCH_TZERO              = {0}", epochStr + EOL);
         sw.Write("META_STOP" + EOL + EOL);
      }

      public static void PrinOCMBody(StreamWriter sw, StringBuilder sbOcmTraj, StringBuilder sbOcmCov, string EOL)
      {
         sw.Write("TRAJ_START" + EOL);
         sw.Write("CENTER_NAME              = EARTH"   + EOL);
         sw.Write("TRAJ_REF_FRAME           = EME2000" + EOL);
         sw.Write("TRAJ_TYPE                = CARTPV"  + EOL);
         sw.Write(sbOcmTraj.ToString());
         sw.Write("TRAJ_STOP" + EOL + EOL);
         sw.Write("PHYS_START" + EOL);
         sw.Write("PHYS_STOP"  + EOL + EOL);
         if(sbOcmCov.Length > 0)
         {
            sw.Write("COV_START" + EOL);
            sw.Write("COV_REF_FRAME            = RSW_INERTIAL" + EOL);
            sw.Write("COV_TYPE                 = CARTPV" + EOL);
            sw.Write("COV_ORDERING             = LTM" + EOL);
            sw.Write(sbOcmCov.ToString());
            sw.Write("COV_STOP" + EOL + EOL);
         }

         sw.Write("]" + EOL + EOL);
      }

      public static void GetOCMTraj(StringBuilder sbTraj, string ds50DTGForm2, double[] posJ2K, double[] velJ2K, string EOL)
      {
         sbTraj.Append(String.Format("{0} {1,15:F6} {2,15:F6} {3,15:F6} {4,15:F9} {5,15:F9} {6,15:F9}{7}", ds50DTGForm2, posJ2K[0], posJ2K[1], posJ2K[2], velJ2K[0], velJ2K[1], velJ2K[2], EOL));
      }

      public static void GetOCMCov(StringBuilder sbCov, string ds50DTGForm2, double[,] uvwCovMtx, string EOL)
      {
         sbCov.Append(ds50DTGForm2 + " ");

         int col = 0;
         for (int i = 0; i < 6; i++)
         {
            for (int j = 0; j < 6; j++)
            {
               if (j <= i)
               {
                  col = col + 1;
                  sbCov.Append(uvwCovMtx[i, j].ToString("+.00000000E+00;-.00000000E+00").Replace("+.", " 0.").Replace("-.", "-0.") + " ");
               }
            }
         }

         sbCov.Append(EOL);
      }

      public static void PrintVecEphPoint(StreamWriter sw, int satNum, string ds50UTCStr, double mse, double[] pos, double[] vel, string orbEltType, string EOL)
      {
         char vecEphFileType = orbEltType == "efg" ? '3' : '1';


         sw.Write(" {0,16:F6} {1,15:F6} {2,15:F6} {3,15:F6} {4:D5} {5} SP {6}{7}", mse, pos[0], pos[1], pos[2], satNum, vecEphFileType, orbEltType, EOL);
         sw.Write("  {0} {1,15:F9} {2,15:F9} {3,15:F9} {4:D5} {5} SP {6}{7}", ds50UTCStr, vel[0], vel[1], vel[2], satNum, vecEphFileType, orbEltType, EOL);
      }



      public static void PrintSimObsPoint(StreamWriter sw, int satNum, string ds50UTCStr, int revNum, double[] pos, double[] vel, string orbEltType, string EOL)
      {
         byte[] dtgByte = new byte[15];
         string posX, posY, posZ, velX, velY, velZ;


         posX = pos[0].ToString("+.000000000000000E+00;-.000000000000000E+00").Replace("+.", "0.");
         posY = pos[1].ToString("+.000000000000000E+00;-.000000000000000E+00").Replace("+.", "0.");
         posZ = pos[2].ToString("+.000000000000000E+00;-.000000000000000E+00").Replace("+.", "0.");
         velX = vel[0].ToString("+.000000000000000E+00;-.000000000000000E+00").Replace("+.", "0.");
         velY = vel[1].ToString("+.000000000000000E+00;-.000000000000000E+00").Replace("+.", "0.");
         velZ = vel[2].ToString("+.000000000000000E+00;-.000000000000000E+00").Replace("+.", "0.");

         sw.Write(" {0:D5} {1}  {2:D5}{3,46}{4} Z{5}", satNum, ds50UTCStr, revNum, " ", orbEltType, EOL);
         sw.Write(" {0} {1} {2} {3,8}{4} Z{5}", posX, posY, posZ, " ", orbEltType, EOL);
         sw.Write(" {0} {1} {2} {3,8}{4} Z{5}", velX, velY, velZ, " ", orbEltType, EOL);

      }

      public static void PrintPObsPoint(StreamWriter sw,int satNum,int senNum, string ds50UTCStr, double[] pos, string EOL)
      {
         double [] posER = new double[3];
         for (int i = 0; i < 3; i++)
			{
            posER[i] = pos[i] / 6378.135; //WGS-72 Earth Radii
			}
         string timeString = ds50UTCStr.Replace(".","");
         string satSenNum = satNum.ToString("00000") + senNum.ToString("000");
         sw.Write("U{0}{1}{2,12:F8}{3,12:F8}{4,12:F8}{5,15}P{6}", satSenNum, timeString,posER[0], posER[1], posER[2]," ",EOL );
      }

      public static void PrintOwnerOperPoint(StreamWriter sw, string ds50UTCStr, double[] pos, double[] vel, string EOL)
      {
         sw.Write("{0} {1,15:F6} {2,15:F6} {3,15:F6} {4,15:F9} {5,15:F9} {6,15:F9}{7}",
            ds50UTCStr, pos[0], pos[1], pos[2], vel[0], vel[1], vel[2], EOL);
      }

      public static void PrintPosLLHPoint(StreamWriter sw, string ds50UTCStr, double[] pos, double[] vel, double[] llh, string EOL)
      {
         sw.Write("{0} {1,15:F6} {2,15:F6} {3,15:F6} {4,15:F9} {5,15:F9} {6,15:F9} {7,15:F9} {8,15:F9} {9,15:F9}{10}",
            ds50UTCStr, pos[0], pos[1], pos[2], vel[0], vel[1], vel[2], llh[0], llh[1], llh[2], EOL);
      }

      public static void PrintITCPoint(StreamWriter sw, string ds50UTCStr, double[] pos, double[] vel, double[,] uvwCovMtx, string EOL)
      {
         sw.Write(" {0} {1,15:F6} {2,15:F6} {3,15:F6} {4,15:F9} {5,15:F9} {6,15:F9}{7}",
            ds50UTCStr, pos[0], pos[1], pos[2], vel[0], vel[1], vel[2], EOL);
         
         int col = 0;
         sw.Write("  ");
         for (int i = 0; i < 6; i++)
         {
            for (int j = 0; j < 6; j++)
            {
               string item;
               if (j <= i)
               {
                  col = col + 1;
                  item = uvwCovMtx[i, j].ToString("+.00000000E+00;-.00000000E+00").Replace("+.", " 0.").Replace("-.", "-0.");
                  sw.Write(" {0}", item);
                  if (col % 7 == 0 && i < 5)
                  {
                     sw.Write(EOL);
                     sw.Write("  ");
                  }
               }
            }
         }
         
         sw.Write(EOL);
         
      }


      public static void PrintOCMPoint(StreamWriter sw, string ds50UTCStr, double[] pos, double[] vel, double[,] uvwCovMtx, string EOL)
      {
         sw.Write(" {0} {1,15:F6} {2,15:F6} {3,15:F6} {4,15:F9} {5,15:F9} {6,15:F9}{7}",
            ds50UTCStr, pos[0], pos[1], pos[2], vel[0], vel[1], vel[2], EOL);

         int col = 0;
         sw.Write("  ");
         for (int i = 0; i < 6; i++)
         {
            for (int j = 0; j < 6; j++)
            {
               string item;
               if (j <= i)
               {
                  col = col + 1;
                  item = uvwCovMtx[i, j].ToString("+.00000000E+00;-.00000000E+00").Replace("+.", " 0.").Replace("-.", "-0.");
                  sw.Write(" {0}", item);
                  if (col % 7 == 0 && i < 5)
                  {
                     sw.Write(EOL);
                     sw.Write("  ");
                  }
               }
            }
         }

         sw.Write(EOL);

      }
   }
}
