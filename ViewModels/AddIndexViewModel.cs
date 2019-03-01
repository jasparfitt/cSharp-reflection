using BookWyrm.Shared.Data;
using BookWyrm.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.ViewModels
{
    public class AddIndexViewModel
    {
        public Challenge Challenge { get; set; }
        
        public List<int> ChallengeBooks { get; set; }

        public SelectList Books { get; set; }

        public void Init(BookRepository bookRepository)
        {
            Books = new SelectList(bookRepository.GetList(), "Id", "Title");
        }
    }
}