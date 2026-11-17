using AFSPC.AstroStds.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFSPC.AstroStds.Models.SensorModel
{
   [Serializable]
   public class SensorBiases
   {
      public double AzRaBias_deg { get; set; }
      public double ElDecBias_deg { get; set; }
      public double RngBias_km { get; set; }
      public double RngRateBias_kmps { get; set; }
      public double TimeBias_sec { get; set; }


      public SensorBiases(double[] xa_sen)
      {
         this.AzRaBias_deg        = xa_sen[SensorWrapper.XA_SEN_GEN_AZBIAS];
         this.ElDecBias_deg        = xa_sen[SensorWrapper.XA_SEN_GEN_ELBIAS];
         this.RngBias_km        = xa_sen[SensorWrapper.XA_SEN_GEN_RGBIAS];
         this.RngRateBias_kmps  = xa_sen[SensorWrapper.XA_SEN_GEN_RRBIAS];
         this.TimeBias_sec       = xa_sen[SensorWrapper.XA_SEN_GEN_TIMEBIAS];
      }
   }
}
