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
using UnityEngine;
using Game.Prefabs.Climate;
using PhotoModePreserve.Initialization;

namespace PhotoModePreserve.Systems
{ 
    public partial class PreservePhotoModeSystem : SystemBase
    {

        InitializationProcedure procedure = new InitializationProcedure();
        protected override void OnUpdate()
        {
            
            procedure.Write();
        }

       


}

  
}
