// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 07-24-2015
//
// Last Modified By : nguyend
// Last Modified On : 07-24-2015
// ***********************************************************************
// <copyright file="LaunchSite.cs" company="">
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
   /// Class LaunchSite.
   /// </summary>
   public class LaunchSite
   {
      /// <summary>
      /// Gets or sets the launch site latitude (deg) (+N) (-S).
      /// </summary>
      /// <value>The launch site latitude (deg) (+N) (-S).</value>
      public double LSLatitude_deg  { get; set; }   
      /// <summary>
      /// Gets or sets the launch site longitude (deg) (+E) (-W).
      /// </summary>
      /// <value>The launch site longitude (deg) (+E) (-W).</value>
      public double LSLongitude_deg { get; set; }   
      /// <summary>
      /// Gets or sets the launch site height (m).
      /// </summary>
      /// <value>The launch site height (m).</value>
      public double LSHeight_m      { get; set; }   

      /// <summary>
      /// Initializes a new instance of the <see cref="LaunchSite"/> class.
      /// </summary>
      public LaunchSite()
      {

      }

      /// <summary>
      /// Initializes a new instance of the <see cref="LaunchSite"/> class.
      /// </summary>
      /// <param name="xa_ls">The xa_ls.</param>
      public LaunchSite(double[] xa_ls)
      {
         this.LSLatitude_deg  = xa_ls[SaasWrapper.XA_LS_LAT];
         this.LSLongitude_deg = xa_ls[SaasWrapper.XA_LS_LON];
         this.LSHeight_m      = xa_ls[SaasWrapper.XA_LS_HEIGHT];
      }

      /// <summary>
      /// To the launch site array, xa_ls.
      /// </summary>
      /// <returns>System.Double[].</returns>
      public double[] ToXaLsArr()
      {
         double[] xa_ls = new double[8];
         xa_ls[SaasWrapper.XA_LS_LAT]    = this.LSLatitude_deg ;
         xa_ls[SaasWrapper.XA_LS_LON]    = this.LSLongitude_deg;
         xa_ls[SaasWrapper.XA_LS_HEIGHT] = this.LSHeight_m;

         return xa_ls;

      }
   }
}
