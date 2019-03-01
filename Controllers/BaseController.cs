using BookWyrm.Shared.Security;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationUserManager _userManager = null;

        public BaseController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        // GET: Base
        public string GetUserRole (string userName = null)
        {
            var self = false;
            if (userName == null)
            {
                userName = User.Identity.GetUserName();
                self = true;
            }
            var user = _userManager.Users.Where(u => u.UserName == userName).SingleOrDefault();
            string role = "None";
            if (user != null)
            {
                var userId = user.Id;
                role = _userManager.GetRoles(userId).SingleOrDefault().ToString();
            }

            if (self)
            {
                ViewBag.Role = role;
            }

            return role;
        }
    }
}