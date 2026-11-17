
using AFSPC.AstroStds.Wrappers;
using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Models.SatelliteModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Drawing;
using System;
using System.Text;

namespace AFSPC.AstroStds.Models.SensorModel
{
   [Serializable]
   public class Sensor
   {
      public int ErrCode { get; set; }
      public long SenKey { get; set; }
      public int SenNum { get; set; }
      public double AstroLatN_deg { get; set; } // (+N) (-S)
      public double AstroLonE_deg { get; set; } // (+E) (-W)
      public double[] SenPosECR_m = new double[3];
      public double[] SenPosEFG_m = new double[3];
      public SatelliteLLH SenPosLLH { get; set; }
      public char SecClass { get; set; }

      public double[] SenPosECR_km
      {
         get
         {
            return new double[] { SenPosECR_m[0] / 1000.0, SenPosECR_m[1] / 1000.0, SenPosECR_m[2] / 1000 };
         }

         set
         {
            double[] ecr_km = new double[3];

            ecr_km = value;

            SenPosECR_m[0] = ecr_km[0] * 1000.0;
            SenPosECR_m[1] = ecr_km[1] * 1000.0;
            SenPosECR_m[2] = ecr_km[2] * 1000.0;
         }
      }



      public double[] SenLLH
      {
         // convert EFG to LLH
         get
         {
            double[] llh = new double[3];
            AstroFuncWrapper.EFGPosToLLH(SenPosECR_km, llh);
            return llh;
         }

         // convert LLH to EFG
         set
         {
            double[] llh = new double[3];
            double[] ecr_km = new double[3];

            llh = value;

            AstroFuncWrapper.LLHToEFGPos(llh, ecr_km);
            SenPosECR_m[0] = ecr_km[0] * 1000.0;
            SenPosECR_m[1] = ecr_km[1] * 1000.0;
            SenPosECR_m[2] = ecr_km[2] * 1000.0;
         }
      }



      public string SenDesc { get; set; }
      public int OrbSatNum { get; set; }
      public char ViewType { get; set; }
      public char ObsType { get; set; }
      public int SenLocType { get; set; }
      public int RngUnits { get; set; }
      public double MaxRngLim { get; set; }
      public double MinRngLim { get; set; }
      public char Boresight1 { get; set; }
      public char Boresight2 { get; set; }
      public double ElLim1 { get; set; }
      public double ElLim2 { get; set; }
      public double ElLim3 { get; set; }
      public double ElLim4 { get; set; }
      public double AzLim1 { get; set; }
      public double AzLim2 { get; set; }
      public double AzLim3 { get; set; }
      public double AzLim4 { get; set; }
      public double Interval { get; set; }
      public int VisFlg { get; set; }
      public int RngLimFlg { get; set; }
      public int MaxPPP { get; set; }
      public int PlntryRes { get; set; }
      public double RrLim { get; set; }
      public int EarthBckgrnd { get; set; }
      public double EarthLimb { get; set; }
      public double SolarXAngle { get; set; }
      public double LunarXAngle { get; set; }
      public double MinIlum { get; set; }
      public double Twilit { get; set; }
      public string SenLocLimLines { get; set; }
      public int IsSpecialSensor { get; set; }
      public SensorSigmas SenSigmas { get; set; }
      public SensorBiases SenBiases { get; set; }


      // Constructor
      public Sensor()
      {

      }


      public Sensor(long senKey)
      {
         double[] xa_sen = new double[128];
         byte[] xs_sen = new byte[512];

         // Retrieve sensor location data from the dll
         this.ErrCode = SensorWrapper.SensorDataToArray(senKey, xa_sen, xs_sen);

         this.SenKey = senKey;

         if (this.ErrCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            return;
         }

         PopulateSensor(xa_sen, xs_sen);

      }

