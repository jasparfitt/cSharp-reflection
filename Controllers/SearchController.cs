using BookWyrm.Shared.Data;
using BookWyrm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.Controllers
{
    public class SearchController : Controller
    {
        private ChallengesRepository _challengeRepository = null;

        public SearchController(ChallengesRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }
        // GET: Search
        public ActionResult Index(int? id, string search)
        {
            var pageNum = id;
            if (pageNum == null)
            {
                pageNum = 1;
            }

            var pageMax = _challengeRepository.CountSearchPages(search);
            if (id < 1 || id > pageMax)
            {
                return new HttpNotFoundResult();
            }
            var challenges = _challengeRepository.GetSearchList(search, (int)pageNum);
            

            var viewModel = new SearchIndexViewModel()
            {
                Search = search,
                Challenges = challenges,
                PageNum = (int)pageNum,
                PageMax = pageMax
            };

            return View(viewModel);
        }
    }
}