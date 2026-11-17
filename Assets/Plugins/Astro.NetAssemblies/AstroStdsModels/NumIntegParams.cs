using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Wrappers;
using System;
using System.ComponentModel;

namespace AFSPC.AstroStds.Models
{

   [Serializable]
   public class NumIntegParams
   {
      public enum GeopotIdxEnum
      {
         [Description("Geopotential File")]
         GeoIdx0 = 0,
         [Description("SP003 (STEM68R) 6th order")]
         GeoIdx3 = 3,
         [Description("SP003 (STEM68R) 8th order")]
         GeoIdx4 = 4,
         [Description("WGS72 J2-J5 (zonals) only")]
         GeoIdx5 = 5,
         [Description("WGS72 thru (6,6) only")]
         GeoIdx6 = 6,
         [Description("WGS72 thru (4,4) only")]
         GeoIdx7 = 7,
         [Description("WGS72 thru (8,8) only")]
         GeoIdx8 = 8,
         [Description("WGS84 9th order")]
         GeoIdx9 = 9,
         [Description("WGS84 J2-J5 (zonals) only")]
         GeoIdx10 = 10,
         [Description("NWL-8C 12 order")]
         GeoIdx11 = 11,
         [Description("WGS72 12th order")]
         GeoIdx12 = 12,
         [Description("WGS84 18th order")]
         GeoIdx18 = 18,
         [Description("WGS72 user-specified")]
         GeoIdx20 = 20,
         [Description("WGS84 user-specified")]
         GeoIdx21 = 21,
         [Description("GEM5 22nd order")]
         GeoIdx22 = 22,
         [Description("WGS72 24th order")]
         GeoIdx24 = 24,
         [Description("WGS72 12th order & (13,13)-(29,13)")]
         GeoIdx25 = 25,
         [Description("WGS72 12th order & (14,14), (15,14)")]
         GeoIdx26 = 26,
         [Description("WGS72 12th order & (13,13), (14,14)")]
         GeoIdx27 = 27,
         [Description("GEM9 30th order (thru 30, 30)")]
         GeoIdx30 = 30,
         [Description("JGM2 41st order (thru 41, 41)")]
         GeoIdx40 = 40,
         [Description("WGS84 41st order (thru 41, 41)")]
         GeoIdx41 = 41,
         [Description("WGS84 full 70th order (thru 70, 70)")]
         GeoIdx70 = 70,
         [Description("EGM96 70th order (thru 70, 70)")]
         GeoIdx71 = 71,
      };

      public enum VarOfIntegCtrlEnum
      {
         [Description("Auto Selection")]
         VarOfIntegCtrl0 = 0,
         [Description("Time Integration")]
         VarOfIntegCtrl1 = 1,
         [Description("S Integration")]
         VarOfIntegCtrl2 = 2
      };

      public enum VarStepSizeCtrlEnum
      {
         [Description("Variable")]
         VarStepSizeCtrl0 = 0,
         [Description("Fixed")]
         VarStepSizeCtrl1 = 1,
         [Description("Semi-Fixed")]
         VarStepSizeCtrl2 = 2
      };

      public enum OutputCoordEnum
      {
         [Description("Date")]
         InpCoordDate = 0,
         [Description("Epoch")]
         InpCoordEpoch = 1,
         [Description("J2000")]
         InpCoordJ2000 = 2
      };


      public GeopotIdxEnum GeoIdx { get; set; }
      public bool ApplyBulge { get; set; }
      public bool ApplyDrag { get; set; }
      public SpPropWrapper.DragPertEnum DragPert { get; set; }
      public bool ApplySRP { get; set; }
      public bool ApplyLunarSolar { get; set; }
      public bool UseJPLFile { get; set; }
      public bool ApplyOutgas { get; set; }
      public bool ApplyTides { get; set; }
      public double F10 { get; set; }
      public double F10Avg { get; set; }
      public double Ap { get; set; }
      public int Trunc { get; set; }
      public int ErrCtrl { get; set; } //convergence criterion E-14
      public int InCoord { get; set; }
      public int NutTerms { get; set; }
      public VarOfIntegCtrlEnum StepMode { get; set; }
      public bool IsFixedStep { get; set; }
      public double InitStepSize_min { get; set; }
      public bool IsFastCorrector { get; set; }
      public string DcaFile { get; set; }
      public string FluxFile { get; set; }
      public string GeoFile { get; set; }
      public string JplFile { get; set; }
      public DaysSince50UTC JplStart { get; set; }
      public DaysSince50UTC JplStop { get; set; }

