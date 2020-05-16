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
    public class ResearcherController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: Researcher
        public ActionResult Index(int? page)
        {
            var researcher = db.Researcher;

            //--Pagination 6 cards
            var researcherResult = researcher.ToList().ToPagedList(page ?? 1, 6);
            return View(researcherResult);
        }

        public ActionResult ListResearcher(int? page)
        {
            var researcher = db.Researcher;

            //--Pagination 6 cards
            var researcherResult = researcher.ToList().ToPagedList(page ?? 1, 6);
            return View(researcherResult);
        }

        public ActionResult CreateResearcher()
        {
            var modelPosition = db.Position.ToList();
            ViewBag.PositionView = (from item in modelPosition
                                     select new SelectListItem
                                     {
                                         Text = item.position1,
                                         Value = item.position1.ToString()
                                     });

            var modelLevel = db.Level.ToList();
            ViewBag.LevelView = (from item in modelLevel
                                 select new SelectListItem
                                    {
                                        Text = item.levels,
                                        Value = item.levels.ToString()
                                    });
            return View();
        }

        [HttpPost]
        public ActionResult CreateResearcher(Researcher researcher)
        {
            if (ModelState.IsValid)
            {
                if (researcher.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(researcher.image2.FileName);
                    string extension = Path.GetExtension(researcher.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    researcher.image = "/ImageResearcher/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageResearcher/"), fileName);
                    researcher.image2.SaveAs(fileName);

                }

                db.Researcher.Add(researcher);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("CreateSuccessMessage");
            }
            return View(researcher);
        }

        public ActionResult DetailResearcher(int id)
        {
            Researcher researcher = db.Researcher.Find(id);
            if (researcher == null)
            {
                return RedirectToAction("Index");
            }
            return View(researcher);
        }
        
        public ActionResult EditResearcher(int id)
        {
            Researcher researcher = db.Researcher.Find(id);
            if (researcher == null)
            {
                return RedirectToAction("Index");
            }

            var modelPosition = db.Position.ToList();
            ViewBag.PositionView = (from item in modelPosition
                                    select new SelectListItem
                                    {
                                        Text = item.position1,
                                        Value = item.position1.ToString()
                                    });

            var modelLevel = db.Level.ToList();
            ViewBag.LevelView = (from item in modelLevel
                                 select new SelectListItem
                                 {
                                     Text = item.levels,
                                     Value = item.levels.ToString()
                                 });

            return View(researcher);
        }

        [HttpPost]
        public ActionResult EditResearcher(Researcher researcher)
        {
            if (ModelState.IsValid)
            {
                if (researcher.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(researcher.image2.FileName);
                    string extension = Path.GetExtension(researcher.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    researcher.image = "/ImageResearcher/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageResearcher/"), fileName);
                    researcher.image2.SaveAs(fileName);
                }

                db.Entry(researcher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditSuccessMessage");
            }

            return View(researcher);
        }

        public ActionResult DeleteResearcher(int id)
        {
            Researcher researcher = db.Researcher.Find(id);
            if (researcher == null)
            {
                return RedirectToAction("Index");
            }
            return View(researcher);
        }

        [HttpPost, ActionName("DeleteResearcher")]
        public ActionResult DeleteResearcherConfirm(int id)
        {
            Researcher researcher = db.Researcher.Find(id);
            db.Researcher.Remove(researcher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateSuccessMessage()
        {
            return View();
        }

        public ActionResult EditSuccessMessage()
        {
            return View();
        }


    }
}