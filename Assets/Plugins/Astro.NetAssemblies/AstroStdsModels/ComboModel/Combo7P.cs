// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 05-19-2015
//
// Last Modified By : nguyend
// Last Modified On : 12-05-2014
// ***********************************************************************
// <copyright file="Combo7P.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Utilities;

/// <summary>
/// The ComboModel namespace.
/// </summary>
namespace AFSPC.AstroStds.Models.ComboModel
{
   /// <summary>
   /// Class Combo7P.
   /// </summary>
   public static class Combo7P
   {
      /// <summary>
      /// Gets or sets the time flag.
      /// </summary>
      /// <value>The time flag.</value>
      public static int TimeFlg { get; set; }
      /// <summary>
      /// Gets or sets the start time.
      /// </summary>
      /// <value>The start time.</value>
      public static double StartTime { get; set; }
      /// <summary>
      /// Gets or sets the stop time.
      /// </summary>
      /// <value>The stop time.</value>
      public static double StopTime { get; set; }
      /// <summary>
      /// Gets or sets the relative minimum flag.
      /// </summary>
      /// <value>The relative minimum flag.</value>
      public static int RelMinFlg { get; set; }
      /// <summary>
      /// Gets or sets the absolute minimum flag.
      /// </summary>
      /// <value>The absolute minimum flag.</value>
      public static int AbsMinFlg { get; set; }
      /// <summary>
      /// Gets or sets the close proximity flag.
      /// </summary>
      /// <value>The close proximity flag.</value>
      public static int CloseProxFlg { get; set; }
      /// <summary>
      /// Gets or sets the relative ephemeris flag.
      /// </summary>
      /// <value>The relative ephemeris flag.</value>
      public static int RelEphFlg { get; set; }
      /// <summary>
      /// Gets or sets the posibility of collision sigma.
      /// </summary>
      /// <value>The posibility of collision sigma.</value>
      public static double PocSigma { get; set; }
      /// <summary>
      /// Gets or sets the relative minimum limit.
      /// </summary>
      /// <value>The relative minimum limit.</value>
      public static double RelMinLim { get; set; }
      /// <summary>
      /// Gets or sets the close proximity limit.
      /// </summary>
      /// <value>The close proximity limit.</value>
      public static double CloseProxLim { get; set; }
      /// <summary>
      /// Gets or sets the relative ephemeris interval.
      /// </summary>
      /// <value>The relative ephemeris interval.</value>
      public static double RelEphInterval { get; set; }
      /// <summary>
      /// Gets or sets the print option.
      /// </summary>
      /// <value>The print option.</value>
      public static int PrtOpt { get; set; }
      /// <summary>
      /// Gets or sets the radius (meter) of the primary satellite
      /// </summary>
      /// <value>the primary satellite radius (meter)</value>
      public static double PriRadius_Meter {get; set; }
      /// <summary>
      /// Gets or sets the radius (meter) of the secondary satellite
      /// </summary>
      /// <value>the secondary satellite radius (meter)</value>
      public static double SecRadius_Meter {get; set; }


      /// <summary>
      /// Initializes static members of the <see cref="Combo7P"/> class.
      /// </summary>
      static Combo7P()
      {
         Get7P();
      }


      // Copy Combo parameter data (Combo 7P-Card) from the Combo dll to Combo7P object
      /// <summary>
      /// Get7s the p.
      /// </summary>
      public static void Get7P()
      {
         int timeFlg;
         double startTime;
         double stopTime;
         int relMinFlg;
         int absMinFlg;
         int closeProxFlg;
         int relEphFlg;
         double pocSigma;
         double relMinLim;
         double closeProxLim;
         double relEphInterval;
         int prtOpt;

         ComboWrapper.ComboGet7pAll(out timeFlg, out startTime, out stopTime, out relMinFlg, out absMinFlg, out closeProxFlg, out relEphFlg,
                                    out pocSigma, out relMinLim, out closeProxLim, out relEphInterval, out prtOpt);


         TimeFlg = timeFlg;
         StartTime = startTime;
         StopTime = stopTime;
         RelMinFlg = relMinFlg;
         AbsMinFlg = absMinFlg;
         CloseProxFlg = closeProxFlg;
         RelEphFlg = relEphFlg;
         PocSigma = pocSigma;
         RelMinLim = relMinLim;
         CloseProxLim = closeProxLim;
         RelEphInterval = relEphInterval;
         PrtOpt = prtOpt;
         PriRadius_Meter = Utility.GetDouble(ComboWrapper.ComboGet7pField, ComboWrapper.XF_7P_PRADIUS);
         SecRadius_Meter = Utility.GetDouble(ComboWrapper.ComboGet7pField, ComboWrapper.XF_7P_SRADIUS);
      }


      // Copy data from a Combo7P object to Combo parameter data (Combo 7P-Card) in the Combo dll
      /// <summary>
      /// Set7s the p.
      /// </summary>
      public static void Set7P()
      {
         int timeFlg;
         double startTime;
         double stopTime;
         int relMinFlg;
         int absMinFlg;
         int closeProxFlg;
         int relEphFlg;
         double pocSigma;
         double relMinLim;
         double closeProxLim;
         double relEphInterval;
         int prtOpt;

         timeFlg = TimeFlg;
         startTime = StartTime;
         stopTime = StopTime;
         relMinFlg = RelMinFlg;
         absMinFlg = AbsMinFlg;
         closeProxFlg = CloseProxFlg;
         relEphFlg = RelEphFlg;
         pocSigma = PocSigma;
         relMinLim = RelMinLim;
         closeProxLim = CloseProxLim;
         relEphInterval = RelEphInterval;
         prtOpt = PrtOpt;


         ComboWrapper.ComboSet7pAll(timeFlg, startTime, stopTime, relMinFlg, absMinFlg, closeProxFlg, relEphFlg,
                                    pocSigma, relMinLim, closeProxLim, relEphInterval, prtOpt);
         ComboWrapper.ComboSet7pField(ComboWrapper.XF_7P_PRADIUS, PriRadius_Meter.ToString());  
         ComboWrapper.ComboSet7pField(ComboWrapper.XF_7P_SRADIUS, SecRadius_Meter.ToString());  



      }

      public static string Get7PString()
      {
         byte[] combo7pCard = new byte[DllMainWrapper.INPUTCARDLEN];

         ComboWrapper.ComboGet7pCard(combo7pCard);

         return Utility.BytesToString(combo7pCard);
      }

      /*
      public override string ToString()
      {
         byte[] combo7pCard = new byte[DllMainWrapper.INPUTCARDLEN];

         ComboWrapper.ComboGet7pCard(combo7pCard);

         return Utility.BytesToString(combo7pCard);

      }
       * */
   }


}
//if (timeFlg == ComboWrapper.CARD_7P_FRDATE)
//{
//   predSpan.UseDateFormat(); // Display date format instead of Minutes Since Epoch Format
//   predSpan.Ds50UTCFrom = startTime;
//   predSpan.Ds50UTCUntil = stopTime; ;
//}
//else
//{
//   predSpan.UseMSEFormat(); // Display Minutes Since Epoch Format instead of date format
//   predSpan.MseFrom = startTime;
//   predSpan.MseUntil = stopTime;
//}
