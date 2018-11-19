 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace BudgetApplicationView.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var httpClient = new HttpClient();
            //var cookie = Request.Cookies["token"];
            //var accessToken = cookie.Value;
            //httpClient.DefaultRequestHeaders.Authorization =
            //   new AuthenticationHeaderValue("Bearer", accessToken);
            //var result = httpClient
            //   .GetStringAsync("http://localhost:54111/api/values")
            //   .Result;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}