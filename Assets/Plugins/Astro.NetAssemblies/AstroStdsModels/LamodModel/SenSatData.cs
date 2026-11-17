using AFSPC.AstroStds.Wrappers;
using System;

namespace AFSPC.AstroStds.Models.LamodModel
{
   [Serializable]
   public class SenSatData
   {
      public int ErrCode { get; set; }
      
      public long SenKey { get; set; }
      public long SatKey { get; set; }
      public long SenSatKey { get; set; }
      public int SenNum { get; set; }
      public int SatNum { get; set; }

      /// <summary>
      /// sensor viewing type
      /// </summary>
      public char ViewType { get; set; }

      /// <summary>
      /// observation type
      /// </summary>
      public char ObsType { get; set; }

      public int MaxPtsPerPass { get; set; }
      public int StepMode { get; set; }
      public int OptVisFlg { get; set; }
      public double StartDs50TAI { get; set; }    // input start time minus half rev
      public double StopDs50TAI { get; set; }     // input stop time plus half rev
      public double Interval_min { get; set; }        // output interval in minutes
      public double Period_min { get; set; }          // satellite period in minutes

      
      public SenSatData(long senSatKey, long senKey, long satKey)
      {
         double[] xa_senSat = new double[32];

         int errCode = LamodWrapper.LamodSenSatDataToArray(senSatKey, xa_senSat);
     

         SatKey = satKey;
         ErrCode = errCode;

         if (ErrCode == 0)
         {
            SenKey       = senKey;
            SenSatKey    = senSatKey;   
            SenNum       = (int)xa_senSat[LamodWrapper.XA_SENSAT_SENNUM];
            SatNum       = (int)xa_senSat[LamodWrapper.XA_SENSAT_SATNUM];
            ViewType     = (char)((int)xa_senSat[LamodWrapper.XA_SENSAT_VIEWTYPE]);
            ObsType      = (char)((int)xa_senSat[LamodWrapper.XA_SENSAT_OBSTYPE]);
            MaxPtsPerPass= (int)xa_senSat[LamodWrapper.XA_SENSAT_PTSPERPAS];
            StepMode     = (int)xa_senSat[LamodWrapper.XA_SENSAT_STEPMODE];
            OptVisFlg  = (int)xa_senSat[LamodWrapper.XA_SENSAT_OPTVISFLAG];
            StartDs50TAI = xa_senSat[LamodWrapper.XA_SENSAT_STARTAI];
            StopDs50TAI  = xa_senSat[LamodWrapper.XA_SENSAT_STOPTAI];
            Interval_min = xa_senSat[LamodWrapper.XA_SENSAT_INTERVAL];
            Period_min   = xa_senSat[LamodWrapper.XA_SENSAT_PERIOD];
         }
      }
   }
}
