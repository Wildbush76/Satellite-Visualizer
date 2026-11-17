// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 05-19-2015
//
// Last Modified By : nguyend
// Last Modified On : 04-15-2015
// ***********************************************************************
// <copyright file="BamData.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AFSPC.AstroStds.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The BamModel namespace.
/// </summary>
namespace AFSPC.AstroStds.Models.BamModel
{
   [Serializable]
   /// <summary>
   /// Class BamData.
   /// </summary>
   public class BamData
   {
      /// <summary>
      /// Gets or sets the error code.
      /// </summary>
      /// <value>The error code.</value>
      public static int ErrCode { get; set;  }
      /// <summary>
      /// Gets or sets the average separate distance.
      /// </summary>
      /// <value>The average separate distance.</value>
      public static double[] AvgSDs { get; set;  }
      /// <summary>
      /// Gets or sets the average missed distance.
      /// </summary>
      /// <value>The average missed distance.</value>
      public static double[] AvgMDs { get; set;  }
      /// <summary>
      /// Gets or sets the separate distance.
      /// </summary>
      /// <value>The separate distance.</value>
      public static double[] SDs { get; set;  }
      /// <summary>
      /// Gets or sets the pinch point xings.
      /// </summary>
      /// <value>The pinch point xings.</value>
      public static double[] PPXings { get; set;  }
      /// <summary>
      /// Gets or sets the DS50 utct ss.
      /// </summary>
      /// <value>The DS50 utct ss.</value>
      public static double[] Ds50UTCTSs { get; set; }
      //public static double[] MSETSs { get; set; }  // minutes since zero hour of epoch day

      /// <summary>
      /// Gets or sets the initial breakup time.
      /// </summary>
      /// <value>The initial breakup time.</value>
      public static double InitialBrkTime { get; set;}
      /// <summary>
      /// Gets or sets the minimum average separate distance.
      /// </summary>
      /// <value>The minimum average separate distance.</value>
      public static double MinAvgSD { get; set; }
      /// <summary>
      /// Gets or sets the initial pinch point position.
      /// </summary>
      /// <value>The initial pinch point position.</value>
      public static double[] InitialPPPos { get; set;}
      /// <summary>
      /// Gets or sets the initial pinch point vel.
      /// </summary>
      /// <value>The initial pinch point vel.</value>
      public static double[] InitialPPVel { get; set; }


      /// <summary>
      /// Gets or sets the new breakup time.
      /// </summary>
      /// <value>The new breakup time.</value>
      public static double NewBrkTime { get; set; }
      /// <summary>
      /// Gets or sets the minimum average missed distance.
      /// </summary>
      /// <value>The minimum average missed distance.</value>
      public static double MinAvgMD { get; set; }
      /// <summary>
      /// Gets or sets the new pinch point position.
      /// </summary>
      /// <value>The new pinch point position.</value>
      public static double[] NewPPPos { get; set; }
      /// <summary>
      /// Gets or sets the new pinch point vel.
      /// </summary>
      /// <value>The new pinch point vel.</value>
      public static double[] NewPPVel { get; set; }

      /// <summary>
      /// Gets or sets the missed distance time.
      /// </summary>
      /// <value>The missed distance time.</value>
      public static double[] MDTime { get; set; }
      /// <summary>
      /// Gets or sets the missed distance nadir.
      /// </summary>
      /// <value>The missed distance nadir.</value>
      public static double[] MDNadir { get; set; }
      /// <summary>
      /// Gets or sets the missed distance range.
      /// </summary>
      /// <value>The missed distance range.</value>
      public static double[] MDRange { get; set; }
      /// <summary>
      /// Gets or sets the missed distance position x.
      /// </summary>
      /// <value>The missed distance position x.</value>
      public static double[] MDPosX { get; set; }
      /// <summary>
      /// Gets or sets the missed distance position y.
      /// </summary>
      /// <value>The missed distance position y.</value>
      public static double[] MDPosY { get; set; }
      /// <summary>
      /// Gets or sets the missed distance position z.
      /// </summary>
      /// <value>The missed distance position z.</value>
      public static double[] MDPosZ { get; set; }

      /// <summary>
      /// Gets or sets the missed distance vel x.
      /// </summary>
      /// <value>The missed distance vel x.</value>
      public static double[] MDVelX { get; set; }
      /// <summary>
      /// Gets or sets the missed distance vel y.
      /// </summary>
      /// <value>The missed distance vel y.</value>
      public static double[] MDVelY { get; set; }
      /// <summary>
      /// Gets or sets the missed distance vel z.
      /// </summary>
      /// <value>The missed distance vel z.</value>
      public static double[] MDVelZ { get; set; }

      /// <summary>
      /// Gets or sets the missed distance lat.
      /// </summary>
      /// <value>The missed distance lat.</value>
      public static double[] MDLat { get; set; }
      /// <summary>
      /// Gets or sets the missed distance lon.
      /// </summary>
      /// <value>The missed distance lon.</value>
      public static double[] MDLon { get; set; }
      /// <summary>
      /// Gets or sets the height of the missed distance.
      /// </summary>
      /// <value>The height of the missed distance.</value>
      public static double[] MDHeight { get; set; }



      /// <summary>
      /// Enum XAxisSelection
      /// </summary>
      public enum XAxisSelection
      {
         /// <summary>
         /// The x date
         /// </summary>
         XDate,
         /// <summary>
         /// The mse
         /// </summary>
         Mse
      }

