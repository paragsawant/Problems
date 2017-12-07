// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountryController.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ClassLibrary;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    public class CountryController : BaseController
    {
        public CountryController(IDataLayerAccess dataLayerAccess) : base(dataLayerAccess)
        {
        }

        [HttpGet]
        [Route("countries")]
        public async Task<HttpResponseMessage> GetCountries()
        {
            var countries = await this.DataLayerAccess.GetCounties();
            return new HttpResponseMessage
                   {
                       Content = new StringContent(JsonConvert.SerializeObject(countries)),
                       StatusCode = HttpStatusCode.OK
                   };
        }
    }
}