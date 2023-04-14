using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelLibrary.Models.ViewModels;
using TravelLibrary.Models;

namespace TravelLibrary.Controllers
{
    public class AuthorController : Controller
    {
        public ActionResult Author()
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
            return View(list);
        }
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(AuthorViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (LIBRARYEntities db = new LIBRARYEntities())
                    {
                        var author = new TAuthor();
                        author.name = model.Name;
                        author.surname = model.Surname;
                        db.TAuthor.Add(author);
                        db.SaveChanges();
                    }
                    return Redirect("/Author/Author");
                }
                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult Edit(int id)
        {
            AuthorViewModel model = new AuthorViewModel();
            using (LIBRARYEntities db = new LIBRARYEntities())
            {
                var author = db.TAuthor.Find(id);
                model.Name = author.name;
                model.Surname = author.surname;
                model.IdAuthor = author.idAuthor;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AuthorViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (LIBRARYEntities db = new LIBRARYEntities())
                    {
                        var author = db.TAuthor.Find(model.IdAuthor);
                        author.name = model.Name;
                        author.surname = model.Surname;
                        db.Entry(author).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("/Author/Author");
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
                var author = db.TAuthor.Find(id);
                db.TAuthor.Remove(author);
                db.SaveChanges();
            }
            return Redirect("/Author/Author");
        }
    }
}