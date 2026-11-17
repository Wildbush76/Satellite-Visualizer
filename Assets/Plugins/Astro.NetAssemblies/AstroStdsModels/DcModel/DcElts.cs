// ***********************************************************************
// Assembly         : AFSPC.AstroStds.Models
// Author           : nguyend
// Created          : 05-19-2015
//
// Last Modified By : nguyend
// Last Modified On : 06-15-2015
// ***********************************************************************
// <copyright file="DcElts.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AFSPC.AstroStds.Models.SatelliteModel;
using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// The DcModel namespace.
/// </summary>
namespace AFSPC.AstroStds.Models.DcModel
{
   [Serializable]
   /// <summary>
   /// Class DcElts.
   /// </summary>
   public class DcElts
   {
      /// <summary>
      /// Gets or sets the error code.
      /// </summary>
      /// <value>The error code.</value>
      public int ErrCode { get; set; }
      /// <summary>
      /// Gets or sets the sat number.
      /// </summary>
      /// <value>The sat number.</value>
      public int SatNum{ get; set; }
      /// <summary>
      /// Gets of sets the satellite name (international designator)
      /// </summary>
      public string SatName { get; set; }
      /// <summary>
      /// Gets or sets the elset number.
      /// </summary>
      /// <value>The elset number.</value>
      public int ElsetNum{ get; set; }
      /// <summary>
      /// Gets or sets the type of the orb.
      /// </summary>
      /// <value>The type of the orb.</value>
      public int OrbType{ get; set; }
      /// <summary>
      /// Gets or sets the type of the property.
      /// </summary>
      /// <value>The type of the property.</value>
      public int PropType{ get; set; }
      /// <summary>
      /// Gets or sets the spectr.
      /// </summary>
      /// <value>The spectr.</value>
      public int Spectr{ get; set; }
      /// <summary>
      /// Gets or sets the drag mod.
      /// </summary>
      /// <value>The drag mod.</value>
      public int DragMod{ get; set; }
      /// <summary>
      /// Gets or sets the RAD pres.
      /// </summary>
      /// <value>The RAD pres.</value>
      public int RadPres{ get; set; }
      /// <summary>
      /// Gets or sets the rev number.
      /// </summary>
      /// <value>The rev number.</value>
      public int RevNum{ get; set; }
      /// <summary>
      /// Gets or sets the iter count.
      /// </summary>
      /// <value>The iter count.</value>
      public int IterCount {get; set;}             // total iteration count



      /// <summary>
      /// Gets or sets the epoch ds50utc.
      /// </summary>
      /// <value>The epoch ds50utc.</value>
      public DaysSince50UTC EpochDs50utc { get; set; }
      /// <summary>
      /// Gets or sets the ndot.
      /// </summary>
      /// <value>The ndot.</value>
      public double Ndot{ get; set; }
      /// <summary>
      /// Gets or sets the n2dot.
      /// </summary>
      /// <value>The n2dot.</value>
      public double N2dot{ get; set; }
      /// <summary>
      /// Gets or sets the bfield.
      /// </summary>
      /// <value>The bfield.</value>
      public double Bfield{ get; set; }
      /// <summary>
      /// Gets or sets the agom.
      /// </summary>
      /// <value>The agom.</value>
      public double Agom{ get; set; }
      /// <summary>
      /// Gets or sets the outgassing parameter.
      /// </summary>
      /// <value>The outgassing parameter.</value>
      public double OgParm{ get; set; }
      /// <summary>
      /// Gets or sets semi major axis A.
      /// </summary>
      /// <value>A.</value>
      public double A{ get; set; }
      /// <summary>
      /// Gets or sets the eccentricity.
      /// </summary>
      /// <value>The e.</value>
      public double E{ get; set; }
      /// <summary>
      /// Gets or sets the incli.
      /// </summary>
      /// <value>The incli.</value>
      public double Incli{ get; set; }
      /// <summary>
      /// Gets or sets the ma.
      /// </summary>
      /// <value>The ma.</value>
      public double Ma{ get; set; }
      /// <summary>
      /// Gets or sets the node.
      /// </summary>
      /// <value>The node.</value>
      public double Node{ get; set; }
      /// <summary>
      /// Gets or sets the omega.
      /// </summary>
      /// <value>The omega.</value>
      public double Omega{ get; set; }
      /// <summary>
      /// Gets or sets the af.
      /// </summary>
      /// <value>The af.</value>
      public double Af{ get; set; }
      /// <summary>
      /// Gets or sets the ag.
      /// </summary>
      /// <value>The ag.</value>
      public double Ag{ get; set; }
      /// <summary>
      /// Gets or sets the chi.
      /// </summary>
      /// <value>The chi.</value>
      public double Chi{ get; set; }
      /// <summary>
      /// Gets or sets the psi.
      /// </summary>
      /// <value>The psi.</value>
      public double Psi{ get; set; }
      /// <summary>
      /// Gets or sets the l.
      /// </summary>
      /// <value>The l.</value>
      public double L{ get; set; }
      /// <summary>
      /// Gets or sets the n.
      /// </summary>
      /// <value>The n.</value>
      public double N{ get; set; }
      /// <summary>
      /// Gets or sets the position x.
      /// </summary>
      /// <value>The position x.</value>
      public double PosX{ get; set; }
      /// <summary>
      /// Gets or sets the position y.
      /// </summary>
      /// <value>The position y.</value>
      public double PosY{ get; set; }
      /// <summary>
      /// Gets or sets the position z.
      /// </summary>
      /// <value>The position z.</value>
      public double PosZ{ get; set; }
      /// <summary>
      /// Gets or sets the vel x.
      /// </summary>
      /// <value>The vel x.</value>
      public double VelX{ get; set; }
      /// <summary>
      /// Gets or sets the vel y.
      /// </summary>
      /// <value>The vel y.</value>
      public double VelY{ get; set; }
      /// <summary>
      /// Gets or sets the vel z.
      /// </summary>
      /// <value>The vel z.</value>
      public double VelZ{ get; set; }

