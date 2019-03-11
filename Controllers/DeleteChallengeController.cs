using BookWyrm.Shared.Data;
using BookWyrm.Shared.Security;
using BookWyrm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.Controllers
{
    public class DeleteChallengeController : BaseController
    {
        private ChallengesRepository _challengeRepository;
        public DeleteChallengeController(ChallengesRepository challengeRepository, ApplicationUserManager userManager) : base(userManager)
        {
            _challengeRepository = challengeRepository;
        }
        // GET: DeleteChallenge
        [HttpPost]
        public ActionResult Index(ChallengeChangeViewModel viewModel)
        {
            GetUserRole();
            _challengeRepository.Remove(viewModel.Id);
            return RedirectToAction("Index", "Home");
        }
    }
}