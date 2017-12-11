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
        public ActionResult PoliciesList()
        {
            List<Policy> policies = new List<Policy>();

            var _policies = new HttpHelper().SendAsync(HttpMethod.Get, "policies");
            policies = JsonConvert.DeserializeObject<List<Policy>>(_policies);
            return View(policies);
        }

        public ActionResult Details(string id)
        {
            var _policy = new HttpHelper().SendAsync(HttpMethod.Get, $"policies/{id}");
            return View(JsonConvert.DeserializeObject<Policy>(_policy));

        }

        public ActionResult DeletePet(int id, string policyNumber)
        {
            HttpHelper helper = new HttpHelper();
            string flag = helper.SendAsync(HttpMethod.Delete, $"policies/{policyNumber}/pet/{id}");
            if (string.IsNullOrEmpty(flag))
            {
                return RedirectToAction("PoliciesList");
            }
            else
            {
                return RedirectToAction("Details", new { id = id });
            }
        }

        public async Task<ActionResult> Create()
        {
            var petType = from PetType p in Enum.GetValues(typeof(PetType))
                          select new
                          {
                              ID = p.ToString(),
                              Name = p.ToString()
                          };
            ViewBag.EnumList = new SelectList(petType, "ID", "Name");
            Country country = new Country();
            country.CountryList = new SelectList(await countryContext.GetCountryListAsync(), "Id", "IsoCode");
            ViewBag.CountryList = country.CountryList;
            return View(new Policy() { Pets = new List<Pet>() { new Pet() { PetType = PetType.Dog, DateOfBirth = DateTime.Now, PetName = "PetName" } } });
        }

        public ActionResult CreateNewPet()
        {

            var pet = new Pet();

            return PartialView("~/Views/Policy/AddPetToPolicy.cshtml", pet);

        }

        [HttpGet]
        public ActionResult AddPetToPolicy(int PetOwnerId, string policyNumber)
        {
            var petType = from PetType p in Enum.GetValues(typeof(PetType))
                          select new
                          {
                              ID = p,
                              Name = p
                          };
            ViewBag.EnumList = new SelectList(petType, "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddPetToPolicy(int PetOwnerId, string policyNumber, FormCollection collection)
        {
            try
            {
                Pet pet = new Pet();
                pet.DateOfBirth = Convert.ToDateTime(collection["DateOfBirth"]);
                pet.PetName = collection["PetName"];
                pet.PetType = (PetType)Enum.Parse(typeof(PetType), collection["PetTypeDropDown"]);
                pet.PetOwnerId = PetOwnerId;
                var requestJson = JsonConvert.SerializeObject(pet);
                var _policy = new HttpHelper().SendAsync(HttpMethod.Post, $"policies/{policyNumber}/pets", requestJson);
                return RedirectToAction("Details");

            }
            catch (Exception)
            {

                return View();
            }

        }
        public ActionResult TransferPet(int id, string policyNumber)
        {
            var petType = from PetType p in Enum.GetValues(typeof(PetType))
                          select new
                          {
                              ID = p,
                              Name = p
                          };
            ViewBag.EnumList = new SelectList(petType, "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Policy policy = new Policy()
                {
                    PetOwnerName = collection["PetOwnerName"],
                    PolicyDate = Convert.ToDateTime(collection["PolicyDate"]),
                    CountryId = Convert.ToInt32(collection["Country"])
                };
                int numberOfPets = (collection.AllKeys.Count() - 4) / 3;
                List<Pet> pets = new List<Pet>();
                for (int i = 0; i < numberOfPets; i++)
                {
                    pets.Add(new Pet() { DateOfBirth = Convert.ToDateTime(collection[$"Pets[{i}].DateOfBirth"]), PetName = collection[$"Pets[{i}].PetName"], PetType = (PetType)Enum.Parse(typeof(PetType), collection[$"{i}PetTypeDropDown"].ToString()) });
                }
                policy.Pets = pets;
                var requestJson = JsonConvert.SerializeObject(policy);
                var _policy = new HttpHelper().SendAsync(HttpMethod.Post, $"policies", requestJson);
                return RedirectToAction("PoliciesList");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(string id)
        {
            var _policy = new HttpHelper().SendAsync(HttpMethod.Get, $"policies/{id}");
            return View(JsonConvert.DeserializeObject<Policy>(_policy));
        }

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

        public ActionResult Delete(string id)
        {
            HttpHelper helper = new HttpHelper();
            string flag = helper.SendAsync(HttpMethod.Delete, $"policies/{id}");

            return RedirectToAction("PoliciesList");

        }

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
