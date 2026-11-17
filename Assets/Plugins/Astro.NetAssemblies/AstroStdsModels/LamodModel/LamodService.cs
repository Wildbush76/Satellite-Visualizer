using System;
using System.Collections.Generic;
using AFSPC.AstroStds.Wrappers;
using System.IO;
using AFSPC.AstroStds.Utilities;
using AFSPC.AstroStds.Models.SatelliteModel;
using AFSPC.AstroStds.Models.SensorModel;

namespace AFSPC.AstroStds.Models.LamodModel
{
   public static class LamodService
   {
      //private static List<LamodLook> lamodLooks = new List<LamodLook>();
      private static List<LookAngle> lamodLooks = new List<LookAngle>();

      static LamodService()
      {
      }


      public static void PrintLamodCtrls(StreamWriter sw)
      {
         string dashLine = new string('-', 79);
         string ddashLine = new string('=', 79);

         Lamod1P.Get1P();


         sw.WriteLine(" " + ddashLine);
         sw.WriteLine(" |                          L A M O D   O P T I O N S                          |");
         sw.WriteLine(" " + dashLine);


         sw.WriteLine(" |  A.  PROCESSING MODE:               {0, -40}|",
            Lamod1P.SchedMode == LamodWrapper.ProcModeEnum.ProcModePasSched ? "OBSCHED (all sensors vs all satellites)" : "PASCHED (all satellites vs all sensors)");
         if (Lamod1P.TimeFlg == LamodWrapper.CARD_1P_FRDATE)
         {
            sw.WriteLine(" |  B.  TIME OF INTEREST START, UTC:   {0, -40}|", TimeFuncWrapper.UTCToDtg20Str(Lamod1P.StartTime));
            sw.WriteLine(" |  C.  TIME OF INTEREST END  , UTC:   {0, -40}|", TimeFuncWrapper.UTCToDtg20Str(Lamod1P.StopTime));
         }
         else
         {
            sw.WriteLine(" |  B.  TIME OF INTEREST START, MSE:   {0,10:F3}{1, -30}|", Lamod1P.StartTime, " ");
            sw.WriteLine(" |  C.  TIME OF INTEREST END  , MSE:   {0,10:F3}{1, -30}|", Lamod1P.StopTime, " ");
         }

         sw.WriteLine(" |  F.  PRINTED OUTPUT OPTION:         {0, -40}|", Lamod1P.PrtOpt == 0 ? "Look Angles" : "Look Angles & Initial Conditions");

         // condition/output output stepSize
         if (Lamod1P.StepSize == 0)
            sw.WriteLine(" |  H.  LOOK ANGLE OUTPUT INTERVAL     {0, -40}|", "Use Sensor's Output Interval Value");
         else
         {
            sw.WriteLine(" |  H.  LOOK ANGLE OUTPUT INTERVAL     {0,7:f2} min{1, -29}|", Lamod1P.StepSize, " ");
            sw.WriteLine(" |      (Note: This output interval overwrites sensor's output interval)       |");
         }

         // condition/output optical visibility flag
         sw.WriteLine(" |  I.  OPTICAL VISIBILITY OPTION:     {0, -40}|", Lamod1P.VisFlg.ToDescription());

         if (Lamod1P.VisFlg != LamodWrapper.SenOptVisFlgEnum.OptVisFlg9) // overwrite visibility flag of sensor's limit card
            sw.WriteLine(" |      (Note: This flag overwrites sensor's visual pass control flag)         |");


         // condition/output solar aspect angle
         if (Lamod1P.SolAspAngle == 0)
            sw.WriteLine(" |  J.  SOLAR ASPECT ANGLE:            {0, -40}|", "(computed value)");
         else
            sw.WriteLine(" |  J.  SOLAR ASPECT ANGLE:            {0,7:f2} deg{1, -29}|", Lamod1P.SolAspAngle, " ");

         // condition/output step mode flag
         sw.WriteLine(" |  K.  STEP/CULMINATION/TASK MODE:    {0, -40}|", Lamod1P.StepMode.ToDescription());
         // condition/output observation generation flag
         sw.WriteLine(" |  O.  OBSERVATION GENERATION:        {0, -40}|", Lamod1P.PunchObs.ToDescription());
         sw.WriteLine(" |  R.  DIAGNOSTICS FLAG:              {0, -40}|", Lamod1P.DiagMode.ToDescription());
         sw.WriteLine(" |  S.  EARTH SHAPE:                   {0, -40}|", EnvConstWrapper.EnvGetEarthShape() == 1 ? "Oblate" : "Spherical");

         sw.WriteLine(" " + ddashLine);
         sw.WriteLine("\n");

      }

      // Print looks in schedule mode, either culmination or step mode
      public static void LamodSenSatPair(StreamWriter sw, StreamWriter swObsFile, long senKey, long satKey, bool isExcelCSV, bool isMaxElev)
      {
         // Initialize sen/sat pair
         long senSatKey = LamodWrapper.LamodInitSenSat(senKey, satKey);

         // Skip this sensor-satellite pair
         if (senSatKey < 0)
         {

            ErrorMessages.SaveErrorMessage();
            return;
         }
         else if (senSatKey == 0)
            return;

         // Retrieve sen/sat pair data
         SenSatData senSatData = new SenSatData(senSatKey, senKey, satKey);


         if (senSatData.ErrCode != 0)
         {

            ErrorMessages.SaveErrorMessage();
            return;
         }

         int totalLookCount;
         int passCount;


         // Retrieve sensor data
         Sensor sensor = new Sensor(senSatData.SenNum);

         if (sensor.SenNum <= 0)
         {

            ErrorMessages.SaveErrorMessage();
            return;
         }

         PrintSenSatHeader(sw, senSatData, sensor, isExcelCSV);

         // DHN 12Apr16 - Special mode that uses ephemeris time points to compute look angles
         if (Lamod1P.UseEphemPoints && (SatStateService.SatStateIsExtEphem(senSatData.SatKey) ||
            SatStateService.SatStateIsExtEphem(SatStateWrapper.SatStateNumToKey(sensor.OrbSatNum))))
         {
            DoStepModeExtEphem(sw, swObsFile, senSatData, sensor, isExcelCSV, out totalLookCount, out passCount);
            PrintSummary(sw, senSatData.SenNum, senSatData.SatNum, totalLookCount, passCount);
            sw.Flush();
            return;
         }

         // Culmination mode
         if (senSatData.StepMode == LamodWrapper.STEPMODE_CULM)
         {
            if (!isMaxElev)
            {
               DoCulmMode(sw, swObsFile, senSatData, sensor, isExcelCSV, out totalLookCount, out passCount);
               PrintSummary(sw, senSatData.SenNum, senSatData.SatNum, totalLookCount, passCount);
            }
            else
            {
               DoMaxElevCulmMode(sw, swObsFile, senSatData, sensor, isExcelCSV, out totalLookCount, out passCount);
               PrintSummary(sw, senSatData.SenNum, senSatData.SatNum, totalLookCount, passCount);
            }
         }
         else if (senSatData.StepMode == LamodWrapper.STEPMODE_STEP)
         {
            if (!isMaxElev)
            {
               DoStepMode(sw, swObsFile, senSatData, sensor, isExcelCSV, out totalLookCount, out passCount);
               PrintSummary(sw, senSatData.SenNum, senSatData.SatNum, totalLookCount, passCount);
            }
            else
            {
               DoMaxElevStepMode(sw, swObsFile, senSatData, sensor, isExcelCSV, out totalLookCount, out passCount);
               PrintSummary(sw, senSatData.SenNum, senSatData.SatNum, totalLookCount, passCount);
            }
         }
         else if (senSatData.StepMode == LamodWrapper.STEPMODE_TASK)
         {
            if (!isMaxElev)
            {
               DoTaskMode(sw, swObsFile, senSatData, sensor, isExcelCSV, out totalLookCount, out passCount);
               PrintSummary(sw, senSatData.SenNum, senSatData.SatNum, totalLookCount, passCount);
            }
            else
            {
               DoMaxElevStepMode(sw, swObsFile, senSatData, sensor, isExcelCSV, out totalLookCount, out passCount);
               PrintSummary(sw, senSatData.SenNum, senSatData.SatNum, totalLookCount, passCount);
            }
         }
         sw.Flush();
      }


