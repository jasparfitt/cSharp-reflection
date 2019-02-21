using BookWyrm.Shared.Data;
using BookWyrm.ViewModels;
using BookWyrm.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace BookWyrm.Controllers
{
    public class HomeController : Controller
    {
        private ChallengesRepository _challengesRepository = null;
        public HomeController(ChallengesRepository challengesRepository)
        {
            _challengesRepository = challengesRepository;
        }

        // GET: Challenges
        public ActionResult Index()
        {
            List<Challenge> challenges = _challengesRepository.GetList();

            var viewModel = new HomeIndexViewModel()
            {
                Challenges = challenges
            };

            return View(viewModel);
        }
    }
}