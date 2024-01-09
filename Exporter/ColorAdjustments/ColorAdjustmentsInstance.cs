using Game.Prefabs;
using Game.Rendering;
using JetBrains.Annotations;
using PhotoModePreserve;
using PhotoModePreserve.Exporter;
using PhotoModePreserve.Exporter.ColorAdjustmentsEnsurance;
using System;
using System.CodeDom;
using System.Reflection;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

internal class ColorAdjustmentsInstance : SystemBase
{


    ColorAdjustmentsManager manager = new ColorAdjustmentsManager();

    public void ExtractAndSetLocalProperties()
    {
        LightingSystem lightingSystem = World.GetOrCreateSystemManaged<LightingSystem>();

        if (lightingSystem == null)
        {
            UnityEngine.Debug.Log("LightingSystem not found or not initialized.");
            return;
        }

        FieldInfo colorAdjustmentsField = typeof(LightingSystem).GetField("m_ColorAdjustments", BindingFlags.NonPublic | BindingFlags.Instance);

        if (colorAdjustmentsField == null)
        {
            UnityEngine.Debug.Log("m_ColorAdjustments field not found in LightingSystem.");
            return;
        }

        ColorAdjustments colorAdjustmentsInstance = (ColorAdjustments)colorAdjustmentsField.GetValue(lightingSystem);

        if (colorAdjustmentsInstance == null)
        {
            UnityEngine.Debug.Log("ColorAdjustments instance not found or not initialized in LightingSystem.");
            return;
        }

        PropertyInfo[] properties = typeof(ColorAdjustments).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        

        foreach (PropertyInfo property in properties)
        {
            string propertyName = property.Name;

            // Filter out unwanted properties
            if (propertyName == "hideFlags" || propertyName == "displayName" || propertyName == "parameters" || propertyName == "name")
            {
                UnityEngine.Debug.Log($"Property {propertyName} skipped.");
                continue;
            }

            // Get the value of each property from the ColorAdjustments instance
            object propertyValue = property.GetValue(colorAdjustmentsInstance);

            // Log property values for debugging
            UnityEngine.Debug.Log($"Property: {propertyName}, Value: {propertyValue}");

            // Assign values to local properties based on extracted values
            switch (propertyName)
            {
                case "postExposure":
                    manager.LocalPostExposure = (FloatParameter)propertyValue;
                    UnityEngine.Debug.Log($"Assigned LocalPostExposure: {propertyValue}");
                    break;
                case "contrast":
                    manager.LocalContrast = (ClampedFloatParameter)propertyValue;
                    UnityEngine.Debug.Log($"Assigned LocalContrast: {propertyValue}");
                    break;
                case "colorFilter":
                    manager.LocalColorFilter = (ColorParameter)propertyValue;
                    UnityEngine.Debug.Log($"Assigned LocalColorFilter: {propertyValue}");
                    break;
                case "hueShift":
                    manager.LocalHueShift = (ClampedFloatParameter)propertyValue;
                    UnityEngine.Debug.Log($"Assigned LocalHueShift: {propertyValue}");
                    break;
                case "saturation":
                    manager.LocalSaturation = (ClampedFloatParameter)propertyValue;
                    UnityEngine.Debug.Log($"Assigned LocalSaturation: {propertyValue}");
                    break;
                // Add cases for other properties if needed
                default:
                    UnityEngine.Debug.Log($"Property {propertyName} not handled.");
                    break;
            }
        }
    }
    


    private void LogInfo(string message)
    {
        Mod.Instance.Log.Info(message);
        UnityEngine.Debug.Log(message);
    }

    private void LogAndAbort(string message)
    {
        string exceptionMessage = "Operation aborted: " + message;
        LogInfo(exceptionMessage);
        throw new InvalidOperationException(exceptionMessage);
    }

    protected override void OnUpdate()
    {
        ExtractAndSetLocalProperties();
        manager.SerializeToXML();
    }
}
