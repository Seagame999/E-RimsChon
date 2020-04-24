using E_RIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace E_RIMS.Controllers
{
    public class InnovatorsController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        
        // GET: Innovators
        public ActionResult Index(int? page)
        {
            var innovator = db.Innovator;

            //--Pagination 6 cards
            var innovatorResult = innovator.ToList().ToPagedList(page ?? 1, 6);

            return View(innovator.ToList());
        }

        public ActionResult InnovatorList()
        {
            var innovator = db.Innovator;
            return View(innovator.ToList());
        }

        public ActionResult CreateInnovator()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateInnovator(Innovator innovator)
        {
            if (ModelState.IsValid)
            {
                if(innovator.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(innovator.image2.FileName);
                    string extension = Path.GetExtension(innovator.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovator.image = "/ImageInnovator/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageInnovator/"), fileName);
                    innovator.image2.SaveAs(fileName);
                }

                db.Innovator.Add(innovator);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }
            return View(innovator);
        }

        public ActionResult DetailInnovator(int id)
        {
            Innovator innovator = db.Innovator.Find(id);
            if(innovator == null)
            {
                return HttpNotFound();
            }
            return View(innovator);
        }

        public ActionResult EditInnovator(int id)
        {
            Innovator innovator = db.Innovator.Find(id);
            if (innovator == null)
            {
                return HttpNotFound();
            }
            return View(innovator);
        }

        [HttpPost]
        public ActionResult EditInnovator(Innovator innovator)
        {
            if (ModelState.IsValid)
            {
                if(innovator.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(innovator.image2.FileName);
                    string extension = Path.GetExtension(innovator.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovator.image = "/ImageInnovator/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageInnovator/"), fileName);
                    innovator.image2.SaveAs(fileName);
                }

                db.Entry(innovator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(innovator);
        }

        public ActionResult DeleteInnovator(int id)
        {
            Innovator innovator = db.Innovator.Find(id);
            if (innovator == null)
            {
                return HttpNotFound();
            }
            return View(innovator);
        }

        [HttpPost, ActionName("DeleteInnovator")]
        public ActionResult DeleteInnovatorConfirm(int id)
        {
            Innovator innovator = db.Innovator.Find(id);
            db.Innovator.Remove(innovator);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}