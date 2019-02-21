using BookWyrm.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWyrm.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Challenge> Challenges { get; set; }
    }
}