      private void PopulateSensor(double[] xa_sen, byte[] xs_sen)
      {
         string xs_sen_str;
         char viewType;
         char obsType;

         xs_sen_str = Utility.BytesToString(xs_sen);
         this.SenNum = (int)(xa_sen[SensorWrapper.XA_SEN_GEN_SENNUM]);
         this.SecClass = xs_sen_str[SensorWrapper.XS_SEN_SECCLASS_0_1];


         this.MinRngLim = xa_sen[SensorWrapper.XA_SEN_GEN_MINRNG];
         this.MaxRngLim = xa_sen[SensorWrapper.XA_SEN_GEN_MAXRNG];
         this.RrLim = xa_sen[SensorWrapper.XA_SEN_GEN_RRLIM];
         this.RngLimFlg = (int)xa_sen[SensorWrapper.XA_SEN_GEN_RNGLIMFLG];
         this.IsSpecialSensor = (int)xa_sen[SensorWrapper.XA_SEN_GEN_SMSEN];

         viewType = xs_sen_str[SensorWrapper.XS_SEN_VIEWTYPE_1_1];
         this.ViewType = viewType;

         if (viewType == '9')
         {
            this.OrbSatNum = (int)(xa_sen[SensorWrapper.XA_SEN_ORB_SATNUM]);
         }
         else   // non-orbiting sensor
         {
            this.SenLocType = (int)(xa_sen[SensorWrapper.XA_SEN_GRN_LOCTYPE]);
            this.AstroLatN_deg = xa_sen[SensorWrapper.XA_SEN_GRN_ASTROLAT];
            this.AstroLonE_deg = -xa_sen[SensorWrapper.XA_SEN_GRN_ASTROLON]; // sensor location in Dll uses (+W) (-E)
            if (SenLocType == SensorWrapper.SENLOC_TYPE_ECR)
               SenPosECR_m = new double[] { xa_sen[SensorWrapper.XA_SEN_GRN_POS1] * 1000, xa_sen[SensorWrapper.XA_SEN_GRN_POS2] * 1000, xa_sen[SensorWrapper.XA_SEN_GRN_POS3] * 1000 };
            else if (SenLocType == SensorWrapper.SENLOC_TYPE_EFG)
               SenPosEFG_m = new double[] { xa_sen[SensorWrapper.XA_SEN_GRN_POS1] * 1000, xa_sen[SensorWrapper.XA_SEN_GRN_POS2] * 1000, xa_sen[SensorWrapper.XA_SEN_GRN_POS3] * 1000 };
            else if ((SenLocType == SensorWrapper.SENLOC_TYPE_LLH))
               SenPosLLH = new SatelliteLLH(new double[] { xa_sen[SensorWrapper.XA_SEN_GRN_POS1], xa_sen[SensorWrapper.XA_SEN_GRN_POS3], xa_sen[SensorWrapper.XA_SEN_GRN_POS3] });
         }


         this.SenDesc = xs_sen_str.Substring(SensorWrapper.XS_SEN_DSCRPTN_3_24, 24).Trim();


         obsType = xs_sen_str[SensorWrapper.XS_SEN_OBSTYPE_2_1];
         this.ObsType = obsType;

         switch (viewType)
         {
            case '2': //VT_BCT:
               this.AzLim1 = xa_sen[SensorWrapper.XA_SEN_BCT_BRSGHTAZ];
               this.ElLim1 = xa_sen[SensorWrapper.XA_SEN_BCT_BRSGHTEL];
               this.AzLim2 = xa_sen[SensorWrapper.XA_SEN_BCT_HALFANG];
               this.ElLim2 = xa_sen[SensorWrapper.XA_SEN_BCT_MINEL];
               break;
            case '3': //VT_CON:
               this.AzLim1 = xa_sen[SensorWrapper.XA_SEN_CON_AZFR1];
               this.AzLim2 = xa_sen[SensorWrapper.XA_SEN_CON_AZTO1];
               this.ElLim1 = xa_sen[SensorWrapper.XA_SEN_CON_ELFR1];
               this.ElLim2 = xa_sen[SensorWrapper.XA_SEN_CON_ELTO1];
               this.AzLim3 = xa_sen[SensorWrapper.XA_SEN_CON_AZFR2];
               this.AzLim4 = xa_sen[SensorWrapper.XA_SEN_CON_AZTO2];
               this.ElLim3 = xa_sen[SensorWrapper.XA_SEN_CON_ELFR2];
               this.ElLim4 = xa_sen[SensorWrapper.XA_SEN_CON_ELTO2];
               break;
            case '4': //VT_OPT:
               this.AzLim1 = xa_sen[SensorWrapper.XA_SEN_OPT_AZFR1];
               this.AzLim2 = xa_sen[SensorWrapper.XA_SEN_OPT_AZTO1];
               this.ElLim1 = xa_sen[SensorWrapper.XA_SEN_OPT_ELFR1];
               this.ElLim2 = xa_sen[SensorWrapper.XA_SEN_OPT_ELTO1];
               this.AzLim3 = xa_sen[SensorWrapper.XA_SEN_OPT_AZFR2];
               this.AzLim4 = xa_sen[SensorWrapper.XA_SEN_OPT_AZTO2];
               this.ElLim3 = xa_sen[SensorWrapper.XA_SEN_OPT_ELFR2];
               this.ElLim4 = xa_sen[SensorWrapper.XA_SEN_OPT_ELTO2];
               this.SolarXAngle = xa_sen[SensorWrapper.XA_SEN_OPT_SEA];
               this.Twilit = xa_sen[SensorWrapper.XA_SEN_OPT_TWILGHT];
               this.VisFlg = (int)xa_sen[SensorWrapper.XA_SEN_OPT_VISCHK];
               break;
            case '7': //VT_FAN:
               this.AzLim1 = xa_sen[SensorWrapper.XA_SEN_FAN_AZ];          // fan's constant azimuth (deg) - fan sweeps a vertical plane  [0, 180)
               this.ElLim2 = xa_sen[SensorWrapper.XA_SEN_FAN_TILTANGLE];   // fan's tilt angle (deg)  (-: North/West tilt, +: South/East tilt) [-90, 90]
               this.AzLim2 = xa_sen[SensorWrapper.XA_SEN_FAN_OBSANGLE];    // fan's off-boresight angle (deg) [0, 90]
               break;
            case '9': //VT_ORB:
               this.OrbSatNum = (int)xa_sen[SensorWrapper.XA_SEN_ORB_SATNUM];
               this.Boresight1 = xs_sen_str[SensorWrapper.XS_SEN_ORB_BSVEC1_27_1];
               this.AzLim1 = xa_sen[SensorWrapper.XA_SEN_ORB_AZMIN1];
               this.AzLim2 = xa_sen[SensorWrapper.XA_SEN_ORB_AZMAX1];
               this.ElLim1 = xa_sen[SensorWrapper.XA_SEN_ORB_ELMIN1];
               this.ElLim2 = xa_sen[SensorWrapper.XA_SEN_ORB_ELMAX1];
               this.PlntryRes = (int)xa_sen[SensorWrapper.XA_SEN_ORB_PLNTRYRES];

               this.Boresight2 = xs_sen_str[SensorWrapper.XS_SEN_ORB_BSVEC2_28_1];
               this.AzLim3 = xa_sen[SensorWrapper.XA_SEN_ORB_AZMIN2];
               this.AzLim4 = xa_sen[SensorWrapper.XA_SEN_ORB_AZMAX2];
               this.ElLim3 = xa_sen[SensorWrapper.XA_SEN_ORB_ELMIN2];
               this.ElLim4 = xa_sen[SensorWrapper.XA_SEN_ORB_ELMAX2];

               this.EarthLimb = xa_sen[SensorWrapper.XA_SEN_ORB_EARTHLIMB];
               this.SolarXAngle = xa_sen[SensorWrapper.XA_SEN_ORB_SEA];
               this.LunarXAngle = xa_sen[SensorWrapper.XA_SEN_ORB_LEA];
               this.MinIlum = xa_sen[SensorWrapper.XA_SEN_ORB_MINILLUM];
               this.EarthBckgrnd = (int)xa_sen[SensorWrapper.XA_SEN_ORB_EARTHBRND];

               break;
            case 'R': //VT_FOR:
               //encoding.GetBytes(azElTableFile, 0, azElTableFile.Length, xs_sen, SensorWrapper.XS_SEN_FOR_AZFILE_255_256);
               break;
            case 'V': //VT_FOV:
               this.ElLim1 = xa_sen[SensorWrapper.XA_SEN_FOV_BEAMWIDTH];    //use 1L - Card's elevation limit #1 columns 17-20
               this.ElLim2 = xa_sen[SensorWrapper.XA_SEN_FOV_TILTANGLE];    //use 1L - Card's elevation limit #2 columns 22-25
               break;
         }



         this.RngUnits = (int)xa_sen[SensorWrapper.XA_SEN_GEN_UNIT];
         this.MaxPPP = (int)xa_sen[SensorWrapper.XA_SEN_GEN_PTSPERPAS];
         this.Interval = xa_sen[SensorWrapper.XA_SEN_GEN_INTERVAL];

         this.SenSigmas = new SensorSigmas(xa_sen);
         this.SenBiases = new SensorBiases(xa_sen);

         byte[] sCard = new byte[DllMainWrapper.GETSETSTRLEN];
         byte[] l1Card = new byte[DllMainWrapper.GETSETSTRLEN];
         byte[] l2Card = new byte[DllMainWrapper.GETSETSTRLEN];
         byte[] bsCard = new byte[DllMainWrapper.GETSETSTRLEN];

         SensorWrapper.SensorGetLines(this.SenKey, sCard, l1Card, l2Card);
         SensorWrapper.SensorGetBSLine(this.SenKey, bsCard);
         SenLocLimLines = Utility.BytesToString(sCard).Substring(0, 80) + "\r\n" +
                               Utility.BytesToString(l1Card).Substring(0, 80) + "\r\n" +
                               Utility.BytesToString(l2Card).Substring(0, 80) + "\r\n" +
                               Utility.BytesToString(bsCard).Substring(0, 80);
      }

