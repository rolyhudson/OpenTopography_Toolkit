using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Adapter;

namespace BH.oM.Adapter.OpenTopography
{
    [Description("Define configuration settings for pulling OpenTopography data using the OpenTopography Adapter")]
    public class OpenTopographyConfig : ActionConfig
    {
        public virtual DemType DemType { get; set; } = DemType.AW3D30;
    }
}
