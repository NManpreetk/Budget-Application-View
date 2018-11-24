using BudgetApplicationView.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BudgetApplicationView.Controllers
{
    public class HouseHoldsController : Controller
    {
        public ActionResult Index()
        {
            var cookie = Request.Cookies["token"];
            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var accessToken = cookie.Value;
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
               new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var result =
               httpClient.GetAsync("http://localhost:54111/api/households/viewHouseHolds").Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;
                var houseHolds = JsonConvert
                   .DeserializeObject<List<ViewHouseHoldViewModel>>(jsonString);
                return View(houseHolds);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateHouseHoldViewModel model)
        {
            var cookie = Request.Cookies["token"];
            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var accessToken = cookie.Value;
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
               new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            
            var parameters = new List<System.Collections.Generic.KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("Name", model.Name));

            var formEncodedValues = new FormUrlEncodedContent(parameters);

            var result =
               httpClient.PostAsync("http://localhost:54111/api/households/PostHouseHolds", formEncodedValues).Result;
            
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(EditHouseHoldViewModel model, int id)
        {
            var cookie = Request.Cookies["token"];
            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var accessToken = cookie.Value;
            var CreatorId = User.Identity.GetUserId();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
               new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var parameters = new List<System.Collections.Generic.KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("Name", model.Name));
            //parameters.Add(new KeyValuePair<string, string>("HouseHoldId", model.HouseHoldId));

            var formEncodedValues = new FormUrlEncodedContent(parameters);
            var result =
               httpClient.PutAsync("http://localhost:54111/api/households/PutHouseHolds/" + id, formEncodedValues).Result;

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;
                var houseHolds = JsonConvert
                   .DeserializeObject<HouseHolds>(jsonString);
                return View(houseHolds);
            }
            return View();
        }

        public ActionResult ViewUsersInHouseHold(ViewUsersInHouseHoldViewModel model, int id)
        {
            var cookie = Request.Cookies["token"];
            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var accessToken = cookie.Value;
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
               new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var result =
               httpClient.GetAsync("http://localhost:54111/api/households/ViewHouseHoldPerUser/"+ id).Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;
                var houseHolds = JsonConvert
                   .DeserializeObject<List<ViewUsersInHouseHoldViewModel>>(jsonString);
                return View(houseHolds);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Invite(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Invite(int id, InviteUserViewModel model)
        {
            var cookie = Request.Cookies["token"];
            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var accessToken = cookie.Value;
            var CreatorId = User.Identity.GetUserId();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
               new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var parameters = new List<System.Collections.Generic.KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("Email", model.Email));
            parameters.Add(new KeyValuePair<string, string>("Id", model.Id));


            var formEncodedValues = new FormUrlEncodedContent(parameters);
            var result =
               httpClient.PostAsync("http://localhost:54111/api/households/Invite/" + id, formEncodedValues).Result;

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;
                var houseHolds = JsonConvert
                   .DeserializeObject<InviteUserViewModel>(jsonString);
                return View(houseHolds);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Join(int id)
        {
            var cookie = Request.Cookies["token"];
            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var accessToken = cookie.Value;
            var CreatorId = User.Identity.GetUserId();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
               new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var parameters = new List<System.Collections.Generic.KeyValuePair<string, string>>();
            //parameters.Add(new KeyValuePair<string, string>("HouseHoldId", model.Id));


            var formEncodedValues = new FormUrlEncodedContent(parameters);
            var result =
               httpClient.PostAsync("http://localhost:54111/api/households/Invite/" + id, formEncodedValues).Result;

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;
                var houseHolds = JsonConvert
                   .DeserializeObject<InviteUserViewModel>(jsonString);
                return View(houseHolds);
            }
            return View();
        }

        

        

        

    }
}