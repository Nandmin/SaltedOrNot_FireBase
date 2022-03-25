using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SaltedOrNot_FireBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaltedOrNot_FireBase.Controllers
{
    public class SaltedOrNotController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "6PK5CuSMjggmKcNc6rlm715mS0lhzeiwztatihBc",
            BasePath = "https://lowsaltnet-default-rtdb.firebaseio.com/"
        };

        string link = "http://localhost:50717/";

        IFirebaseClient client;

        // GET: SaltedOrNot
        public ActionResult Index()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("SaltedOrNot");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Ingredients>();

            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Ingredients>(((JProperty)item).Value.ToString()));

            }

            return View(list);
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult Create(Ingredients ingredients)
        {
            try
            {
                AddIngredientsToFireBase(ingredients);
                ModelState.AddModelError(string.Empty, "Sikeresen hozzáadva");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }

        private void AddIngredientsToFireBase(Ingredients ingredients)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = ingredients;
            PushResponse response = client.Push("SaltedOrNot/", data);
            data.ID = response.Result.name;
            SetResponse setResponse = client.Set("SaltedOrNot/" + data.ID, data);
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("SaltedOrNot/" + id);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            return View(data);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("SaltedOrNot/" + id);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Ingredients ingredients)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse response = client.Set("SaltedOrNot/" + ingredients.ID, ingredients);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("SaltedOrNot/" + id);

            return RedirectToAction("Index");
        }
    }
}