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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult trackResearch(int? page)
        {
            var research = db.Research;

            //--Pagination 10 each
            var researchResult = research.ToList().ToPagedList(page ?? 1, 10);

            return View(researchResult);
        }

        [HttpPost]
        public ActionResult trackResearch(string budgetYear, string workOverview, string name, string creator, string workGroup, int? page)
        {
            var research = db.Research;

            //--Pagination 10 each
            var researchResult = research.ToList().ToPagedList(page ?? 1, 10);

            //--Search Engine
            if (budgetYear != "-- ปีงบประมาณ --")
            {
                researchResult = db.Research.Where(x => x.budgetYear.StartsWith(budgetYear) || x.budgetYear.Equals(budgetYear)).ToList().ToPagedList(page ?? 1, 10);

                if (researchResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิจัย";
                }

                return View(researchResult);
            }

            if (name != "")
            {
                researchResult = db.Research.Where(x => x.name.StartsWith(name) || x.name.Equals(name)).ToList().ToPagedList(page ?? 1, 10);

                if (researchResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิจัย";
                }

                return View(researchResult);
            }
            if (creator != "")
            {
                researchResult = db.Research.Where(x => x.creator.StartsWith(creator) || x.creator.Equals(creator)).ToList().ToPagedList(page ?? 1, 10);

                if (researchResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิจัย";
                }

                return View(researchResult);
            }
            if (workGroup != "-- กลุ่มงาน --")
            {
                researchResult = db.Research.Where(x => x.workGroup.StartsWith(workGroup) || x.workGroup.Equals(workGroup)).ToList().ToPagedList(page ?? 1, 10);

                if (researchResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิจัย";
                }

                return View(researchResult);
            }
            else
            {
                return View(researchResult);
            }
        }

        public ActionResult trackInnovation(int? page)
        {
            var innovation = db.Innovation;

            //--Pagination 10 each
            var innovationResult = innovation.ToList().ToPagedList(page ?? 1, 10);

            return View(innovationResult);

        }

        [HttpPost]
        public ActionResult trackInnovation(string budgetYear, string workOverview, string name, string creator, string workGroup, int? page)
        {
            var innovation = db.Innovation;

            //--Pagination 10 each
            var innovationResult = innovation.ToList().ToPagedList(page ?? 1, 10);

            //--Search Engine
            if (budgetYear != "-- ปีงบประมาณ --")
            {
                innovationResult = db.Innovation.Where(x => x.budgetYear.StartsWith(budgetYear) || x.budgetYear.Equals(budgetYear)).ToList().ToPagedList(page ?? 1, 10);

                if (innovationResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบนวัตกรรม";
                }

                return View(innovationResult);
            }

            if (name != "")
            {
                innovationResult = db.Innovation.Where(x => x.name.StartsWith(name) || x.name.Equals(name)).ToList().ToPagedList(page ?? 1, 10);

                if (innovationResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบนวัตกรรม";
                }

                return View(innovationResult);
            }
            if (creator != "-- นวัตกร --")
            {
                innovationResult = db.Innovation.Where(x => x.creator.StartsWith(creator) || x.creator.Equals(creator)).ToList().ToPagedList(page ?? 1, 10);

                if (innovationResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบนวัตกรรม";
                }

                return View(innovationResult);
            }
            if (workGroup != "-- กลุ่มงาน --")
            {
                innovationResult = db.Innovation.Where(x => x.workGroup.StartsWith(workGroup) || x.workGroup.Equals(workGroup)).ToList().ToPagedList(page ?? 1, 10);

                if (innovationResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบนวัตกรรม";
                }

                return View(innovationResult);
            }
            else
            {
                return View(innovationResult);
            }
        }

        public ActionResult updateResearchStatus(int id)
        {
            Research research = db.Research.Find(id);
            
            if (research == null)
            {
                return RedirectToAction("trackResearch");
            }

            return View(research);
        }

        [HttpPost]
        public ActionResult updateResearchStatus(Research research)
        {
            if (ModelState.IsValid)
            {
                research.date = DateTime.Today;
                db.Entry(research).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("trackResearch");
            }
            return View(research);
        }

        public ActionResult updateInnovationStatus(int id)
        {
            Innovation innovation = db.Innovation.Find(id);
            if (innovation == null)
            {
                return RedirectToAction("trackInnovation");
            }
            return View(innovation);
        }

        [HttpPost]
        public ActionResult updateInnovationStatus(Innovation innovation)
        {
            if (ModelState.IsValid)
            {
                innovation.date = DateTime.Today;
                db.Entry(innovation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("trackInnovation");
            }
            return View(innovation);
        }
    }
}