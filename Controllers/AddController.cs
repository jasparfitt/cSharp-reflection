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
            var userRole = GetUserRole();

            if (userRole != "Admin" && userRole != "Creator")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var newChallengeBooks = new List<int>();
            foreach (var book in viewModel.ChallengeBooks)
            {
                if (book != 0)
                {
                    newChallengeBooks.Add(book);
                }
            }

            viewModel.ChallengeBooks = newChallengeBooks;

            if (viewModel.ChallengeBooks.Count() < 1)
            {
                ModelState.AddModelError("No Books", "A challenge must have at least 1 book.");
            }

            if (!ModelState.IsValid)
            {
                viewModel.Init(_bookRepository);
                return View(viewModel);
            }

            _challengeRepository.Add(viewModel.Challenge, viewModel.ChallengeBooks);
            return RedirectToAction("Index", "Home");
        }
    }
}