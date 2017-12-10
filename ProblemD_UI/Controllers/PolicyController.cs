using Newtonsoft.Json;
using ProblemD_UI.Context;
using ClassLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProblemD_UI.Controllers
{
    public class PolicyController : Controller
    {
        CountryContext countryContext = new CountryContext();
        // GET: Policy
        public async Task<ActionResult> PolicyDetailsView()
        {
            List<Policy> policies = new List<Policy>();
            string Baseurl = "http://localhost:46070/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("policies");
                if (response.IsSuccessStatusCode)
                {
                    var _policies = response.Content.ReadAsStringAsync().Result;
                    policies = JsonConvert.DeserializeObject<List<Policy>>(_policies);
                }
            }
            return View(policies);
        }

        // GET: Policy/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Policy/Create
        public async Task<ActionResult> Create()
        {
            Country country = new Country();
            country.CountryList = new SelectList(await countryContext.GetCountryListAsync(), "Id", "IsoCode");
            ViewBag.CountryList = country.CountryList;
            return View(new Policy());
        }

        // POST: Policy/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Policy/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Policy/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Policy/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Policy/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
