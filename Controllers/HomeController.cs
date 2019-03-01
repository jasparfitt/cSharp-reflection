using BookWyrm.Shared.Data;
using BookWyrm.ViewModels;
using BookWyrm.Shared.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using BookWyrm.Shared.Security;

namespace BookWyrm.Controllers
{
    public class HomeController : BaseController
    {
        private ChallengesRepository _challengesRepository = null;
        

        public HomeController(ChallengesRepository challengesRepository, ApplicationUserManager userManager) : base(userManager)
        {
            _challengesRepository = challengesRepository;
        }

        // GET: Challenges
        public ActionResult Index()
        {
            GetUserRole();

            List<Challenge> challenges = _challengesRepository.GetList();

            var viewModel = new HomeIndexViewModel()
            {
                Challenges = challenges
            };

            return View(viewModel);
        }
    }
}