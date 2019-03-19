using BookWyrm.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWyrm.ViewModels
{
    public class SearchIndexViewModel : ListOfChallengesViewModel
    {
        public override string Page
        {
            get
            {
                return "Search";
            }
            set { }
        }
    }
}