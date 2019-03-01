using BookWyrm.Shared.Security;
using BookWyrm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.Controllers
{
    [Authorize]
    public class PromoteController : BaseController
    {
        public PromoteController(ApplicationUserManager userManager) : base(userManager)
        {

        }

        [HttpPost]
        public async Task<ActionResult> Index(UsersIndexViewModel viewModel)
        {
            if(GetUserRole() != "Admin")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var userRole = GetUserRole(viewModel.UserName);

            if(viewModel.UserName == null)
            {
                userRole = null;
            }

            switch(userRole)
            {
                case "Admin":
                    return RedirectToAction("Index", "Users", new { msg = "err-admin" });
                case "Creator":
                    return RedirectToAction("Index", "Users", new { msg = "err-no-promote" });
                case "User":
                    var user = _userManager.Users.Where(u => u.UserName == viewModel.UserName).SingleOrDefault();
                    await _userManager.RemoveFromRoleAsync(user.Id, "User");
                    await _userManager.AddToRoleAsync(user.Id, "Creator");
                    return RedirectToAction("Index", "Users", new { msg = "msg-promote-success" });
                default:
                    return RedirectToAction("Index", "Users", new { msg = "err-no-user" });
            }
        }
    }
}