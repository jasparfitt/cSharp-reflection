using BookWyrm.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookWyrm.ViewModels
{
    public class AddBookIndexViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Authors { get; set; }
    }
}