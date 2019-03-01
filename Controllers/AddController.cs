using BookWyrm.Shared.Data;
using BookWyrm.Shared.Security;
using BookWyrm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.Controllers
{
    [Authorize]
    public class AddController : BaseController
    {
        private ChallengesRepository _challengeRepository;
        private BookRepository _bookRepository;
        public AddController(BookRepository bookRepository, ApplicationUserManager userManager, ChallengesRepository challengeRepository) : base(userManager)
        {
            _bookRepository = bookRepository;
            _challengeRepository = challengeRepository;
        }

        // GET: Add
        public ActionResult Index()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin" && userRole != "Creator")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            var viewModel = new AddIndexViewModel();
            viewModel.Init(_bookRepository);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(AddIndexViewModel viewModel)
        {
            _challengeRepository.Add(viewModel.Challenge, viewModel.ChallengeBooks);
            return RedirectToAction("Index", "Home");
        }
    }
}