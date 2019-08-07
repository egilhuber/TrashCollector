using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using TrashCollector.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrashCollector.Controllers
{
    internal class ApplicationUserManager<T>
    {
        private UserStore<ApplicationUser> userStore;

        public ApplicationUserManager(UserStore<ApplicationUser> userStore)
        {
            this.userStore = userStore;
        }

        internal object GetRoles(string v)
        {
            throw new NotImplementedException();
        }
    }
}