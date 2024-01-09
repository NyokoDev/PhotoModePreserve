using Game.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.HighDefinition;

namespace PhotoModePreserve.Exporter
{
    internal class ColorAdjustmentsInstance
    {
        private ColorAdjustments adjustments;

        public ColorAdjustments GetColorAdjustments()
        {
            // Create an instance of the LightingSystem class
            LightingSystem lightingSystemInstance = new LightingSystem();

            // Get the type of LightingSystem
            Type type = lightingSystemInstance.GetType();

            // Get the field information for the instance field "m_ColorAdjustments"
            FieldInfo fieldInfo = type.GetField("m_ColorAdjustments", BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo != null)
            {
                // Access the field value using the lightingSystemInstance
                adjustments = (ColorAdjustments)fieldInfo.GetValue(lightingSystemInstance);
                UnityEngine.Debug.Log("FIELD FOUND: " + adjustments);
            }
            else
            {
                UnityEngine.Debug.Log("CRITICAL: NOT FOUND");
            }

            return adjustments;
        }
    }
}