      // Print looks obtained by a sen-sat pair (senSatKey) in Culmination step mode
      private static void DoCulmMode(StreamWriter sw, StreamWriter swObsFile, SenSatData senSatData, Sensor sensor, bool isExcelCSV, out int totalLookCount, out int passCount)
      {
         double currDs50TAI;

         bool newPass;
         double deltaT;
         int numOfPasses;
         bool emptyPass;
         double[,] rcsTimeArr;
         double passStartDs50TAI, passStopDs50TAI;
         double[] rcs = new double[3]; // Rise, Culmination, and Set times for this pass
         double[] lookArr = new double[6];
         double[] senElts = new double[9];
         double[] satElts = new double[9];
         double[] xlpos = new double[3];
         char currStep;
         double riseTime, culmTime, setTime;
         int numLooks;
         int stepMode;



         passCount = 0;
         totalLookCount = 0;

         // Get the number of possible passes for this sen/sat pair within the specified time span
         numOfPasses = LamodWrapper.LamodGetNumPasses(senSatData.SenSatKey);
         if (numOfPasses <= 0)
            return;

         // Dynamically allocate the array to store the rise/culmination/set times
         rcsTimeArr = new double[numOfPasses, 3];

         // Get all the rise/culmination/set time of all the passes
         LamodWrapper.LamodGetRiseCulmSetTimes(senSatData.SenSatKey, rcsTimeArr);

         // Loop through all the possible passes
         for (int k = 0; k < numOfPasses; k++)
         {
            newPass = true;

            // Get the rise/culmination/set time from the array
            for (int i = 0; i < 3; i++) rcs[i] = rcsTimeArr[k, i];

            riseTime = rcs[0];
            culmTime = rcs[1];
            setTime = rcs[2];


            // Handle cases when only rise time is found in the last pass (culmination and set times 
            // pass the requested stop time). This needs to switch to step mode 
            if (culmTime == 0 && setTime == 0)
            {
               stepMode = LamodWrapper.STEPMODE_STEP;
               sw.WriteLine("+++++ STEP-MODE INVOKED FOR LAST PASS +++++\n\n");
            }
            // Handle cases when multiple maximum elevations detected in the pass
            else if (culmTime == -1.0)
            {
               stepMode = LamodWrapper.STEPMODE_STEP;
               sw.WriteLine("+++++ STEP-MODE INVOKED DUE TO MULTIPLE MAXIMUM ELEVATIONS +++++\n\n");

            }
            else
               stepMode = LamodWrapper.STEPMODE_CULM;

            // Adjust the start/stop time if Rise/Set time is outside of the requested time span
            passStartDs50TAI = Math.Max(riseTime, senSatData.StartDs50TAI);

            // For external ephemeris, the last pass's rise time might be set to -1.0 to indicate the value is not 
            // available because the ephemeris data doesn't cover the possible rise time. 
            // This case already handle with the above max() function
            passStopDs50TAI = Math.Min(setTime, senSatData.StopDs50TAI);

            // For external ephemeris, the last pass's set time might be set to -1.0 to indicate the value is not 
            // available because the ephemeris data doesn't cover the possible set time. 
            if (k == numOfPasses - 1 && setTime == -1.0)
               passStopDs50TAI = senSatData.StopDs50TAI;


            // if the user wants to print the whole pass even if the rise or set times is outside of the requested time span
            //passStartDs50TAI = riseTime;
            //passStopDs50TAI = setTime;

            currDs50TAI = passStartDs50TAI;

            emptyPass = true;
            numLooks = 0;

            // Loop through all the time steps in each pass
            while (true)
            {
               // Compute the look angle at the specified time
               LookAngle la = new LookAngle(senSatData, currDs50TAI);

               if (la.ErrCode != 0)
               {
                  ErrorMessages.SaveErrorMessage();
                  return;
               }

               // Check to see if the look is valid
               if (la.LookCode == LamodWrapper.LOOK_VALID)
               {
                  la.PassNum = passCount + 1;
                  lamodLooks.Add(la);

                  if (senSatData.ViewType == '7')
                     currStep = 'x'; // indicate time when satellite crosses the fan
                  else
                     currStep = CurrPrintStep(currDs50TAI, rcs, senSatData.Interval_min, senSatData.StopDs50TAI);

                  PrintLook(sw, senSatData, sensor, currStep, la, isExcelCSV);
                  emptyPass = false;
                  totalLookCount++;
                  numLooks++;

                  // Print obs if requested
                  if ((int)Lamod1P.PunchObs > 0)
                     PrintObs(swObsFile, senSatData.SenSatKey);
               }

               // For Fan - the only one crossing time was already reported - exit the loop
               if (senSatData.ViewType == '7')
                  break;

               // Print diagnostic data if requested
               if ((int)Lamod1P.DiagMode != 0)
               {
                  if (la.LookCode != LamodWrapper.LOOK_VALID) // DHN 28Dec17 - allow bad look to be printed out in diagnostic mode
                     PrintLook(sw, senSatData, sensor, ' ', la, isExcelCSV);
                  PrintNosticdata(sw, senSatData, sensor.ViewType, la);
               }


               // After the first point, set steps at even offset (Interval) from culmination time
               if (newPass && stepMode == LamodWrapper.STEPMODE_CULM)
               {
                  newPass = false;

                  // The differen in step sizes between the pass start time and the culmination time
                  deltaT = (Math.Abs(culmTime - passStartDs50TAI) / (senSatData.Interval_min / Constants.MINSPERDAY));

                  // The fraction part of the difference
                  deltaT = deltaT - (long)deltaT;

                  if (culmTime < passStartDs50TAI)
                     deltaT = 1.0 - deltaT;

                  currDs50TAI = passStartDs50TAI + (deltaT * (senSatData.Interval_min / Constants.MINSPERDAY));

                  // The second point is within 1msec of the first point, increment the second point
                  if (Math.Abs(currDs50TAI - passStartDs50TAI) < LamodWrapper.EPSTIMEDAYS)
                     currDs50TAI += (senSatData.Interval_min / Constants.MINSPERDAY);
               }
               else
               {
                  // If the current time is within ~1msec the last point then exit
                  if (Math.Abs(currDs50TAI - passStopDs50TAI) < LamodWrapper.EPSTIMEDAYS)
                     break;
                  currDs50TAI = currDs50TAI + (senSatData.Interval_min / Constants.MINSPERDAY);
               }

               // Implement the maximum number of points per pass check
               if (sensor.MaxPPP != 0 && numLooks >= sensor.MaxPPP)
                  break;

               // Handle the last point of this pass
               if (currDs50TAI > passStopDs50TAI)
               {
                  if (currDs50TAI - passStopDs50TAI < LamodWrapper.EPSTIMEDAYS)
                     // the last point is within 1msec passed the passStopDs50TAI
                     continue;
                  // The set time is within the previous point and the last point (currDs50TAI)
                  // replace the last point with the set time
                  else if (setTime <= passStopDs50TAI && stepMode == LamodWrapper.STEPMODE_CULM)
                     currDs50TAI = passStopDs50TAI;
                  else
                     break;
               }
            }

            if (!emptyPass)
            {
               passCount++;

               // Print pass number at the end of each pass
               sw.WriteLine("{0,67}{1,7}", " ", passCount);
            }
         }
      }


      // Print looks obtained by a sen-sat pair (senSatKey) in Step step mode
      private static void DoStepMode(StreamWriter sw, StreamWriter swObsFile, SenSatData senSatData, Sensor sensor, bool isExcelCSV, out int totalLookCount, out int passCount)
      {
         double deltaTimeMin;
         double currDs50TAI;
         double lastValidLookTime;
         bool newPass;
         char currStep;
         int numLooks;
         char viewType;
         char obsType;
         int count = 0;


         totalLookCount = 0;
         passCount = 0;
         numLooks = 0;

         viewType = sensor.ViewType;
         obsType = sensor.ObsType;


         // Find the time duration to separate/distiguish new passes
         deltaTimeMin = Math.Max(Math.Max(senSatData.Interval_min, 0.4 * senSatData.Period_min), 40.0);

         newPass = true;
         lastValidLookTime = senSatData.StartDs50TAI;

         currDs50TAI = senSatData.StartDs50TAI;

         while (currDs50TAI <= senSatData.StopDs50TAI + LamodWrapper.EPSTIMEDAYS)
         {
            // Determine if this start a new pass
            if (!newPass && Math.Abs(currDs50TAI - lastValidLookTime) > (deltaTimeMin / Constants.MINSPERDAY))
            {
               newPass = true;

               // Print pass number at the end of the previous pass
               if (numLooks > 0)
               {
                  passCount++;
                  sw.WriteLine("{0,67}{1,7}", " ", passCount);
               }

               numLooks = 0;
            }


            LookAngle la = new LookAngle(senSatData, currDs50TAI);
            if (la.ErrCode != 0)
            {
               ErrorMessages.SaveErrorMessage();
               return;
            }

            // Check to see if the look is valid
            if (la.LookCode == LamodWrapper.LOOK_VALID || (int)Lamod1P.DiagMode != 0) // DHN 28Dec17 - allow bad look to be printed out in diagnostic mode
            {
               la.PassNum = passCount + 1;
               lamodLooks.Add(la);
               newPass = false;

               // Implement max number of points per pass check
               if (sensor.MaxPPP == 0 || numLooks < sensor.MaxPPP)
               {
                  currStep = ' ';

                  if (currDs50TAI + senSatData.Interval_min / Constants.MINSPERDAY > senSatData.StopDs50TAI)
                     currStep = 'p';
                  PrintLook(sw, senSatData, sensor, currStep, la, isExcelCSV);
                  totalLookCount++;
                  numLooks++;

                  if ((int)Lamod1P.PunchObs > 0)
                     PrintObs(swObsFile, senSatData.SenSatKey);
               }

               // Record the time of the last valid look
               lastValidLookTime = currDs50TAI;
            }

            if ((int)Lamod1P.DiagMode != 0)
            {
               //PrintPassByPassNostic(sw, senSatKey, viewType);
               if (la.LookCode != LamodWrapper.LOOK_VALID) // DHN 28Dec17 - allow bad look to be printed out in diagnostic mode
                  PrintLook(sw, senSatData, sensor, ' ', la, isExcelCSV);

               PrintNosticdata(sw, senSatData, sensor.ViewType, la);
            }


            currDs50TAI = currDs50TAI + (senSatData.Interval_min / Constants.MINSPERDAY);
            count++;
            currDs50TAI = senSatData.StartDs50TAI + (count * senSatData.Interval_min / Constants.MINSPERDAY);
         }

         // This pass might not have been counted
         if (!newPass)
         {
            // Print pass number at the end of the previous pass
            if (numLooks > 0)
            {
               passCount++;
               sw.WriteLine("{0,67}{1,7}", " ", passCount);
            }
         }
      }

