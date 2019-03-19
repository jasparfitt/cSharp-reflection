using BookWyrm.Shared.Data;
using BookWyrm.Shared.Models;
using BookWyrm.Shared.Security;
using BookWyrm.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        
        private ChallengesRepository _challengesRepository;
        private UserRepository _userRepository;

        public ProfileController(ApplicationUserManager userManager, ChallengesRepository challengesRepository, UserRepository userRepository) : base(userManager)
        {
            _userManager = userManager;
            _challengesRepository = challengesRepository;
            _userRepository = userRepository;
        }
        // GET: Profile
        public ActionResult Index(string msg)
        {
            switch(msg)
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

            var userName = User.Identity.GetUserName();

            var user = _userRepository.GetUser(userName);
            Challenge activeChallenge = null;
            if (user.ChallengeId != null)
            {
                activeChallenge = _challengesRepository.Get((int)user.ChallengeId, userName, true);
            }
            var viewModel = new ProfileIndexViewModel()
            {
                ActiveChallenge = activeChallenge,
                Name = user.Name,
                CompletedChallenges = user.Challenges.Select(c => c.Challenge).ToList(),
                Location = "Profile",
                IsCurrentChallenge = true
            };

            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(ChallengeChangeViewModel viewModel) 
        {
            var userName = User.Identity.GetUserName();
            string msg = null;

            if(_userRepository.AllBooksAreRead(viewModel.Id, userName))
            {
                _userRepository.CompleteChallenge(viewModel.Id, userName);
            }
            else
            {
                msg = "not-all";
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