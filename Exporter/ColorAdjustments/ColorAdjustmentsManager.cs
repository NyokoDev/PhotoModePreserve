using Game.Prefabs.Climate;
using Game.Rendering;
using System;
using System.IO;
using System.Xml;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Log = NyokoLogging.LoggerNyoko;

namespace PhotoModePreserve.Exporter.ColorAdjustmentsEnsurance
{

    public class ColorAdjustmentsManager
    {
        ColorAdjustmentsInstance instance = new ColorAdjustmentsInstance();
        public ColorAdjustments properties;

        public FloatParameter PostExposure { get; set; }
        public ClampedFloatParameter Contrast { get; set; }
        public ColorParameter ColorFilter { get; set; }
        public ClampedFloatParameter HueShift { get; set; }
        public ClampedFloatParameter Saturation { get; set; }

        public void SetProperties()
        {

            PostExposure = properties.postExposure;
            Contrast = properties.contrast;
            ColorFilter = properties.colorFilter;
            HueShift = properties.hueShift;
            Saturation = properties.saturation;
        }

     
        // Method to serialize values to XML
        public void SerializeToXML()
        {
            string assemblyDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(assemblyDirectory, "ColorAdjustmentsProperties.xml");

            XmlDocument xmlDoc = new XmlDocument();

            XmlElement root = xmlDoc.CreateElement("ColorAdjustmentsProperties");
            xmlDoc.AppendChild(root);

            XmlElement postExposureElem = xmlDoc.CreateElement("PostExposure");
            postExposureElem.InnerText = PostExposure.ToString();
            root.AppendChild(postExposureElem);

            XmlElement contrastElem = xmlDoc.CreateElement("Contrast");
            contrastElem.InnerText = Contrast.ToString();
            root.AppendChild(contrastElem);

            XmlElement colorFilterElem = xmlDoc.CreateElement("ColorFilter");
            XmlAttribute colorAttr = xmlDoc.CreateAttribute("value");
            colorAttr.Value = ColorUtility.ToHtmlStringRGBA(ColorFilter.value); // Access the color value from ColorParameter
            colorFilterElem.Attributes.Append(colorAttr);
            root.AppendChild(colorFilterElem);



            XmlElement hueShiftElem = xmlDoc.CreateElement("HueShift");
            hueShiftElem.InnerText = HueShift.ToString();
            root.AppendChild(hueShiftElem);

            XmlElement saturationElem = xmlDoc.CreateElement("Saturation");
            saturationElem.InnerText = Saturation.ToString();
            root.AppendChild(saturationElem);

            xmlDoc.Save(filePath);
            Console.WriteLine($"Values serialized to {filePath}");
        }

       
    }
    
}