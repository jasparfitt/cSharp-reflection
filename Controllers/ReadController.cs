using BookWyrm.Shared.Data;
using BookWyrm.Shared.Security;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.Controllers
{
    [Authorize]
    public class ReadController : Controller
    {
        private ApplicationUserManager _userManager;
        private BookRepository _bookRepository = null;
        public ReadController(BookRepository bookRepository, ApplicationUserManager userManager)
        {
            _bookRepository = bookRepository;
            _userManager = userManager;
        }

        [HttpPost]
        public void Index(int? id)
        {
            if (id == null)
            {
                return;
            }

            var userName = User.Identity.GetUserName();
            var user = _userManager.Users.Where(u => u.UserName == userName).SingleOrDefault();

            if (_bookRepository.GetReadStatus((int)id, user))
            {
                _bookRepository.MarkAsUnread((int)id, user);
            }
            else
            {
                _bookRepository.MarkAsRead((int)id, user);
            }
        }
    }
}