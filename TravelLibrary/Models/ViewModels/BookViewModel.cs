using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace TravelLibrary.Models.ViewModels
{
    public class BookViewModel
    {
        [Required]
        [Display(Name = "Autor")]
        public int IdAuthor { get; set; }

        [Required]
        [Display(Name = "ISBN")]
        public int ISBN { get; set; }

        [Required]    
        [Display(Name = "Editorial")]
        public int IdEditorial { get; set; }

        [Required]
        [StringLength(45)]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required]
        [StringLength(45)]
        [Display(Name = "Sinopsis")]
        public string Synopsis { get; set; }

        [Required]
        [StringLength(45)]
        [Display(Name = "Páginas")]
        public string Pages { get; set; }      
    }
}