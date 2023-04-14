using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TravelLibrary.Models.ViewModels
{
    public class AuthorBookViewModel
    {
        [Required]
        [StringLength(5)]
        [Display(Name = "Autor")]
        public int IdAuthor { get; set; }

        [Required]
        [StringLength(45)]
        [Display(Name = "ISBN")]
        public int ISBN { get; set; }

    }
}