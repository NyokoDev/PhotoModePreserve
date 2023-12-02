using Game.UI.InGame;
using Game.UI;
using System;
using System.Reflection;
using System.Reflection.Emit;
using Game.Rendering;
using Colossal.UI.Binding;
using Game.Tools;
using Game.Simulation;
using Unity.Entities;
using Game.UI.Widgets;
using System.Collections.Generic;
using Game;
using Game.Rendering.CinematicCamera;

namespace DynamicResolutionExtended.Systems
{

    
    public partial class DyUISystem
    {
        private IEnumerable<KeyValuePair<string, PhotoModeProperty>> photoModeProperties;

        public static DyUISystem Instance { get; private set; }
        public void DisableAllCameraProperties()
        {
            foreach (KeyValuePair<string, PhotoModeProperty> photoModeProperty in photoModeProperties)
            {
                photoModeProperty.Value.setEnabled.Invoke(obj: true);
            
            }
        }


    }
}
