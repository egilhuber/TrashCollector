using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrashCollector.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (this.User.IsInRole("8fa092f5-e95e-41d7-9b5e-cdad3cb51600") || this.User.IsInRole("Customer"))
            {
                return RedirectToAction("Details", "Details", "Customers");
            }
            else if (this.User.IsInRole("c2752e08-c8a5-4b91-9f77-16cb3a1d8183") || this.User.IsInRole("Employee"))
            {
                return RedirectToAction("Index", "Employees");
            }
            else
            {
                return View();
            }
            
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