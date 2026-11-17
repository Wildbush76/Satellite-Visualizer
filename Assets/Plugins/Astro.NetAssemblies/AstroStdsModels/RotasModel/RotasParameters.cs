using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Utilities;
using System.ComponentModel;

namespace AFSPC.AstroStds.Models.RotasModel
{
   public static class RotasParameters
   {
/*
   real(8) :: GROSSBETA=0.0D0                !  gross beta limit (deg)
   real(8) :: BLIMIT=0.0D0                   !  ASTAT 0 beta limit (deg)
   real(8) :: TLIM0=0.0D0                    !  ASTAT 0 delta-t limit (min)
   real(8) :: HLIMIT=0.0D0                   !  ASTAT 0 delta-height limit (km)
   integer :: MULT2=3                        !  ASTAT 2 multiplier
   integer :: PRTRESFL=1                     !  residual print flag
                                             !   1 = print all residuals
                                             !   2 = print ASTAT 1s only
                                             !   3 = print no residuals
                                             !   4 = print ASTAT 1 & 2s only
                                             
   integer :: RETAGFL=0                      !  retag ASTAT 1 obs flag
                                             !   0 = do not retag any obs
                                             !   1 = retag ASTAT 0/1, punch B3
                                             !   2 = retag ASTAT 0/1, punch TTY
                                             !   3 = retag ASTAT 0/1/2, punch B3
                                             
   integer :: NEWDT=0                        !  delta-t computation method
                                             ! 0: no change to current setting
                                             ! 1: DELTA/427M method
                                             ! 2: SPADOC 4C method         
                                             
   integer :: LITIM=0                        !  light-time correction flag
                                             ! 0: do not appy light-time correction
                                             ! 1: apply light-time correction analytical
                                             ! 2: apply light-time correction exactly
                                             
   !integer :: MAXASSOC=10                    !  max # of ALLEL associations/ob                                             
   integer :: NUMASSOC=3                     !  max # of ALLEL assoc's to compute
   integer :: NOSTIC=0                       !  diagnostic print flag
   integer :: PROPCOV=0                      !  flag to propagate/print covariance
   integer :: JTRACK=0                       !  If set, perform track processing    
   integer :: ISTEAL=0                       !  if set, retagged obs are
 
 * */
      
      public enum ResidualPrintFlag
      {
         [Description("Print all residuals")]
         PrintAll = 1,
         [Description("Print ASTAT 1s only")]
         ASTAT1Only = 2,
         [Description("Print no residuals")]
         PrintNone = 3,
      };

      public enum ASTAT1RetagFlag
      {
         [Description("Do not retag obs")]
         DonotRetagObs = 0,
         [Description("Retag ASTAT1s to B3 obs")]
         RetagToB3 = 1,
         [Description("Retag ASTAT1s to TTY obs")]
         RetagToTTY = 2,
      };

      /*
      public enum Type5ObLTC
      {
         [Description("Do not apply light-time correction")]
         NoLTC = 0,
         [Description("Apply light-time correction analytical")]
         Analytical = 1,
         [Description("Apply light-time correction exactly")]
         Exactly = 2,
      };
      */




      public static string RotasMode { get; set; }
      public static double GrossBeta { get; set; }
      public static double BetaLimit { get; set; }
      public static double DeltaTLimit { get; set; }
      public static double DeltaHLimit { get; set; }
      public static int ASTAT2Mul { get; set; }
      public static ResidualPrintFlag ResPrntFlg { get; set; }
      public static ASTAT1RetagFlag ASTAT1RetagFlg { get; set; }
      public static RotasWrapper.LightTimeCorrection LTCFlg { get; set; }
      public static RotasWrapper.ResCompMethodFlag ResCompMethod { get; set; }
      public static int MaxAssoPerOb { get; set; }
      public static bool DebiasObs { get; set; }
      public static int DiagMode { get; set; }
      public static int CovPrtFlag { get; set; }
      public static int TrackFlag { get; set; }
      public static int RemRetagObs { get; set; }
      public static bool ApplyAnnAb { get; set; }
      public static bool ApplyDiuAb { get; set; }
      public static double[] XA_RT_Parms { get; set; }
      static RotasParameters()
      {
         Get1P();
      }