      public NumIntegParams()
      {
         int geoIdx, bulgePert, dragPert, radPresPert, lunSolPert, f10, f10Avg, ap, trunc,
            incr, ogPert, tidePert, inCoord, nTerms, reEval, integStepMode, stepSizeMethod;

         double initStepSize;
         byte[] dcaFile = new byte[DllMainWrapper.FILEPATHLEN];
         byte[] fluxFile = new byte[DllMainWrapper.FILEPATHLEN];
         byte[] geoFile = new byte[DllMainWrapper.FILEPATHLEN];
         byte[] jplFile = new byte[DllMainWrapper.FILEPATHLEN];
         byte[] jplStart = new byte[DllMainWrapper.FILEPATHLEN];
         byte[] jplStop = new byte[DllMainWrapper.FILEPATHLEN];

         SpPropWrapper.SpGet4pAll(out geoIdx, out bulgePert, out dragPert, out radPresPert, out lunSolPert,
                                  out f10, out f10Avg, out ap, out trunc, out incr, out ogPert, out tidePert,
                                  out inCoord, out nTerms, out reEval, out integStepMode, out stepSizeMethod,
                                  out initStepSize, dcaFile, fluxFile, geoFile, jplFile, jplStart, jplStop);

         this.GeoIdx = (GeopotIdxEnum)geoIdx;
         this.ApplyBulge = (bulgePert == 1);
         this.ApplyDrag = (dragPert != 0);
         this.DragPert = (SpPropWrapper.DragPertEnum)dragPert;
         this.ApplySRP = (radPresPert == 1);
         this.ApplyLunarSolar = (lunSolPert != 0);
         this.UseJPLFile = (lunSolPert == 2);
         this.ApplyOutgas = (ogPert == 1);
         this.ApplyTides = (tidePert == 1);
         this.F10 = f10;
         this.F10Avg = f10Avg;
         this.Ap = ap;
         this.Trunc = trunc;
         this.ErrCtrl = Math.Max(10, incr);
         this.NutTerms = nTerms;
         this.IsFastCorrector = (reEval == 0);
         this.StepMode = (VarOfIntegCtrlEnum)integStepMode;
         this.IsFixedStep = (stepSizeMethod == 1);
         this.InitStepSize_min = initStepSize;
         this.DcaFile = Utility.BytesToStringRTrim(dcaFile);
         this.FluxFile = Utility.BytesToStringRTrim(fluxFile);
         this.GeoFile = Utility.BytesToStringRTrim(geoFile);
         this.JplFile = Utility.BytesToStringRTrim(jplFile);
         this.JplStart = new DaysSince50UTC(Utility.BytesToStringRTrim(jplStart));
         this.JplStop = new DaysSince50UTC(Utility.BytesToStringRTrim(jplStop));
      }

      public NumIntegParams(double[] xa_spVec)
      {
         this.GeoIdx = (GeopotIdxEnum)xa_spVec[SpVecWrapper.XA_SPVEC_GEOIDX];
         this.ApplyBulge = (xa_spVec[SpVecWrapper.XA_SPVEC_BULGEFLG] == 1);
         this.ApplyDrag = (xa_spVec[SpVecWrapper.XA_SPVEC_DRAGFLG] != 0);
         this.DragPert = (SpPropWrapper.DragPertEnum)xa_spVec[SpVecWrapper.XA_SPVEC_DRAGFLG];
         this.ApplySRP = (xa_spVec[SpVecWrapper.XA_SPVEC_RADFLG] == 1);
         this.ApplyLunarSolar = (xa_spVec[SpVecWrapper.XA_SPVEC_LUNSOLFLG] != 0);
         this.UseJPLFile = (xa_spVec[SpVecWrapper.XA_SPVEC_LUNSOLFLG] == 2);
         this.ApplyOutgas = (xa_spVec[SpVecWrapper.XA_SPVEC_OGFLG] == 1);
         this.ApplyTides = (xa_spVec[SpVecWrapper.XA_SPVEC_TIDESFLG] == 1);
         this.F10 = xa_spVec[SpVecWrapper.XA_SPVEC_F10];
         this.F10Avg = xa_spVec[SpVecWrapper.XA_SPVEC_F10AVG];
         this.Ap = xa_spVec[SpVecWrapper.XA_SPVEC_AP];
         this.Trunc = (int)xa_spVec[SpVecWrapper.XA_SPVEC_TRUNC];
         this.ErrCtrl = Math.Max(10, (int)xa_spVec[SpVecWrapper.XA_SPVEC_CONVERG]);
         this.NutTerms = (int)xa_spVec[SpVecWrapper.XA_SPVEC_NTERMS];
         this.IsFastCorrector = ((int)xa_spVec[SpVecWrapper.XA_SPVEC_REEVAL] == 0);
         this.StepMode = (VarOfIntegCtrlEnum)(int)xa_spVec[SpVecWrapper.XA_SPVEC_INTEGCTRL];
         this.IsFixedStep = ((int)xa_spVec[SpVecWrapper.XA_SPVEC_VARSTEP] == 1);
         this.InitStepSize_min = xa_spVec[SpVecWrapper.XA_SPVEC_INITSTEP];

         /*
         this.DcaFile = Utility.BytesToStringRTrim(dcaFile);
         this.FluxFile = Utility.BytesToStringRTrim(fluxFile);
         this.GeoFile = Utility.BytesToStringRTrim(geoFile);
         this.JplFile = Utility.BytesToStringRTrim(jplFile);
         this.JplStart = new DaysSince50UTC(Utility.BytesToStringRTrim(jplStart));
         this.JplStop = new DaysSince50UTC(Utility.BytesToStringRTrim(jplStop));
          * */

      }
   }
}


