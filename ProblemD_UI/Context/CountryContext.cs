using Newtonsoft.Json;
using ClassLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ProblemD_UI.Context
{
    public class CountryContext
    {
        public async Task<IEnumerable<Country>> GetCountryListAsync()
        {
            List<Country> countries = new List<Country>();
            string Baseurl = "http://localhost:46070/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("countries");
                if (response.IsSuccessStatusCode)
                {
                    var _countries = response.Content.ReadAsStringAsync().Result;
                    countries = JsonConvert.DeserializeObject<List<Country>>(_countries);
                }
            }

            return countries;
        }
    }
}