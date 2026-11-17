using System;
using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Utilities;
using System.ComponentModel;


namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class SatelliteState
   {
      [Browsable(false)]
      public int ErrCode { get; set; }
      public enum PropTimeType
      {
         Mse = 1,     //SatStateWrapper.TIME_IS_MSE,
         Ds50TAI = 2, //SatStateWrapper.TIME_IS_TAI
         Ds50UTC = 3  //SatStateWrapper.TIME_IS_UTC,
      }

      public long SatKey;

      public int SatNum
      {
         get
         {
            return ElOpsWrapper.SatNumOf(SatKey);
         }
      }
      public string YYDDDHHMMSS
      {
         get
         {
            return cDs50UTC.Dtg23Str;
         }
      }

      public string DTGForm2
      {
         get
         {
            return cDs50UTC.ToDTGForm2();
         }
      }

      public string DDD_ddd
      {
         get
         {
            return cDs50UTC.Dtg17Str.Substring(5);
         }
      }

      [Browsable(false)]
      private DaysSince50UTC cDs50UTC { get; set; }

      public double Ds50UTC { get; set; }
      public double Mse { get; set; }
      public int RevNum { get; set; }

      public double[] PosDateArr = new double[3];
      public double[] VelDateArr = new double[3];

      public SatellitePosition PosDate { get; set; }
      public SatelliteVelocity VelDate { get; set; }
      public SatelliteLLH LLH { get; set; }
      public SatelliteEFG EFG { get; set; }

      [Browsable(false)]
      public double GeocenLat_deg { get; set; }

      public SatelliteState() { }

      public SatelliteState(long satKey, PropTimeType propTimeType, double timeIn)
      {
         double ds50UTC, ds50TAI, mse;
         double[] pos = new double[3];
         double[] vel = new double[3];
         double[] llh = new double[3];
         int revNum;

         ds50UTC = mse = 0;
         revNum = 0;

         if (propTimeType == PropTimeType.Mse)
         {
            mse = timeIn;
            this.ErrCode = SatStateWrapper.SatStateMse(satKey, timeIn, out ds50UTC, out revNum, pos, vel, llh);
         }
         else if (propTimeType == PropTimeType.Ds50UTC)
         {
            ds50UTC = timeIn;
            this.ErrCode = SatStateWrapper.SatStateDs50UTC(satKey, timeIn, out mse, out revNum, pos, vel, llh);
         }
         else if (propTimeType == PropTimeType.Ds50TAI)
         {
            ds50TAI = timeIn;
            ds50UTC = TimeFuncWrapper.TAIToUTC(ds50TAI);
            this.ErrCode = SatStateWrapper.SatStateDs50UTC(satKey, ds50UTC, out mse, out revNum, pos, vel, llh);
         }

         this.Ds50UTC = ds50UTC;

         if (ErrCode != 0)
            return;

         this.SetSatelliteState(satKey, mse, ds50UTC, revNum, pos, vel, llh);
      }


      public SatelliteState(long satKey, double mse, double ds50UTC, int revNum, double[] pos, double[] vel, double[] llh)
      {
         this.SetSatelliteState(satKey, mse, ds50UTC, revNum, pos, vel, llh);
      }

      public void SetSatelliteState(long satKey, double mse, double ds50UTC, int revNum, double[] pos, double[] vel, double[] llh)
      {
         double[] posEFG = new double[3];
         double[] velEFG = new double[3];

         this.SatKey = satKey;
         this.cDs50UTC = new DaysSince50UTC(ds50UTC);
         this.Mse = mse;
         this.RevNum = revNum;
         this.PosDate = new SatellitePosition(pos);
         this.VelDate = new SatelliteVelocity(vel);
         this.PosDateArr = pos;
         this.VelDateArr = vel;
         this.LLH = new SatelliteLLH(llh);
         AstroFuncWrapper.ECIToEFGTime(ds50UTC, pos, vel, posEFG, velEFG);
         this.EFG = new SatelliteEFG(posEFG);
         this.GeocenLat_deg = Math.Atan(pos[2] / Math.Sqrt(pos[0] * pos[0] + pos[1] * pos[1])) * Constants.RAD2DEG;
         this.Ds50UTC = ds50UTC;
      }


      [Browsable(false)]
      public KepElts KepElts
      {
         get
         {
            return new KepElts(this.PosDateArr, this.VelDateArr);
         }
      }


   }

}


//public static void SatStateInitSat(long satKey)
//{
//   int errCode = SatStateWrapper.SatStateInitSat(satKey);
//   if (errCode != 0)
//   {
//      InstantMessageBox.Show();
//      return;
//   }
//}

//public void SatStateMse(double mse)
//{

//   int errCode = SatStateWrapper.SatStateMse(satKey, mse, out this.Ds50UTC, out this.RevNum, pos, vel, llh);
//   if (errCode != 0)
//   {
//      InstantMessageBox.Show();
//      return;
//   }
//   SatPosECI = new SatellitePosition(pos);
//   VelDate = new SatelliteVelocity(vel);
//   LLH = new SatelliteLLH(llh);
//}

//public void SatStateDs50UTC(double mse)
//{

//   int errCode = SatStateWrapper.SatStateDs50UTC(satKey, ds50UTC, out this.Mse, out this.RevNum, pos, vel, llh);
//   if (errCode != 0)
//   {
//      InstantMessageBox.Show();
//      return;
//   }
//   SatPosECI = new SatellitePosition(pos);
//   VelDate = new SatelliteVelocity(vel);
//   LLH = new SatelliteLLH(llh);
//}
