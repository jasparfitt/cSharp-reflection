using BookWyrm.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWyrm.ViewModels
{
    public class ProfileIndexViewModel : ChallengeDetailsViewModel
    {
        public string Name { get; set; }
        public Challenge ActiveChallenge
        {
            get
            {
                return Challenge;
            }
            set
            {
                Challenge = value;
            }
        }
        public List<Challenge> CompletedChallenges { get; set; }
    }
}