using System.Collections.Generic;
using System.Xml;
using AFSPC.AstroStds.Wrappers;

namespace AFSPC.AstroStds.Models
{
   public static class XmlEphemReader
   {
      public static int ReadFile(string xmlEphemFile)
      {
         //bool readJobID = false;
         bool readEphemerisRecords = false;
         bool readCovarianceData = false;
         
         //XmlReader reader = XmlReader.Create("products.xml");
         XmlReader reader = XmlReader.Create(xmlEphemFile);
         int i = 0;
         List<string> ephemTime = new List<string>();
         List<double> posXs = new List<double>();
         List<double> posYs = new List<double>();
         List<double> posZs = new List<double>();
         List<double> velXs = new List<double>();
         List<double> velYs = new List<double>();
         List<double> velZs = new List<double>();
         List<double[]> covMtxs = new List<double[]>();
         int satNum = 0;
         string coordStr;

         while(reader.Read())
         {
            if (reader.NodeType == XmlNodeType.Element)
            {
               if (reader.LocalName == "objectID")
               {
                  //readJobID = true;
                  satNum = reader.ReadElementContentAsInt();
               }
               else if (reader.LocalName == "coordinateAxes")
               {
                  coordStr = reader.ReadElementContentAsString(); // "j2000", "trueOfDate", "icrf", "fixed", "temeOfDate"
               }
               else if (reader.LocalName == "ephemerisRecords")
               {
                  readEphemerisRecords = true;
               }
               else if (reader.LocalName == "covarianceData")
               {
                  readCovarianceData = true;
                  readEphemerisRecords = false;
               }
               else if (readEphemerisRecords && reader.LocalName == "epoch")
               {
                  ephemTime.Add(reader.ReadElementContentAsString());
                  i = 0;
               }
               else if (readEphemerisRecords && (reader.LocalName == "x" || reader.LocalName == "y" || reader.LocalName == "z"))
               {
                  if (i % 6 == 0)
                     posXs.Add(reader.ReadElementContentAsDouble());
                  else if (i % 6 == 1)
                     posYs.Add(reader.ReadElementContentAsDouble());
                  else if (i % 6 == 2)
                     posZs.Add(reader.ReadElementContentAsDouble());
                  else if (i % 6 == 3)
                     velXs.Add(reader.ReadElementContentAsDouble());
                  else if (i % 6 == 4)
                     velYs.Add(reader.ReadElementContentAsDouble());
                  else if (i % 6 == 5)
                     velZs.Add(reader.ReadElementContentAsDouble());
                  i++;
               }
               else if (readCovarianceData && reader.LocalName == "covariance") // "xyz", "ric", "fgEquinoctial", "ntc"
               {
                  double[] covMtx = ReadCovMtx(reader);
                  covMtxs.Add(covMtx);
               }
               
            }
         }

         DaysSince50UTC epochDs50UTC = new DaysSince50UTC(ephemTime[0]);
         long satKey = ExtEphemWrapper.ExtEphAddSat(satNum, epochDs50UTC.Ds50UTC, 0.0, 0.0, ExtEphemWrapper.COORD_J2K); // coordSys);

         for(int k = 0; k < ephemTime.Count; k++)
         {
            DaysSince50UTC ephemDs50UTC = new DaysSince50UTC(ephemTime[k]);
            double[] pos = new double[3] { posXs[k] / 1000.0, posYs[k] / 1000.0, posZs[k] / 1000.0 };
            double[] vel = new double[3] { velXs[k] / 1000.0, velYs[k] / 1000.0, velZs[k] / 1000.0 };
            ExtEphemWrapper.ExtEphAddSatEphemCovMtx(satKey, ephemDs50UTC.Ds50UTC, pos, vel, 0, covMtxs[k]);
         }

         return 0;

         ////XElement xelement = XElement.Load("products.xml");
         //XElement xelement = XElement.Load("29735_j2k_3mins_Oct16_2015.xml");

         //IEnumerable<XElement> products = xelement.Elements();
         //foreach(var product in products)
         //{
         //   Console.WriteLine("{0}  {1}", product.Element("id").Value, product.Element("name").Value);
         //}
      }

      static private double[] ReadCovMtx(XmlReader reader)
      {
         double[] covMtx = new double[21];

         int i;

         i = 0;
         while(reader.Read() && i < 21)
         {
            // velocityTerms can be null
            if (reader.LocalName == "velocityTerms" && reader.IsEmptyElement) 
               break;

            if (reader.NodeType == XmlNodeType.Element && reader.LocalName != "velocityTerms")
            {
               covMtx[i] = reader.ReadElementContentAsDouble();
               i++;
            }
         }

         return covMtx;
      }

      static private int StrToCoord(string strCoord)
      {
         switch (strCoord)
         {
            case "j2000":
               return ExtEphemWrapper.COORD_J2K;
            case "temeOfDate":
               return ExtEphemWrapper.COORD_ECI;
            case "icrf":
               return ExtEphemWrapper.COORD_ECR;
            case "fixed":
               return ExtEphemWrapper.COORD_EFG;
            default:
               return ExtEphemWrapper.COORD_INVALID;
         }
      }
   }
}
