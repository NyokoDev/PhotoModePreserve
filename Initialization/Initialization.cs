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
    public class InitializationProcedure
    {

        public void Write()
        {
            ColorAdjustmentsManager properties = new ColorAdjustmentsManager();

            properties.SerializeToXML();
            Mod.Instance.Log.Debug("Ran SetProperties() and SerializetoXML at InitializationProcedure class.");
        }

    
    }

       
    }

