using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AFSPC.AstroStds.Wrappers;

namespace AFSPC.AstroStds.Models.RotasModel
{
   public class RotasParameter
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
         [EnumDescription("Print all residuals")]
         PrintAll = 1,
         [EnumDescription("Print ASTAT 1s only")]
         ASTAT1Only = 2,
         [EnumDescription("Print no residuals")]
         PrintNone = 3,
      };

      public enum ASTAT1RetagFlag
      {
         [EnumDescription("Do not retag obs")]
         DonotRetagObs = 0,
         [EnumDescription("Retag ASTAT1s to B3 obs")]
         RetagToB3 = 1,
         [EnumDescription("Retag ASTAT1s to TTY obs")]
         RetagToTTY = 2,
      };

      public enum Type5ObLTC
      {
         [EnumDescription("Do not apply light-time correction")]
         NoLTC = 0,
         [EnumDescription("Apply light-time correction analytical")]
         Analytical = 1,
         [EnumDescription("Apply light-time correction exactly")]
         Exactly = 2,
      };




      public string RotasMode { get; set; }
      public double BetaLimit { get; set; }
      public double DeltaTLimit { get; set; }
      public double DeltaHLimit { get; set; }
      public int ASTAT2Mul { get; set; }
      public ResidualPrintFlag ResPrntFlg;
      public ASTAT1RetagFlag ASTAT1RetagFlg;
      public Type5ObLTC LTCFlg;
      public int MaxAssoPerOb { get; set; }



   }
}
