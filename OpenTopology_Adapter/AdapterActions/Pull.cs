using BH.oM.Adapter;
using BH.oM.Adapter.OpenTopology;
using BH.oM.Adapters.HTTP;
using BH.oM.Data.Requests;
using BH.oM.Geospatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Adapter.OpenTopology
{
    public partial class OpenTopologyAdapter
    {
        public override IEnumerable<object> Pull(IRequest request, PullType pullType = PullType.AdapterDefault, ActionConfig actionConfig = null)
        {
            if (!(actionConfig is OpenTopologyConfig))
                actionConfig = new OpenTopologyConfig();


            if (request is GlobalDEMRequest)
            {
                Response response = new Response();
                MultiPoint dtm = new MultiPoint();
                foreach (object r in Pull(request as GlobalDEMRequest, actionConfig as OpenTopologyConfig))
                {
                    //should be a single ASCII text object
                    response.FeatureCollection.Features.Add(new Feature() { Geometry = Convert.ToGeospatial(r) });
                }
                return new List<object> { response };
            }

            Engine.Reflection.Compute.RecordError("This type of request is not supported.");
            return new List<object>();
        }

        /***************************************************/

        public IEnumerable<object> Pull(GlobalDEMRequest request, OpenTopologyConfig config)
        {
            GetRequest getRequest = BH.Engine.Adapter.OpenTopology.Create.GetRequest(request.BoundingBox, config);
            List<object> responses = new List<object>();
            responses.Add(m_HTTPAdapter.Pull(getRequest).First());

            return responses;
        }
    }
}
