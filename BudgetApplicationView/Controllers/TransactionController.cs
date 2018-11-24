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
    public class TransactionController : Controller
    {
        [HttpGet]
        public ActionResult CreateTransactions(int id)
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

            var category =
               httpClient.GetAsync("http://localhost:54111/api/categories/View/" + id).Result;

            var account =
               httpClient.GetAsync("http://localhost:54111/api/accounts/View/" + id).Result;

            if (category.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonStringCategory = category.Content.ReadAsStringAsync().Result;
                var jsonStringAccount = account.Content.ReadAsStringAsync().Result;

                var categories = JsonConvert
                   .DeserializeObject<List<CreateCategoryViewModel>>(jsonStringCategory);
                var accounts = JsonConvert
                   .DeserializeObject<List<AccountManagementViewModel>>(jsonStringAccount);

                var selectcategories = new SelectList(categories, "Id", "Name");
                ViewBag.selectcategories = selectcategories;

                var selectaccounts = new SelectList(accounts, "Id", "Name");
                ViewBag.selectaccounts = selectaccounts;
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateTransactions(CreateTransactionsViewModel model, int id)
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
            parameters.Add(new KeyValuePair<string, string>("Description", model.Description));
            parameters.Add(new KeyValuePair<string, string>("AccountId", model.AccountId.ToString()));
            parameters.Add(new KeyValuePair<string, string>("Amount", model.Amount.ToString()));
            parameters.Add(new KeyValuePair<string, string>("CategoryId", model.CategoryId.ToString()));
            parameters.Add(new KeyValuePair<string, string>("Date", model.Date.ToString()));


            var formEncodedValues = new FormUrlEncodedContent(parameters);

            var result =
               httpClient.PostAsync("http://localhost:54111/api/transactions/PostTransactions", formEncodedValues).Result;

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;
                //var houseHolds = JsonConvert
                //   .DeserializeObject<CreateTransactionsViewModel>(jsonString);
                return View();
            }
            return View();
        }

        [HttpGet]
        public ActionResult ViewTransactions(int id)
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
               httpClient.GetAsync("http://localhost:54111/api/transactions/View/" + id).Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;
                var houseHolds = JsonConvert
                   .DeserializeObject<List<CreateTransactionsViewModel>>(jsonString);
                return View(houseHolds);
            }
            return View();
        }

        [HttpGet]
        public ActionResult VoidTransactions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VoidTransactions(VoidTransactionViewModel model)
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
            parameters.Add(new KeyValuePair<string, string>("Id", model.Id.ToString()));

            var formEncodedValues = new FormUrlEncodedContent(parameters);

            var result =
               httpClient.PutAsync("http://localhost:54111/api/transactions/VoidTransaction", formEncodedValues).Result;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = result.Content.ReadAsStringAsync().Result;
                var Voided = JsonConvert
                   .DeserializeObject<List<CreateTransactionsViewModel>>(jsonString);
                ViewBag.Voided = "This transaction has been voided";
                return View(Voided);
            }
            //else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            //{
            //    var jsonString = result.Content.ReadAsStringAsync().Result;
            //    var error = JsonConvert.DeserializeObject<ApiResponseModel>(jsonString);
            //    //ModelState.AddModelError("", error.Message);
            //    ViewBag.ErrorMessage = error.Message;
            //}
            //else
            //{
            //    ViewBag.ErrorMessage = "Something went wrong. Please try again.";
            //    //ModelState.AddModelError("", "Something went wrong");
            //}
            return View();
        }
    }
}