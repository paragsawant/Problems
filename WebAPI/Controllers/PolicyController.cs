using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using ClassLibrary;
using ClassLibrary.Model;

namespace WebAPI.Controllers
{
    public class PolicyController : BaseController
    {
        public PolicyController(IDataLayerAccess dataLayerAccess) : base(dataLayerAccess)
        {
        }

        [HttpDelete]
        [Route("policy/{policyid}")]
        public async Task<HttpResponseMessage> CancelPolicy(string policyid)
        {
            return new HttpResponseMessage
                   {
                       Content = new StringContent(JsonConvert.SerializeObject("Test")),
                       StatusCode = HttpStatusCode.OK
                   };
        }

        [HttpPost]
        [Route("policy/{policyid}")]
        public async Task<HttpResponseMessage> EnrollPolicy(string policyid, [FromBody] Policy policy)
        {
            return new HttpResponseMessage
                   {
                       Content = new StringContent(JsonConvert.SerializeObject("Test")),
                       StatusCode = HttpStatusCode.OK
                   };
        }

        [HttpGet]
        [Route("policies")]
        public async Task<HttpResponseMessage> GetPolicies()
        {
            var policies = await this.DataLayerAccess.GetPolicies();
            return new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(policies)),
                StatusCode = HttpStatusCode.OK
            };
        }

        [HttpGet]
        [Route("policy/{policyid}")]
        public async Task<HttpResponseMessage> GetPolicy(string policyid)
        {
            return new HttpResponseMessage
                   {
                       Content = new StringContent(
                           JsonConvert.SerializeObject("Test :" + policyid)),
                       StatusCode = HttpStatusCode.OK
                   };
        }

        [HttpPut]
        [Route("policy/{policyid}")]
        public async Task<HttpResponseMessage> UpdatePolicy(string policyid)
        {
            return new HttpResponseMessage
                   {
                       Content = new StringContent(JsonConvert.SerializeObject("Test")),
                       StatusCode = HttpStatusCode.OK
                   };
        }
    }
}