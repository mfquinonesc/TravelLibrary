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
                            Title = book.title,
                            Synopsis = book.synopsis,
                            Pages = book.pages,
                            EditorialName = book.editorialName
                        }).ToList();
            }
            return View(list);
        }
        public ActionResult New()
        {
            GetEditorialList();
            GetAuthorList();
            return View();
        }

        [HttpPost]
        public ActionResult New(BookViewModel model, FormCollection form)
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
                        book.idEditorial = Convert.ToInt32(form["EditorialList"]);

                        var tAuthorHasBook = new TAuthorHasBook();
                        tAuthorHasBook.idAuthor = Convert.ToInt32(form["AuthorList"]);
                        tAuthorHasBook.iSBN = model.ISBN;

                        db.TAuthorHasBook.Add(tAuthorHasBook);
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
        private void GetAuthorList()
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

            List<SelectListItem> items = list.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = $"{d.Name} {d.Surname}",
                    Value = d.IdAuthor.ToString(),
                    Selected = false
                };
            });

            ViewBag.ItemsAuthor = items;
        }
        private void GetEditorialList()
        {
            List<ListEditorialViewModels> list;
            using (LIBRARYEntities db = new LIBRARYEntities())
            {
                list = (from Editorial in db.TEditorial
                        select new ListEditorialViewModels
                        {
                            IdEditorial = Editorial.idEditorial,
                            EditorialName = Editorial.editorialName,
                            EditorialLocation = Editorial.editorialLocation
                        }).ToList();
            }

            List<SelectListItem> items = list.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.EditorialName,
                    Value = d.IdEditorial.ToString(),
                    Selected = false
                };
            });

            ViewBag.ItemsEditorial = items;
        }
        public ActionResult Edit(int id)
        {
            BookViewModel model = new BookViewModel();
            using (LIBRARYEntities db = new LIBRARYEntities())
            {
                var book = db.TBook.Find(id);
                model.ISBN = book.iSBN;
                model.Title = book.title;
                model.Pages = book.pages;
                model.Synopsis = book.synopsis;

                var tAuthorHasBook = db.TAuthorHasBook.First(d => d.iSBN == book.iSBN);

                GetEditorialList();
                GetAuthorList();

                List<SelectListItem> itemsAuthor = (List<SelectListItem>)ViewBag.ItemsAuthor;
                itemsAuthor.First(d => d.Value == tAuthorHasBook.idAuthor.ToString()).Selected = true;

                List<SelectListItem> itemsEditorial = (List<SelectListItem>)ViewBag.ItemsEditorial;
                itemsEditorial.First(d => d.Value == book.idEditorial.ToString()).Selected = true;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel model, FormCollection form)
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
                        book.idEditorial = Convert.ToInt32(form["EditorialList"]);

                        var tAuthorHasBook = new TAuthorHasBook();
                        tAuthorHasBook.idAuthor = Convert.ToInt32(form["AuthorList"]);
                        tAuthorHasBook.iSBN = model.ISBN;

                        db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                        db.Entry(tAuthorHasBook).State = System.Data.Entity.EntityState.Modified;
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

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (LIBRARYEntities db = new LIBRARYEntities())
            {
                var book = db.TBook.Find(id);
                var tAuthorHasBook = db.TAuthorHasBook.First(e => e.iSBN == book.iSBN);

                db.TBook.Remove(book);
                db.TAuthorHasBook.Remove(tAuthorHasBook);
                db.SaveChanges();
            }
            return Redirect("/Book/Book");
        }
    }
}