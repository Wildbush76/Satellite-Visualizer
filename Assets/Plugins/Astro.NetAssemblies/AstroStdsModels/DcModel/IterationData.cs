using System;
using AFSPC.AstroStds.Wrappers;

namespace AFSPC.AstroStds.Models.DcModel
{
   [Serializable]
   public class IterationData
   {
      public int    SubIter{get; set;}            // sub iteration number
      public int    NumResAccepted{get; set;}     // number of residuals accpeted
      public int    NumResRejected{get; set;}     // number of residuals rejected
      public int    CorrType{get; set;}           // correction type: 0=TIME, 1=PLANE", 2=7-ELT, 3=IN-TRK, 4=8-ELT, 5=SUBELT
      public int[]  Idc{get; set;}                // corrected elts flags


      public double RMS_km{get; set;}             // RMS
      public double UnweightedRMS_km{get; set;}   // unweighted RMS (km)
      public double DeltaTRMS_min{get; set;}      // delta T RMS (min)
      public double BetaRMS_deg{get; set;}        // beta RMS (deg)
      public double DeltaHRMS_km{get; set;}       // delta height RMS (km)
      public double ConvValue_km{get; set;}       // convergence value(km)
      public double PredRMS_km{get; set;}         // predicted RMS (km)
      public double[] XMatrix{get; set;}          // X matrix L, N, CHI, PSI, AF, AG, BTERM, (n/a), AGOM
      public double[] EqnxCovMtx{get; set;}       // equinoctial covariance matrix - the lower triangular half 0-44

      public IterationData(int[] xai_dcElts, double[] xar_dcElts, byte[] xas_dcElts)
      {
         // iteration data
         SubIter = xai_dcElts[BatchDCWrapper.XAI_DCELTS_SUBITER];
         NumResAccepted = xai_dcElts[BatchDCWrapper.XAI_DCELTS_RESACC];
         NumResRejected = xai_dcElts[BatchDCWrapper.XAI_DCELTS_RESREJ];
         CorrType = xai_dcElts[BatchDCWrapper.XAI_DCELTS_CORRTYPE];

         Idc = new int[9];
         Array.Copy(xai_dcElts, BatchDCWrapper.XAI_DCELTS_CORRFLGS, Idc,  0, 9); 


         RMS_km = xar_dcElts[BatchDCWrapper.XAR_DCELTS_RMS];

         UnweightedRMS_km = xar_dcElts[BatchDCWrapper.XAR_DCELTS_RMSUNWTD]; //unweighted RMS (km)
         DeltaTRMS_min = xar_dcElts[BatchDCWrapper.XAR_DCELTS_DELTATRMS];    //delta T RMS (min)
         BetaRMS_deg = xar_dcElts[BatchDCWrapper.XAR_DCELTS_BETARMS];        //beta RMS (deg)
         DeltaHRMS_km = xar_dcElts[BatchDCWrapper.XAR_DCELTS_DELTAHTRMS];   //delta height RMS (km)
         ConvValue_km = xar_dcElts[BatchDCWrapper.XAR_DCELTS_CONVQLTY];     //convergence value(km)
         PredRMS_km = xar_dcElts[BatchDCWrapper.XAR_DCELTS_RMSPD];          //predicted RMS 

         XMatrix = new double[9];
         EqnxCovMtx = new double[45];
         Array.Copy(xar_dcElts, 60, XMatrix, 0, 9); 
         Array.Copy(xar_dcElts, 200, EqnxCovMtx, 0, 45); 
      }
   }
}
