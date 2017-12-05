using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    //[RoutePrefix("polcies")]
    public class PolicyController : ApiController
    {
        [HttpGet]
        [Route("polcies")]
        public async Task<HttpResponseMessage> GetPolicies()
        {
            return new HttpResponseMessage() {  Content = new StringContent(JsonConvert.SerializeObject("Test")), StatusCode=HttpStatusCode.OK};
        }

        [HttpGet]
        [Route("policy/{policyid}")]
        public async Task<HttpResponseMessage> GetPolicy(string policyid)
        {
            return new HttpResponseMessage() { Content = new StringContent(JsonConvert.SerializeObject("Test :"+ policyid)), StatusCode = HttpStatusCode.OK };
        }

        [HttpPost]
        [Route("policy/{policyid}")]
        public async Task<HttpResponseMessage> EnrollPolicy(string policyid, [FromBody] object policy)
        {
            return new HttpResponseMessage() { Content = new StringContent(JsonConvert.SerializeObject("Test")), StatusCode = HttpStatusCode.OK };
        }

        [HttpDelete]
        [Route("policy/{policyid}")]
        public async Task<HttpResponseMessage> CancelPolicy(string policyid)
        {
            return new HttpResponseMessage() { Content = new StringContent(JsonConvert.SerializeObject("Test")), StatusCode = HttpStatusCode.OK };
        }

        [HttpPut]
        [Route("policy/{policyid}")]
        public async Task<HttpResponseMessage> UpdatePolicy(string policyid)
        {
            return new HttpResponseMessage() { Content = new StringContent(JsonConvert.SerializeObject("Test")), StatusCode = HttpStatusCode.OK };
        }
    }
}
