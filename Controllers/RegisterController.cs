﻿using BookWyrm.Shared.Models;
using BookWyrm.Shared.Security;
using BookWyrm.ViewModels;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationLoginManager _loginManager;
        private readonly IAuthenticationManager _authenticationManger;

        public RegisterController(ApplicationUserManager userManager, ApplicationLoginManager loginManager, IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _loginManager = loginManager;
            _authenticationManger = authenticationManager;
        }

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(RegisterIndexViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new User()
            {
                UserName = viewModel.Email,
                Email = viewModel.Email,
                Name = viewModel.Name
            };
            var result = await _userManager.CreateAsync(user, viewModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user.Id, "User");
                await _loginManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                var displayError = error;
                if (error.Substring(0, 4) == "Name")
                {
                    displayError = "Email" + error.Substring(4);
                }
                ModelState.AddModelError("", displayError);
            }

            return View(viewModel);
        }
    }
}