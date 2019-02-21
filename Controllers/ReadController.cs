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
        public ReadController()
        {

        }

        [HttpPost]
        public void Index(int? id)
        {
            if (id == null)
            {
                return;
            }

            
        }
    }
}