      // Print looks obtained by a sen-sat pair (senSatKey) in Step step mode
      private static void DoTaskMode(StreamWriter sw, StreamWriter swObsFile, SenSatData senSatData, Sensor sensor, bool isExcelCSV, out int totalLookCount, out int passCount)
      {
         double deltaTimeMin;
         double currDs50TAI, prevDs50TAI, inOutTime, passStartTime;
         bool newPass;
         char currStep;
         int numLooks;
         char viewType;
         char obsType;
         int count = 0;
         bool prevValidLook, currValidLook, handleLastLook;
         bool isEndOfPass;


         totalLookCount = 0;
         passCount = 0;
         numLooks = 0;

         viewType = sensor.ViewType;
         obsType = sensor.ObsType;


         // Find the time duration to separate/distiguish new passes
         deltaTimeMin = Math.Max(Math.Max(senSatData.Interval_min, 0.4 * senSatData.Period_min), 40.0);

         newPass = true;

         prevDs50TAI = currDs50TAI = passStartTime = senSatData.StartDs50TAI;

         prevValidLook = currValidLook = false;
         isEndOfPass = false;
         handleLastLook = false;

         while (currDs50TAI <= senSatData.StopDs50TAI + LamodWrapper.EPSTIMEDAYS || !handleLastLook)
         {
            if (currDs50TAI > senSatData.StopDs50TAI + LamodWrapper.EPSTIMEDAYS)
            {
               currDs50TAI = senSatData.StopDs50TAI;
               handleLastLook = true;
            }

            // Determine if this start a new pass
            if (isEndOfPass)
            {
               newPass = true;

               // Print pass number at the end of the previous pass
               if (numLooks > 0)
               {
                  passCount++;
                  sw.WriteLine("{0,67}{1,7}", " ", passCount);
               }

               numLooks = 0;
            }


            LookAngle la = new LookAngle(senSatData, currDs50TAI);
            if (la.ErrCode != 0)
            {
               ErrorMessages.SaveErrorMessage();
               return;
            }


            currValidLook = (la.LookCode == LamodWrapper.LOOK_VALID);
            isEndOfPass = (prevValidLook && !currValidLook); // happens whenever a valid look turns to an invalid look


            // whenever a look validity changes between two different times
            if (currDs50TAI != senSatData.StartDs50TAI && currValidLook != prevValidLook)
            {
               inOutTime = LamodWrapper.LamodFindExactIOTime(senSatData.SenSatKey, prevDs50TAI, currDs50TAI);

               // Can't find the exact in/out time
               if (inOutTime == 0)
                  inOutTime = currDs50TAI;

               currDs50TAI = inOutTime;

               la = new LookAngle(senSatData, currDs50TAI);

               if (!isEndOfPass)
               {
                  passStartTime = currDs50TAI;
                  count = 0;
               }
            }


            // Check to see if the look is valid
            if (la.LookCode == LamodWrapper.LOOK_VALID) // DHN 28Dec17 - allow bad look to be printed out in diagnostic mode
            {
               la.PassNum = passCount + 1;
               lamodLooks.Add(la);
               newPass = false;

               // Implement max number of points per pass check
               if (sensor.MaxPPP == 0 || numLooks < sensor.MaxPPP)
               {
                  currStep = ' ';

                  if (currDs50TAI + senSatData.Interval_min / Constants.MINSPERDAY > senSatData.StopDs50TAI)
                     currStep = 'p';
                  PrintLook(sw, senSatData, sensor, currStep, la, isExcelCSV);
                  totalLookCount++;
                  numLooks++;

                  if ((int)Lamod1P.PunchObs > 0)
                     PrintObs(swObsFile, senSatData.SenSatKey);
               }
            }

            if ((int)Lamod1P.DiagMode != 0)
            {
               if (la.LookCode != LamodWrapper.LOOK_VALID)
                  PrintLook(sw, senSatData, sensor, ' ', la, isExcelCSV);
               PrintNosticdata(sw, senSatData, sensor.ViewType, la);
            }

            prevValidLook = currValidLook;
            prevDs50TAI = currDs50TAI;

            count++;
            currDs50TAI = passStartTime + (count * senSatData.Interval_min / Constants.MINSPERDAY);
         }

         // This pass might not have been counted
         if (!newPass)
         {
            // Print pass number at the end of the previous pass
            if (numLooks > 0)
            {
               passCount++;
               sw.WriteLine("{0,67}{1,7}", " ", passCount);
            }
         }
      }

      // Print looks obtained by a sen-sat pair (senSatKey) in Step step mode
      private static void DoStepModeExtEphem(StreamWriter sw, StreamWriter swObsFile, SenSatData senSatData, Sensor sensor, bool isExcelCSV, out int totalLookCount, out int passCount)
      {
         double deltaTimeMin;
         double currDs50TAI;
         double lastValidLookTime;
         bool newPass;
         char currStep;
         int numLooks;
         char viewType;
         char obsType;
         int count = 0;
         long orbSatKey;

         int numPts;

         orbSatKey = SatStateWrapper.SatStateNumToKey(sensor.OrbSatNum);
         bool isOrbSatExtEphem = SatStateService.SatStateIsExtEphem(orbSatKey);

         if (isOrbSatExtEphem)
            numPts = Utility.GetKey64Int(ExtEphemWrapper.ExtEphGetField, orbSatKey, ExtEphemWrapper.XF_EXTEPH_NUMOFPTS);
         else
            numPts = Utility.GetKey64Int(ExtEphemWrapper.ExtEphGetField, senSatData.SatKey, ExtEphemWrapper.XF_EXTEPH_NUMOFPTS);

         List<DaysSince50UTC> ds50UTCList = new List<DaysSince50UTC>(numPts);

         int ii = 1;

         // Colect all the time points in the external ephemeris file
         while (ii <= numPts)
         {
            double currDs50UTC;
            double[] refPos = new double[3];
            double[] refVel = new double[3];
            int revNum;

            if (isOrbSatExtEphem)
               ExtEphemWrapper.ExtEphGetEphemeris(orbSatKey, ii, out currDs50UTC, refPos, refVel, out revNum);
            else
               ExtEphemWrapper.ExtEphGetEphemeris(senSatData.SatKey, ii, out currDs50UTC, refPos, refVel, out revNum);
            ii++;
            ds50UTCList.Add(currDs50UTC);
         }

         totalLookCount = 0;
         passCount = 0;
         numLooks = 0;

         viewType = sensor.ViewType;
         obsType = sensor.ObsType;


         // Find the time duration to separate/distiguish new passes
         deltaTimeMin = Math.Max(Math.Max(senSatData.Interval_min, 0.4 * senSatData.Period_min), 40.0);

         newPass = true;
         lastValidLookTime = ds50UTCList[0].TAI;


         // compute looks for all the time points in the external ephemeris file
         foreach (DaysSince50UTC ds50UTC in ds50UTCList)
         {
            currDs50TAI = ds50UTC.TAI;

            // Determine if this start a new pass
            if (!newPass && Math.Abs(ds50UTC.TAI - lastValidLookTime) > (deltaTimeMin / Constants.MINSPERDAY))
            {
               newPass = true;

               // Print pass number at the end of the previous pass
               if (numLooks > 0)
               {
                  passCount++;
                  sw.WriteLine("{0,67}{1,7}", " ", passCount);
               }

               numLooks = 0;
            }


            LookAngle la = new LookAngle(senSatData, currDs50TAI);
            if (la.ErrCode != 0)
            {
               ErrorMessages.SaveErrorMessage();
               return;
            }

            // Check to see if the look is valid
            if (la.LookCode == LamodWrapper.LOOK_VALID || (int)Lamod1P.DiagMode != 0)
            {
               la.PassNum = passCount + 1;
               lamodLooks.Add(la);
               newPass = false;

               // Implement max number of points per pass check
               if (sensor.MaxPPP == 0 || numLooks < sensor.MaxPPP)
               {
                  currStep = ' ';

                  if (currDs50TAI + senSatData.Interval_min / Constants.MINSPERDAY > senSatData.StopDs50TAI)
                     currStep = 'p';
                  PrintLook(sw, senSatData, sensor, currStep, la, isExcelCSV);
                  totalLookCount++;
                  numLooks++;

                  if ((int)Lamod1P.PunchObs > 0)
                     PrintObs(swObsFile, senSatData.SenSatKey);
               }

               // Record the time of the last valid look
               lastValidLookTime = currDs50TAI;
            }

            if ((int)Lamod1P.DiagMode != 0)
            {
               //PrintPassByPassNostic(sw, senSatKey, viewType);
               if (la.LookCode != LamodWrapper.LOOK_VALID) // DHN 28Dec17 - allow bad look to be printed out in diagnostic mode
                  PrintLook(sw, senSatData, sensor, ' ', la, isExcelCSV);

               PrintNosticdata(sw, senSatData, sensor.ViewType, la);
            }


            currDs50TAI = currDs50TAI + (senSatData.Interval_min / Constants.MINSPERDAY);
            count++;
            currDs50TAI = senSatData.StartDs50TAI + (count * senSatData.Interval_min / Constants.MINSPERDAY);
         }

         // This pass might not have been counted
         if (!newPass)
         {
            // Print pass number at the end of the previous pass
            if (numLooks > 0)
            {
               passCount++;
               sw.WriteLine("{0,67}{1,7}", " ", passCount);
            }
         }
      }

