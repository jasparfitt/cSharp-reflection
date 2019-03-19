using BookWyrm.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWyrm.ViewModels
{
    public class ListOfChallengesViewModel
    {
        public List<Challenge> Challenges { get; set; }
        public int PageNum { get; set; }
        public int PageMax { get; set; }
        public virtual string Page { get; set; }
        public string Search { get; set; }
        public IEnumerable<int> PageDisplayNum
        {
            get
            {
                var min = 1;
                var max = PageMax;
                if (PageMax > 5)
                {
                    if (PageNum == 1 || PageNum == 2)
                    {
                        min = 1;
                        max = 5;
                    }
                    else if (PageNum == PageMax || PageNum == PageMax - 1)
                    {
                        min = PageMax - 5;
                        max = PageMax;
                    }
                    else
                    {
                        min = PageNum - 2;
                        max = PageNum + 2;
                    }

                    if (min < 1)
                    {
                        min = 1;
                    }
                    if (max > PageMax)
                    {
                        max = PageMax;
                    }
                }
                
                var range = Enumerable.Range(min, max + 1 - min);
                return range;
            }
        } 
    }
}