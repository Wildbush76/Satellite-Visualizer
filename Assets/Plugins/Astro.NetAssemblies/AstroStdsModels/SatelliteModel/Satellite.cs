using System;
using AFSPC.AstroStds.Wrappers;
using System.ComponentModel;
using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Models.ElsetModel;


namespace AFSPC.AstroStds.Models.SatelliteModel
{
   [Serializable]
   public class Satellite
   {
      // SelectedValue numbers match eltType in DllMainWrapper

      // Options for showing different orbital element types
      public enum OrbEltSelectionEnum
      {
         [Description("ECI TEME of DATE")]
         ECI_DATE = 0,
         [Description("ECI MEME of J2K")]
         ECI_J2K = 1,
         [Description("EFG Vector")]
         EFG = 2,
         [Description("Classical Elements")]
         CLASS = 3,
         [Description("Equinoctial Elements")]
         EQNX = 4,
         [Description("PeriodApoPerigee/LLH")]
         PAPLLH = 5,
      };

      public int ErrCode { get; set; }
      public long SatKey { get; set; }
      public int SatNum { get; set; }
      public string SatName { get; set; }
      public char SecClass { get; set; }
      //public string RecName { get; set; }
      public int RevNum { get; set; }
      public int ElsetNum { get; set; }
      public DaysSince50UTC EpochDs50UTC { get; set; }
      public DllMainWrapper.EltTypeEnum EltType { get; set; }
      public double BTerm { get; set; }
      public double BStar { get; set; }
      public double OgParm { get; set; }
      public double Agom { get; set; }
      public double CmOffset { get; set; }
      public B1CoordSysEnum CoordSys_B1 { get; set; }
      private double[] posDate = new double[3];
      private double[] velDate = new double[3];

      [Browsable(false)]
      public string OrbEltLines { get; set; }
      public int XPFlag { get; set; }
      public ClassElts ClassElts;

      public void UpdateClassElts()
      {
         double[] eqnx = new double[6];
         AstroFuncWrapper.PosVelToEqnx(this.posDate, this.velDate, eqnx);
         double[] classEltsArr = new double[6];
         AstroFuncWrapper.EqnxToClass(eqnx, classEltsArr);
         this.ClassElts = new ClassElts(classEltsArr);
      }

      public void UpdatePosVel(double[] classEltsArr)
      {
         double[] eqnx = new double[6];
         AstroFuncWrapper.ClassToEqnx(classEltsArr, eqnx);
         AstroFuncWrapper.EqnxToPosVel(eqnx, this.posDate, this.velDate);
      }


      public enum B1CoordSysEnum
      {
         B1_COORD_DATE = 0,
         B1_COORD_J2K = 1,
      };

      // Constructor
      public Satellite()
      {
         this.SatKey = 0;
         this.SatNum = 99999;
         this.SatName = "NA";
         //this.RecName = "My SAT ";
         this.RevNum = 0;
         this.ElsetNum = 0;
         this.EpochDs50UTC = TimeFuncWrapper.CurrTimeInDs50UTC();
         this.EltType = DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_SGP4;
         this.BTerm = 0;
         this.BStar = 0;
         this.OgParm = 0;
         this.Agom = 0;
         this.CmOffset = 0;
         this.posDate = new double[] { -808, 6242, 7.88 };
         this.velDate = new double[] { -3.56, -0.63, 7.038 };
         this.OrbEltLines = "Not Available";
         UpdateClassElts();
      }


