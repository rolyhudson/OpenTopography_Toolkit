using BH.oM.Adapters.HTTP;
using BH.oM.Data.Collections;
using Geosp = BH.oM.Geospatial;
using Geom = BH.oM.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Adapter.OpenTopography;
using BH.Engine.Geospatial;

namespace BH.Engine.Adapter.OpenTopography
{
    public static partial class Create
    {
        public static GetRequest GetRequest(Geosp.BoundingBox boundingBox, OpenTopographyConfig config)
        {
            return new GetRequest()
            {
                BaseUrl = m_GlobalDEMBaseURL,
                Parameters = Parameters(boundingBox,config)
            };
        }

        private static Dictionary<string, object> Parameters(Geosp.BoundingBox boundingBox, OpenTopographyConfig config)
        {
            Domain lonDom = boundingBox.Domain("Longitude");
            Domain latDom = boundingBox.Domain("Latitude");
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demtype", config.DemType);
            parameters.Add("outputFormat", "AAIGrid");
            parameters.Add("north", latDom.Max);
            parameters.Add("south", latDom.Min);
            parameters.Add("west",  lonDom.Min);
            parameters.Add("east", lonDom.Max);
            return parameters;
        }

        private static string m_GlobalDEMBaseURL = "https://portal.opentopography.org/API/globaldem?";
    }
}