      // Print looks obtained by a sen-sat pair (senSatKey) in First/Last/Max Elevation step mode
      private static void DoMaxElevCulmMode(StreamWriter sw, StreamWriter swObsFile, SenSatData senSatData, Sensor sensor, bool isExcelCSV, out int totalLookCount, out int passCount)
      {
         double currDs50TAI;

         bool newPass;
         double deltaT;
         int numOfPasses;
         bool emptyPass;
         double[,] rcsTimeArr;
         double passStartDs50TAI, passStopDs50TAI;
         double[] rcs = new double[3]; // Rise, Culmination, and Set times for this pass
         double passStart, passEnd, passMax, riseTime, culmTime, setTime;
         double maxElev;
         int numLooks;
         int stepMode;



         passCount = 0;
         totalLookCount = 0;

         // Get the number of possible passes for this sen/sat pair within the specified time span
         numOfPasses = LamodWrapper.LamodGetNumPasses(senSatData.SenSatKey);
         if (numOfPasses <= 0)
            return;

         // Dynamically allocate the array to store the rise/culmination/set times
         rcsTimeArr = new double[numOfPasses, 3];

         // Get all the rise/culmination/set times of all the passes
         LamodWrapper.LamodGetRiseCulmSetTimes(senSatData.SenSatKey, rcsTimeArr);

         // Loop through all the possible passes
         for (int k = 0; k < numOfPasses; k++)
         {
            newPass = true;

            maxElev = 0;
            passStart = 0;
            passEnd = 0;
            passMax = 0;


            // Get the rise/culmination/set time from the array
            for (int i = 0; i < 3; i++) rcs[i] = rcsTimeArr[k, i];

            riseTime = rcs[0];
            culmTime = rcs[1];
            setTime = rcs[2];


            // Handle cases when only rise time is found in the last pass (culmination and set times 
            // pass the requested stop time). This needs to switch to step mode 
            if (culmTime == 0 && setTime == 0)
            {
               stepMode = LamodWrapper.STEPMODE_STEP;
               sw.WriteLine("+++++ STEP-MODE INVOKED FOR LAST PASS +++++\n\n");
            }
            // Handle cases when multiple maximum elevations detected in the pass
            else if (culmTime == -1.0)
            {
               stepMode = LamodWrapper.STEPMODE_STEP;
               sw.WriteLine("+++++ STEP-MODE INVOKED DUE TO MULTIPLE MAXIMUM ELEVATIONS +++++\n\n");

            }
            else
               stepMode = LamodWrapper.STEPMODE_CULM;

            // Adjust the start/stop time if Rise/Set time is outside of the requested time span
            //passStartDs50TAI = Math.Max(riseTime, senSatData.StartDs50TAI);
            passStartDs50TAI = riseTime;

            // For external ephemeris, the last pass's rise time might be set to -1.0 to indicate the value is not 
            // available because the ephemeris data doesn't cover the possible rise time. 
            // This case already handle with the above max() function
            //passStopDs50TAI = Math.Min(setTime, senSatData.StopDs50TAI);
            passStopDs50TAI = setTime;

            // For external ephemeris, the last pass's set time might be set to -1.0 to indicate the value is not 
            // available because the ephemeris data doesn't cover the possible set time. 
            if (k == numOfPasses - 1 && setTime == -1.0)
               passStopDs50TAI = senSatData.StopDs50TAI;


            // if the user wants to print the whole pass even if the rise or set times is outside of the requested time span
            //passStartDs50TAI = riseTime;
            //passStopDs50TAI = setTime;

            currDs50TAI = passStartDs50TAI;

            emptyPass = true;
            numLooks = 0;

            // Loop through all the time steps in each pass
            while (true)
            {
               // Compute the look angle at the specified time
               LookAngle la = new LookAngle(senSatData, currDs50TAI);
               if (la.ErrCode != 0)
               {
                  ErrorMessages.SaveErrorMessage();
                  return;
               }

               // Check to see if the look is valid
               if (la.LookCode == LamodWrapper.LOOK_VALID)
               {
                  // store time of the first valid look of the pass
                  if (passStart == 0)
                     passStart = currDs50TAI;

                  // store time of the last valid look of the pass
                  passEnd = currDs50TAI;

                  // store time of the look that currently has maximum elevation
                  if (maxElev < la.Elevation_deg)
                  {
                     maxElev = la.Elevation_deg;
                     passMax = currDs50TAI;
                  }

                  emptyPass = false;
                  totalLookCount++;
                  numLooks++;
               }

               // Print diagnostic data if requested
               if ((int)Lamod1P.DiagMode != 0)
                  PrintNosticdata(sw, senSatData, sensor.ViewType, la);


               // After the first point, set steps at even offset (Interval) from culmination time
               if (newPass && stepMode == LamodWrapper.STEPMODE_CULM)
               {
                  newPass = false;

                  // The differen in step sizes between the pass start time and the culmination time
                  deltaT = (Math.Abs(culmTime - passStartDs50TAI) / (senSatData.Interval_min / Constants.MINSPERDAY));

                  // The fraction part of the difference
                  deltaT = deltaT - (long)deltaT;

                  if (culmTime < passStartDs50TAI)
                     deltaT = 1.0 - deltaT;

                  currDs50TAI = passStartDs50TAI + (deltaT * (senSatData.Interval_min / Constants.MINSPERDAY));

                  // The second point is within 1msec of the first point, increment the second point
                  if (Math.Abs(currDs50TAI - passStartDs50TAI) < LamodWrapper.EPSTIMEDAYS)
                     currDs50TAI += (senSatData.Interval_min / Constants.MINSPERDAY);
               }
               else
               {
                  // If the current time is within ~1msec the last point then exit
                  if (Math.Abs(currDs50TAI - passStopDs50TAI) < LamodWrapper.EPSTIMEDAYS)
                     break;
                  currDs50TAI = currDs50TAI + (senSatData.Interval_min / Constants.MINSPERDAY);
               }

               // Implement the maximum number of points per pass check
               //if (sensor.MaxPPP != 0 && numLooks >= sensor.MaxPPP)
               //   break;

               // Handle the last point of this pass
               if (currDs50TAI > passStopDs50TAI)
               {
                  if (currDs50TAI - passStopDs50TAI < LamodWrapper.EPSTIMEDAYS)
                     // the last point is within 1msec passed the passStopDs50TAI
                     continue;
                  // The set time is within the previous point and the last point (currDs50TAI)
                  // replace the last point with the set time
                  else if (setTime <= passStopDs50TAI) // && stepMode == LamodWrapper.STEPMODE_CULM)
                     currDs50TAI = passStopDs50TAI;
                  else
                     break;
               }
            }

            if (!emptyPass)
            {
               passCount++;

               PrintFirsLastMaxElev(sw, swObsFile, senSatData, sensor, isExcelCSV, passStart, passEnd, passMax, passCount);


               // Print pass number at the end of each pass
               sw.WriteLine("{0,67}{1,7}", " ", passCount);
            }
         }
      }


