using AFSPC.AstroStds.Wrappers;
using System;

namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class KepElts
    {
        public double A_km { get; set; }
        public double E { get; set; }
        public double Incli_deg { get; set; }
        public double MnAnomaly_deg { get; set; }
        public double RAAN_deg { get; set; }
        public double Omega_deg { get; set; }

        private void ExtractDataFrArray(double[] kepElts)
        {
            this.A_km = kepElts[AstroFuncWrapper.XA_KEP_A];
            this.E = kepElts[AstroFuncWrapper.XA_CLS_E];
            this.Incli_deg = kepElts[AstroFuncWrapper.XA_CLS_INCLI];
            this.MnAnomaly_deg = kepElts[AstroFuncWrapper.XA_CLS_MA];
            this.RAAN_deg = kepElts[AstroFuncWrapper.XA_CLS_NODE];
            this.Omega_deg = kepElts[AstroFuncWrapper.XA_CLS_OMEGA];
        }

        public KepElts(double[] kepElts)
        {
            ExtractDataFrArray(kepElts);
        }

        public KepElts(double[] posDate, double[] velDate)
        {
            double[] eqnx = new double[6];
            double[] kepElts = new double[6];

            AstroFuncWrapper.PosVelToEqnx(posDate, velDate, eqnx);
            AstroFuncWrapper.EqnxToKep(eqnx, kepElts);
            ExtractDataFrArray(kepElts);
        }


        public double N_revspday
        {
            get
            {
                return AstroFuncWrapper.AToN(this.A_km);
            }
        }

        public double[] KepEltsArr
        {
            get
            {
                double[] kepElts = new double[6];

                kepElts[AstroFuncWrapper.XA_KEP_A] = A_km;
                kepElts[AstroFuncWrapper.XA_KEP_E] = E;
                kepElts[AstroFuncWrapper.XA_KEP_INCLI] = Incli_deg;
                kepElts[AstroFuncWrapper.XA_KEP_MA] = MnAnomaly_deg;
                kepElts[AstroFuncWrapper.XA_KEP_NODE] = RAAN_deg;
                kepElts[AstroFuncWrapper.XA_KEP_OMEGA] = Omega_deg;

                return kepElts;
            }
        }

        public double TrueAnomaly_deg
        {
            get
            {
                return AstroFuncWrapper.CompTrueAnomaly(this.KepEltsArr);
            }
        }
    }
}
