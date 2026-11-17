using AFSPC.AstroStds.Wrappers;
using System;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class ClassElts
    {
        public double MeanMotion_rpd { get; set; }
        public double Eccentricity { get; set; }
        public double Inclination_deg { get; set; }
        public double MeanAnom_deg { get; set; }
        public double RAAN_deg { get; set; }
        public double ArgOfPer_deg { get; set; }

        private void ExtractDataFrArray(double[] classElts)
        {
            this.MeanMotion_rpd = classElts[AstroFuncWrapper.XA_CLS_N];
            this.Eccentricity = classElts[AstroFuncWrapper.XA_CLS_E];
            this.Inclination_deg = classElts[AstroFuncWrapper.XA_CLS_INCLI];
            this.MeanAnom_deg = classElts[AstroFuncWrapper.XA_CLS_MA];
            this.RAAN_deg = classElts[AstroFuncWrapper.XA_CLS_NODE];
            this.ArgOfPer_deg = classElts[AstroFuncWrapper.XA_CLS_OMEGA];
        }

        public ClassElts(double[] classElts)
        {
            ExtractDataFrArray(classElts);
        }

        public ClassElts(double[] posDate, double[] velDate)
        {
            double[] eqnx = new double[6];
            double[] classElts = new double[6];

            AstroFuncWrapper.PosVelToEqnx(posDate, velDate, eqnx);
            AstroFuncWrapper.EqnxToClass(eqnx, classElts);
            ExtractDataFrArray(classElts); 
        }


        public double A_km
        {
            get
            {
                return AstroFuncWrapper.NToA(this.MeanMotion_rpd);
            }
        }

        public double[] ClassEltsArr
        {
            get
            {
                double[] classElts = new double[6];

                classElts[AstroFuncWrapper.XA_CLS_N] = MeanMotion_rpd;
                classElts[AstroFuncWrapper.XA_CLS_E] = Eccentricity;
                classElts[AstroFuncWrapper.XA_CLS_INCLI] = Inclination_deg;
                classElts[AstroFuncWrapper.XA_CLS_MA] = MeanAnom_deg;
                classElts[AstroFuncWrapper.XA_CLS_NODE] = RAAN_deg;
                classElts[AstroFuncWrapper.XA_CLS_OMEGA] = ArgOfPer_deg;

                return classElts;
            }
        }

        
    }
}
