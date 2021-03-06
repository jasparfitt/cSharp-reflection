﻿using BookWyrm.Shared.Data;
using BookWyrm.Shared.Models;
using BookWyrm.ViewModels;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookWyrm.Shared.Security;

namespace BookWyrm.Controllers
{
    public class ChallengeController : BaseController
    {
        private ChallengesRepository _challengesRepository = null;
        private UserRepository _userRepository = null;

        public ChallengeController(ChallengesRepository challengesRepository, UserRepository userRepository, ApplicationUserManager userManager) : base(userManager)
        {
            _challengesRepository = challengesRepository;
            _userRepository = userRepository;
        }

        public ActionResult Index(int? id, string msg)
        {
            switch (msg)
            {
                case "not-all":
                    ModelState.AddModelError("not-all", "You can't complete a challenge without marking all books as read.");
                    break;
                case "no-start":
                    ModelState.AddModelError("no-start", "You can't start a challenge that you have marked as completed.");
                    break;
                default:
                    break;
            }
            GetUserRole();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == -1)
            {
                id = _challengesRepository.GetRandomId();
                return RedirectToAction("Index", "Challenge", new { id = id });
            }
            var userName = User.Identity.GetUserName();
            var currentChallengeId = _userRepository.GetActiveChallengeId(userName);

            Challenge challenge = _challengesRepository.Get((int)id, userName, currentChallengeId == id);

            if (challenge == null)
            {
                return new HttpNotFoundResult();
            }

            bool isCurrentChallenge = _userRepository.GetActiveChallengeId(User.Identity.GetUserName()) == id;

            var viewModel = new ChallengeIndexViewModel()
            {
                Challenge = challenge,
                IsCurrentChallenge = isCurrentChallenge,
                Location = "Challenge"
            };

            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(ChallengeChangeViewModel viewModel)
        {
            var userName = User.Identity.GetUserName();
            string msg = null;

            if(_userRepository.ChallengeIsCompleted(viewModel.Id, userName))
            {
                 msg = "no-start";
            }
            else
            {
                _userRepository.StartChallenge(viewModel.Id, userName);
            }
            

            switch (viewModel.Return)
            {
                case "Profile":
                    return RedirectToAction("Index", viewModel.Return, new { msg = msg });
                case "Challenge":
                    return RedirectToAction("Index", viewModel.Return, new { id = viewModel.Id, msg = msg });
                default:
                    return RedirectToAction("Index", "Home", new { msg = "unknown" });
            }
        }
    }
}