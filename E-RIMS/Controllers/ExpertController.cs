﻿using E_RIMS.Models;
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
    public class ExpertController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: Expert

        public ActionResult Index(int? page)
        {
            var expert = db.Expert;

            //--Pagination 6 cards
            var expertResult = expert.ToList().ToPagedList(page ?? 1, 6);

            return View(expertResult);
        }

        public ActionResult ListExpert()
        {
            var expert = db.Expert;
            return View(expert.ToList());
        }

        public ActionResult CreateExpert()
        {
           var modelNameTitle =  db.NameTitle.ToList();
            ViewBag.nameTitleView = (from item in modelNameTitle
                                     select new SelectListItem
                                     {
                                         Text = item.nameTitle1,
                                         Value = item.nameTitle1.ToString()
                                     });
            return View();
        }

        [HttpPost]
        public ActionResult CreateExpert(Expert expert)
        {
            if (ModelState.IsValid)
            {
                if(expert.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(expert.image2.FileName);
                    string extension = Path.GetExtension(expert.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    expert.image = "~/ImageExpert/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageExpert/"), fileName);
                    expert.image2.SaveAs(fileName);
                }

                db.Expert.Add(expert);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }
            return View(expert);
        }

        public ActionResult ExpertDetails(int id)
        {
            Expert expert = db.Expert.Find(id);
            if(expert == null)
            {
                return HttpNotFound();
            }
            return View(expert);
        }

        public ActionResult EditExpert(int id)
        {
            Expert expert = db.Expert.Find(id);
            if(expert == null)
            {
                return HttpNotFound();
            }

            var modelNameTitle = db.NameTitle.ToList();
            ViewBag.nameTitleView = (from item in modelNameTitle
                                     select new SelectListItem
                                     {
                                         Text = item.nameTitle1,
                                         Value = item.nameTitle1.ToString()
                                     });

            return View(expert);
        }

        [HttpPost]
        public ActionResult EditExpert(Expert expert)
        {
            if (ModelState.IsValid)
            {
                if (expert.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(expert.image2.FileName);
                    string extension = Path.GetExtension(expert.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    expert.image = "~/ImageExpert/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageExpert/"), fileName);
                    expert.image2.SaveAs(fileName);
                }

                db.Entry(expert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expert);
            
        }

        public ActionResult DeleteExpert(int id)
        {
            Expert expert = db.Expert.Find(id);
            if(expert == null)
            {
                return HttpNotFound();
            }
            return View(expert);
        }
        
        [HttpPost,ActionName("DeleteExpert")]
        public ActionResult DeleteExpertConfirm(int id)
        {
            Expert expert = db.Expert.Find(id);
            db.Expert.Remove(expert);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}