using BookWyrm.Shared.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {

        public UsersController(ApplicationUserManager userManager) : base(userManager) { }

        // GET: Users
        public ActionResult Index(string msg)
        {
            ViewBag.msg = new List<string>();
            switch (msg)
            {
                case "err-admin":
                    ModelState.AddModelError("Admin", "You cannot promote or demote an admin");
                    break;
                case "err-no-user":
                    ModelState.AddModelError("None", "There was no account found with this email");
                    break;
                case "err-no-promote":
                    ModelState.AddModelError("Creator", "This account is already a creator");
                    break;
                case "err-no-demote":
                    ModelState.AddModelError("User", "This account is already a user");
                    break;
                case "msg-promote-success":
                    ViewBag.msg.Add("Account promoted to creator");
                    break;
                case "msg-demote-success":
                    ViewBag.msg.Add("Account demoted to user");
                    break;
                default:
                    break;
            }

            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View();
        }
    }
}