      private static void DoMaxElevStepMode(StreamWriter sw, StreamWriter swObsFile, SenSatData senSatData, Sensor sensor, bool isExcelCSV, out int totalLookCount, out int passCount)
      {
         double deltaTimeMin;
         double currDs50TAI;
         double passEnd = 0;
         double passStart = 0;
         double passMax = 0;
         double currMaxElev = -90;
         bool newPass;
         int numLooks;
         char viewType;
         char obsType;


         totalLookCount = 0;
         passCount = 0;
         numLooks = 0;

         viewType = sensor.ViewType;
         obsType = sensor.ObsType;


         // Find the time duration to separate/distiguish new passes
         deltaTimeMin = Math.Max(Math.Max(senSatData.Interval_min, 0.4 * senSatData.Period_min), 40.0);

         newPass = true;
         passStart = 0;
         passEnd = 0;
         passMax = 0;
         currMaxElev = -90;

         currDs50TAI = senSatData.StartDs50TAI;

         while (currDs50TAI <= senSatData.StopDs50TAI + LamodWrapper.EPSTIMEDAYS)
         {
            // Determine if this start a new pass
            if (!newPass && Math.Abs(currDs50TAI - passEnd) > (deltaTimeMin / Constants.MINSPERDAY))
            {
               newPass = true;

               if (passStart != 0)
               {
                  PrintFirsLastMaxElev(sw, swObsFile, senSatData, sensor, isExcelCSV, passStart, passEnd, passMax, passCount);
               }

               // Print pass number at the end of the previous pass
               if (numLooks > 0)
               {
                  passCount++;
                  sw.WriteLine("{0,67}{1,7}", " ", passCount);
               }

               passStart = 0;
               passEnd = 0;
               passMax = 0;
               currMaxElev = -90;
               numLooks = 0;
            }

            LookAngle la = new LookAngle(senSatData, currDs50TAI);
            if (la.ErrCode != 0)
            {
               ErrorMessages.SaveErrorMessage();
               return;
            }

            // Check to see if the look is valid
            if (la.LookCode == LamodWrapper.LOOK_VALID)
            {
               if (passStart == 0)
                  passStart = currDs50TAI;
               newPass = false;

               totalLookCount++;
               numLooks++;

               // Record the time of the last valid look
               passEnd = currDs50TAI;

               if (currMaxElev < la.Elevation_deg)
               {
                  currMaxElev = la.Elevation_deg;
                  passMax = currDs50TAI;
               }
            }

            if ((int)Lamod1P.DiagMode != 0)
               PrintNosticdata(sw, senSatData, sensor.ViewType, la);


            currDs50TAI = currDs50TAI + (senSatData.Interval_min / Constants.MINSPERDAY);
         }

         if (!newPass) // When passCount == 0, the pass may still not finish but the requested stop time is already passed
         {
            if (passStart != 0)
            {
               PrintFirsLastMaxElev(sw, swObsFile, senSatData, sensor, isExcelCSV, passStart, passEnd, passMax, passCount);
            }

            // Print pass number at the end of each pass
            sw.WriteLine("{0,67}{1,7}", " ", passCount);
         }

      }

      private static void PrintFirsLastMaxElev(StreamWriter sw, StreamWriter swObsFile, SenSatData senSatData, Sensor sensor, bool isExcelCSV, double passStart, double passEnd, double passMax, int passNum)
      {
         double maxElev;

         LookAngle la;

         la = new LookAngle(senSatData, passStart);
         la.PassNum = passNum;
         lamodLooks.Add(la);
         PrintLook(sw, senSatData, sensor, 'f', la, isExcelCSV);
         string cStartTIME = TimeFuncWrapper.UTCToDtg18Str(la.LookTimeDs50UTC);

         la = new LookAngle(senSatData, passMax);
         la.PassNum = passNum;
         lamodLooks.Add(la);
         PrintLook(sw, senSatData, sensor, 'm', la, isExcelCSV);
         maxElev = la.Elevation_deg;
         string cMaxTime = TimeFuncWrapper.UTCToDtg18Str(la.LookTimeDs50UTC);

         la = new LookAngle(senSatData, passEnd);
         la.PassNum = passNum;
         lamodLooks.Add(la);
         PrintLook(sw, senSatData, sensor, 'l', la, isExcelCSV);
         string cEndTime = TimeFuncWrapper.UTCToDtg18Str(la.LookTimeDs50UTC);

         string formatString = "{0,5},{1,3},{2},{3},{4},{5,7:F3}";
         swObsFile.WriteLine(formatString, senSatData.SatNum, senSatData.SenNum, cStartTIME, cMaxTime, cEndTime, maxElev);
      }



      // Print generated obs to file
      private static void PrintObs(StreamWriter swObsFile, long senSatKey)
      {
         byte[] obsLine1 = new byte[DllMainWrapper.INPUTCARDLEN];
         byte[] obsLine2 = new byte[DllMainWrapper.INPUTCARDLEN];
         int numLines;

         LamodWrapper.LamodGenObs(senSatKey, Lamod1P.ApplyLTC ? 10 + (int)Lamod1P.PunchObs : (int)Lamod1P.PunchObs, 'U', obsLine1, obsLine2, out numLines);
         string line = Utility.BytesToStringLRTrim(obsLine1);
         swObsFile.WriteLine("{0}", line);
         if (numLines == 2)
         {
            line = Utility.BytesToStringLRTrim(obsLine2);
            swObsFile.WriteLine("{0}", line);
         }

         //Console.WriteLine(line);
      }


      // Print looks of a satellite against many sensors
      public static void DoSatellite(StreamWriter sw, long satKey, List<long> selectedSenList, double begDs50TAI, double endDs50TAI, double stepSize, bool isExcelCSV)
      {
         double currDs50TAI;

         long[] senSatKeys = new long[selectedSenList.Count];
         long[] senKeys = new long[selectedSenList.Count];


         int senCount = 0;

         // Initialize sen-sat pairs
         foreach (long senKey in selectedSenList)
         {
            senSatKeys[senCount] = LamodWrapper.LamodInitSenSat(senKey, satKey);
            if (senSatKeys[senCount] <= 0) continue;

            senKeys[senCount] = senKey;
            senCount++;
         }



         sw.WriteLine("\n\n\n");
         sw.WriteLine("LOOK ANGLES FOR SATELLITE {0, 5}", ElOpsWrapper.SatNumOf(satKey));

         sw.WriteLine("SEN  TIME                 ELEV    AZ         RNG");
         sw.WriteLine("NUM  YY DDD HHMM SS.SSS   deg     deg        km ");
         sw.WriteLine("---------------------------------------------------");


         currDs50TAI = begDs50TAI;

         // Go through the time span
         while (currDs50TAI <= endDs50TAI)
         {
            // At each time step, loop through all the sensors and check 
            // to see if they have look of the satellite
            for (int i = 0; i < senCount; i++)
            {
               SenSatData senSatData = new SenSatData(senSatKeys[i], senKeys[i], satKey);
               if (senSatData.ErrCode != 0)
               {
                  ErrorMessages.SaveErrorMessage();
                  return;
               }

               LookAngle la = new LookAngle(senSatData, currDs50TAI);
               if (la.ErrCode != 0)
               {
                  ErrorMessages.SaveErrorMessage();
                  return;
               }

               // Check to see if the look is valid
               if (la.LookCode == LamodWrapper.LOOK_VALID)
               {
                  PrintConcurrentLook(sw, senSatData.SenNum, la, 1);
               }
            }

            currDs50TAI = currDs50TAI + (stepSize / Constants.MINSPERDAY);
         }
      }


      // Print looks of a sensor against many satellites
      public static void DoSensor(StreamWriter sw, long senKey, List<long> selectedSatList, bool isExcelCSV) //Sensor/TimeSpance/Satellite
      {
         double currDs50TAI;
         double interval;
         double period;
         double begDs50TAI, endDs50TAI, stepSize;


         int[] satNums = new int[selectedSatList.Count];
         char viewType;
         char obsType;
         int optVisFlg = 0;

         byte[] byteArrValue = new byte[DllMainWrapper.GETSETSTRLEN];


         begDs50TAI = endDs50TAI = stepSize = 0;


         SensorWrapper.SensorGetLimField(senKey, SensorWrapper.XF_SENLIM_VIEWTYPE, byteArrValue);
         //viewType = int.Parse(Utility.BytesToString(byteArrValue));
         viewType = (char)byteArrValue[0];

         SensorWrapper.SensorGetLimField(senKey, SensorWrapper.XF_SENLIM_OBSTYPE, byteArrValue);
         //obsType = int.Parse(Utility.BytesToString(byteArrValue));
         obsType = (char)byteArrValue[0];

         long[] senSatKeys = new long[selectedSatList.Count];


         int satCount = 0;
         int[] ssInt = new int[3];
         double[] ssReal = new double[3];

         // Initialize sen-sat pairs
         foreach (long satKey in selectedSatList)
         {
            int stepMode;
            double startDs50TAI, stopDs50TAI;
            long senSatKey;

            senSatKey = LamodWrapper.LamodInitSenSat(senKey, satKey);
            if (senSatKey <= 0) continue;


            int errCode = LamodWrapper.LamodGetSenSatDataAll(senSatKey, out stepMode, out optVisFlg,
                                                             out startDs50TAI, out stopDs50TAI, out interval, out period);
            if (errCode != 0)
            {

               ErrorMessages.SaveErrorMessage();
               return;
            }


            if (begDs50TAI == 0)
               begDs50TAI = startDs50TAI;

            // store the smallest values of startDs50TAI as start time
            begDs50TAI = Math.Min(begDs50TAI, startDs50TAI);

            if (endDs50TAI == 0)
               endDs50TAI = stopDs50TAI;
            // store the largest value of stopDs50TAIs as end time
            endDs50TAI = Math.Max(endDs50TAI, stopDs50TAI);


            // use the smallest step size
            if (stepSize == 0)
               stepSize = interval;
            stepSize = Math.Min(stepSize, interval);


            senSatKeys[satCount] = senSatKey;
            satNums[satCount] = ElOpsWrapper.SatNumOf(satKey);
            satCount++;
         }

         sw.WriteLine("\n\n\n");
         sw.WriteLine("LOOK ANGLES FOR SENSOR {0, 3}", senKey);

         sw.WriteLine("SAT    TIME                 ELEV    AZ         RNG");
         sw.WriteLine("NUM    YY DDD HHMM SS.SSS   deg     deg        km ");
         sw.WriteLine("---------------------------------------------------");


         currDs50TAI = begDs50TAI;

         // Go through the time span
         while (currDs50TAI <= endDs50TAI)
         {
            // At each time step, loop through all the satellites and check 
            // to see if the sensor has looks of these satellites
            for (int i = 0; i < satCount; i++)
            {
               SenSatData senSatData = new SenSatData(senSatKeys[i], senKey, selectedSatList[i]);
               if (senSatData.ErrCode != 0)
               {
                  ErrorMessages.SaveErrorMessage();
                  return;
               }

               LookAngle la = new LookAngle(senSatData, currDs50TAI);
               if (la.ErrCode != 0)
               {
                  ErrorMessages.SaveErrorMessage();
                  return;
               }

               // Check to see if the look is valid
               if (la.LookCode == LamodWrapper.LOOK_VALID)
               {
                  PrintConcurrentLook(sw, satNums[i], la, 2);
               }
            }

            currDs50TAI = currDs50TAI + (stepSize / Constants.MINSPERDAY);
         }
      }



