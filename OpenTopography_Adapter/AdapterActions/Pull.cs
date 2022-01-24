using BH.oM.Adapter;
using BH.oM.Adapter.OpenTopography;
using BH.oM.Adapters.HTTP;
using BH.oM.Data.Requests;
using BH.oM.Geospatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Adapter.OpenTopography
{
    public partial class OpenTopographyAdapter
    {
        public override IEnumerable<object> Pull(IRequest request, PullType pullType = PullType.AdapterDefault, ActionConfig actionConfig = null)
        {
            if (!(actionConfig is OpenTopographyConfig))
                actionConfig = new OpenTopographyConfig();


            if (request is GlobalDEMRequest)
            {
                Response response = new Response();
                MultiPoint dtm = new MultiPoint();
                foreach (object r in Pull(request as GlobalDEMRequest, actionConfig as OpenTopographyConfig))
                {
                    //should be a single ASCII text object
                    response.FeatureCollection.Features.Add(new Feature() { Geometry = Convert.ToGeospatial(r) });
                }
                return new List<object> { response };
            }

            Engine.Base.Compute.RecordError("This type of request is not supported.");
            return new List<object>();
        }

        /***************************************************/

        public IEnumerable<object> Pull(GlobalDEMRequest request, OpenTopographyConfig config)
        {
            GetRequest getRequest = BH.Engine.Adapter.OpenTopography.Create.GetRequest(request.BoundingBox, config);
            List<object> responses = new List<object>();
            responses.Add(m_HTTPAdapter.Pull(getRequest).First());

            return responses;
        }
    }
}
