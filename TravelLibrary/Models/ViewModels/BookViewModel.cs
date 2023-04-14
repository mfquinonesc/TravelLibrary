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
        [StringLength(5)]
        [Display(Name = "Autor")]
        public int IdAuthor { get; set; }

        [Required]
        [StringLength(45)]
        [Display(Name = "ISBN")]
        public int ISBN { get; set; }

        [Required]
        [StringLength(5)]
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

        public List<SelectListItem> AuthorList
        {
            get
            {
                List<ListAuthorViewModels> list;
                using (LIBRARYEntities db = new LIBRARYEntities())
                {
                    list = (from author in db.TAuthor
                            select new ListAuthorViewModels
                            {
                                IdAuthor = author.idAuthor,
                                Name = author.name,
                                Surname = author.surname
                            }).ToList();
                }
                List<SelectListItem> listItems = new List<SelectListItem>();
                foreach(var item in list)
                {
                    listItems.Add(new SelectListItem {
                        Text = $"{item.Name} {item.Surname}",
                        Value = item.IdAuthor.ToString() }); 
                }
                
                return listItems;
            }
        }


    }
}