      public Satellite(long satKey)
      {
         byte[] line1 = new byte[DllMainWrapper.INPUTCARDLEN];
         byte[] line2 = new byte[DllMainWrapper.INPUTCARDLEN];


         int satNum;
         byte[] satName = new byte[8];
         double epochDs50UTC;
         double[] pos = new double[3]; //(km)
         double[] vel = new double[3]; //(km/sec)

         //try
         {
            SatelliteParameters satParm = new SatelliteParameters(satKey);

            //int eltType = SatStateService.GetOrbEltType(satKey); 
            DllMainWrapper.EltTypeEnum eltType = satParm.EltType; // SatStateService.GetOrbEltType(satKey);

            this.SatKey = satKey;

            if (eltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_SGP || eltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_SGP4 || 
                eltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_PPT3 ||
                eltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_XP || eltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_SP)
            {
               Tle tle = new Tle(satKey);
               StoreSatData(eltType, tle);
               this.OrbEltLines = tle.Line1 + "\r\n" + tle.Line2;
            }
            else if (eltType == DllMainWrapper.EltTypeEnum.ELTTYPE_SPVEC_B1P)
            {
               SpVec spVec = new SpVec(satKey);
               StoreSatData(eltType, spVec);
               this.OrbEltLines = spVec.Line1 + "\r\n" + spVec.Line2;
            }
            else if (eltType == DllMainWrapper.EltTypeEnum.ELTTYPE_VCM)
            {
               Vcm vcm = new Vcm(satKey);
               StoreSatData(eltType, vcm);
               this.OrbEltLines = vcm.VcmLines.Replace("<", "\r\n<");
            }
            else if (eltType == DllMainWrapper.EltTypeEnum.ELTTYPE_EXTEPH)
            {
               double AE, Ke;
               int coordSys;
               int numOfPts;
               double[] posDate = new double[3];
               double[] velDate = new double[3];
               byte[] fileLoc = new byte[DllMainWrapper.FILEPATHLEN];
               byte[] recNameB = new byte[128];

               int errCode = ExtEphemWrapper.ExtEphGetAllFields(satKey, out satNum, satName, recNameB, out epochDs50UTC, out AE, out Ke, pos, vel,
                                                            out coordSys, out numOfPts, fileLoc);
               if (errCode != 0)
                  throw new KeyNotFoundException("ExtEphemWrapper.ExtEphGetAllFields", satKey);

               // Input coordinate systems is in J2000
               if (coordSys == ExtEphemWrapper.COORD_J2K)
               {
                  // Convert J2000 to Date
                  //AstroFuncWrapper.RotJ2KToDate(1, 4, TimeFuncWrapper.UTCToTAI(epochDs50UTC), pos, vel, posDate, velDate);
                  // DHN 16Jun15 - start using full nutation terms of 106
                  AstroFuncWrapper.RotJ2KToDate(1, 106, TimeFuncWrapper.UTCToTAI(epochDs50UTC), pos, vel, posDate, velDate);
               }
               // Input coordinate systems is either ECR or EFG
               else if (coordSys == ExtEphemWrapper.COORD_ECR || coordSys == ExtEphemWrapper.COORD_EFG)
               {
                  double[] posEFG = new double[3];
                  double[] velEFG = new double[3];

                  // Convert ECR to EFG
                  if (coordSys == ExtEphemWrapper.COORD_ECR)
                  {
                     AstroFuncWrapper.ECRToEFGTime(epochDs50UTC, pos, vel, posEFG, velEFG);
                  }
                  else
                  {
                     posEFG = pos;
                     velEFG = vel;
                  }

                  // Convert EFG to ECI
                  AstroFuncWrapper.EFGToECITime(epochDs50UTC, posEFG, velEFG, posDate, velDate);

               }
               else if (coordSys == ExtEphemWrapper.COORD_WPTRL || coordSys == ExtEphemWrapper.COORD_HCSRL ||
                        coordSys == ExtEphemWrapper.COORD_WPTGC || coordSys == ExtEphemWrapper.COORD_HCSGC)
               {
                  double[] posEFG = new double[3];
                  double[] velEFG = new double[3];
                  AstroFuncWrapper.LLHToEFGPos(pos, posEFG);
                  double THDOT = 5.883354205754931E-2;
                  double KS2EK = 0.126496284857181;

                  velEFG[0] = -THDOT * (posEFG[1] / 6378.135 / KS2EK);
                  velEFG[1] = THDOT * (posEFG[0] / 6378.135 / KS2EK);
                  velEFG[2] = 0;

                  // Convert EFG to ECI
                  AstroFuncWrapper.EFGToECITime(epochDs50UTC, posEFG, velEFG, posDate, velDate);
               }
               // Input coordinate systems is already in Date
               else
               {
                  posDate = pos;
                  velDate = vel;
               }

               StoreSatData(satNum, Utility.BytesToString(satName), ' ', 0, 0, epochDs50UTC, eltType, 0, 0, 0, 0, 0.0, posDate, velDate);

               double startTimeDs50UTC, endTimeDs50UTC;

               errCode = ExtEphemWrapper.ExtEphStartEndTime(satKey, out startTimeDs50UTC, out endTimeDs50UTC);
               if (errCode != 0)
                  throw new KeyNotFoundException("ExtEphemWrapper.ExtEphStartEndTime", satKey);

               this.OrbEltLines = "Input Coordinate System: " + ((ExtEphemWrapper.InputCoordSystems)coordSys).ToDescription() +
                                  "\r\n" + "Number of ephemeris points: " + numOfPts +
                                  "\r\n" + "Ephemeris time span: " + TimeFuncWrapper.UTCToDtg20Str(startTimeDs50UTC) + " to " + TimeFuncWrapper.UTCToDtg20Str(endTimeDs50UTC) +
                                  "\r\n" + "File Path: " + Utility.BytesToString(fileLoc);

            }
         }
         //catch (KeyNotFoundException ex)
         //{
         //   throw ex;
         //}
      }


