using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelLibrary.Models.ViewModels;
using TravelLibrary.Models;

namespace TravelLibrary.Controllers
{
    public class EditorialController : Controller
    {
        public ActionResult Editorial()
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
            return View(list);
        }
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(EditorialViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (LIBRARYEntities db = new LIBRARYEntities())
                    {
                        var Editorial = new TEditorial();
                        Editorial.editorialName = model.EditorialName;
                        Editorial.editorialLocation = model.EditorialLocation;
                        db.TEditorial.Add(Editorial);
                        db.SaveChanges();
                    }
                    return Redirect("/Editorial/Editorial");
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
            EditorialViewModel model = new EditorialViewModel();
            using (LIBRARYEntities db = new LIBRARYEntities())
            {
                var Editorial = db.TEditorial.Find(id);
                model.EditorialName = Editorial.editorialName;
                model.EditorialLocation = Editorial.editorialLocation;
                model.IdEditorial = Editorial.idEditorial;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditorialViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (LIBRARYEntities db = new LIBRARYEntities())
                    {
                        var Editorial = db.TEditorial.Find(model.IdEditorial);
                        Editorial.editorialName = model.EditorialName;
                        Editorial.editorialLocation = model.EditorialLocation;
                        db.Entry(Editorial).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("/Editorial/Editorial");
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
                var Editorial = db.TEditorial.Find(id);
                db.TEditorial.Remove(Editorial);
                db.SaveChanges();
            }
            return Redirect("/Editorial/Editorial");
        }
    }
}