      public static void Get1P()
      {
         byte[] rotasMode = new byte[5];
         double grossBeta;
         double betaLimit;
         double deltaTLimit;
         double deltaHLimit;
         int astat2Mult;
         int prtFlag;
         int retagFlag;  
         int ltcFlag;  
         int maxNumAssoc;  
         int debiasFlag;  
         int diagMode;  
         int covPrtFlag;  
         int trackFlag;  
         int remRetagObs;
         double[] extArr = new double[128];


         RotasWrapper.RotasGetPAll(rotasMode, out grossBeta, out betaLimit, out deltaTLimit, out deltaHLimit, out astat2Mult, out prtFlag, 
            out retagFlag, out ltcFlag, out maxNumAssoc, out debiasFlag, out diagMode, out covPrtFlag, out trackFlag, out remRetagObs, extArr);

         RotasMode = Utility.BytesToString(rotasMode);
         GrossBeta = grossBeta;
         BetaLimit = betaLimit;
         DeltaTLimit = deltaTLimit;
         DeltaHLimit = deltaHLimit;
         ASTAT2Mul = astat2Mult;
         ResPrntFlg = (ResidualPrintFlag)prtFlag;
         ASTAT1RetagFlg = (ASTAT1RetagFlag)retagFlag;
         LTCFlg = (RotasWrapper.LightTimeCorrection)ltcFlag;

         int resCompMethod = Utility.GetInt(RotasWrapper.RotasGetPField, RotasWrapper.XF_RP_RESCOMPMETH);
         ResCompMethod = (RotasWrapper.ResCompMethodFlag)resCompMethod;

         MaxAssoPerOb = maxNumAssoc;
         DebiasObs = (debiasFlag == 1);
         DiagMode = diagMode;
         CovPrtFlag = covPrtFlag;
         TrackFlag = trackFlag;
         RemRetagObs = remRetagObs;
         double[] xa_rt_parms = new double[16];
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_GROSSBETA] = GrossBeta;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_BETALIM] = BetaLimit;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_DELTATLIM] = DeltaTLimit;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_DELTAHLIM] = DeltaHLimit;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_ASTAT2MULT] = ASTAT2Mul;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_LTC] = (double)LTCFlg;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_DEBIAS] = DebiasObs ? 1 : 0;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_RESCOMPMETH] = (double)ResCompMethod;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_ANNUALAB] = ApplyAnnAb ? 1 : 0;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_DIURNALAB] = ApplyDiuAb ? 1 : 0;
         XA_RT_Parms = xa_rt_parms;

      }

      public static void Set1P()
      {
         string rotasMode   = RotasMode;
         double grossBeta   = GrossBeta;
         double betaLimit   = BetaLimit;
         double deltaTLimit = DeltaTLimit;
         double deltaHLimit = DeltaHLimit;
         int astat2Mult     = (int) ASTAT2Mul;
         int prtFlag        = (int) ResPrntFlg; 
         int retagFlag      = (int) ASTAT1RetagFlg;
         int ltcFlag        = (int) LTCFlg;
         int resCompMeth    = (int) ResCompMethod;
         int maxNumAssoc    = MaxAssoPerOb;
         int debiasFlag     = DebiasObs ? 1 : 0;
         int diagMode       = DiagMode;
         int covPrtFlag     = CovPrtFlag ;
         int trackFlag      = TrackFlag;
         int remRetagObs    = RemRetagObs;
         double[] extArr = new double[128];


         RotasWrapper.RotasSetPAll(rotasMode, grossBeta, betaLimit, deltaTLimit, deltaHLimit, astat2Mult, prtFlag,
            retagFlag, ltcFlag, maxNumAssoc, debiasFlag, diagMode, covPrtFlag, trackFlag, remRetagObs, extArr);

         RotasWrapper.RotasSetPField(RotasWrapper.XF_RP_RESCOMPMETH, resCompMeth.ToString());
         double[] xa_rt_parms = new double[16];
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_GROSSBETA] = GrossBeta;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_BETALIM] = BetaLimit;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_DELTATLIM] = DeltaTLimit;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_DELTAHLIM] = DeltaHLimit;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_ASTAT2MULT] = ASTAT2Mul;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_LTC] = (double)LTCFlg;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_DEBIAS] = DebiasObs ? 1 : 0;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_RESCOMPMETH] = (double)ResCompMethod;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_ANNUALAB] = ApplyAnnAb ? 1 : 0;
         xa_rt_parms[RotasWrapper.XA_RT_PARMS_DIURNALAB] = ApplyDiuAb ? 1 : 0;
         XA_RT_Parms = xa_rt_parms;
      }



   }
}