      /// <summary>
      /// Create satellite object from an input tle
      /// </summary>
      /// <param name="eltType"></param>
      /// <param name="tle"></param>
      private void StoreSatData(DllMainWrapper.EltTypeEnum eltType, Tle tle)
      {
         this.SatNum = tle.SatNum;
         this.SatName = tle.SatName;
         this.SecClass = tle.SecClass;
         this.RevNum = tle.RevNum;
         this.ElsetNum = tle.ElsetNum;
         this.EpochDs50UTC = tle.EpochTimeDs50UTC;

         this.EltType = eltType;


         if (tle.TleType == TleWrapper.TleTypeEnum.TLETYPE_XP)
            this.Agom = tle.GP_Agom;
         else if (tle.TleType == TleWrapper.TleTypeEnum.TLETYPE_SP)
            this.Agom = tle.SP_Agom;

         this.BTerm = tle.BTerm;
         this.BStar = tle.BStar;
         this.OgParm = tle.OgParm;
         
         this.CmOffset = 0;

         double[] classEltsArr = new double[6];
         classEltsArr[AstroFuncWrapper.XA_CLS_INCLI] = tle.Inclination_deg;
         classEltsArr[AstroFuncWrapper.XA_CLS_NODE] = tle.Node_deg;
         classEltsArr[AstroFuncWrapper.XA_CLS_E] = tle.Eccentricity;
         classEltsArr[AstroFuncWrapper.XA_CLS_OMEGA] = tle.Omega_deg;
         classEltsArr[AstroFuncWrapper.XA_CLS_MA] = tle.MeanAnomaly_deg;
         classEltsArr[AstroFuncWrapper.XA_CLS_N] = tle.MeanMotion_revpday;
         this.ClassElts = new SatelliteModel.ClassElts(classEltsArr);
         UpdatePosVel(classEltsArr);
      }

      /// <summary>
      /// Create satellite object from an input spvec
      /// </summary>
      /// <param name="spvec"></param>
      private void StoreSatData(DllMainWrapper.EltTypeEnum eltType, SpVec spvec)
      {
         this.SatNum = spvec.SatNum;
         this.SatName = spvec.SatName;
         this.SecClass = spvec.SecClass;
         this.RevNum = spvec.RevNum;
         this.ElsetNum = spvec.ElsetNum;
         this.EpochDs50UTC = spvec.EpochTimeDs50UTC.Ds50UTC;
         this.EltType = eltType;
         this.BTerm = spvec.BTerm;
         this.BStar = 0;
         this.OgParm = spvec.OgParm;
         this.Agom = spvec.Agom;
         this.CmOffset = 0;
         this.posDate = spvec.PosDate;
         this.velDate = spvec.VelDate;
         UpdateClassElts();
      }

      /// <summary>
      /// Create satellite object from an input spvec
      /// </summary>
      /// <param name="spvec"></param>
      private void StoreSatData(DllMainWrapper.EltTypeEnum eltType, Vcm vcm)
      {
         this.SatNum = vcm.SatNum;
         this.SatName = vcm.SatName;
         this.SecClass = ' '; // vcm doesn't have SecClass field
         this.RevNum = vcm.RevNum;
         this.EpochDs50UTC = vcm.EpochTimeDs50UTC.Ds50UTC;
         this.EltType = eltType;
         this.BTerm = vcm.BTerm;
         this.BStar = 0;
         this.OgParm = vcm.OgParm;
         this.Agom = vcm.Agom;
         this.CmOffset = 0;
         this.posDate = vcm.ECISatPos.ToArray();
         this.velDate = vcm.ECISatVel.ToArray();
         UpdateClassElts();
      }


