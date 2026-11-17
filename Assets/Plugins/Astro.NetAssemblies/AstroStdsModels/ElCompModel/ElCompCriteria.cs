using AFSPC.AstroStds.Wrappers;
using System;

namespace AFSPC.AstroStds.Models.ElCompModel
{

   [Serializable]
   public class ElCompCriteria
   {
      public double IncMax1{get; set;}
      public double IncMax2{get; set;}
      public double IncMax3{get; set;}

      public double RaMax1 {get; set;}
      public double RaMax2 {get; set;}
      public double RaMax3 {get; set;}

      public double PhtMax1{get; set;}
      public double PhtMax2{get; set;}
      public double PhtMax3{get; set;}

      public double EccMax1{get; set;}
      public double EccMax2{get; set;}
      public double EccMax3{get; set;}

      public double PerMax1{get; set;}
      public double PerMax2{get; set;}
      public double PerMax3{get; set;}

      public double ApMax1 {get; set;}
      public double ApMax2 {get; set;}
      public double ApMax3 {get; set;}
               

      public ElCompCriteria()
      {
         GetCurrElCompCriteria();
      }

      public ElCompCriteria(double[] xa_ecDiff)
      {
         PopulateElCompCriteria(xa_ecDiff);
      }


      private void PopulateElCompCriteria(double[] xa_ecDiff)
      {
         IncMax1 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_INCMAX1];
         IncMax2 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_INCMAX2];
         IncMax3 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_INCMAX3];

         RaMax1 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_RAMAX1];
         RaMax2 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_RAMAX2];
         RaMax3 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_RAMAX3];

         PhtMax1 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_PHTMAX1];
         PhtMax2 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_PHTMAX2];
         PhtMax3 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_PHTMAX3];

         EccMax1 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_ECCMAX1];
         EccMax2 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_ECCMAX2];
         EccMax3 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_ECCMAX3];

         PerMax1 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_PERMAX1];
         PerMax2 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_PERMAX2];
         PerMax3 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_PERMAX3];

         ApMax1 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_APMAX1];
         ApMax2 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_APMAX2];
         ApMax3 = xa_ecDiff[ElCompWrapper.XA_ECDIFF_APMAX3];

      }


      public void GetCurrElCompCriteria()
      {
         double[] xa_ecDiff = new double[32];
         ElCompWrapper.ElCompGetCriteria(xa_ecDiff);

         PopulateElCompCriteria(xa_ecDiff);
      }


      public void SetElCompCriteria()
      {
         double[] xa_ecDiff = new double[32];

         xa_ecDiff[ElCompWrapper.XA_ECDIFF_INCMAX1] = IncMax1;
         xa_ecDiff[ElCompWrapper.XA_ECDIFF_INCMAX2] = IncMax2;
         xa_ecDiff[ElCompWrapper.XA_ECDIFF_INCMAX3] = IncMax3;

         xa_ecDiff[ElCompWrapper.XA_ECDIFF_RAMAX1] = RaMax1;
         xa_ecDiff[ElCompWrapper.XA_ECDIFF_RAMAX2] = RaMax2;
         xa_ecDiff[ElCompWrapper.XA_ECDIFF_RAMAX3] = RaMax3;

         xa_ecDiff[ElCompWrapper.XA_ECDIFF_PHTMAX1] = PhtMax1;
         xa_ecDiff[ElCompWrapper.XA_ECDIFF_PHTMAX2] = PhtMax2;
         xa_ecDiff[ElCompWrapper.XA_ECDIFF_PHTMAX3] = PhtMax3;

         xa_ecDiff[ElCompWrapper.XA_ECDIFF_ECCMAX1] = EccMax1;
         xa_ecDiff[ElCompWrapper.XA_ECDIFF_ECCMAX2] = EccMax2;
         xa_ecDiff[ElCompWrapper.XA_ECDIFF_ECCMAX3] = EccMax3;


         xa_ecDiff[ElCompWrapper.XA_ECDIFF_PERMAX1] = PerMax1;
         xa_ecDiff[ElCompWrapper.XA_ECDIFF_PERMAX2] = PerMax2;
         xa_ecDiff[ElCompWrapper.XA_ECDIFF_PERMAX3] = PerMax3;

         xa_ecDiff[ElCompWrapper.XA_ECDIFF_APMAX1] = ApMax1;
         xa_ecDiff[ElCompWrapper.XA_ECDIFF_APMAX2] = ApMax2;
         xa_ecDiff[ElCompWrapper.XA_ECDIFF_APMAX3] = ApMax3;

         ElCompWrapper.ElCompSetCriteria(xa_ecDiff);
      }

      public static void SetElCompCriteria(double[] xa_ecDiff)
      {
         ElCompWrapper.ElCompSetCriteria(xa_ecDiff);
      }
















   }
}
