using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TravelLibrary.Models.ViewModels
{
    public class EditorialViewModel
    {
        public int IdEditorial { get; set; }

        [Required]
        [StringLength(45)]
        [Display(Name = "Nombre")]
        public string EditorialName { get; set; }

        [Required]
        [StringLength(45)]
        [Display(Name = "Ubicación")]
        public string EditorialLocation { get; set; }
    }
}