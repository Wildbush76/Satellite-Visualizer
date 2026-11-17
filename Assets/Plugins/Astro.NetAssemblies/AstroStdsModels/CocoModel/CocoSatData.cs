// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 07-22-2015
//
// Last Modified By : nguyend
// Last Modified On : 07-23-2015
// ***********************************************************************
// <copyright file="CocoSatData.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AFSPC.AstroStds.Wrappers;
using System;

/// <summary>
/// The CocoModel namespace.
/// </summary>
namespace AFSPC.AstroStds.Models.CocoModel
{
   [Serializable]
   /// <summary>
   /// Class CocoSatData.
   /// </summary>
   public class CocoSatData
   {
      /// <summary>
      /// Gets or sets the inclination (deg).
      /// </summary>
      /// <value>The inclination (deg).</value>
      public double Inclination_deg {get; set;}
      /// <summary>
      /// Gets or sets the node (deg).
      /// </summary>
      /// <value>The node (deg).</value>
      public double Node_deg {get; set;}
      /// <summary>
      /// Gets or sets the eccentricity.
      /// </summary>
      /// <value>The eccentricity.</value>
      public double Eccentricity {get; set;}
      /// <summary>
      /// Gets or sets the omega (deg).
      /// </summary>
      /// <value>The omega (deg).</value>
      public double Omega_deg {get; set;}
      /// <summary>
      /// Gets or sets the mean anomaly (deg).
      /// </summary>
      /// <value>The mean anomaly (deg).</value>
      public double MnAnomaly_deg {get; set;}
      /// <summary>
      /// Gets or sets the mean motion (revs/day).
      /// </summary>
      /// <value>The mean motion (revs/day).</value>
      public double MnMotion_revpd {get; set;}
      /// <summary>
      /// Gets or sets the epoch_ds50 UTC.
      /// </summary>
      /// <value>The epoch_ds50 UTC.</value>
      public double Epoch_ds50UTC {get; set;}
      /// <summary>
      /// Gets or sets the perigee height (km).
      /// </summary>
      /// <value>The perigee height (km).</value>
      public double PerigeeHt_km {get; set;}
      /// <summary>
      /// Gets or sets the period (min).
      /// </summary>
      /// <value>The period (min).</value>
      public double Period_min {get; set;}

      /// <summary>
      /// Initializes a new instance of the <see cref="CocoSatData"/> class.
      /// </summary>
      public CocoSatData()
      {

      }

      /// <summary>
      /// Initializes a new instance of the <see cref="CocoSatData"/> class.
      /// </summary>
      /// <param name="satData">The sat data.</param>
      public CocoSatData(double[] satData)
      {
         this.Inclination_deg = satData[ElCompWrapper.XA_SATDATA_INCLI];
         this.Node_deg = satData[ElCompWrapper.XA_SATDATA_NODE];
         this.Eccentricity = satData[ElCompWrapper.XA_SATDATA_E];
         this.Omega_deg = satData[ElCompWrapper.XA_SATDATA_OMEGA];
         this.MnAnomaly_deg = satData[ElCompWrapper.XA_SATDATA_MNANOM];
         this.MnMotion_revpd = satData[ElCompWrapper.XA_SATDATA_MNMOTION];
         this.Epoch_ds50UTC = satData[ElCompWrapper.XA_SATDATA_EPOCH];

         this.PerigeeHt_km = satData[ElCompWrapper.XA_SATDATA_PHT];
         this.Period_min = satData[ElCompWrapper.XA_SATDATA_PERIOD];
      }

      /// <summary>
      /// To the sat data array.
      /// </summary>
      /// <returns>System.Double[].</returns>
      public double[] ToSatDataArr()
      {
         double[] satData = new double[16];

         satData[ElCompWrapper.XA_SATDATA_INCLI] = this.Inclination_deg;
         satData[ElCompWrapper.XA_SATDATA_NODE] = this.Node_deg;
         satData[ElCompWrapper.XA_SATDATA_E] = this.Eccentricity;
         satData[ElCompWrapper.XA_SATDATA_OMEGA] = this.Omega_deg;
         satData[ElCompWrapper.XA_SATDATA_MNANOM] = this.MnAnomaly_deg;
         satData[ElCompWrapper.XA_SATDATA_MNMOTION] = this.MnMotion_revpd;
         satData[ElCompWrapper.XA_SATDATA_EPOCH] = this.Epoch_ds50UTC;

         satData[ElCompWrapper.XA_SATDATA_PHT] = this.PerigeeHt_km;
         satData[ElCompWrapper.XA_SATDATA_PERIOD] = this.Period_min;

         return satData;
      }
   }
}

         