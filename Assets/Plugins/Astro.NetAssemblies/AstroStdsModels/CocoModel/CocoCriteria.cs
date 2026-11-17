// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 07-22-2015
//
// Last Modified By : nguyend
// Last Modified On : 07-22-2015
// ***********************************************************************
// <copyright file="CocoCriteria.cs" company="">
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
   /// Class CocoCriteria.
   /// </summary>
   public class CocoCriteria
   {
      /// <summary>
      /// Gets or sets the inclination max1.
      /// </summary>
      /// <value>The inclination max1.</value>
      public double IncMax1 {get; set;}
      /// <summary>
      /// Gets or sets the inclination max2.
      /// </summary>
      /// <value>The inclination max2.</value>
      public double IncMax2 {get; set;}
      /// <summary>
      /// Gets or sets the inclination max3.
      /// </summary>
      /// <value>The inclination max3.</value>
      public double IncMax3 {get; set;}
      /// <summary>
      /// Gets or sets the right ascension max1.
      /// </summary>
      /// <value>The right ascension max1.</value>
      public double RaMax1 {get; set;}
      /// <summary>
      /// Gets or sets the right ascension max2.
      /// </summary>
      /// <value>The right ascension max2.</value>
      public double RaMax2 {get; set;}
      /// <summary>
      /// Gets or sets the right ascension max3.
      /// </summary>
      /// <value>The right ascension max3.</value>
      public double RaMax3 {get; set;}
      /// <summary>
      /// Gets or sets the perigee height max1.
      /// </summary>
      /// <value>The perigee height max1.</value>
      public double PhtMax1 {get; set;}
      /// <summary>
      /// Gets or sets the perigee height max2.
      /// </summary>
      /// <value>The perigee height max2.</value>
      public double PhtMax2 {get; set;}
      /// <summary>
      /// Gets or sets the perigee height max3.
      /// </summary>
      /// <value>The perigee height max3.</value>
      public double PhtMax3 {get; set;}
      /// <summary>
      /// Gets or sets the period max1.
      /// </summary>
      /// <value>The period max1.</value>
      public double PerMax1 {get; set;}
      /// <summary>
      /// Gets or sets the period max2.
      /// </summary>
      /// <value>The period max2.</value>
      public double PerMax2 {get; set;}
      /// <summary>
      /// Gets or sets the period max3.
      /// </summary>
      /// <value>The period max3.</value>
      public double PerMax3 {get; set;}

      /// <summary>
      /// Initializes a new instance of the <see cref="CocoCriteria"/> class.
      /// </summary>
      public CocoCriteria()
      {
         GetCurrCocoCriteria();
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="CocoCriteria"/> class.
      /// </summary>
      /// <param name="xa_cocoDiff">The xa_coco difference.</param>
      public CocoCriteria(double[] xa_cocoDiff)
      {
         PopulateCocoCriteria(xa_cocoDiff);
      }

      /// <summary>
      /// Populates the coco criteria.
      /// </summary>
      /// <param name="xa_cocoDiff">The xa_coco difference.</param>
      private void PopulateCocoCriteria(double[] xa_cocoDiff)
      {
         IncMax1 = xa_cocoDiff[ElCompWrapper.XA_COCODIFF_INCMAX1];
         IncMax2 = xa_cocoDiff[ElCompWrapper.XA_COCODIFF_INCMAX2];
         IncMax3 = xa_cocoDiff[ElCompWrapper.XA_COCODIFF_INCMAX3];

         RaMax1 = xa_cocoDiff[ElCompWrapper.XA_COCODIFF_RAMAX1];
         RaMax2 = xa_cocoDiff[ElCompWrapper.XA_COCODIFF_RAMAX2];
         RaMax3 = xa_cocoDiff[ElCompWrapper.XA_COCODIFF_RAMAX3];

         PhtMax1 = xa_cocoDiff[ElCompWrapper.XA_COCODIFF_PHTMAX1];
         PhtMax2 = xa_cocoDiff[ElCompWrapper.XA_COCODIFF_PHTMAX2];
         PhtMax3 = xa_cocoDiff[ElCompWrapper.XA_COCODIFF_PHTMAX3];

         PerMax1 = xa_cocoDiff[ElCompWrapper.XA_COCODIFF_PERMAX1];
         PerMax2 = xa_cocoDiff[ElCompWrapper.XA_COCODIFF_PERMAX2];
         PerMax3 = xa_cocoDiff[ElCompWrapper.XA_COCODIFF_PERMAX3];
      }

      /// <summary>
      /// Gets the curr coco criteria.
      /// </summary>
      public void GetCurrCocoCriteria()
      {
         double[] xa_cocoDiff = new double[32];
         ElCompWrapper.CocoGetCriteria(xa_cocoDiff);

         PopulateCocoCriteria(xa_cocoDiff);
      }



      /// <summary>
      /// Sets the coco criteria.
      /// </summary>
      public void SetCocoCriteria()
      {
         double[] xa_cocoDiff = new double[32];

         xa_cocoDiff[ElCompWrapper.XA_COCODIFF_INCMAX1] = IncMax1;
         xa_cocoDiff[ElCompWrapper.XA_COCODIFF_INCMAX2] = IncMax2;
         xa_cocoDiff[ElCompWrapper.XA_COCODIFF_INCMAX3] = IncMax3;

         xa_cocoDiff[ElCompWrapper.XA_COCODIFF_RAMAX1] = RaMax1;
         xa_cocoDiff[ElCompWrapper.XA_COCODIFF_RAMAX2] = RaMax2;
         xa_cocoDiff[ElCompWrapper.XA_COCODIFF_RAMAX3] = RaMax3;

         xa_cocoDiff[ElCompWrapper.XA_COCODIFF_PHTMAX1] = PhtMax1;
         xa_cocoDiff[ElCompWrapper.XA_COCODIFF_PHTMAX2] = PhtMax2;
         xa_cocoDiff[ElCompWrapper.XA_COCODIFF_PHTMAX3] = PhtMax3;

         xa_cocoDiff[ElCompWrapper.XA_COCODIFF_PERMAX1] = PerMax1;
         xa_cocoDiff[ElCompWrapper.XA_COCODIFF_PERMAX2] = PerMax2;
         xa_cocoDiff[ElCompWrapper.XA_COCODIFF_PERMAX3] = PerMax3;

         ElCompWrapper.CocoSetCriteria(xa_cocoDiff);
      }

      public static void SetCocoCriteria(double[] xa_cocoDiff)
      {
         ElCompWrapper.CocoSetCriteria(xa_cocoDiff);
      }

   }
}
