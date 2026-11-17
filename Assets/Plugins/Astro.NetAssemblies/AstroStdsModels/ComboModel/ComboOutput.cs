// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 05-19-2015
//
// Last Modified By : nguyend
// Last Modified On : 11-06-2014
// ***********************************************************************
// <copyright file="ComboOutput.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AFSPC.AstroStds.Wrappers;
using System;

/// <summary>
/// The ComboModel namespace.
/// </summary>
namespace AFSPC.AstroStds.Models.ComboModel
{
   [Serializable]
   /// <summary>
   /// Class ComboOutput.
   /// </summary>
   public class ComboOutput
   {
      /// <summary>
      /// Gets or sets the primary sat key.
      /// </summary>
      /// <value>The primary sat key.</value>
      public long PriSatKey { get; set; }          // primary satellite key
      /// <summary>
      /// Gets or sets the secondary sat key.
      /// </summary>
      /// <value>The secondary sat key.</value>
      public long SecSatKey { get; set; }              // secondary satellite key
      /// <summary>
      /// Gets or sets the time at absolute minimum distance, in days since 1950 UTC.
      /// </summary>
      /// <value>The time at absolute minimum distance, in days since 1950 UTC.</value>
      public double AbsMinDs50UTC { get; set; }          // time in ds50UTC at absolute minimum 
      /// <summary>
      /// Gets or sets the absolute minimum distance.
      /// </summary>
      /// <value>The absolute minimum distance.</value>
      public double AbsMinDist { get; set; }             // absolute minimum distance in km
      /// <summary>
      /// The relative minima times, in days since 1950 UTC
      /// </summary>
      private double[] relMinDs50UTCs = new double[1000];   // array of times in ds50UTC of relative minima
      /// <summary>
      /// Gets or sets the number of relative minima.
      /// </summary>
      /// <value>The number of relative minima.</value>
      public int NumOfRelMins { get; set; }           // number of relative minima
      /// <summary>
      /// The entry times, in days since 1950 UTC
      /// </summary>
      private double[] entryDs50UTCs = new double[1000];    // array of entry times in ds50UTC
      /// <summary>
      /// The exit times, in days since 1950 UTC
      /// </summary>
      private double[] exitDs50UTCs = new double[1000];     // array of exit times in ds50UTC
      /// <summary>
      /// Gets or sets the number of entries/exits.
      /// </summary>
      /// <value>The number of entries/exits.</value>
      public int NumOfEntryExits { get; set; }        // number of entry/exit times
      /// <summary>
      /// Gets or sets the error code.
      /// </summary>
      /// <value>The error code.</value>
      public int ErrCode { get; set; }                // returned error code
      /// <summary>
      /// Gets or sets the error message.
      /// </summary>
      /// <value>The error message.</value>
      public string ErrMessage { get; set; }

      /// <summary>
      /// Gets the relative minima times, in days since 1950 UTC.
      /// </summary>
      /// <value>The relative minima times, in days since 1950 UTC.</value>
      public double[] RelMinDs50UTCs
      {
         get
         {
            return relMinDs50UTCs;
         }
      }

      /// <summary>
      /// Gets the exit times, in days since 1950 UTC.
      /// </summary>
      /// <value>The exit times, in days since 1950 UTC.</value>
      public double[] ExitDs50UTCs
      {
         get
         {
            return exitDs50UTCs;
         }
      }

      /// <summary>
      /// Gets the entry times, in days since 1950 UTC.
      /// </summary>
      /// <value>The entry times, in days since 1950 UTC.</value>
      public double[] EntryDs50UTCs
      {
         get
         {
            return entryDs50UTCs;
         }
      }




      /// <summary>
      /// Initializes a new instance of the <see cref="ComboOutput"/> class.
      /// </summary>
      /// <param name="priSatKey">The primary sat key.</param>
      /// <param name="secSatKey">The secondary sat key.</param>
      public ComboOutput(long priSatKey, long secSatKey)
      {
         this.PriSatKey = priSatKey;
         this.SecSatKey = secSatKey;
         double absMinDs50UTC, absMinDist;
         int numOfRelMins, numOfEntryExits;

         // Calling the Combo dll's function to return combo output data
         this.ErrCode = ComboWrapper.ComboCompPriSec(priSatKey, secSatKey,
            out absMinDs50UTC,
            out absMinDist,
            this.relMinDs50UTCs,
            out numOfRelMins,
            this.entryDs50UTCs,
            this.exitDs50UTCs,
            out numOfEntryExits);

         if (this.ErrCode != 0)
            this.ErrMessage = DllMainWrapper.GetLastErrMsgStr();
         else
            this.ErrMessage = "";

         this.AbsMinDs50UTC = absMinDs50UTC;
         this.AbsMinDist = absMinDist;
         this.NumOfRelMins = numOfRelMins;
         this.NumOfEntryExits = numOfEntryExits;
      }

   }
}
