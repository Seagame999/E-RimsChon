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
    public class WorkTrackingController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: WorkTraking
        public ActionResult Index(int? page)
        {
            var work = db.OverallOperation;

            //--Pagination 10 works
            var workResult = work.ToList().ToPagedList(page ?? 1, 10);

            return View();
        }

        public ActionResult trackResearch()
        {
            return View();
        }

        public ActionResult trackInnovation()
        {
            return View();
        }

        public ActionResult updateResearchStatus()
        {
            return View();
        }

        public ActionResult updateInnovationStatus()
        {
            return View();
        }

        public ActionResult Listwork()
        {
            var work = db.OverallOperation;
            return View(work.ToList());
        }

        public ActionResult CreateWork()
        {
            var percentSelect = db.PercentActivities.ToList();
            ViewBag.percentSelect = (from item in percentSelect
                                     select new SelectListItem
                                     {
                                         Text = item.status,
                                         Value = item.percents.ToString()
                                     });


            return View();
        }

        [HttpPost]
        public ActionResult CreateWork(OverallOperation overallOperation)
        {
            if (ModelState.IsValid)
            {
                db.OverallOperation.Add(overallOperation);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }

            return RedirectToAction("CreateWork");
        }

        public ActionResult DetailsWork(int id)
        {
            OverallOperation overallOperation = db.OverallOperation.Find(id);

            if(overallOperation == null)
            {
                return RedirectToAction("Index");
            }

            return View(overallOperation);
        }

        public ActionResult EditWork(int id)
        {
            OverallOperation overallOperation = db.OverallOperation.Find(id);
            if (overallOperation == null)
            {
                return RedirectToAction("Index");
            }

            var percentSelect = db.PercentActivities.ToList();

            ViewBag.percentSelect = (from item in percentSelect
                                     select new SelectListItem
                                     {
                                         Text = item.status,
                                         Value = item.percents.ToString()
                                     });

            return View(overallOperation);
        }

        [HttpPost]
        public ActionResult EditWork(OverallOperation overallOperation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(overallOperation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(overallOperation);
        }


    }
}