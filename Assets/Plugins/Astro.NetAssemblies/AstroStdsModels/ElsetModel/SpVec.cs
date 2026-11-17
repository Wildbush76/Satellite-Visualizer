using System;
using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Models.SatelliteModel;


namespace AFSPC.AstroStds.Models.ElsetModel
{
   [Serializable]
   public class SpVec
   {
      public int ErrCode { get; set; }
      public string Line1 { get; set; }
      public string Line2 { get; set; }
      public SatellitePosition SatPos { get; set; }
      public SatelliteVelocity SatVel { get; set; }
      public char SecClass { get; set; }
      public int SatNum { get; set; }
      public string SatName { get; set; }
      public DaysSince50UTC EpochTimeDs50UTC { get; set; }
      public int RevNum { get; set; }
      public int ElsetNum { get; set; }
      public double BTerm { get; set; }
      public double Agom { get; set; }
      public double OgParm { get; set; }
      public string CoordSys { get; set; }

      public NumIntegParams NumIntegParms { get; set; }

      public double[] PosDate { get; set; }
      public double[] VelDate { get; set; }

      public SpVec(string line1, string line2)
      {
         double[] pos = new double[3];
         double[] vel = new double[3];
         char secClass;
         int satNum;
         byte[] satName = new byte[8];
         byte[] epochDtg = new byte[17];
         int revNum;
         int elsetNum;
         double bterm, agom, ogParm;
         byte[] coordSys = new byte[5];



         ErrCode = SpVecWrapper.SpVecParse(line1, line2, pos, vel, out secClass, out satNum, satName, epochDtg,
            out revNum, out elsetNum, out bterm, out agom, out ogParm, coordSys);

         if (ErrCode != 0)
            return;

         this.Line1 = line1;
         this.Line2 = line2;
         this.SatPos = new SatellitePosition(pos);
         this.SatVel = new SatelliteVelocity(vel);
         this.SecClass = secClass;
         this.SatNum = satNum;
         this.SatName = Utility.BytesToString(satName);
         this.EpochTimeDs50UTC = new DaysSince50UTC(Utility.BytesToString(epochDtg));
         this.RevNum = revNum;
         this.ElsetNum = elsetNum;
         this.BTerm = bterm;
         this.Agom = agom;
         this.OgParm = ogParm;
         this.CoordSys = Utility.BytesToString(coordSys);
         ComputePosVelDate();
      }

      public SpVec(long satKey)
      {
         double[] xa_SpVec = new double[512];
         byte[] xs_SpVec = new byte[512];

         this.ErrCode = SpVecWrapper.SpVecDataToArray(satKey, xa_SpVec, xs_SpVec);
         if (this.ErrCode != 0)
            throw new KeyNotFoundException("SpVecWrapper.SpVecDataToArray", satKey);


         PopulateDataFrArray(xa_SpVec, Utility.BytesToString(xs_SpVec));

      }


      public SpVec(double[] xa_SpVec, byte[] xs_SpVec)
      {
         string s_SpVec = Utility.BytesToString(xs_SpVec);

         PopulateDataFrArray(xa_SpVec, s_SpVec);
      }

      public SpVec(double[] xa_SpVec, string s_SpVec)
      {
         PopulateDataFrArray(xa_SpVec, s_SpVec);
      }


      private void PopulateDataFrArray(double[] xa_SpVec, string s_SpVec)
      {
         this.ErrCode = 0;
         this.SatPos = new SatellitePosition(new double[] {xa_SpVec[SpVecWrapper.XA_SPVEC_POS1],
                                                           xa_SpVec[SpVecWrapper.XA_SPVEC_POS2],
                                                           xa_SpVec[SpVecWrapper.XA_SPVEC_POS3]});
         this.SatVel = new SatelliteVelocity(new double[] {xa_SpVec[SpVecWrapper.XA_SPVEC_VEL1] / 1000,
                                                           xa_SpVec[SpVecWrapper.XA_SPVEC_VEL2] / 1000,
                                                           xa_SpVec[SpVecWrapper.XA_SPVEC_VEL3] / 1000});
         this.SecClass = s_SpVec[SpVecWrapper.XS_SPVEC_SECCLASS_0_1];
         this.SatNum = (int)(xa_SpVec[SpVecWrapper.XA_SPVEC_SATNUM]);
         this.SatName = s_SpVec.Substring(SpVecWrapper.XS_SPVEC_SATNAME_1_8, 8);
         this.EpochTimeDs50UTC = xa_SpVec[SpVecWrapper.XA_SPVEC_EPOCH];

         this.EpochTimeDs50UTC = new DaysSince50UTC(xa_SpVec[SpVecWrapper.XA_SPVEC_EPOCH]);
         this.RevNum = (int)xa_SpVec[SpVecWrapper.XA_SPVEC_REVNUM];
         this.ElsetNum = (int)xa_SpVec[SpVecWrapper.XA_SPVEC_ELSETNUM];
         this.BTerm = xa_SpVec[SpVecWrapper.XA_SPVEC_BTERM];
         this.Agom = xa_SpVec[SpVecWrapper.XA_SPVEC_AGOM];
         this.OgParm = xa_SpVec[SpVecWrapper.XA_SPVEC_OGPARM];
         if (xa_SpVec[SpVecWrapper.XA_SPVEC_INPCOORD] == 1)
            this.CoordSys = "TMDAT";
         else if (xa_SpVec[SpVecWrapper.XA_SPVEC_INPCOORD] == 2)
            this.CoordSys = "MMJ2K";
         else
            this.CoordSys = "";

         ComputePosVelDate();

         this.ElsetNum = (int)xa_SpVec[SpVecWrapper.XA_SPVEC_ELSETNUM];
         this.RevNum = (int)xa_SpVec[SpVecWrapper.XA_SPVEC_REVNUM];

         byte[] line1 = new byte[512];
         byte[] line2 = new byte[512];
         SpVecWrapper.SpVecArrayToLines(xa_SpVec, s_SpVec, line1, line2);

         this.Line1 = Utility.BytesToStringRTrim(line1);
         this.Line2 = Utility.BytesToStringRTrim(line2);

         this.NumIntegParms = new NumIntegParams(xa_SpVec);

      }

      private void ComputePosVelDate()
      {
         // Input coordinate systems is in J2000
         if (this.CoordSys == "MMB50" || this.CoordSys == "MMJ2K")
         {
            this.PosDate = new double[3];
            this.VelDate = new double[3];

            // Convert J2000 to Date
            //AstroFuncWrapper.RotJ2KToDate(1, 4, TimeFuncWrapper.UTCToTAI(epochDs50UTC), pos, vel, posDate, velDate);
            // DHN 16Jun15 - start using full nutation terms of 106
            AstroFuncWrapper.RotJ2KToDate(1, 106, TimeFuncWrapper.UTCToTAI(this.EpochTimeDs50UTC.Ds50UTC), this.SatPos.ToArray(), SatVel.ToArray(), PosDate, VelDate);
         }
         else // input coordinate is already in TEME of Date
         {
            this.PosDate = this.SatPos.ToArray();
            this.VelDate = this.SatVel.ToArray();

         }
      }
   }
}