      public void UpdateSenDataToDll()
      {
         //SensorWrapper.SensorRemove(SenNum);

         this.ErrCode = SensorWrapper.SensorSetLocAll(SenNum, AstroLatN_deg, -AstroLonE_deg, SenPosECR_m, SenDesc, OrbSatNum, ' ');
         if (this.ErrCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            SensorWrapper.SensorRemove(SenNum); // remove unfinished add/update sensor
            return;
         }

         this.ErrCode = SensorWrapper.SensorSet1L(SenNum, ViewType, ObsType, RngUnits, MaxRngLim, Boresight1, ElLim1, ElLim2, AzLim1, AzLim2,
            Interval, VisFlg, RngLimFlg, MaxPPP, MinRngLim, PlntryRes, RrLim);
         if (this.ErrCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            SensorWrapper.SensorRemove(SenNum); // remove unfinished add/update sensor
            return;
         }

         this.ErrCode = SensorWrapper.SensorSet2L(SenNum, Boresight2, ElLim3, ElLim4, AzLim3, AzLim4, EarthBckgrnd,
                                             EarthLimb, SolarXAngle, LunarXAngle, MinIlum, Twilit);
         if (this.ErrCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            SensorWrapper.SensorRemove(SenNum); // remove unfinished add/update sensor
            return;
         }

         SensorWrapper.SensorSetLimField(SenNum, SensorWrapper.XF_SENLIM_SMSEN, IsSpecialSensor == 1 ? "1" : "0");

         double[] xaf_senbs = new double[16];

         if (this.SenSigmas != null)
         {
            xaf_senbs[SensorWrapper.XAF_SENBS_AZSIGMA] = this.SenSigmas.AzRaSigma_deg;
            xaf_senbs[SensorWrapper.XAF_SENBS_ELSIGMA] = this.SenSigmas.ElDecSigma_deg;
            xaf_senbs[SensorWrapper.XAF_SENBS_RGSIGMA] = this.SenSigmas.RngSigma_km;
            xaf_senbs[SensorWrapper.XAF_SENBS_RRSIGMA] = this.SenSigmas.RngRateSigma_kmps;
         }

         if (this.SenBiases != null)
         {
            xaf_senbs[SensorWrapper.XAF_SENBS_AZBIAS] = this.SenBiases.AzRaBias_deg;
            xaf_senbs[SensorWrapper.XAF_SENBS_ELBIAS] = this.SenBiases.ElDecBias_deg;
            xaf_senbs[SensorWrapper.XAF_SENBS_RGBIAS] = this.SenBiases.RngBias_km;
            xaf_senbs[SensorWrapper.XAF_SENBS_RRBIAS] = this.SenBiases.RngRateBias_kmps;
            xaf_senbs[SensorWrapper.XAF_SENBS_TIMEBIAS] = this.SenBiases.TimeBias_sec;
         }

         this.ErrCode = SensorWrapper.SensorSetBS(SenNum, xaf_senbs);
         if (this.ErrCode != 0)
         {
            ErrorMessages.SaveErrorMessage();
            SensorWrapper.SensorRemove(SenNum); // remove unfinished add/update sensor
            return;
         }
      }

