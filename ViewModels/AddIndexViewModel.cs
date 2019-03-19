using BookWyrm.Shared.Data;
using BookWyrm.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookWyrm.ViewModels
{
    public class AddIndexViewModel
    {
        public Challenge Challenge { get; set; }

        [DisplayName("Books"), Required]
        public List<int> ChallengeBooks { get; set; }

        public SelectList BooksSelectList { get; set; }

        public List<Book> Books { get; set; }

        public HttpPostedFileBase Badge { get; set; }

        public void Init(BookRepository bookRepository)
        {
            Books = bookRepository.GetList();
            BooksSelectList = Sort(-1);
        }

        public SelectList Sort(int id)
        {
            if (id == -1)
            {
                return new SelectList(Books, "Id", "Title");
            }

            var book = Books.Where(b => b.Id == id).ToList();

            if (book == null)
            {
                return null;
            }
            return new SelectList(book, "Id", "Title");
        }
    }
}