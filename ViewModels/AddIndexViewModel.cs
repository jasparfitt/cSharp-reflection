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

        public SelectList BooksSelectList { get; set; }

        public List<Book> Books { get; set; }

        public void Init(BookRepository bookRepository)
        {
            Books = bookRepository.GetList();
            BooksSelectList = Sort(-1);
        }

        public SelectList Sort(int id)
        {
            var book = Books.Where(b => b.Id == id).ToList();
            if (book.Count == 0)
            {
                return new SelectList(Books, "Id", "Title");
            }
            return new SelectList(book, "Id", "Title");
        }
    }
}