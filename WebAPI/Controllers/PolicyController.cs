using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using ClassLibrary;
using ClassLibrary.Model;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [RoutePrefix("api")]
    public class PolicyController : BaseController
    {
        public PolicyController(IDataLayerAccess dataLayerAccess) : base(dataLayerAccess)
        {
        }

        [HttpDelete]
        [Route("policies/{policyNumber}")]
        public async Task<HttpResponseMessage> CancelPolicy(string policyNumber)
        {
            var result = await this.DataLayerAccess.CancelPolicy(policyNumber);
            return new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(result)),
                StatusCode = HttpStatusCode.NoContent
            };
        }

        [HttpPost]
        [Route("policies")]
        public async Task<HttpResponseMessage> EnrollPolicy([FromBody] Policy policy)
        {
            var result = await this.DataLayerAccess.EnrollPolicy(policy);
            return new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(result)),
                StatusCode = HttpStatusCode.Created
            };
        }

        [HttpPost]
        [Route("policies/{policyNumber}/pets")]
        public async Task<HttpResponseMessage> AddPetToPolicy(string policyNumber, [FromBody] List<Pet> pets)
        {
            var result = await this.DataLayerAccess.AddPetToPolicy(policyNumber, pets);
            return new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(result)),
                StatusCode = HttpStatusCode.Created
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
        [Route("policies/{policyNumber}")]
        public async Task<HttpResponseMessage> GetPolicy(string policyNumber)
        {
            var policies = await this.DataLayerAccess.GetPolicy(policyNumber);
            return new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(policies)),
                StatusCode = HttpStatusCode.OK
            };
        }

        [HttpDelete]
        [Route("policies/{policyNumber}/pet/{petid}")]
        public async Task<HttpResponseMessage> RemovePetFromPolicy(string policyNumber, int petid)
        {
            var policies = await this.DataLayerAccess.RemovePetFromPolicy(policyNumber, petid);
            return new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(policies)),
                StatusCode = HttpStatusCode.NoContent
            };
        }

        [HttpPut]
        [Route("policies/{oldPetOwnerId}/pet/{petid}/transfer/{newPetOwnerId}")]
        public async Task<HttpResponseMessage> TransferPet(int oldPetOwnerId, int petid, int newPetOwnerId)
        {
            var policies = await this.DataLayerAccess.TransferPet(oldPetOwnerId, petid, newPetOwnerId);
            return new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(policies)),
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}