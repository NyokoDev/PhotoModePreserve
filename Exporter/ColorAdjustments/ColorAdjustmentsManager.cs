using Colossal.UI;
using Game.Prefabs.Climate;
using Game.Rendering;
using System;
using System.IO;
using System.Reflection;
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


 
            public FloatParameter LocalPostExposure { get; set; }
            public ClampedFloatParameter LocalContrast { get; set; }
            public ColorParameter LocalColorFilter { get; set; }
            public ClampedFloatParameter LocalHueShift { get; set; }
            public ClampedFloatParameter LocalSaturation { get; set; }


        public bool completed;





        // Method to serialize values to XML
        public void SerializeToXML()

            
        {
            if (completed)
            {
                string assemblyDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(assemblyDirectory, "ColorAdjustmentsProperties.xml");

                XmlDocument xmlDoc = new XmlDocument();

                XmlElement root = xmlDoc.CreateElement("ColorAdjustmentsProperties");
                xmlDoc.AppendChild(root);

                XmlElement postExposureElem = xmlDoc.CreateElement("PostExposure");
                postExposureElem.InnerText = LocalPostExposure.ToString();
                root.AppendChild(postExposureElem);

                XmlElement contrastElem = xmlDoc.CreateElement("Contrast");
                contrastElem.InnerText = LocalContrast.ToString();
                root.AppendChild(contrastElem);

                XmlElement colorFilterElem = xmlDoc.CreateElement("ColorFilter");
                XmlAttribute colorAttr = xmlDoc.CreateAttribute("value");
                colorAttr.Value = ColorUtility.ToHtmlStringRGBA(LocalColorFilter.value); // Access the color value from ColorParameter
                colorFilterElem.Attributes.Append(colorAttr);
                root.AppendChild(colorFilterElem);



                XmlElement hueShiftElem = xmlDoc.CreateElement("HueShift");
                hueShiftElem.InnerText = LocalHueShift.ToString();
                root.AppendChild(hueShiftElem);

                XmlElement saturationElem = xmlDoc.CreateElement("Saturation");
                saturationElem.InnerText = LocalSaturation.ToString();
                root.AppendChild(saturationElem);

                xmlDoc.Save(filePath);
                Console.WriteLine($"Values serialized to {filePath}");
            }
            else
                UnityEngine.Debug.Log("Completed is not true");
        }


    }
    
}