      public static void PrintSenSatHeader(StreamWriter sw, SenSatData senSatData, Sensor sensor, bool isExcelCSV)
      {
         if (isExcelCSV)
         {
            sw.WriteLine("SatNum,SenNum,DDD.ddd,YY DDD HH MM SS.SSSSSSS,Azimuth_deg,Elevation_deg,Range_km,RangeRate_kmps,RA_deg," +
                         "Declination_degDec,SAA_deg,Illumination,RelVel,RA_hr,RA_min,RA_sec,DeclinationSign,Declination_degInt,Declination_min," +
                         "Declination_sec,SunSenSat_deg,SunEarthSat_deg,SunEarthSen_deg,MooonSenSat_deg,MoonEarthSat_deg," +
                         "MoonEarthSen_deg,OffBoreElev1_deg,OffBoreAzim1_deg,OffBoreElev2_deg,OffBoreAzim2_deg,PassNum,(R)ise/(C)ulm/(S)et");
            return;
         }
         string CSAT;
         string CUN;
         char dash = '-';
         int lineLen;
         int units;
         int elsetNum;
         double ds50UTC;
         int stepModeReq, stepModeUsed;
         char obsType, viewType;
         int optVisFlg;
         int isSpecialSensor;

         Lamod1P.Get1P();

         stepModeReq = (int)Lamod1P.StepMode;
         stepModeUsed = senSatData.StepMode;
         obsType = sensor.ObsType;
         viewType = sensor.ViewType;
         optVisFlg = senSatData.OptVisFlg;
         units = sensor.RngUnits;
         isSpecialSensor = sensor.IsSpecialSensor;



         if (stepModeReq == LamodWrapper.STEPMODE_STEP)
            sw.WriteLine("\n\n+++++ STEP-MODE REQUESTED BY USER +++++\n\n");
         else if (stepModeReq == LamodWrapper.STEPMODE_CULM)
            sw.WriteLine("\n\n+++++ CULMINATION-MODE REQUESTED BY USER +++++\n\n");
         else if (stepModeReq == LamodWrapper.STEPMODE_TASK)
            sw.WriteLine("\n\n+++++ TASK-MODE REQUESTED BY USER +++++\n\n");

         // The stepmode has been changed from what requested
         // DHN 05Jan18 - Use correct variable, stepModeUsed instead of stepModeReq
         //if (stepModeReq != stepModeUsed && stepModeUsed == LamodWrapper.STEPMODE_STEP)
         if (stepModeReq != stepModeUsed && stepModeReq == LamodWrapper.STEPMODE_CULM)

         {
            if (viewType == '9')
               sw.WriteLine("+++++ STEP-MODE INVOKED FOR ORBITING SENSOR +++++\n\n");
            else
               sw.WriteLine("+++++ STEP-MODE INVOKED FOR LARGE PERIOD ORBIT +++++\n\n");
         }


         if (viewType == '9')
            CSAT = "SAT #" + sensor.OrbSatNum;
         else
            CSAT = " ";


         if (units != 0)
            CUN = "nm";
         else
            CUN = "km";


         sw.WriteLine("LOOK ANGLES FOR SATELLITE {0,5}{1,14}SENSOR {2:D3}", senSatData.SatNum, " ", senSatData.SenNum);

         elsetNum = (int)SatStateService.GetSatDataDoubleField(senSatData.SatKey, SatStateWrapper.XF_SAT_ELSETNUM);
         ds50UTC = SatStateService.GetSatDataDoubleField(senSatData.SatKey, SatStateWrapper.XF_SAT_EPOCH);

         sw.WriteLine("EPOCH TIME {0}     ELEMENT SET NO.{1,4}", TimeFuncWrapper.UTCToDtg20Str(ds50UTC), elsetNum);



         if (obsType == '8' || obsType == 'M')
         {
            if (!isExcelCSV) // ascii
            {
               sw.WriteLine("TIME                  ELEV    AZ         RNG      RelVel.  OB        SENSOR POSITION");
               sw.WriteLine("YY DDD HHMM SS.SSS    deg     deg        {0}       km/sec       E km      F km      G km", CUN);
            }
            else
            {
               sw.WriteLine("TIME,ELEV,AZ,RNG,RelVel.,OB,SENSOR POSITION");
               sw.WriteLine("YY DDD HHMM SS.SSS,deg,deg,{0},km/sec,E km,F km,G km", CUN);
            }

            lineLen = 91;
         }
         else if (obsType == '9' || obsType == 'O')
         {
            if (!isExcelCSV) // ascii
            {
               sw.WriteLine("TIME                  ELEV    AZ         RNG      RelVel. RT.ASCEN.     DECLINATION   SOLAR PHASE I OB        SENSOR POSITION");
               sw.WriteLine("YY DDD HHMM SS.SSS    deg     deg        {0}       km/sec  HR MN SEC     deg MA SA     ANGLE (deg) L       E km      F km      G km", CUN);
            }
            else
            {
               sw.WriteLine("TIME,ELEV,AZ,RNG,RelVel.,RT.ASCEN.,,,Sign,DECLINATION,,,SOLAR PHASE,I,OB,SENSOR POSITION");
               sw.WriteLine("YY DDD HHMM SS.SSS,deg,deg,{0},km/sec,HR,MN,SEC,,deg,MA,SA,ANGLE (deg),L,,E km,F km,G km", CUN);
            }

            lineLen = 132;
         }
         else if (obsType == '5')
         {
            if (!isExcelCSV) // ascii
            {
               sw.WriteLine("TIME                  ELEV    AZ         RNG      RNG-RT  RT.ASCEN.     DECLINATION   SOLAR PHASE  I OB");
               sw.WriteLine("YY DDD HHMM SS.SSS    deg     deg        {0}       {1}/sec  HR MN SEC     deg MA SA     ANGLE (deg)  L   ", CUN, CUN);
            }
            else
            {
               sw.WriteLine("TIME,ELEV,AZ,RNG,RNG-RT,RT.ASCEN.,,,Sign,DECLINATION,,,SOLAR PHASE,I,OB");
               sw.WriteLine("YY DDD HHMM SS.SSS,deg,deg,{0},{1}/sec,HR,MN,SEC,,deg,MA,SA,ANGLE (deg),L,,", CUN, CUN);

            }
            lineLen = 103;
         }
         else
         {
            if (!isExcelCSV) //ascii
            {
               sw.WriteLine("TIME                  ELEV    AZ         RNG      RNG-RT   OB");
               sw.WriteLine("YY DDD HHMM SS.SSS    deg     deg        {0}       {1}/sec   ", CUN, CUN);
            }
            else
            {
               sw.WriteLine("TIME,ELEV,AZ,RNG,RNG-RT,OB");
               sw.WriteLine("YY DDD HHMM SS.SSS,deg,deg,{0},{1}/sec", CUN, CUN);
            }
            lineLen = 61;
         }

         if (!isExcelCSV)
            sw.WriteLine(new String(dash, lineLen));
      }


      // Determine if the current print step is rise: 'r', set: 's', culmination: 'c', pass: 'p', or normal step ' '
      private static char CurrPrintStep(double currDs50TAI, double[] rcs, double interval, double stopDs50TAI)
      {
         char currStep = ' ';

         if (Math.Abs(currDs50TAI - rcs[2]) <= LamodWrapper.EPSTIMEDAYS)
            currStep = 's';
         else if (Math.Abs(currDs50TAI - rcs[1]) <= LamodWrapper.EPSTIMEDAYS)
            currStep = 'c';
         else if (Math.Abs(currDs50TAI - rcs[0]) <= LamodWrapper.EPSTIMEDAYS)
            currStep = 'r';
         else if (currDs50TAI + (interval / Constants.MINSPERDAY) > stopDs50TAI)
            currStep = 'p';

         return currStep;
      }

