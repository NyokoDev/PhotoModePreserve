using Game.Prefabs.Climate;
using Game.Rendering;
using PhotoModePreserve.Exporter.ColorAdjustmentsEnsurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine.Rendering.HighDefinition;

namespace PhotoModePreserve.Initialization
{
    public class InitializationProcedure : SystemBase
    {
        
        public void Write()
        {
            ColorAdjustmentsManager properties = World.GetOrCreateSystemManaged<ColorAdjustmentsManager>();
            properties.SetProperties();
            properties.SerializeToXML();
        }

        protected override void OnUpdate()
        {
            ColorAdjustmentsManager properties = World.GetOrCreateSystemManaged<ColorAdjustmentsManager>();
            properties.SetProperties();
            properties.SerializeToXML();
        }
    }

       
    }

