using BookWyrm.Shared.Security;
using BookWyrm.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationLoginManager _loginManager;
        private readonly IAuthenticationManager _authenticationManger;

        public LoginController(ApplicationUserManager userManager, ApplicationLoginManager loginManager, IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _loginManager = loginManager;
            _authenticationManger = authenticationManager;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginIndexViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var result = await _loginManager.PasswordSignInAsync(
                viewModel.Email, viewModel.Password, viewModel.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.Failure:
                    return View(viewModel);
                default:
                    throw new Exception("Unexpected Log In status");
            }
        }
    }
}