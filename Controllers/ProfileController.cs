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
        public ActionResult Index()
        {
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
            if(_userRepository.AllBooksAreRead(viewModel.Id, userName))
            {
                _userRepository.CompleteChallenge(viewModel.Id, userName);
            }
            else
            {
                ModelState.AddModelError("NoBooksRead", "All books must be marked as read before completing a challenge");
            }
            
            switch (viewModel.Return)
            {
                case "Profile":
                    return RedirectToAction("Index", viewModel.Return);
                case "Challenge":
                    return RedirectToAction("Index", viewModel.Return, new { id = viewModel.Id });
                default:
                    return RedirectToAction("Index", "Home");
            }
        }
    }
}