      /// <summary>
      /// Enum YAxisSelection
      /// </summary>
      public enum YAxisSelection
      {
         /// <summary>
         /// The average separate distance
         /// </summary>
         AvgSD,
         /// <summary>
         /// The average missed distance
         /// </summary>
         AvgMD,
         /// <summary>
         /// The separate distance
         /// </summary>
         SD,
         /// <summary>
         /// The pinch point xing
         /// </summary>
         PPXing,
         /// <summary>
         /// The time dist
         /// </summary>
         TimeDist,
      }

      /// <summary>
      /// Gets the results.
      /// </summary>
      /// <param name="satKeys">The sat keys.</param>
      /// <param name="totalSats">The total sats.</param>
      /// <param name="startDs50UTC">The start DS50 UTC.</param>
      /// <param name="stopDs50UTC">The stop DS50 UTC.</param>
      /// <param name="stepSizeMin">The step size minimum.</param>
      public static void GetResults(long[] satKeys, int totalSats, double startDs50UTC, double stopDs50UTC, double stepSizeMin)
      {
         int steps = BamWrapper.BamCompNumTSs(startDs50UTC, stopDs50UTC, stepSizeMin);
         AvgSDs = new double[steps];
         AvgMDs = new double[steps];
         Ds50UTCTSs = new double[steps];
         
         MDTime   = new double[totalSats];
         MDNadir  = new double[totalSats];
         MDRange  = new double[totalSats];
         MDPosX   = new double[totalSats];
         MDPosY   = new double[totalSats];
         MDPosZ   = new double[totalSats];
         
         MDVelX   = new double[totalSats];
         MDVelY   = new double[totalSats];
         MDVelZ   = new double[totalSats];
         
         MDLat    = new double[totalSats];
         MDLon    = new double[totalSats];
         MDHeight = new double[totalSats];

         double[] extBamArr = new double[64];
         int errCode;
         // zeroHrEpoch;

         //zeroHrEpoch = (int)startDs50UTC;

         for(int step = 0; step < steps; step++)
         {
            Ds50UTCTSs[step] = startDs50UTC + (step * stepSizeMin / Constants.MINSPERDAY);
            //MSETSs[step] = (Ds50UTCTSs[step] - zeroHrEpoch) * Constants.MINSPERDAY;
         }

         InitialPPPos = new double[3];
         InitialPPVel = new double[3];
         
         NewPPPos = new double[3];
         NewPPVel = new double[3];


         BamWrapper.BamCompute(satKeys, totalSats, startDs50UTC, stopDs50UTC, stepSizeMin, AvgSDs, AvgMDs, extBamArr, out errCode);
         InitialBrkTime = extBamArr[BamWrapper.BAM_SD_TIME];
         MinAvgSD = extBamArr[BamWrapper.BAM_SD_DIST];

         InitialPPPos[0] = extBamArr[BamWrapper.BAM_SD_POSX];
         InitialPPPos[1] = extBamArr[BamWrapper.BAM_SD_POSY];
         InitialPPPos[2] = extBamArr[BamWrapper.BAM_SD_POSZ];
         InitialPPVel[0] = extBamArr[BamWrapper.BAM_SD_VELX];
         InitialPPVel[1] = extBamArr[BamWrapper.BAM_SD_VELY];
         InitialPPVel[2] = extBamArr[BamWrapper.BAM_SD_VELZ];

         NewBrkTime = extBamArr[BamWrapper.BAM_MD_TIME];
         MinAvgMD = extBamArr[BamWrapper.BAM_MD_DIST];

         NewPPPos[0] = extBamArr[BamWrapper.BAM_MD_POSX];
         NewPPPos[1] = extBamArr[BamWrapper.BAM_MD_POSY];
         NewPPPos[2] = extBamArr[BamWrapper.BAM_MD_POSZ];
         NewPPVel[0] = extBamArr[BamWrapper.BAM_MD_VELX];
         NewPPVel[1] = extBamArr[BamWrapper.BAM_MD_VELY];
         NewPPVel[2] = extBamArr[BamWrapper.BAM_MD_VELZ];

         BamWrapper.BamGetResults(BamWrapper.XF_BAM_MDTIME, MDTime );
         BamWrapper.BamGetResults(BamWrapper.XF_BAM_NADIR, MDNadir);
         BamWrapper.BamGetResults(BamWrapper.XF_BAM_RANGE, MDRange);
         BamWrapper.BamGetResults(BamWrapper.XF_BAM_POSX, MDPosX );
         BamWrapper.BamGetResults(BamWrapper.XF_BAM_POSY, MDPosY );
         BamWrapper.BamGetResults(BamWrapper.XF_BAM_POSZ, MDPosZ );
         BamWrapper.BamGetResults(BamWrapper.XF_BAM_VELX, MDVelX );
         BamWrapper.BamGetResults(BamWrapper.XF_BAM_VELY, MDVelY );
         BamWrapper.BamGetResults(BamWrapper.XF_BAM_VELZ, MDVelZ );
         BamWrapper.BamGetResults(BamWrapper.XF_BAM_LAT, MDLat  );
         BamWrapper.BamGetResults(BamWrapper.XF_BAM_LON, MDLon  );
         BamWrapper.BamGetResults(BamWrapper.XF_BAM_HEIGHT, MDHeight);


         ErrCode = errCode;

      }

      /// <summary>
      /// Gets the y values.
      /// </summary>
      /// <param name="axisSelection">The axis selection.</param>
      /// <returns>List&lt;System.Double&gt;.</returns>
      public static List<double> GetYValues(YAxisSelection axisSelection)
      {
         if (axisSelection == YAxisSelection.AvgMD)
            return AvgMDs.ToList<double>();
         else if (axisSelection == YAxisSelection.AvgSD)
            return AvgSDs.ToList<double>();
         return null;
      }
   }
}
