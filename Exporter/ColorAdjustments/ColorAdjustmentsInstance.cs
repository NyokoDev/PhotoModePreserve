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

        MemberInfo[] members = typeof(ColorAdjustments).GetMembers(BindingFlags.Public | BindingFlags.Instance);

        foreach (MemberInfo member in members)
        {
            string memberName = member.Name;

            // Check if the member is a field
            if (member.MemberType == MemberTypes.Field)
            {
                FieldInfo field = (FieldInfo)member;

                // Get the value of each field from the ColorAdjustments instance
                object fieldValue = field.GetValue(colorAdjustmentsInstance);

                // Log field values for debugging
                UnityEngine.Debug.Log($"Field: {memberName}, Value: {fieldValue}");

                // Assign values to local properties based on extracted values
                switch (memberName)
                {
                    case "postExposure":
                        if (fieldValue is FloatParameter postExposureValue)
                        {
                            manager.LocalPostExposure = postExposureValue;
                            UnityEngine.Debug.Log($"Assigned LocalPostExposure: {fieldValue}");
                        }
                        break;
                    case "contrast":
                        if (fieldValue is ClampedFloatParameter contrastValue)
                        {
                            manager.LocalContrast = contrastValue;
                            UnityEngine.Debug.Log($"Assigned LocalContrast: {fieldValue}");
                        }
                        break;
                    case "colorFilter":
                        if (fieldValue is ColorParameter colorFilterValue)
                        {
                            manager.LocalColorFilter = colorFilterValue;
                            UnityEngine.Debug.Log($"Assigned LocalColorFilter: {fieldValue}");
                        }
                        break;
                    case "hueShift":
                        if (fieldValue is ClampedFloatParameter hueShiftValue)
                        {
                            manager.LocalHueShift = hueShiftValue;
                            UnityEngine.Debug.Log($"Assigned LocalHueShift: {fieldValue}");
                        }
                        break;
                    case "saturation":
                        if (fieldValue is ClampedFloatParameter saturationValue)
                        {
                            manager.LocalSaturation = saturationValue;
                            UnityEngine.Debug.Log($"Assigned LocalSaturation: {fieldValue}");
                        }
                        break;
                    // Add cases for other members if needed
                    default:
                        UnityEngine.Debug.Log($"Member {memberName} not handled.");
                        break;
                }
            }
            
        }

        manager.completed = true;
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
