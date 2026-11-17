using AFSPC.AstroStds.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFSPC.AstroStds.Models.SensorModel
{
   [Serializable]
   public class SensorSigmas
   {
      public double AzRaSigma_deg { get; set; }
      public double ElDecSigma_deg { get; set; }
      public double RngSigma_km { get; set; }
      public double RngRateSigma_kmps { get; set; }
      public double AzRaRateSigma_dps { get; set; }
      public double ElDecRateSigma_dps { get; set; }


      public SensorSigmas(double[] xa_sen)
      {
         this.AzRaSigma_deg       = xa_sen[SensorWrapper.XA_SEN_GEN_AZSIGMA];
         this.ElDecSigma_deg       = xa_sen[SensorWrapper.XA_SEN_GEN_ELSIGMA];
         this.RngSigma_km       = xa_sen[SensorWrapper.XA_SEN_GEN_RGSIGMA];
         this.RngRateSigma_kmps = xa_sen[SensorWrapper.XA_SEN_GEN_RRSIGMA];
         this.AzRaRateSigma_dps   = xa_sen[SensorWrapper.XA_SEN_GEN_ARSIGMA];
         this.ElDecRateSigma_dps   = xa_sen[SensorWrapper.XA_SEN_GEN_ERSIGMA];
      }
   }
}

