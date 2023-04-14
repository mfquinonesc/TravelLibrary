using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelLibrary.Models.ViewModels
{
    public class ListBookViewModels
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int IdAuthor { get; set; }
        public int ISBN { get; set; }
        public int IdEditorial { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Pages { get; set; }
        public string EditorialName { get; set; }
        public string EditorialLocation { get; set; }
    }
}