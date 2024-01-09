using Game.Rendering;
using JetBrains.Annotations;
using PhotoModePreserve;
using PhotoModePreserve.Exporter.ColorAdjustmentsEnsurance;
using System;
using System.CodeDom;
using System.Reflection;
using Unity.Entities;
using UnityEngine.Rendering.HighDefinition;

internal class ColorAdjustmentsInstance : SystemBase
{
    public ColorAdjustments adjustments;
    ColorAdjustmentsManager manager;



    public void GetColorAdjustments()
    {
        LightingSystem lightingSystemInstance = World.GetOrCreateSystemManaged<LightingSystem>();

        if (lightingSystemInstance == null)
        {
            LogAndAbort("LightingSystem instance is null.");
            
        }

        ColorAdjustments GetInstance()
        {
            return adjustments;
        }



        Type type = lightingSystemInstance.GetType();
        FieldInfo fieldInfo = type.GetField("m_ColorAdjustments", BindingFlags.NonPublic | BindingFlags.Instance);

        if (fieldInfo != null)
        {
            adjustments = (ColorAdjustments)fieldInfo.GetValue(lightingSystemInstance);
            if (adjustments != null)
            {
                LogInfo("FIELD FOUND: " + adjustments.ToString());
                manager.properties = adjustments;
            }
        }

        LogAndAbort("Field 'm_ColorAdjustments' not found or value is null.");
      
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
        
    }
}
