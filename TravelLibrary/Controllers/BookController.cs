using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelLibrary.Models.ViewModels;
using TravelLibrary.Models;
using System.Xml.Linq;

namespace TravelLibrary.Controllers
{
    public class BookController : Controller
    {
        public ActionResult Book()
        {
            List<ListBookViewModels> list;
            using (LIBRARYEntities db = new LIBRARYEntities())
            {
                list = (from book in db.VBookInfo
                        select new ListBookViewModels
                        {
                            ISBN = book.iSBN,
                            Name = book.name,
                            Surname = book.surname,
                            Title= book.title,
                            Synopsis= book.synopsis,
                            Pages= book.pages,
                            EditorialName= book.editorialName                            
                        }).ToList();
            }
            return View(list);
        }
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(BookViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (LIBRARYEntities db = new LIBRARYEntities())
                    {
                        var book = new TBook();
                        book.iSBN = model.ISBN;                       
                        book.title = model.Title;
                        book.synopsis = model.Synopsis;
                        book.pages = model.Pages;
                        book.idEditorial = model.IdEditorial;
                        
                        
                        db.TBook.Add(book);
                        db.SaveChanges();
                    }
                    return Redirect("/Book/Book");
                }
                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }        

        //public ActionResult Edit(int id)
        //{
        //    BookViewModel model = new BookViewModel();
        //    using (LIBRARYEntities db = new LIBRARYEntities())
        //    {
        //        var book = db.TBook.Find(id);
        //        model.IdAuthor = book.idAuthor;
        //        model.Surname = book.surname;
        //        model.IdBook = book.idBook;
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Edit(BookViewModel model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            using (LIBRARYEntities db = new LIBRARYEntities())
        //            {
        //                var book = db.TBook.Find(model.IdBook);
        //                book.name = model.Name;
        //                book.surname = model.Surname;
        //                db.Entry(Book).State = System.Data.Entity.EntityState.Modified;
        //                db.SaveChanges();
        //            }
        //            return Redirect("/Book/Book");
        //        }
        //        return View(model);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    using (LIBRARYEntities db = new LIBRARYEntities())
        //    {
        //        var book = db.TBook.Find(id);
        //        db.TBook.Remove(book);
        //        db.SaveChanges();
        //    }
        //    return Redirect("/Book/Book");
        //}
    }
}