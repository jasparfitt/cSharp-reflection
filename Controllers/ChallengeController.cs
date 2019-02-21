using BookWyrm.Shared.Data;
using BookWyrm.Shared.Models;
using BookWyrm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.Controllers
{
    public class ChallengeController : Controller
    {
        private ChallengesRepository _challengesRepository = null;
        public ChallengeController(ChallengesRepository challengesRepository)
        {
            _challengesRepository = challengesRepository;
        }

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Challenge challenge = _challengesRepository.Get((int)id);

            if (challenge == null)
            {
                return new HttpNotFoundResult();
            }

            var viewModel = new ChallengeIndexViewModel()
            {
                Challenge = challenge
            };

            return View(viewModel);
        }
    }
}