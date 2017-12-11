using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProblemD_UI
{
    public class HttpHelper
    {
        private const string Endpoint = "http://localhost:46070/api/";
        private readonly HttpClient httpClient;

        public HttpHelper()
        {
            this.httpClient = new HttpClient();
        }

        public string SendAsync(HttpMethod method, string url, string requestJson = null)
        {
            string result = string.Empty;
            try
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var requestBody = requestJson == null ? null : new StringContent(requestJson, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage { RequestUri = new Uri(Endpoint + url), Method = method, Content = requestBody };
                var response = this.httpClient.SendAsync(request).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                result = $"{this.FormatText(content)}";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return result;
        }

        public string FormatText(string input)
        {
            try
            {
                return JToken.Parse(input).ToString();
            }
            catch
            {
                return input;
            }
        }

    }
}