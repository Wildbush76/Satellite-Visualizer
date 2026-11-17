// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 07-24-2015
//
// Last Modified By : nguyend
// Last Modified On : 07-24-2015
// ***********************************************************************
// <copyright file="SatPen.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AFSPC.AstroStds.Wrappers;
using System;

/// <summary>
/// The SaasModel namespace.
/// </summary>
namespace AFSPC.AstroStds.Models.SaasModel
{
   [Serializable]
   /// <summary>
   /// Class SatPen.
   /// </summary>
   public class SatPen
   {
      /// <summary>
      /// Gets or sets the impact time in days since 1950 UTC.
      /// </summary>
      /// <value>The impact time in days since 1950 UTC.</value>
      public double ImpactTime_ds50UTC { get; set; } 
      /// <summary>
      /// Gets or sets the launch time in days since 1950 UTC.
      /// </summary>
      /// <value>The launch time in days since 1950 UTC.</value>
      public double LaunchTime_ds50UTC { get; set; }    
      /// <summary>
      /// Gets or sets the azimuth (deg).
      /// </summary>
      /// <value>The azimuth (deg).</value>
      public double Azimuth_deg        { get; set; }    
      /// <summary>
      /// Gets or sets the elevation (deg).
      /// </summary>
      /// <value>The elevation (deg).</value>
      public double Elevation_deg      { get; set; }    // elevation (deg)
      /// <summary>
      /// Gets or sets the range (km).
      /// </summary>
      /// <value>The range (km).</value>
      public double Range_km           { get; set; }    // range (km)
      /// <summary>
      /// Gets or sets the range rate (km/s).
      /// </summary>
      /// <value>The range rate (km/s).</value>
      public double RngRate_kmps       { get; set; }    // range rate (km/s)
      /// <summary>
      /// Gets or sets the solar aspect angle (deg).
      /// </summary>
      /// <value>The solar aspect angle (deg).</value>
      public double SolarAspAngle_deg  { get; set; }    // solar aspect angle (deg)
      /// <summary>
      /// Gets or sets the attack angle (deg).
      /// </summary>
      /// <value>The attack angle (deg).</value>
      public double AttackAngle_deg    { get; set; }    // attack angle (deg)
      /// <summary>
      /// Gets or sets the satellite height (km).
      /// </summary>
      /// <value>The satellite height (km).</value>
      public double SatHeight_km       { get; set; }    // satellite's height (km)
      /// <summary>
      /// Gets or sets a value indicating whether this instance is entry penetration.
      /// </summary>
      /// <value><c>true</c> if this instance is entry penetration; otherwise, <c>false</c>.</value>
      public bool   IsEntryPen         { get; set; }    // ring penetration entry or exit time (true=entry, false=exit)


      /// <summary>
      /// Initializes a new instance of the <see cref="SatPen"/> class.
      /// </summary>
      /// <param name="satPen">The satellite penetration.</param>
      public SatPen(double[] satPen)
      {
         this.ImpactTime_ds50UTC = satPen[SaasWrapper.XA_SATPEN_IMPTIME];
         this.LaunchTime_ds50UTC = satPen[SaasWrapper.XA_SATPEN_LAUTIME];
         this.Azimuth_deg        = satPen[SaasWrapper.XA_SATPEN_AZ     ];
         this.Elevation_deg      = satPen[SaasWrapper.XA_SATPEN_EL     ];
         this.Range_km           = satPen[SaasWrapper.XA_SATPEN_RANGE  ];
         this.RngRate_kmps       = satPen[SaasWrapper.XA_SATPEN_RNGRATE];
         this.SolarAspAngle_deg  = satPen[SaasWrapper.XA_SATPEN_SOLANG ];
         this.AttackAngle_deg    = satPen[SaasWrapper.XA_SATPEN_ATTCKANG];
         this.SatHeight_km       = satPen[SaasWrapper.XA_SATPEN_SATHGHT];
         this.IsEntryPen         = satPen[SaasWrapper.XA_SATPEN_EN0EX1] == 0;
      }
   }
}
