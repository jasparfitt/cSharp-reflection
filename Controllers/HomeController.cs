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
        public ActionResult Index(int? id, string msg)
        {
            switch (msg)
            {
                case "unknown":
                    ModelState.AddModelError("unknown", "An unknown error occured.");
                    break;
                default:
                    break;
            }
            GetUserRole();
            var pageNum = id;
            if (pageNum == null)
            {
                pageNum = 1;
            }

            int pageMax = _challengesRepository.CountAllPages();

            if (pageNum < 1 || pageNum > pageMax)
            {
                return new HttpNotFoundResult();
            }

            List<Challenge> challenges = _challengesRepository.GetNewestList((int)pageNum);
            
            

            var viewModel = new HomeIndexViewModel()
            {
                Challenges = challenges,
                PageNum = (int)pageNum,
                PageMax = pageMax
            };

            return View(viewModel);
        }
    }
}