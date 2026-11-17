// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 07-22-2015
//
// Last Modified By : nguyend
// Last Modified On : 07-22-2015
// ***********************************************************************
// <copyright file="CocoResults.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AFSPC.AstroStds.Models.SatelliteModel;
using AFSPC.AstroStds.Wrappers;
using System;

/// <summary>
/// The CocoModel namespace.
/// </summary>
namespace AFSPC.AstroStds.Models.CocoModel
{
   [Serializable]
   /// <summary>
   /// Class CocoResults.
   /// </summary>
   public class CocoResults
   {
      public int PriSatNum { get; set; }
      public int SecSatNum { get; set; }

      /// <summary>
      /// Gets or sets the inclination_deg.
      /// </summary>
      /// <value>The inclination_deg.</value>
      public double Inclination_deg { get; set; }
      /// <summary>
      /// Gets or sets the node_deg.
      /// </summary>
      /// <value>The node_deg.</value>
      public double Node_deg { get; set; }
      /// <summary>
      /// Gets or sets the perigee HGT_KM.
      /// </summary>
      /// <value>The perigee HGT_KM.</value>
      public double PerigeeHt_km { get; set; }
      /// <summary>
      /// Gets or sets the period_min.
      /// </summary>
      /// <value>The period_min.</value>
      public double Period_min { get; set; }
      /// <summary>
      /// Gets or sets the coplaner angle_deg.
      /// </summary>
      /// <value>The angle (deg) difference between primary and secondary w vectors at the primary's epoch time.</value>
      public double CoplanerAngle_deg { get; set; }
      /// <summary>
      /// Gets or sets the r a_ dot.
      /// </summary>
      /// <value>The r a_ dot.</value>
      public double RA_Dot { get; set; }
      /// <summary>
      /// Gets or sets the t node.
      /// </summary>
      /// <value>The t node.</value>
      public double TNode { get; set; }
      /// <summary>
      /// Gets or sets the t zero.
      /// </summary>
      /// <value>The t zero.</value>
      public double TZero_days { get; set; }
      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="CocoResults"/> is sustained close approach possible.
      /// </summary>
      /// <value><c>true</c> if sustained close approach possible; otherwise, <c>false</c>.</value>
      public bool SCAP { get; set; }
      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="CocoResults"/> is PCM.
      /// </summary>
      /// <value><c>true</c> if potential constellation member; otherwise, <c>false</c>.</value>
      public bool PCM { get; set; }

      /// <summary>
      /// time in ds50UTC when primary at nodal crossing (prior to its epoch)
      /// </summary>
      public DaysSince50UTC PNodalX { get; set; }

      /// <summary>
      /// time in ds50UTC when secondary at nodal crossing (prior to its epoch)
      /// </summary>
      public DaysSince50UTC SNodalX { get; set; }

      public DaysSince50UTC TZeroTime { get; set; }

      public string ASTAT { get; set; }

      public CocoResults()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="CocoResults"/> class.
      /// </summary>
      /// <param name="xa_coco">The xa_coco.</param>
      public CocoResults(double[] xa_coco)
      {
         PopulateCocoResults(xa_coco);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="CocoResults"/> class.
      /// </summary>
      /// <param name="xa_coco">The xa_coco.</param>
      public CocoResults(long priSatKey, long secSatKey, double[] xa_coco, int astat)
      {
         PopulateCocoResults(xa_coco);

         this.PriSatNum = ElOpsWrapper.SatNumOf(priSatKey);
         this.SecSatNum = ElOpsWrapper.SatNumOf(secSatKey);
         if (astat == ElCompWrapper.COCO_ASTAT_CLOSE)
            this.ASTAT = "CLOSE";
         else if (astat == ElCompWrapper.COCO_ASTAT_NEARBY)
            this.ASTAT = "NEARBY";
         else if (astat == ElCompWrapper.COCO_ASTAT_NONE)
            this.ASTAT = "NONE";
         else if (astat == ElCompWrapper.COCO_ASTAT_SAME)
            this.ASTAT = "SAME";

         if (this.TZero_days == 0)
         {
            this.TZeroTime = 0;
         }
         else
         {
            double priEpochTimeDs50UTC = SatStateService.GetSatDataDoubleField(priSatKey, SatStateWrapper.XF_SAT_EPOCH);
            this.TZeroTime = priEpochTimeDs50UTC + this.TZero_days;
         }

      }


      private void PopulateCocoResults(double[] xa_coco)
      {
         this.Inclination_deg = xa_coco[ElCompWrapper.XA_COCO_INCLI];
         this.Node_deg = xa_coco[ElCompWrapper.XA_COCO_NODE];
         this.PerigeeHt_km = xa_coco[ElCompWrapper.XA_COCO_PHT];
         this.Period_min = xa_coco[ElCompWrapper.XA_COCO_PERIOD];
         this.CoplanerAngle_deg = xa_coco[ElCompWrapper.XA_COCO_WDOT];
         this.RA_Dot = xa_coco[ElCompWrapper.XA_COCO_RADOT];
         this.TNode = xa_coco[ElCompWrapper.XA_COCO_TNODE];
         this.TZero_days = xa_coco[ElCompWrapper.XA_COCO_TZERO];
         this.SCAP = xa_coco[ElCompWrapper.XA_COCO_SCAP] == 1;
         this.PCM = xa_coco[ElCompWrapper.XA_COCO_PCM] == 1;
         this.PNodalX = new DaysSince50UTC(xa_coco[ElCompWrapper.XA_COCO_PNODALX]);
         this.SNodalX = new DaysSince50UTC(xa_coco[ElCompWrapper.XA_COCO_SNODALX]);
      }
   }
}

