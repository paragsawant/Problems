using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class CountryController
    {
        public CountryController()
        {

        }

        [HttpGet]
        [Route("countries")]
        public async Task<HttpResponseMessage> GetCountries()
        {
            return new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject("Test")),
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}