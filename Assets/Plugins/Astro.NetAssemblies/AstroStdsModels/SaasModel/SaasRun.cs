// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 07-24-2015
//
// Last Modified By : nguyend
// Last Modified On : 07-24-2015
// ***********************************************************************
// <copyright file="SaasRun.cs" company="">
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
   /// Class SaasRun.
   /// </summary>
   public class SaasRun
   {
      /// <summary>
      /// Gets or sets the maximum number of penetrations allowed.
      /// </summary>
      /// <value>The maximum number of penetrations allowed.</value>
      public int MaxPen {get; set;}               
      /// <summary>
      /// Gets or sets the SAAS start time in days since 1950 UTC.
      /// </summary>
      /// <value>The SAAS start time in days since 1950 UTC.</value>
      public double Start_ds50UTC {get; set;}    
      /// <summary>
      /// Gets or sets the SAAS stop time in days since 1950 UTC.
      /// </summary>
      /// <value>The SAAS stop time in days since 1950 UTC.</value>
      public double Stop_ds50UTC {get; set;}     
      /// <summary>
      /// Gets or sets the half angle of attack cone (deg) (0=auto cone).
      /// </summary>
      /// <value>The half angle of attack cone (deg) (0=auto cone).</value>
      public double HalfCone_deg {get; set;}       

      /// <summary>
      /// Initializes a new instance of the <see cref="SaasRun"/> class.
      /// </summary>
      public SaasRun()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="SaasRun"/> class.
      /// </summary>
      /// <param name="xa_saasRun">The xa_saas run.</param>
      public SaasRun(double[] xa_saasRun)
      {
         this.MaxPen        = (int) xa_saasRun[SaasWrapper.XA_SAASRUN_MAXPENS];
         this.Start_ds50UTC = xa_saasRun[SaasWrapper.XA_SAASRUN_MAXPENS];
         this.Stop_ds50UTC  = xa_saasRun[SaasWrapper.XA_SAASRUN_STOP];
         this.HalfCone_deg  = xa_saasRun[SaasWrapper.XA_SAASRUN_HALFCONE];
      }

      /// <summary>
      /// To the saas run array.
      /// </summary>
      /// <returns>System.Double[].</returns>
      public double[] ToSaasRunArr()
      {
         double[] xa_saasRun = new double[8];

         xa_saasRun[SaasWrapper.XA_SAASRUN_MAXPENS]  = this.MaxPen;
         xa_saasRun[SaasWrapper.XA_SAASRUN_START]    = this.Start_ds50UTC;
         xa_saasRun[SaasWrapper.XA_SAASRUN_STOP]     = this.Stop_ds50UTC;
         xa_saasRun[SaasWrapper.XA_SAASRUN_HALFCONE] = this.HalfCone_deg;

         return xa_saasRun;
      }

   }
}         
