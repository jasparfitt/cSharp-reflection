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
    public class DemoteController : BaseController
    {
        public DemoteController(ApplicationUserManager userManager) : base(userManager)
        {

        }
        // GET: Demote
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

            switch (userRole)
            {
                case "Admin":
                    return RedirectToAction("Index", "Users", new { msg = "err-admin" });
                case "Creator":
                    var user = _userManager.Users.Where(u => u.UserName == viewModel.UserName).SingleOrDefault();
                    await _userManager.RemoveFromRoleAsync(user.Id, "Creator");
                    await _userManager.AddToRoleAsync(user.Id, "User");
                    return RedirectToAction("Index", "Users", new { msg = "msg-demote-success" });
                case "User":
                    return RedirectToAction("Index", "Users", new { msg = "err-no-demote" });
                default:
                    return RedirectToAction("Index", "Users", new { msg = "err-no-user" });
            }
        }
    }
}