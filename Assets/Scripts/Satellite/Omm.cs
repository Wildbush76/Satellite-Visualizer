using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Zeptomoby.OrbitTools;
using UnityEngine;


namespace Assets.Scripts.Satellite
{
    public class Omm : OrbitalElements
    {

        private static readonly double Deg2Rad = Math.PI / 180.0;
        public string Classification { get; protected set; }

        public static Omm Parse(XmlNode ommItem)
        {
            Omm omm = new();

            XmlNode tleMetaDataNode = ommItem.SelectSingleNode("body/segment/metadata");
            XmlNode tleMeanElementsNode = ommItem.SelectSingleNode("body/segment/data/meanElements");
            XmlNode tleParametersNode = ommItem.SelectSingleNode("body/segment/data/tleParameters");




            //metadata
            omm.SatelliteName = tleMetaDataNode.SelectSingleNode("OBJECT_NAME").InnerText;
            omm.IntlDesignatorStr = tleMetaDataNode.SelectSingleNode("OBJECT_ID").InnerText;
            string timeSystem = tleMetaDataNode.SelectSingleNode("TIME_SYSTEM").InnerText;
            string model = tleMetaDataNode.SelectSingleNode("MEAN_ELEMENT_THEORY").InnerText;

            if (!model.Equals("SGP4") && !model.Equals("SGP"))
            {
                Debug.LogWarning($"Object {omm.SatelliteName} is {model} and not SGP");
            }

            //meanElements

            DateTime epoch = DateTime.Parse(tleMeanElementsNode.SelectSingleNode("EPOCH").InnerText);
            omm.Epoch = new Julian(epoch);

            omm.MeanMotion = ParseStringToDouble(tleMeanElementsNode.SelectSingleNode("MEAN_MOTION").InnerText);
            omm.Eccentricity = ParseStringToDouble(tleMeanElementsNode.SelectSingleNode("ECCENTRICITY").InnerText);

            omm.InclinationDeg = ParseStringToDouble(tleMeanElementsNode.SelectSingleNode("INCLINATION").InnerText);
            omm.InclinationRad = Deg2Rad * omm.InclinationDeg;


            omm.RAANodeDeg = ParseStringToDouble(tleMeanElementsNode.SelectSingleNode("RA_OF_ASC_NODE").InnerText);
            omm.RAANodeRad = Deg2Rad * omm.RAANodeDeg;

            omm.ArgPerigeeDeg = ParseStringToDouble(tleMeanElementsNode.SelectSingleNode("ARG_OF_PERICENTER").InnerText);
            omm.ArgPerigeeRad = Deg2Rad * omm.ArgPerigeeDeg;

            omm.MeanAnomalyDeg = ParseStringToDouble(tleMeanElementsNode.SelectSingleNode("MEAN_ANOMALY").InnerText);
            omm.MeanAnomalyRad = Deg2Rad * omm.MeanAnomalyDeg;

            //tleParameters
            omm.Classification = tleParametersNode.SelectSingleNode("CLASSIFICATION_TYPE").InnerText;
            omm.NoradIdStr = tleParametersNode.SelectSingleNode("NORAD_CAT_ID").InnerText;
            omm.SetNumber = Int32.Parse(tleParametersNode.SelectSingleNode("ELEMENT_SET_NO").InnerText);
            omm.RevAtEpoch = Int32.Parse(tleParametersNode.SelectSingleNode("REV_AT_EPOCH").InnerText);
            omm.BStar = ParseStringToDouble(tleParametersNode.SelectSingleNode("BSTAR").InnerText);
            omm.MeanMotionDt = ParseStringToDouble(tleParametersNode.SelectSingleNode("MEAN_MOTION_DOT").InnerText);

            /*
                NOT NEEDED
                double secondMeanMotion = ParseStringToDouble(tleParametersNode.SelectSingleNode("MEAN_MOTION_DDOT").InnerText);
                string refFrame = tleMetaDataNode.SelectSingleNode("REF_FRAME").InnerText;
                string centerName = tleMetaDataNode.SelectSingleNode("CENTER_NAME").InnerText;
                double ephemeris = ParseStringToDouble(tleParametersNode.SelectSingleNode("EPHEMERIS_TYPE").InnerText);
            */

            return omm;
        }

        public static List<Omm> Parse(XmlDocument document)
        {
            XmlNodeList ommNodes = document.GetElementsByTagName("omm");
            List<Omm> ommList = new();

            foreach (XmlNode ommNode in ommNodes)
            {
                ommList.Add(Parse(ommNode));
            }

            return ommList;
        }

        private static double ParseStringToDouble(string rawValue)
        {
            return double.Parse(rawValue, CultureInfo.InvariantCulture);
        }
    }


}