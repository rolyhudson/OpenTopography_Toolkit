using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.Engine.Reflection;
using BH.oM.Geospatial;

namespace BH.Adapter.OpenTopography
{
    public static partial class Convert
    {
        public static IGeospatial ToGeospatial(object httpResponse)
        {
            if (httpResponse is string)
            {
                //convert the http response to a point cloud
                return ToMultiPoint(httpResponse as string);
            }
            Compute.RecordError("Response was not in the expected format.");
            return null;
        }
        
    }
}
