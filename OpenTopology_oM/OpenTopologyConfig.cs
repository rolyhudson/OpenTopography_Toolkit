using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Adapter;

namespace BH.oM.Adapter.OpenTopology
{
    [Description("Define configuration settings for pulling OpenTopology data using the OpenTopology Adapter")]
    public class OpenTopologyConfig : ActionConfig
    {
        public virtual DemType DemType { get; set; } = DemType.AW3D30;
    }
}
