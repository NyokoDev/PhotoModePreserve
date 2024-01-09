using Game.Prefabs.Climate;
using PhotoModePreserve.Exporter.ColorAdjustmentsEnsurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.HighDefinition;

namespace PhotoModePreserve.Initialization
{
    public class InitializationProcedure
    {
        ColorAdjustmentsManager properties = new ColorAdjustmentsManager();
        public void Write()
        {

            properties.SetProperties();
            properties.SerializeToXML();
        }

    }
}