      /// <summary>
      /// Gets or sets the RMS.
      /// </summary>
      /// <value>The RMS.</value>
      public double RMS { get; set; }


      public DcElts()
      {

      }


      /// <summary>
      /// Initializes a new instance of the <see cref="DcElts"/> class.
      /// </summary>
      /// <param name="satKey">The sat key.</param>
      public DcElts(long satKey)
      {
         int[] xai_dcElts = new int[256];
         double[] xar_dcElts =  new double[256];
         byte[] xas_dcElts = new byte[512];


         // Calling the BatchDC dll
         ErrCode = BatchDCWrapper.BatchDCSolve(satKey, xai_dcElts, xar_dcElts, xas_dcElts);

         if (ErrCode != DllMainWrapper.IDX_ERR_NONE)
            return;
         SatNum = xai_dcElts[BatchDCWrapper.XAI_DCELTS_SATNUM];
         ElsetNum = xai_dcElts[BatchDCWrapper.XAI_DCELTS_ELSETNUM];
         OrbType = xai_dcElts[BatchDCWrapper.XAI_DCELTS_ORBTYPE];
         PropType = xai_dcElts[BatchDCWrapper.XAI_DCELTS_PROPTYPE];
         Spectr = xai_dcElts[BatchDCWrapper.XAI_DCELTS_SPECTR];
         RevNum = xai_dcElts[BatchDCWrapper.XAI_DCELTS_REVNUM];
         IterCount = xai_dcElts[BatchDCWrapper.XAI_DCELTS_ITERCOUNT];

         EpochDs50utc = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EPOCHDS50UTC];
         Ndot = xar_dcElts[BatchDCWrapper.XAR_DCELTS_NDOT];
         N2dot = xar_dcElts[BatchDCWrapper.XAR_DCELTS_N2DOT];
         Bfield = xar_dcElts[BatchDCWrapper.XAR_DCELTS_BFIELD];
         Agom = xar_dcElts[BatchDCWrapper.XAR_DCELTS_AGOM];
         OgParm = xar_dcElts[BatchDCWrapper.XAR_DCELTS_OGPARM];
         A = xar_dcElts[BatchDCWrapper.XAR_DCELTS_KEP_A];
         E = xar_dcElts[BatchDCWrapper.XAR_DCELTS_KEP_E];
         Incli = xar_dcElts[BatchDCWrapper.XAR_DCELTS_KEP_INCLI];
         Ma = xar_dcElts[BatchDCWrapper.XAR_DCELTS_KEP_MA];
         Node = xar_dcElts[BatchDCWrapper.XAR_DCELTS_KEP_NODE];
         Omega = xar_dcElts[BatchDCWrapper.XAR_DCELTS_KEP_OMEGA];
         Af = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EQNX_AF];
         Ag = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EQNX_AG];
         Chi = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EQNX_CHI];
         Psi = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EQNX_PSI];
         L = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EQNX_L];
         N = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EQNX_N];
         PosX = xar_dcElts[BatchDCWrapper.XAR_DCELTS_POSX];
         PosY = xar_dcElts[BatchDCWrapper.XAR_DCELTS_POSY];
         PosZ = xar_dcElts[BatchDCWrapper.XAR_DCELTS_POSZ];
         VelX = xar_dcElts[BatchDCWrapper.XAR_DCELTS_VELX];
         VelY = xar_dcElts[BatchDCWrapper.XAR_DCELTS_VELY];
         VelZ = xar_dcElts[BatchDCWrapper.XAR_DCELTS_VELZ];
         RMS = xar_dcElts[BatchDCWrapper.XAR_DCELTS_RMS];

         string dcEltsStr = Utility.BytesToStringRTrim(xas_dcElts);
         SatName = dcEltsStr.Substring(BatchDCWrapper.XAS_DCELTS_SATNAME_0TO7, 8);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="DcElts"/> class.
      /// </summary>
      /// <param name="xai_dcElts">The dc elt ints.</param>
      /// <param name="xar_dcElts">The dc elt reals.</param>
      /// <param name="xas_dcElts">The dc elt STRS.</param>
      public DcElts(int[] xai_dcElts, double[] xar_dcElts, byte[] xas_dcElts)
      {
         PopulateData(xai_dcElts, xar_dcElts, xas_dcElts);
      }



      
      public DcElts(long satKey, double[] xa_egpCtrl)
      {
         // just create a different constructor so this can be used to run extrapolation GP
         int[] xai_dcElts = new int[256];
         double[] xar_dcElts = new double[256];
         byte[] xas_dcElts = new byte[512];
         List<string> tles = new List<string>();

         ErrCode = BatchDCWrapper.SpToEGP(satKey, xa_egpCtrl, xai_dcElts, xar_dcElts, xas_dcElts);
         if (ErrCode == 0)
         {
            PopulateData(xai_dcElts, xar_dcElts, xas_dcElts);
         }
      }

      public DcElts(long satKey, double[] xa_egpCtrl, string verboseFile)
      {
         // just create a different constructor so this can be used to run extrapolation GP
         int[] xai_dcElts = new int[256];
         double[] xar_dcElts = new double[256];
         byte[] xas_dcElts = new byte[512]; 
         List<string> tles = new List<string>();

         for(int i = 0; i < verboseFile.Length; i++)
         {
            byte current = Convert.ToByte(verboseFile[i]);
            xas_dcElts[i] = current;
         }

         //System.Buffer.BlockCopy(verboseFile.ToCharArray(), 0, xas_dcElts, 0, verboseFile.Length);

         ErrCode = BatchDCWrapper.SpToEGP(satKey, xa_egpCtrl, xai_dcElts, xar_dcElts, xas_dcElts);
         if (ErrCode == 0)
         {
            PopulateData(xai_dcElts, xar_dcElts, xas_dcElts);
         }
      }


      public DcElts(string extEphFile, double[] xa_egpCtrl)
      {
         // just create a different constructor so this can be used to run extrapolation GP
         int[] xai_dcElts = new int[256];
         double[] xar_dcElts = new double[256];
         byte[] xas_dcElts = new byte[512];
         List<string> tles = new List<string>();

         ErrCode = BatchDCWrapper.ExtEphemToEGP(extEphFile, xa_egpCtrl, xai_dcElts, xar_dcElts, xas_dcElts);
         if (ErrCode == 0)
         {
            PopulateData(xai_dcElts, xar_dcElts, xas_dcElts);
         }
      }

      public DcElts(string extEphFile, double[] xa_egpCtrl, string verboseFile)
      {
         // just create a different constructor so this can be used to run extrapolation GP
         int[] xai_dcElts = new int[256];
         double[] xar_dcElts = new double[256];
         byte[] xas_dcElts = new byte[512];
         List<string> tles = new List<string>();

         for (int i = 0; i < verboseFile.Length; i++)
         {
            byte current = Convert.ToByte(verboseFile[i]);
            xas_dcElts[i] = current;
         }

         //System.Buffer.BlockCopy(verboseFile.ToCharArray(), 0, xas_dcElts, 0, verboseFile.Length);

         ErrCode = BatchDCWrapper.ExtEphemToEGP(extEphFile, xa_egpCtrl, xai_dcElts, xar_dcElts, xas_dcElts);
         if (ErrCode == 0)
         {
            PopulateData(xai_dcElts, xar_dcElts, xas_dcElts);
         }
      }


      private void PopulateData(int[] xai_dcElts, double[] xar_dcElts, byte[] xas_dcElts)
      {
         SatNum = xai_dcElts[BatchDCWrapper.XAI_DCELTS_SATNUM];
         ElsetNum = xai_dcElts[BatchDCWrapper.XAI_DCELTS_ELSETNUM];
         OrbType = xai_dcElts[BatchDCWrapper.XAI_DCELTS_ORBTYPE];
         PropType = xai_dcElts[BatchDCWrapper.XAI_DCELTS_PROPTYPE];
         Spectr = xai_dcElts[BatchDCWrapper.XAI_DCELTS_SPECTR];
         RevNum = xai_dcElts[BatchDCWrapper.XAI_DCELTS_REVNUM];
         IterCount = xai_dcElts[BatchDCWrapper.XAI_DCELTS_ITERCOUNT];

         EpochDs50utc = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EPOCHDS50UTC];
         Ndot = xar_dcElts[BatchDCWrapper.XAR_DCELTS_NDOT];
         N2dot = xar_dcElts[BatchDCWrapper.XAR_DCELTS_N2DOT];
         Bfield = xar_dcElts[BatchDCWrapper.XAR_DCELTS_BFIELD];
         Agom = xar_dcElts[BatchDCWrapper.XAR_DCELTS_AGOM];
         OgParm = xar_dcElts[BatchDCWrapper.XAR_DCELTS_OGPARM];
         A = xar_dcElts[BatchDCWrapper.XAR_DCELTS_KEP_A];
         E = xar_dcElts[BatchDCWrapper.XAR_DCELTS_KEP_E];
         Incli = xar_dcElts[BatchDCWrapper.XAR_DCELTS_KEP_INCLI];
         Ma = xar_dcElts[BatchDCWrapper.XAR_DCELTS_KEP_MA];
         Node = xar_dcElts[BatchDCWrapper.XAR_DCELTS_KEP_NODE];
         Omega = xar_dcElts[BatchDCWrapper.XAR_DCELTS_KEP_OMEGA];
         Af = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EQNX_AF];
         Ag = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EQNX_AG];
         Chi = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EQNX_CHI];
         Psi = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EQNX_PSI];
         L = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EQNX_L];
         N = xar_dcElts[BatchDCWrapper.XAR_DCELTS_EQNX_N];
         PosX = xar_dcElts[BatchDCWrapper.XAR_DCELTS_POSX];
         PosY = xar_dcElts[BatchDCWrapper.XAR_DCELTS_POSY];
         PosZ = xar_dcElts[BatchDCWrapper.XAR_DCELTS_POSZ];
         VelX = xar_dcElts[BatchDCWrapper.XAR_DCELTS_VELX];
         VelY = xar_dcElts[BatchDCWrapper.XAR_DCELTS_VELY];
         VelZ = xar_dcElts[BatchDCWrapper.XAR_DCELTS_VELZ];
         RMS = xar_dcElts[BatchDCWrapper.XAR_DCELTS_RMS];

         string dcEltsStr = Utility.BytesToString(xas_dcElts);
         SatName = dcEltsStr.Substring(BatchDCWrapper.XAS_DCELTS_SATNAME_0TO7, 8);
      }

      /// <summary>
      /// Creates a gp tle.
      /// </summary>
      /// <param name="line1">The line1.</param>
      /// <param name="line2">The line2.</param>
      public void CreateNewGpTle(out string line1, out string line2)
      {
         int year;
         double daysOfYear;
         byte[] bline1 = new byte[512];
         byte[] bline2 = new byte[512];
         int ephType;


         TimeFuncWrapper.UTCToYrDays(this.EpochDs50utc, out year, out daysOfYear);
         double n = this.N; // Brouwer mean motion

         double agomOrNdotsqr = 0;

         if (this.OrbType == DllMainWrapper.ELTTYPE_TLE_XP) // XP
         {
            ephType = 4; // SGP4-XP DC
            agomOrNdotsqr = this.Agom;
         }
         else
         {
            if (this.OrbType == DllMainWrapper.ELTTYPE_TLE_SGP4)
            {
               ephType = 0; // SGP4 DC
            }
            else
            {
               ephType = 3; // PPT3
            }
            agomOrNdotsqr = this.N2dot;
            n = AstroFuncWrapper.BrouwerToKozai(this.E, this.Incli, n); // Kozai mean motion
         }

         // generate GP TLE from data fields
         TleWrapper.TleGPFieldsToLines(this.SatNum, 'U', this.SatName, year, daysOfYear, this.Ndot, agomOrNdotsqr, this.Bfield, ephType, this.ElsetNum,
            this.Incli, this.Node, this.E, this.Omega, this.Ma, n, this.RevNum, bline1, bline2);
         line1 = Utility.BytesToStringRTrim(bline1);
         line2 = Utility.BytesToStringRTrim(bline2);
      }

      /// <summary>
      /// Creates an SGP4 tle.
      /// </summary>
      /// <param name="line1">The line1.</param>
      /// <param name="line2">The line2.</param>
      public void CreateNewSgp4Tle(out string line1, out string line2)
      {
         int year;
         double daysOfYear;
         byte[] bline1 = new byte[512];
         byte[] bline2 = new byte[512];


         TimeFuncWrapper.UTCToYrDays(this.EpochDs50utc, out year, out daysOfYear);

         // generate SGP4 TLE from data fields
         TleWrapper.TleGPFieldsToLines(this.SatNum, 'U', this.SatName, year, daysOfYear, this.Ndot, this.N2dot, this.Bfield, 2, this.ElsetNum,
            this.Incli, this.Node, this.E, this.Omega, this.Ma, this.N, this.RevNum, bline1, bline2);
         line1 = Utility.BytesToStringRTrim(bline1);
         line2 = Utility.BytesToStringRTrim(bline2);
      }

      /// <summary>
      /// Creates an sp tle.
      /// </summary>
      /// <param name="line1">The line1.</param>
      /// <param name="line2">The line2.</param>
      public void CreateNewSpTle(out string line1, out string line2)
      {
         int year;
         double daysOfYear;
         byte[] bline1 = new byte[512];
         byte[] bline2 = new byte[512];


         TimeFuncWrapper.UTCToYrDays(this.EpochDs50utc, out year, out daysOfYear);

         // generate SP TLE from data fields
         TleWrapper.TleSPFieldsToLines(this.SatNum, 'U', this.SatName, year, daysOfYear, this.Bfield, this.OgParm, this.Agom, this.ElsetNum,
            this.Incli, this.Node, this.E, this.Omega, this.Ma, this.N, this.RevNum, bline1, bline2);
         line1 = Utility.BytesToStringRTrim(bline1);
         line2 = Utility.BytesToStringRTrim(bline2);
      }


      // Create SPVEC by using the data stored in the dcElts structure
      /// <summary>
      /// Creates the new sp vec.
      /// </summary>
      /// <param name="line1">The line1.</param>
      /// <param name="line2">The line2.</param>
      /// <param name="card4P">The card4 p.</param>
      public void CreateNewSpVec(out string line1, out string line2, out string card4P)
      {
         double[] pos = new double[3];
         double[] velMPS = new double[3];
         byte[] bline1 = new byte[512];
         byte[] bline2 = new byte[512];
         byte[] bcard4P = new byte[512];


         pos[0] = this.PosX;
         pos[1] = this.PosY;
         pos[2] = this.PosZ;

         // SpVec takes velocity in m/s, need to convert vel from km/s to m/s
         velMPS[0] = this.VelX * 1000;
         velMPS[1] = this.VelY * 1000;
         velMPS[2] = this.VelZ * 1000;

         // generate osculating vector 1P/2P format
         SpVecWrapper.SpVecFieldsToLines(pos, velMPS, 'U', this.SatNum, this.SatName, this.EpochDs50utc.Dtg17aStr,
            this.RevNum, this.ElsetNum, this.Bfield, this.Agom, this.OgParm, "TMDAT", bline1, bline2);
         line1 = Utility.BytesToStringRTrim(bline1);
         line2 = Utility.BytesToStringRTrim(bline2);

         SpPropWrapper.SpGet4PCard(bcard4P);
         card4P = Utility.BytesToStringRTrim(bcard4P);

      }


      /// <summary>
      /// Gets the dc output elset.
      /// </summary>
      /// <param name="sb">The sb.</param>
      public void GetDcOutputElset(StringBuilder sb, out string line1, out string line2)
      {
         string card4P;

         if (PropType == DllMainWrapper.PROPTYPE_GP)
         {
            // TLE type 0 - GP
            CreateNewGpTle(out line1, out line2);
            sb.AppendLine(line1);
            sb.AppendLine(line2);

            // TLE type 2 - SGP4
            //CreateNewSgp4Tle(out line1, out line2);
            //sb.AppendLine(line1);
            //sb.AppendLine(line2);
         }
         else
         {
            // TLE type 6 - SP
            CreateNewSpTle(out line1, out line2);
            sb.AppendLine(line1);
            sb.AppendLine(line2);

            // SP vector
            CreateNewSpVec(out line1, out line2, out card4P);
            sb.AppendLine(line1);
            sb.AppendLine(line2);
            sb.AppendLine(card4P);
         }
      }

      public static void GetMarginAndSpan(Satellite sat, out double margin, out double span)
      {
         double edr = Utility.GetKey64Double(VcmWrapper.VcmGetField, sat.SatKey, VcmWrapper.XF_VCM_EDR);
         double agomp = sat.Agom * (sat.Period_min / 1440.0);

         if (agomp > 1.0)
         {
            margin = 1;
            span = 0.5;
         }
         else if (agomp > 0.15)
         {
            margin = 1.5;
            span = 1.5;
         }
         else if (agomp > 0.07)
         {
            margin = 3.0;
            span = 3.0;
         }
         else if (sat.Period_min > 1800)
         {
            margin = 0.5 * (sat.Period_min / 1440.0);
            span = 0.5 * (sat.Period_min / 1440.0);
         }
         else if (sat.Period_min > 1000)
         {
            margin = 20;
            span = 20;
         }
         else if (sat.ClassElts.Eccentricity > 0.76)
         {
            margin = 2.5;
            span = 2.5;
         }
         else if (sat.Perigee_km - 6378.135 < 150)
         {
            margin = 0.5;
            span = 0.5;
         }
         else
         {
            UseEDRTable(edr, out margin, out span);
         }

         //margin = Math.Max(margin, minDays);
         //span = Math.Max(span, minDays);
      }


      public static void UseEDRTable(double edr, out double margin, out double span)
      {
         margin = 0;
         span = 0;

         if (edr <= 0)
         {
            margin = 12.0;
            span = 12.0;
         }
         else if (edr > 0 && edr <= 0.00060)
         {
            margin = 7.0;
            span = 7.0;
         }
         else if (edr > 0.00060 && edr <= 0.001)
         {
            margin = 4.0;
            span = 4.0;
         }
         else if (edr > 0.001 && edr <= 0.0015)
         {
            margin = 3.0;
            span = 3.0;
         }
         else if (edr > 0.0015 && edr <= 0.002)
         {
            margin = 2.5;
            span = 2.5;
         }
         else if (edr > 0.002 && edr <= 0.003)
         {
            margin = 2.0;
            span = 2.0;
         }
         else if (edr > 0.003 && edr <= 0.006)
         {
            margin = 1.5;
            span = 1.5;
         }
         else if (edr > 0.006 && edr <= 0.009)
         {
            margin = 1.5;
            span = 1.0;
         }
         else if (edr > 0.009 && edr <= 0.015)
         {
            margin = 1.0;
            span = 1.0;
         }
         else if (edr > 0.015 && edr <= 0.05)
         {
            margin = 1.0;
            span = 0.5;
         }
         else if (edr > 0.05)
         {
            margin = 0.5;
            span = 0.5;
         }
      }
   }
}
