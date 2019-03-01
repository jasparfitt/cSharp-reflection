using BookWyrm.Shared.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.Controllers
{
    public class LogOutController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationLoginManager _loginManager;
        private readonly IAuthenticationManager _authenticationManger;

        public LogOutController(ApplicationUserManager userManager, ApplicationLoginManager loginManager, IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _loginManager = loginManager;
            _authenticationManger = authenticationManager;
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            _authenticationManger.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }
    }
}