      /// <summary>
      /// Create satellite object from the input satellite parameters
      /// </summary>
      /// <param name="satNum"></param>
      /// <param name="satName"></param>
      /// <param name="revNum"></param>
      /// <param name="elsetNum"></param>
      /// <param name="epochDs50UTC"></param>
      /// <param name="eltType"></param>
      /// <param name="bTerm"></param>
      /// <param name="bStar"></param>
      /// <param name="ogParm"></param>
      /// <param name="agom"></param>
      /// <param name="cmOffset"></param>
      /// <param name="posDate"></param>
      /// <param name="velDate"></param>
      private void StoreSatData(int satNum, string satName, char secClass, int revNum, int elsetNum, double epochDs50UTC,
                       DllMainWrapper.EltTypeEnum eltType, double bTerm, double bStar, double ogParm, double agom,
                       double cmOffset, double[] posDate, double[] velDate)
      {
         this.SatNum = satNum;
         this.SatName = satName;
         this.SecClass = secClass;
         //this.RecName = recName;
         this.RevNum = revNum;
         this.ElsetNum = elsetNum;
         this.EpochDs50UTC = epochDs50UTC;
         this.EltType = (DllMainWrapper.EltTypeEnum)eltType;
         this.BTerm = bTerm;
         this.BStar = bStar;
         this.OgParm = ogParm;
         this.Agom = agom;
         this.CmOffset = cmOffset;
         this.posDate = posDate;

         if (eltType == DllMainWrapper.EltTypeEnum.ELTTYPE_SPVEC_B1P) // vel in m/sec, need to convert them to km/sec
         {
            this.velDate[0] = velDate[0] / 1000.0;
            this.velDate[1] = velDate[1] / 1000.0;
            this.velDate[2] = velDate[2] / 1000.0;
         }
         else
            this.velDate = velDate;
         UpdateClassElts();
      }


      // Constructor using classical elements
      private void StoreSatData(int satNum, string satName, char secClass, int revNum, int elsetNum, double epochDs50UTC,
                       int eltType, double bTerm, double bStar, double ogParm, double agom,
                       double cmOffset, double[] classEltsArr)
      {
         this.SatNum = satNum;
         this.SatName = satName;
         this.SecClass = secClass;
         //this.RecName = recName;
         this.RevNum = revNum;
         this.ElsetNum = elsetNum;
         this.EpochDs50UTC = epochDs50UTC;
         this.EltType = (DllMainWrapper.EltTypeEnum)eltType;
         this.BTerm = bTerm;
         this.BStar = bStar;
         this.OgParm = ogParm;
         this.Agom = agom;
         this.CmOffset = cmOffset;
         this.ClassElts = new SatelliteModel.ClassElts(classEltsArr);
         UpdatePosVel(classEltsArr);
      }


      [Browsable(false)]
      public double[] PosDate
      {
         get
         {
            return this.posDate;
         }
         set
         {
            this.posDate = value;
         }
      }

      [Browsable(false)]
      public double[] VelDate
      {
         get
         {
            return this.velDate;
         }
         set
         {
            this.velDate = value;
         }
      }

      [Browsable(false)]
      public double[] PosJ2K
      {
         get
         {
            double[] posJ2K = new double[3];
            double[] velJ2K = new double[3];

            AstroFuncWrapper.RotDateToJ2K(1, 106, EpochDs50UTC.TAI, this.posDate, this.velDate, posJ2K, velJ2K);

            return posJ2K;
         }
      }

      [Browsable(false)]
      public double[] VelJ2K
      {
         get
         {
            double[] posJ2K = new double[3];
            double[] velJ2K = new double[3];

            AstroFuncWrapper.RotDateToJ2K(1, 106, EpochDs50UTC.TAI, this.posDate, this.velDate, posJ2K, velJ2K);
            return velJ2K;
         }
      }