      public void ToArray(double[] xa_sen, byte[] xs_sen)
      {
         System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

         // clear array content
         Array.Clear(xa_sen, 0, xa_sen.Length);
         for(int i = 0; i < xs_sen.Length; i++)
         {
            xs_sen[i] = 0x20;
         }
         

         xa_sen[SensorWrapper.XA_SEN_GEN_SENNUM] = this.SenNum;
         xs_sen[SensorWrapper.XS_SEN_SECCLASS_0_1] = (byte)this.SecClass;


         xa_sen[SensorWrapper.XA_SEN_GEN_MINRNG] = this.MinRngLim;
         xa_sen[SensorWrapper.XA_SEN_GEN_MAXRNG] = this.MaxRngLim;
         xa_sen[SensorWrapper.XA_SEN_GEN_RRLIM] = this.RrLim;
         xa_sen[SensorWrapper.XA_SEN_GEN_RNGLIMFLG] = this.RngLimFlg;
         xa_sen[SensorWrapper.XA_SEN_GEN_SMSEN] = this.IsSpecialSensor;

         if (this.ViewType == '9')
         {
            xa_sen[SensorWrapper.XA_SEN_ORB_SATNUM] = this.OrbSatNum;
         }
         else   // non-orbiting sensor
         {
            xa_sen[SensorWrapper.XA_SEN_GRN_LOCTYPE] = this.SenLocType;
            xa_sen[SensorWrapper.XA_SEN_GRN_ASTROLAT] = this.AstroLatN_deg;
            xa_sen[SensorWrapper.XA_SEN_GRN_ASTROLON] = -this.AstroLonE_deg;

            if (this.SenLocType == SensorWrapper.SENLOC_TYPE_ECR)
            {
               xa_sen[SensorWrapper.XA_SEN_GRN_POS1] = this.SenPosECR_km[0];
               xa_sen[SensorWrapper.XA_SEN_GRN_POS2] = this.SenPosECR_km[1];
               xa_sen[SensorWrapper.XA_SEN_GRN_POS3] = this.SenPosECR_km[2];
            }
            else if (this.SenLocType == SensorWrapper.SENLOC_TYPE_EFG)
            {
               xa_sen[SensorWrapper.XA_SEN_GRN_POS1] = this.SenPosEFG_m[0] / 1000;
               xa_sen[SensorWrapper.XA_SEN_GRN_POS2] = this.SenPosEFG_m[1] / 1000;
               xa_sen[SensorWrapper.XA_SEN_GRN_POS3] = this.SenPosEFG_m[2] / 1000;

            }
            else if (this.SenLocType == SensorWrapper.SENLOC_TYPE_LLH)
            {
               xa_sen[SensorWrapper.XA_SEN_GRN_POS1] = this.SenPosLLH.Latitude_deg;
               xa_sen[SensorWrapper.XA_SEN_GRN_POS2] = this.SenPosLLH.Longitude_deg;
               xa_sen[SensorWrapper.XA_SEN_GRN_POS3] = this.SenPosLLH.Height_km;
            }
         }


         switch (this.ViewType)
         {
            case '2': //VT_BCT:
               xa_sen[SensorWrapper.XA_SEN_BCT_BRSGHTAZ] = this.AzLim1;
               xa_sen[SensorWrapper.XA_SEN_BCT_BRSGHTEL] = this.ElLim1;
               xa_sen[SensorWrapper.XA_SEN_BCT_HALFANG] = this.AzLim2;
               xa_sen[SensorWrapper.XA_SEN_BCT_MINEL] = this.ElLim2;
               break;
            case '3': //VT_CON:
               xa_sen[SensorWrapper.XA_SEN_CON_AZFR1] = this.AzLim1;
               xa_sen[SensorWrapper.XA_SEN_CON_AZTO1] = this.AzLim2;
               xa_sen[SensorWrapper.XA_SEN_CON_ELFR1] = this.ElLim1;
               xa_sen[SensorWrapper.XA_SEN_CON_ELTO1] = this.ElLim2;
               xa_sen[SensorWrapper.XA_SEN_CON_AZFR2] = this.AzLim3;
               xa_sen[SensorWrapper.XA_SEN_CON_AZTO2] = this.AzLim4;
               xa_sen[SensorWrapper.XA_SEN_CON_ELFR2] = this.ElLim3;
               xa_sen[SensorWrapper.XA_SEN_CON_ELTO2] = this.ElLim4;
               break;
            case '4': //VT_OPT:
               xa_sen[SensorWrapper.XA_SEN_OPT_AZFR1] = this.AzLim1;
               xa_sen[SensorWrapper.XA_SEN_OPT_AZTO1] = this.AzLim2;
               xa_sen[SensorWrapper.XA_SEN_OPT_ELFR1] = this.ElLim1;
               xa_sen[SensorWrapper.XA_SEN_OPT_ELTO1] = this.ElLim2;
               xa_sen[SensorWrapper.XA_SEN_OPT_AZFR2] = this.AzLim3;
               xa_sen[SensorWrapper.XA_SEN_OPT_AZTO2] = this.AzLim4;
               xa_sen[SensorWrapper.XA_SEN_OPT_ELFR2] = this.ElLim3;
               xa_sen[SensorWrapper.XA_SEN_OPT_ELTO2] = this.ElLim4;
               xa_sen[SensorWrapper.XA_SEN_OPT_SEA] = this.SolarXAngle;
               xa_sen[SensorWrapper.XA_SEN_OPT_TWILGHT] = this.Twilit;
               xa_sen[SensorWrapper.XA_SEN_OPT_VISCHK] = this.VisFlg;
               break;
            case '7': //VT_FAN:
               xa_sen[SensorWrapper.XA_SEN_FAN_AZ] = this.AzLim1;
               xa_sen[SensorWrapper.XA_SEN_FAN_TILTANGLE] = this.ElLim2;    //use 1L - Card's elevation limit #2 columns 22-25
               xa_sen[SensorWrapper.XA_SEN_FAN_OBSANGLE] = this.AzLim2;
               break;
            case '9': //VT_ORB:
               xa_sen[SensorWrapper.XA_SEN_ORB_SATNUM] = this.OrbSatNum;
               xs_sen[SensorWrapper.XS_SEN_ORB_BSVEC1_27_1] = (byte)this.Boresight1;
               xa_sen[SensorWrapper.XA_SEN_ORB_AZMIN1] = this.AzLim1;
               xa_sen[SensorWrapper.XA_SEN_ORB_AZMAX1] = this.AzLim2;
               xa_sen[SensorWrapper.XA_SEN_ORB_ELMIN1] = this.ElLim1;
               xa_sen[SensorWrapper.XA_SEN_ORB_ELMAX1] = this.ElLim2;
               this.PlntryRes = (int)xa_sen[SensorWrapper.XA_SEN_ORB_PLNTRYRES];

               xs_sen[SensorWrapper.XS_SEN_ORB_BSVEC2_28_1] = (byte)this.Boresight2;
               xa_sen[SensorWrapper.XA_SEN_ORB_AZMIN2] = this.AzLim3;
               xa_sen[SensorWrapper.XA_SEN_ORB_AZMAX2] = this.AzLim4;
               xa_sen[SensorWrapper.XA_SEN_ORB_ELMIN2] = this.ElLim3;
               xa_sen[SensorWrapper.XA_SEN_ORB_ELMAX2] = this.ElLim4;

               xa_sen[SensorWrapper.XA_SEN_ORB_EARTHLIMB] = this.EarthLimb;
               xa_sen[SensorWrapper.XA_SEN_ORB_SEA] = this.SolarXAngle;
               xa_sen[SensorWrapper.XA_SEN_ORB_LEA] = this.LunarXAngle;
               xa_sen[SensorWrapper.XA_SEN_ORB_MINILLUM] = this.MinIlum;
               xa_sen[SensorWrapper.XA_SEN_ORB_EARTHBRND] = this.EarthBckgrnd;
               break;
            case 'R': //VT_FOR:
               break;
            case 'V': //VT_FOV:
               xa_sen[SensorWrapper.XA_SEN_FOV_BEAMWIDTH] = this.ElLim1;    //use 1L - Card's elevation limit #1 columns 17-20
               xa_sen[SensorWrapper.XA_SEN_FOV_TILTANGLE] = this.ElLim2;    //use 1L - Card's elevation limit #2 columns 22-25
               break;
         }

         if (this.SenBiases != null)
         {
            xa_sen[SensorWrapper.XA_SEN_GEN_AZBIAS]   = this.SenBiases.AzRaBias_deg    ;
            xa_sen[SensorWrapper.XA_SEN_GEN_ELBIAS]   = this.SenBiases.ElDecBias_deg   ;
            xa_sen[SensorWrapper.XA_SEN_GEN_RGBIAS]   = this.SenBiases.RngBias_km      ;
            xa_sen[SensorWrapper.XA_SEN_GEN_RRBIAS]   = this.SenBiases.RngRateBias_kmps;
            xa_sen[SensorWrapper.XA_SEN_GEN_TIMEBIAS] = this.SenBiases.TimeBias_sec;
         }

         if (this.SenSigmas != null)
         {
            xa_sen[SensorWrapper.XA_SEN_GEN_AZSIGMA] = this.SenSigmas.AzRaSigma_deg     ;
            xa_sen[SensorWrapper.XA_SEN_GEN_ELSIGMA] = this.SenSigmas.ElDecSigma_deg    ;
            xa_sen[SensorWrapper.XA_SEN_GEN_RGSIGMA] = this.SenSigmas.RngSigma_km       ;
            xa_sen[SensorWrapper.XA_SEN_GEN_RRSIGMA] = this.SenSigmas.RngRateSigma_kmps ;
            xa_sen[SensorWrapper.XA_SEN_GEN_ARSIGMA] = this.SenSigmas.AzRaRateSigma_dps ;
            xa_sen[SensorWrapper.XA_SEN_GEN_ERSIGMA] = this.SenSigmas.ElDecRateSigma_dps;
         }

         xa_sen[SensorWrapper.XA_SEN_GEN_UNIT] = this.RngUnits;
         xa_sen[SensorWrapper.XA_SEN_GEN_PTSPERPAS] = this.MaxPPP;
         xa_sen[SensorWrapper.XA_SEN_GEN_INTERVAL] = this.Interval;

         //byte[] sensorDesc = Encoding.ASCII.GetBytes(this.SenDesc.Trim());
         //sensorDesc.CopyTo(xs_sen, SensorWrapper.XS_SEN_DSCRPTN_3_24);
         encoding.GetBytes(this.SenDesc, 0, this.SenDesc.Length, xs_sen, SensorWrapper.XS_SEN_DSCRPTN_3_24);

         xs_sen[SensorWrapper.XS_SEN_SECCLASS_0_1] = (byte)this.SecClass;
         xs_sen[SensorWrapper.XS_SEN_VIEWTYPE_1_1] = (byte)this.ViewType;
         xs_sen[SensorWrapper.XS_SEN_OBSTYPE_2_1] = (byte)this.ObsType;
      }
   }
}
