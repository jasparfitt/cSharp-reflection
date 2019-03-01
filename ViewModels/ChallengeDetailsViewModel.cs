using BookWyrm.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWyrm.ViewModels
{
    public class ChallengeDetailsViewModel
    {
        public Challenge Challenge { get; set; }
        public string Location { get; set; }
        public bool IsCurrentChallenge { get; set; }
    }
}