      public static void PrintLook(StreamWriter sw, SenSatData senSatData, Sensor sensor, char currStep, LookAngle la, bool isExcelCSV)
      {
         PrintLook(sw, senSatData, sensor, currStep, la, isExcelCSV, 0.0, 0.0, 0.0, 0.0, 0.0);
      }

      public static void PrintLook(StreamWriter sw, SenSatData senSatData, Sensor sensor, char currStep, LookAngle la, bool isExcelCSV, double halfCone, double anglebtw, double earthSenLimb, double earthSenVic, double earthSenTarg)
      {
         string cobp;

         string formatStr;
         string decSignStr = "";
         char sign = ' '; // only use when writing to csv file since excel doesn't reserve the sign of "-0" number
         char viewType, obsType;
         int optVisFlg;

         viewType = sensor.ViewType;
         obsType = sensor.ObsType;
         optVisFlg = senSatData.OptVisFlg;

         //double[] angArr = new double[5];
         //LamodWrapper.LamodGetViewDataField(senSatData.SenSatKey, LamodWrapper.XF_VIEW_ANGARR, angArr);



         if (la.DecliDeg == 0 && la.Declination_degDec < 0)
         {
            decSignStr = " -0";
            sign = '-';
         }
         else
         {
            decSignStr = String.Format("{0,3}", la.DecliDeg);
            sign = '+';
         }

         cobp = "  ";

         string CTIME = TimeFuncWrapper.UTCToDtg18Str(la.LookTimeDs50UTC);
         if (isExcelCSV)
         {
            formatStr = "{0:D5}," +      // sat num
                         "{1:D3}," +      // sen num
                         "{2,8:F3}," +    // DDD.ddd
                         "{3}," +         // YY DDD HH MM SS.SSS
                         "{4,8:F3}," +    // Azimuth
                         "{5,8:F3}," +    // Elevation
                         "{6,10:F3}," +   // Range
                         "{7,10:F5}," +   // Range Rate
                         "{8,8:F3}," +    // RA_deg
                         "{9,8:F3}," +    // Declination_degDec
                         "{10,8:F3}," +   // SAA_deg
                         "{11,1}," +      // Illumination
                         "{12,10:F5}," +  // RelVel
                         "{13:D2}," +     // RA_hr
                         "{14:D2}," +     // RA_min
                         "{15,6:F3}," +   // RA_sec
                         "{16,1}," +      // Declination sign
                         "{17:D2}," +     // Declination_degInt
                         "{18:D2}," +     // Declination_min
                         "{19,6:F3}," +   // Declination_sec
                         "{20,8:F3}," +   // SunSenSat_deg
                         "{21,8:F3}," +   // SunEarthSat_deg
                         "{22,8:F3}," +   // SunEarthSen_deg
                         "{23,8:F3}," +   // MoonSenSat_deg
                         "{24,8:F3}," +   // MoonEarthSat_deg
                         "{25,8:F3}," +   // MoonEarthSen_deg
                         "{26,8:F3}," +   // OffBoreElev1
                         "{27,8:F3}," +   // OffBoreAzim1
                         "{28,8:F3}," +   // OffBoreElev2
                         "{29,8:F3}," +   // OffBoreAzim2
                         "{30:D3}," +     // PassNum
                         "{31,1}";        // Rise/Set/Culm

            sw.WriteLine(formatStr,
               la.SatNum,              // sat num
               la.SenNum,              // sen num
               la.DDD_ddd,             // DDD.ddd
               la.YYDDDHHMMSS,         // YY DDD HH MM SS.SSS
               la.Azimuth_deg,         // Azimuth
               la.Elevation_deg,       // Elevation
               la.Range_km,            // Range
               la.RangeRate_kmps,      // Range Rate
               la.RA_deg,              // RA_deg
               la.Declination_degDec,  // Declination_degDec
               la.SAA_deg,             // SAA_deg
               la.Illumination,        // Illumination
               la.RelVel,              // RelVel
               la.RAHr,                // RA_hr
               la.RAMin,               // RA_min
               la.RASec,               // RA_sec
               sign,
               la.DecliDeg,            // Declination_degInt
               la.DecliMin,            // Declination_min
               la.DecliSec,            // Declination_sec
               la.SunSenSat_deg,       // SunSenSat_deg
               la.SunEarthSat_deg,     // SunEarthSat_deg
               la.SunEarthSen_deg,     // SunEarthSen_deg
               la.MoonSenSat_deg,      // MoonSenSat_deg
               la.MoonEarthSat_deg,    // MoonEarthSat_deg
               la.MoonEarthSen_deg,    // MoonEarthSen_deg
               la.OffBoreElev1_deg,    // OffBoreElev1
               la.OffBoreAzim1_deg,    // OffBoreAzim1
               la.OffBoreElev2_deg,    // OffBoreElev2
               la.OffBoreAzim2_deg,    // OffBoreAzim2
               la.PassNum,             // PassNum
               char.ToUpper(currStep)); // Rise/Set/Culm

         }
         else if (obsType == '8' || obsType == 'M')
         {
            formatStr = "{0},{1,7:F3},{2,7:F3},{3,10:F3},{4,8:F4},{5},{6,10:F3},{7,10:F3},{8,10:F3},{9,6:F2},{10,6:F2},{11,6:f2}{12,6:F2}";
            sw.WriteLine(formatStr, CTIME, la.Elevation_deg, la.Azimuth_deg, la.Range_km, la.RelVel, cobp, la.SenPosEFG[0], la.SenPosEFG[1], la.SenPosEFG[2], halfCone, anglebtw, earthSenLimb, earthSenTarg, earthSenVic);
         }
         else if (obsType == '9' || obsType == 'O')
         {
            formatStr = "{0}  {1,7:F3}  {2,7:F3} {3,10:F3} {4,8:F3}  {5:D2} {6:D2} {7,6:F3}  {8,3} {9:D2} {10,5:F2}" +
                        "   {11,8:F3}  {12}{13} {14,2}{15,10:F3}{16,10:F3}{17,10:F3} {18,6:F2}{19,7:F2}{20,6:F2}{21,7:F2}";
            sw.WriteLine(formatStr,
            CTIME, la.Elevation_deg, la.Azimuth_deg, la.Range_km, la.RelVel, la.RAHr, la.RAMin, la.RASec, decSignStr, la.DecliMin, la.DecliSec,
            la.SAA_deg, ' ', la.Illumination, cobp, la.SenPosEFG[0], la.SenPosEFG[1], la.SenPosEFG[2], halfCone, anglebtw, earthSenLimb, earthSenTarg, earthSenVic);
         }
         else if (obsType == '5')
         {
            formatStr = "{0}  {1,7:F3}  {2,7:F3} {3,10:F3} {4,8:F3}  {5:D2} {6:D2} {7,6:F3}  {8,3} {9:D2} {10,5:F2}" +
                        " {11,1} {12,8:F3}    {13,1} {14,2} {15,6:F2}{16,6:F2}{17,6:f2}{18,6:F2}";
            sw.WriteLine(formatStr,
            CTIME, la.Elevation_deg, la.Azimuth_deg, la.Range_km, la.RangeRate_kmps, la.RAHr, la.RAMin, la.RASec, decSignStr, la.DecliMin,
            la.DecliSec, currStep, la.SAA_deg, la.Illumination, cobp, halfCone, anglebtw, earthSenLimb, earthSenTarg, earthSenVic);
         }
         else
         {
            formatStr = "{0}  {1,7:F3}  {2,7:F3} {3,10:F3} {4,8:F3} {5} {6,2}{7,6:f2}{8,6:F2}{9,6:f2}{10,6:F2}";
            sw.WriteLine(formatStr, CTIME, la.Elevation_deg, la.Azimuth_deg, la.Range_km, la.RangeRate_kmps, currStep, cobp, halfCone, anglebtw, earthSenLimb, earthSenTarg, earthSenVic);
         }
      }



      // format =  1: for sensor, 2: for satellite

      private static void PrintConcurrentLook(StreamWriter sw, int idNum, LookAngle la, int format)
      {
         string formatStr = "";

         string CTIME = TimeFuncWrapper.UTCToDtg18Str(la.LookTimeDs50UTC);

         if (format == 1)
            formatStr = "{0,3} {1}  {2,7:F3}  {3,7:F3} {4,10:F3}";
         else if (format == 2)
            formatStr = "{0,5} {1}  {2,7:F3}  {3,7:F3} {4,10:F3}";

         sw.WriteLine(formatStr, idNum, CTIME, la.Elevation_deg, la.Azimuth_deg, la.Range_km);
      }



      private static void PrintSummary(StreamWriter sw, int senNum, int satNum, int totalLookCount, int numPasses)
      {
         sw.WriteLine("");
         sw.WriteLine("      Summary For Satellite {0,5} and Sensor {1,3}:", satNum, senNum);
         sw.WriteLine("      ------------------------------------------");
         if (totalLookCount == 0)
            sw.WriteLine("      *** No Look Angles Found ***");
         else
         {
            sw.WriteLine("      {0, 6} look angle(s) printed", totalLookCount);
            sw.WriteLine("        {0, 4} pass(es) computed", numPasses);
         }

         //sw.Write("           0 observation(s) generated\n");
         sw.WriteLine("      ------------------------------------------");

      }



