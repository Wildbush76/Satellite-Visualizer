// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 07-24-2015
//
// Last Modified By : nguyend
// Last Modified On : 07-24-2015
// ***********************************************************************
// <copyright file="MissleProfile.cs" company="">
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
   /// Class MissleProfile.
   /// </summary>
   public class MissleProfile
   {
      /// <summary>
      /// Gets or sets the minimum height (km).
      /// </summary>
      /// <value>The minimum height (km).</value>
      public double MinHeight_km      {get; set;} // missile's minimum height (km)
      /// <summary>
      /// Gets or sets the maximum height (km).
      /// </summary>
      /// <value>The maximum height (km).</value>
      public double MaxHeight_km      {get; set;} // missile's maximum height (km)
      /// <summary>
      /// Gets or sets the maximum solar aspect angle (deg).
      /// </summary>
      /// <value>The maximum solar aspect angle (deg).</value>
      public double MaxSolAngle_deg   {get; set;} // maxinum solar aspect angle (deg)
      /// <summary>
      /// Gets or sets the MSL maximum angle (deg).
      /// </summary>
      /// <value>The MSL maximum angle (deg).</value>
      public double MslMaxAngle_deg   {get; set;} // missile's maximum angle
      /// <summary>
      /// Gets or sets the rejection flag.
      /// </summary>
      /// <value>The rejection flag.</value>
      public double RejFlag           {get; set;} // rejection flag (-1:reject negative, 0: reject neg and pos, 1: reject pos) 
      /// <summary>
      /// Gets or sets the time coeff1 *X**2  (sec).
      /// </summary>
      /// <value>The time coeff1 *X**2  (sec).</value>
      public double TimeCoeff1        {get; set;} 
      /// <summary>
      /// Gets or sets the time coeff2 *X   (sec).
      /// </summary>
      /// <value>The time coeff2 *X   (sec).</value>
      public double TimeCoeff2        {get; set; } 
      /// <summary>
      /// Gets or sets the time coeff3 C   (sec).
      /// </summary>
      /// <value>The time coeff3 C   (sec).</value>
      public double TimeCoeff3        {get; set; } // missile's time C      (sec) 
      /// <summary>
      /// Gets or sets the range coeff1 *X**2 (km).
      /// </summary>
      /// <value>The range coeff1 *X**2 (km).</value>
      public double RangeCoeff1       {get; set; } 
      /// <summary>
      /// Gets or sets the range coeff2 *X    (km).
      /// </summary>
      /// <value>The range coeff2 *X    (km).</value>
      public double RangeCoeff2       {get; set; } 
      /// <summary>
      /// Gets or sets the range coeff3 C     (km).
      /// </summary>
      /// <value>The range coeff3 C     (km).</value>
      public double RangeCoeff3       {get; set; } 

      /// <summary>
      /// Initializes a new instance of the <see cref="MissleProfile"/> class.
      /// </summary>
      public MissleProfile()
      {

      }

      /// <summary>
      /// Initializes a new instance of the <see cref="MissleProfile"/> class.
      /// </summary>
      /// <param name="xa_msl">The xa_msl.</param>
      public MissleProfile(double[] xa_msl)
      {
         this.MinHeight_km    = xa_msl[SaasWrapper.XA_MSL_MINHGT];
         this.MaxHeight_km    = xa_msl[SaasWrapper.XA_MSL_MAXHGT];
         this.MaxSolAngle_deg = xa_msl[SaasWrapper.XA_MSL_MAXSA];
         this.MslMaxAngle_deg = xa_msl[SaasWrapper.XA_MSL_ANGMAX];
         this.RejFlag         = xa_msl[SaasWrapper.XA_MSL_REJFLG];
         this.TimeCoeff1      = xa_msl[SaasWrapper.XA_MSL_TIMCOE1];
         this.TimeCoeff2      = xa_msl[SaasWrapper.XA_MSL_TIMCOE2];
         this.TimeCoeff3      = xa_msl[SaasWrapper.XA_MSL_TIMCOE3];
         this.RangeCoeff1     = xa_msl[SaasWrapper.XA_MSL_RNGCOE1];
         this.RangeCoeff2     = xa_msl[SaasWrapper.XA_MSL_RNGCOE2];
         this.RangeCoeff3     = xa_msl[SaasWrapper.XA_MSL_RNGCOE3];
      }

      /// <summary>
      /// To the MSL array, xa_msl.
      /// </summary>
      /// <returns>System.Double[].</returns>
      public double[] ToXaMslArr()
      {
         double[] xa_msl = new double[16];

         xa_msl[SaasWrapper.XA_MSL_MINHGT]  = this.MinHeight_km;
         xa_msl[SaasWrapper.XA_MSL_MAXHGT]  = this.MaxHeight_km;
         xa_msl[SaasWrapper.XA_MSL_MAXSA]   = this.MaxSolAngle_deg;
         xa_msl[SaasWrapper.XA_MSL_ANGMAX]  = this.MslMaxAngle_deg;
         xa_msl[SaasWrapper.XA_MSL_REJFLG]  = this.RejFlag;
         xa_msl[SaasWrapper.XA_MSL_TIMCOE1] = this.TimeCoeff1;
         xa_msl[SaasWrapper.XA_MSL_TIMCOE2] = this.TimeCoeff2;
         xa_msl[SaasWrapper.XA_MSL_TIMCOE3] = this.TimeCoeff3;
         xa_msl[SaasWrapper.XA_MSL_RNGCOE1] = this.RangeCoeff1;
         xa_msl[SaasWrapper.XA_MSL_RNGCOE2] = this.RangeCoeff2;
         xa_msl[SaasWrapper.XA_MSL_RNGCOE3] = this.RangeCoeff3;

         return xa_msl;

      }
   }
}