      [Browsable(false)]
      public double[] LLH
      {
         get
         {
            double[] llh = new double[3];
            AstroFuncWrapper.XYZToLLHTime(EpochDs50UTC, this.posDate, llh);

            return llh;
         }

         set
         {
            double[] llh = new double[3];
            llh[0] = value[0];
            llh[1] = value[1];
            llh[2] = value[2];
            AstroFuncWrapper.LLHToXYZTime(EpochDs50UTC, llh, this.posDate);
         }
      }

      [Browsable(false)]
      public double[] PAP
      {
         get
         {
            double[] pap = new double[3];

            double A = this.ClassElts.A_km;

            pap[0] = 1440.0 / this.ClassElts.MeanMotion_rpd;
            pap[1] = A * (1 + this.ClassElts.Eccentricity);
            pap[2] = A * (1 - this.ClassElts.Eccentricity);

            return pap;

         }
      }

      public double PosX
      {
         get
         {
            return posDate[0];
         }
      }
      public double PosY
      {
         get
         {
            return posDate[1];
         }
      }
      public double PosZ
      {
         get
         {
            return posDate[2];
         }
      }
      public double Inclination_deg
      {
         get
         {
            return this.ClassElts.Inclination_deg;
         }
      }
      public double RAAN_deg
      {
         get
         {
            return this.ClassElts.RAAN_deg;
         }
      }

      public double Eccentricity
      {
         get
         {
            return this.ClassElts.Eccentricity;
         }
      }

      public double ArgOfPer_deg
      {
         get
         {
            return this.ClassElts.ArgOfPer_deg;
         }
      }

      public double MeanAnom_deg
      {
         get
         {
            return this.ClassElts.MeanAnom_deg;
         }
      }

      public double MeanMotion_rpd
      {
         get
         {
            return this.ClassElts.MeanMotion_rpd;
         }
      }

      public double Period_min
      {
         get
         {
            return PAP[0];
         }
      }

      public double Apogee_km
      {
         get
         {
            return PAP[1];
         }
      }

      public double Perigee_km
      {
         get
         {
            return PAP[2];
         }
      }
      public double Latitude_deg
      {
         get
         {
            return LLH[0];
         }
      }
      public double Longitude_deg
      {
         get
         {
            return LLH[1];
         }
      }
      public double Height_km
      {
         get
         {
            return LLH[2];
         }
      }

      public double A_km
      {
         get
         {
            return this.ClassElts.A_km;
         }
      }

      [Browsable(false)]
      public double[] PosVelECI
      {
         get
         {
            double[] posVelECI = new double[6];
            posVelECI[0] = this.posDate[0];
            posVelECI[1] = this.posDate[1];
            posVelECI[2] = this.posDate[2];
            posVelECI[3] = this.velDate[0];
            posVelECI[4] = this.velDate[1];
            posVelECI[5] = this.velDate[2];

            return posVelECI;
         }

         set
         {
            this.posDate[0] = value[0];
            this.posDate[1] = value[1];
            this.posDate[2] = value[2];
            this.velDate[0] = value[3];
            this.velDate[1] = value[4];
            this.velDate[2] = value[5];
            UpdateClassElts();
         }
      }

      [Browsable(false)]
      public double[] PosVelJ2K
      {
         get
         {
            double[] posJ2K = new double[3];
            double[] velJ2K = new double[3];

            AstroFuncWrapper.RotDateToJ2K(1, 106, EpochDs50UTC.TAI, this.posDate, this.velDate, posJ2K, velJ2K);
            double[] posVelJ2K = new double[6];
            posVelJ2K[0] = posJ2K[0];
            posVelJ2K[1] = posJ2K[1];
            posVelJ2K[2] = posJ2K[2];
            posVelJ2K[3] = velJ2K[0];
            posVelJ2K[4] = velJ2K[1];
            posVelJ2K[5] = velJ2K[2];

            return posVelJ2K;
         }

         set
         {
            double[] posJ2K = new double[3];
            double[] velJ2K = new double[3];
            posJ2K[0] = value[0];
            posJ2K[1] = value[1];
            posJ2K[2] = value[2];
            velJ2K[0] = value[3];
            velJ2K[1] = value[4];
            velJ2K[2] = value[5];

            AstroFuncWrapper.RotJ2KToDate(1, 106, EpochDs50UTC.TAI, posJ2K, velJ2K, this.posDate, this.velDate);
            UpdateClassElts();
         }
      }


