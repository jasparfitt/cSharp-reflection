using Microsoft.AspNet.Identity;
using BookWyrm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookWyrm.Shared.Data;

namespace BookWyrm.Controllers
{
    [Authorize]
    public class RemoveChallengeController : Controller
    {
        private UserRepository _userRepository;
        public RemoveChallengeController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // GET: RemoveChallenge
        [HttpPost]
        public ActionResult Index(ChallengeChangeViewModel viewModel)
        {
            var userName = User.Identity.GetUserName();
            _userRepository.RemoveCompletedChallenge(viewModel.Id, userName);
            return RedirectToAction("index", "Profile");
        }
    }
}