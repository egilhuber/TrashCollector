using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            ApplicationDbContext context = new ApplicationDbContext();
            var Roles = context.Roles.ToList();
            return View(Roles);
        }
    }
}