      private static void PrintNosticdata(StreamWriter sw, SenSatData senSatData, char viewType, LookAngle la)
      {
         // Print sensor/satellite number at each step
         sw.WriteLine("");
         sw.WriteLine("   >>> Sensor: {0,3:D} - Satellite: {1, 6:D} - Time: {2,20} - Mse: {3,14:F6}",
            senSatData.SenNum,
            senSatData.SatNum,
            TimeFuncWrapper.UTCToDtg20Str(la.LookTimeDs50UTC),
            la.LookTimeMSE);


         // Print pass-by-pass diagnostics
         //if (nosticFlg == 2 || nosticFlg == 3)
         {
            if (la.LookCode == LamodWrapper.LOOK_VALID)
               sw.WriteLine("    +++ The sensor/satellite PASSED all limit tests +++");
            else if (la.LookCode == LamodWrapper.LOOK_HTEST)
               sw.WriteLine("    --- The sensor/satellite FAILED horizon break test ---");
            else
            {
               //check for ground-based optical illumination: sat must be in sunlight & sensor in darkness
               if (la.Illumination == 'N')
                  sw.WriteLine("    --- The sensor/satellite FAILED illumination test (sensor isn't in darkness & sensor isn't sunlit)---");
               else if (la.Illumination == 'A')
                  sw.WriteLine("    --- The sensor/satellite FAILED illumination test (satellite isn't sunlit)---");
               else if (la.Illumination == 'E')
                  sw.WriteLine("    --- The sensor/satellite FAILED illumination test (sensor isn't in darkness)---");
               PrintPassByPassNostic(sw, senSatData.SenSatKey, viewType);
            }
         }


         // Print sensor/satellite position at each look if requested
         //if (la.LookCode == LamodWrapper.LOOK_VALID && (Lamod1P.DiagMode == 1 || Lamod1P.DiagMode == 3))
         //if (Lamod1P.DiagMode != 0) // DHN 28Dec17 - allow bad look to be printed out in diagnostic mode
         PrintSenSatPosition(sw, viewType, la);
      }


      // Print pass-by-pass diagnostics
      private static void PrintPassByPassNostic(StreamWriter sw, long senSatKey, char viewType)
      {
         double[] limFlgArr = new double[12];
         string msgStrFormat = "    --- The sensor/satellite FAILED {0} test ---";

         LamodWrapper.LamodGetViewDataField(senSatKey, LamodWrapper.XF_VIEW_LIMFLGS, limFlgArr);

         // --------- check for failed range limits
         if (limFlgArr[LamodWrapper.XA_LIMFLG_MINRANGE] == 0)
            sw.WriteLine(msgStrFormat, "minimum range");

         if (limFlgArr[LamodWrapper.XA_LIMFLG_MAXRANGE] == 0)
            sw.WriteLine(msgStrFormat, "max range");

         /*
         // --------- visibility checks: earth-based sensors...
         if (limFlgArr[LamodWrapper.XA_LIMFLG_Visual] == 0)
            sw.WriteLine(msgStrFormat, "illumination");
         */


         // --------- orbiting sensors: check for eclipse conditions
         if (viewType == '9')
            if (limFlgArr[LamodWrapper.XA_LIMFLG_UMBECLIP] == 0)
               sw.WriteLine(msgStrFormat, "umbral eclipse");
            else if (limFlgArr[LamodWrapper.XA_LIMFLG_PENECLIP] == 0)
               sw.WriteLine(msgStrFormat, "penum. eclipse");

         // --------- pass failed sensor az limit
         if (limFlgArr[LamodWrapper.XA_LIMFLG_AZLIM] == 0)
            sw.WriteLine(msgStrFormat, "az/ra limit");

         // --------- pass failed sensor el limit
         if (limFlgArr[LamodWrapper.XA_LIMFLG_ELLIM] == 0)
            sw.WriteLine(msgStrFormat, "el/dec limit");


         // --------- pass failed solar aspect angle test
         if (limFlgArr[LamodWrapper.XA_LIMFLG_SAALIM] == 0)
            sw.WriteLine(msgStrFormat, "solar aspect angle");

         // --------- pass failed range-rate/relative velocity limit
         if (limFlgArr[LamodWrapper.XA_LIMFLG_RVELLIM] == 0)
            sw.WriteLine(msgStrFormat, "rge-rt/rel-vel limit");

         // --------- for sat-to-sat, check for planetary obstructions: earth first
         //!CVIS2 = " "
         if (limFlgArr[LamodWrapper.XA_LIMFLG_EARTHOB] == 0)
            sw.WriteLine(msgStrFormat, "earth obstruction");
         else if (limFlgArr[LamodWrapper.XA_LIMFLG_EARTHBK] == 0)
            sw.WriteLine(msgStrFormat, "earth in background");
         // dhn 28Mar07 - Turn of this output because it doesn"t work
         //!else
         //   CVIS2 = "E" 

         // --------- ..., check for sun too close to observed satellite
         if (limFlgArr[LamodWrapper.XA_LIMFLG_SOLEXCL] == 0)
            sw.WriteLine(msgStrFormat, "solar exclusion");

         // --------- ...finally, check for moon too close to observation
         if (limFlgArr[LamodWrapper.XA_LIMFLG_LUNEXCL] == 0)
            sw.WriteLine(msgStrFormat, "lunar exclusion");

         // DHN 02Jan18 - extra lunar exclusion print statement
         // --------- for optical ground, check for moon too close to observation 10DEC04
         //if (limFlgArr[LamodWrapper.XA_LIMFLG_LUNEXCL] == 0)
         //   sw.WriteLine(msgStrFormat, "lunar exclusion");
      }


      // Print sensor/satellite positions at the time the look is computed
      private static void PrintSenSatPosition(StreamWriter sw, char viewType, LookAngle la)
      {
         sw.WriteLine("    Sensor Position: X   ={0,10:F3} km;  Y   ={1,10:F3} km;  Z   ={2,10:F3} km ", la.SenPosECI.X_km, la.SenPosECI.Y_km, la.SenPosECI.Z_km);
         sw.WriteLine("           Velocity: XD  ={0,10:F5} k/s; YD  ={1,10:F5} k/s; ZD  ={2,10:F5} k/s", la.SenVelECI.XD_kmps, la.SenVelECI.YD_kmps, la.SenVelECI.ZD_kmps);
         sw.WriteLine("           Geod    : nLat={0,10:F5} deg; eLon={1,10:F5} deg; Hght={2,10:F3} km ", la.SenLLH.Latitude_deg, la.SenLLH.Longitude_deg, la.SenLLH.Height_km);
         sw.WriteLine("    Sat Position   : X   ={0,10:F3} km;  Y   ={1,10:F3} km;  Z   ={2,10:F3} km ", la.SatPosECI.X_km, la.SatPosECI.Y_km, la.SatPosECI.Z_km);
         sw.WriteLine("        Velocity   : XD  ={0,10:F5} k/s; YD  ={1,10:F5} k/s; ZD  ={2,10:F5} k/s", la.SatVelECI.XD_kmps, la.SatVelECI.YD_kmps, la.SatVelECI.ZD_kmps);
         sw.WriteLine("        Geod       : nLat={0,10:F5} deg; eLon={1,10:F5} deg; Hght={2,10:F3} km ", la.SatLLH.Latitude_deg, la.SatLLH.Longitude_deg, la.SatLLH.Height_km);

         // For orbiting sensors
         if (viewType == '9')
         {
            sw.WriteLine("    Solar Unit Vec : X   ={0,14:F8}      Y   ={1,14:F8}      Z   ={2,14:F8}    ",
               la.SunVec[0], la.SunVec[1], la.SunVec[2]);
            sw.WriteLine("    Lunar Unit Vec : X   ={0,14:F8}      Y   ={1,14:F8}      Z   ={2,14:F8}    ",
               la.MoonVec[0], la.MoonVec[1], la.MoonVec[2]);
            sw.WriteLine("    Angles (deg): Sun/sen/sat ={0,8:F3}; Sun/earth/sat ={1,8:F3}; Sun/earth/sen ={2,8:F3}; Moon/sen/sat ={3,8:F3}",
               la.SunSenSat_deg, la.SunEarthSat_deg, la.SunEarthSen_deg, la.MoonSenSat_deg);

            if (la.NBore1 > 0)
            {
               sw.WriteLine("    Off-boresight & clock angles (#1) ={0,8:F3} & {1,8:F3} deg, ",
                  la.OffBoreElev1_deg, la.OffBoreAzim1_deg);

               if (la.NBore2 > 0 && la.NBore2 < 7)
                  sw.WriteLine("    Off-boresight & clock angles (#2) ={0,8:F3} & {1,8:F3} deg, ",
                     la.OffBoreElev2_deg, la.OffBoreAzim2_deg);
            }
         }

         sw.WriteLine("");
      }

      public static List<LookAngle> GetLamodLooks()
      {
         return lamodLooks;
      }


      public static void ClearLamodLooks()
      {
         lamodLooks.Clear();
      }

   }
}
