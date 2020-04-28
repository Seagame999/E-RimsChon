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
    public class ResearchController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: Research
        public ActionResult Index(string search,int? page)
        {
            var research = db.Research;

            //--Pagination 10 each
            var researchResult = research.ToList().ToPagedList(page ?? 1, 10);

            //--Search Engine
            if (search != null)
            {
                if (search != null)
                {
                    researchResult = db.Research.Where(x => x.name.StartsWith(search) || x.name == search).ToList().ToPagedList(page ?? 1, 10);
                }       
                else if (search != null)
                {
                    researchResult = db.Research.Where(x => x.@abstract.StartsWith(search) || x.@abstract == search).ToList().ToPagedList(page ?? 1, 10);
                }
                else if (search != null)
                {
                    researchResult = db.Research.Where(x => x.creator.StartsWith(search) || x.creator == search).ToList().ToPagedList(page ?? 1, 10);
                }

            }
            //--

            return View(researchResult);
        }

        public ActionResult AllResearch()
        {
            var research = db.Research;
            return View(research.ToList());
        }

        public ActionResult CreateResearch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateResearch(Research research)
        {
            if (ModelState.IsValid)
            {
                if (research.files2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.files2.FileName);
                    string extension = Path.GetExtension(research.files2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.files = "/docUploadResearch/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadResearch/"), fileNameDoc);
                    research.files2.SaveAs(path);
                }

                db.Research.Add(research);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }

            return View(research);
        }


        public ActionResult DetailResearch(int id)
        {
            Research research = db.Research.Find(id);
            if (research == null)
            {
                return RedirectToAction("Index");
            }
            return View(research);
        }

        public ActionResult EditResearch(int id)
        {
            Research research = db.Research.Find(id);
            if (research == null)
            {
                return RedirectToAction("Index");
            }
            return View(research);
        }

        [HttpPost]
        public ActionResult EditResearch(Research research)
        {
            if (ModelState.IsValid)
            {
                if (research.files2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.files2.FileName);
                    string extension = Path.GetExtension(research.files2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.files = "/docUploadResearch/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadResearch/"), fileNameDoc);
                    research.files2.SaveAs(path);
                }

                db.Entry(research).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(research);
        }

        public ActionResult DeleteResearch(int id)
        {
            Research research = db.Research.Find(id);
            if (research == null)
            {
                return RedirectToAction("Index");
            }
            return View(research);
        }

        [HttpPost,ActionName("DeleteResearch")]
        public ActionResult DeleteResearchConfirm(int id)
        {
            Research research = db.Research.Find(id);
            db.Research.Remove(research);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}