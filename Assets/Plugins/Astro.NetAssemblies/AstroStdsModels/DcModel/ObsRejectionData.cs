using System;
using AFSPC.AstroStds.Wrappers;


namespace AFSPC.AstroStds.Models.DcModel
{
   [Serializable]
   public class ObsRejectionData
   {
      public int Decayed{get; set;}               //satellite has decayed at the time of the obs
      public int Error{get; set;}                 //obs residual computation error code: 1: no sensor
      public int Azimuth{get; set;}               //azimuth residual rejected
      public int Beta{get; set;}                  //beta residual rejected
      public int Elevation{get; set;}             //elevation residual rejected
      public int Height{get; set;}                //delta h residual rejected
      public int Range{get; set;}                 //range residual rejected
      public int RangeRate{get; set;}             //range rate residual rejected
      public int Time{get; set;}                  //delta t residual rejected

      public ObsRejectionData(int[] obsRejFlgs)
      {
         Decayed    = obsRejFlgs[BatchDCWrapper.XA_REJFLG_DECAYED];
         Error      = obsRejFlgs[BatchDCWrapper.XA_REJFLG_ERROR];
         Azimuth    = obsRejFlgs[BatchDCWrapper.XA_REJFLG_RA];
         Beta       = obsRejFlgs[BatchDCWrapper.XA_REJFLG_BETA];
         Elevation  = obsRejFlgs[BatchDCWrapper.XA_REJFLG_DEC];
         Height     = obsRejFlgs[BatchDCWrapper.XA_REJFLG_HEIGHT];
         Range      = obsRejFlgs[BatchDCWrapper.XA_REJFLG_RANGE];
         RangeRate  = obsRejFlgs[BatchDCWrapper.XA_REJFLG_RR];
         Time       = obsRejFlgs[BatchDCWrapper.XA_REJFLG_TIME];
      }
   }
}
