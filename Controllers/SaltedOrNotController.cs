using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
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
            return View();
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
    }
}