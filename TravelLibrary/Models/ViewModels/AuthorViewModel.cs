using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TravelLibrary.Models.ViewModels
{
    public class AuthorViewModel
    {
        public int IdAuthor { get; set; }

        [Required]
        [StringLength(45)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [StringLength(45)]
        [Display(Name = "Apellido")]
        public string Surname { get; set; }
    }
}