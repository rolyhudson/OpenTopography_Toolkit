using BH.oM.Base;
using BH.oM.Data.Requests;
using BH.oM.Geospatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.oM.Adapter.OpenTopology
{
    public class GlobalDEMRequest : BHoMObject, IRequest
    {
        public virtual BoundingBox BoundingBox { get; set; } = new BoundingBox();
    }
}
