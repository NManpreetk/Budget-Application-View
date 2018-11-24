using BudgetApplicationView.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BudgetApplicationView.Controllers
{
    public class AccountsController : Controller
    {
        // GET: AccountsManagement
        public ActionResult ViewAccounts(int id)
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
               httpClient.GetAsync("http://localhost:54111/api/accounts/View/" + id).Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;
                var houseHolds = JsonConvert
                   .DeserializeObject<List<AccountManagementViewModel>>(jsonString);
                return View(houseHolds);
            }
            return View();
        }


        [HttpGet]
        public ActionResult CreateAccounts()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccounts(AccountManagementViewModel model, int id)
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
            parameters.Add(new KeyValuePair<string, string>("Balance", model.Balance));
            parameters.Add(new KeyValuePair<string, string>("HouseHoldId", model.Id.ToString()));

            var formEncodedValues = new FormUrlEncodedContent(parameters);

            var result =
               httpClient.PostAsync("http://localhost:54111/api/accounts/Postaccounts/", formEncodedValues).Result;

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;
                var houseHolds = JsonConvert
                   .DeserializeObject<AccountManagementViewModel>(jsonString);
                return View(houseHolds);
            }
            return View();
        }
    }
}