      [Browsable(false)]
      public double[] EqnxElts
      {
         get
         {
            double[] eqnx = new double[6];

            AstroFuncWrapper.PosVelToEqnx(this.posDate, this.velDate, eqnx);
            return eqnx;
         }
         set
         {
            AstroFuncWrapper.EqnxToPosVel(value, this.posDate, this.velDate);
            UpdateClassElts();
         }
      }

      [Browsable(false)]
      public double[] EFG
      {
         get
         {
            double[] posEFG = new double[3];
            double[] velEFG = new double[3];
            double[] efg = new double[6];

            AstroFuncWrapper.ECIToEFGTime(EpochDs50UTC, this.posDate, this.velDate, posEFG, velEFG);
            efg[0] = posEFG[0];
            efg[1] = posEFG[1];
            efg[2] = posEFG[2];
            efg[3] = velEFG[0];
            efg[4] = velEFG[1];
            efg[5] = velEFG[2];

            return efg;
         }

         set
         {
            double[] posEFG = new double[3];
            double[] velEFG = new double[3];
            double[] efg = new double[6];

            posEFG[0] = value[0];
            posEFG[1] = value[1];
            posEFG[2] = value[2];
            velEFG[0] = value[3];
            velEFG[1] = value[4];
            velEFG[2] = value[5];

            AstroFuncWrapper.EFGToECITime(EpochDs50UTC, posEFG, velEFG, this.posDate, this.velDate);
            UpdateClassElts();
         }
      }

      /// <summary>
      /// Using data stored in the satellite object to add new satellite to the dll
      /// </summary>
      /// <returns></returns>
      public long AddToDll()
      {
         long satKey = 0;
         int tleType = ToTleType();

         if (this.EltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_SGP  || this.EltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_SGP4 ||
             this.EltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_PPT3 || this.EltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_XP)
         {
            satKey = TleWrapper.TleAddSatFrFieldsGP(this.SatNum, this.SecClass, this.SatName, this.EpochDs50UTC.Year, this.EpochDs50UTC.DayOfYearReal, this.BStar, tleType, this.ElsetNum,
               ClassElts.Inclination_deg, ClassElts.RAAN_deg, ClassElts.Eccentricity, ClassElts.ArgOfPer_deg, ClassElts.MeanAnom_deg, ClassElts.MeanMotion_rpd, RevNum);
         }

         return satKey;
      }

      /// <summary>
      /// Remove the satellite data from the appropriate dll
      /// </summary>
      /// <returns>true: remove successfully; false: satellite doesn't exist or remove unsuccessfully</returns>
      public bool DeleteFrDll()
      {
         int errCode = SatStateWrapper.SatStateRemoveSat(this.SatKey);
         if (errCode == 0)
            return true;
         else
            return false;
      }

      /// <summary>
      /// Update the satellite data in the dll with the updated data from the satellite object
      /// </summary>
      /// <returns>true: update successfully; false: satellite doesn't exist or update was unsuccessfully</returns>
      public bool UpdateInDll()
      {
         if (this.EltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_SGP  || this.EltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_SGP4 ||
             this.EltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_PPT3 || this.EltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_XP)
         {
            
            ErrCode = TleWrapper.TleUpdateSatFrFieldsGP(SatKey, SecClass, SatName, BStar, ElsetNum,
               ClassElts.Inclination_deg, ClassElts.RAAN_deg, ClassElts.Eccentricity, ClassElts.ArgOfPer_deg, ClassElts.MeanAnom_deg, ClassElts.MeanMotion_rpd, RevNum);

            if (ErrCode != 0)
            {
               ErrorMessages.SaveErrorMessage();
               return false;
            }

         }

         return true;

      }

      public int ToTleType()
      {
         int tleType = 0;

         if (this.EltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_SGP)
            tleType = 0;
         else if (this.EltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_SGP4)
            tleType = 2;
         else if (this.EltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_PPT3)
            tleType = 3;
         else if (this.EltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_XP)
            tleType = 4;
         else if (this.EltType == DllMainWrapper.EltTypeEnum.ELTTYPE_TLE_SP)
            tleType = 6;

         return tleType;
      }
   }
}

