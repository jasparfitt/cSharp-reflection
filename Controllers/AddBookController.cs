using BookWyrm.Shared.Data;
using BookWyrm.Shared.Security;
using BookWyrm.ViewModels;
using BookWyrm.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.Controllers
{
    public class AddBookController : BaseController
    {
        private BookRepository _bookRepository;
        private AuthorsRepository _authorsRepository;
        public AddBookController(ApplicationUserManager userManager, BookRepository bookRepository, AuthorsRepository authorsRepository) : base(userManager)
        {
            _authorsRepository = authorsRepository;
            _bookRepository = bookRepository;
        }
        // GET: AddBook
        public ActionResult Index()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin" && userRole != "Creator")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            var viewModel = new AddBookIndexViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(AddBookIndexViewModel viewModel)
        {
            var userRole = GetUserRole();
            if (userRole != "Admin" && userRole != "Creator")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var Book = new Book()
            {
                Title = viewModel.Title
            };

            var bookExists = _bookRepository.CheckBookExists(Book, viewModel.Authors);
            if (bookExists)
            {
                ModelState.AddModelError("Exists","Book already exists.");
                return View(viewModel);
            }
            var authorsString = _bookRepository.SplitAuthorString(viewModel.Authors);
            List<Author> authors = new List<Author>();
            foreach(var authorString in authorsString)
            {
                var author = _bookRepository.SplitAuthorName(authorString);
                var authorExists = _authorsRepository.CheckAuthorExists(author);
                if (!authorExists)
                {
                    Book.AddAuthor(author);
                    _authorsRepository.Add(author);
                }
                else
                {
                    var newAuthor = _authorsRepository.Get(author.FirstName, author.LastName);
                    Book.AddAuthor(newAuthor);
                }
            }
            _bookRepository.Add(Book);
            return RedirectToAction("Index", "Home");
        }
    }
}