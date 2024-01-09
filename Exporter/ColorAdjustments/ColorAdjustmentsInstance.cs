using Game.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine.Rendering.HighDefinition;
using Log = NyokoLogging.LoggerNyoko;



namespace PhotoModePreserve.Exporter
{
    internal class ColorAdjustmentsInstance : SystemBase
    {
        private ColorAdjustments adjustments;

        public ColorAdjustments GetColorAdjustments()
        {
            // Create an instance of the LightingSystem class
            LightingSystem lightingSystemInstance = World.GetOrCreateSystemManaged<LightingSystem>();

            // Check if lightingSystemInstance is null
            if (lightingSystemInstance == null)
            {
                Log.LogStringToFile("LightingSystem instance is null.");
                
            }

            // Get the type of LightingSystem
            Type type = lightingSystemInstance.GetType();

            // Get the field information for the instance field "m_ColorAdjustments"
            FieldInfo fieldInfo = type.GetField("m_ColorAdjustments", BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo != null)
            {
                // Access the field value using the lightingSystemInstance
                adjustments = (ColorAdjustments)fieldInfo.GetValue(lightingSystemInstance);
                if (adjustments != null)
                {
                    Log.LogStringToFile("FIELD FOUND: " + adjustments.ToString());
                    return adjustments;
                }
                else
                {
                    Log.LogStringToFile("Field 'm_ColorAdjustments' found but value is null.");
                    return null; // or handle the situation according to your logic
                }
            }
            else
            {
                Log.LogStringToFile("CRITICAL: Field 'm_ColorAdjustments' NOT FOUND.");
                return null; // or handle the situation according to your logic
            }
        
    
}


protected override void OnUpdate()
        {
      
        }
    }
}