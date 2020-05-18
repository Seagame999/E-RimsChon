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
    public class ResearchController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: Research
        public ActionResult Index(int? page)
        {
            var research = db.Research;

            //--Pagination 10 each
            var researchResult = research.ToList().ToPagedList(page ?? 1, 10);

            return View(researchResult);
        }

        [HttpPost]
        public ActionResult Index(string budgetYear, string name, string creator, string workGroup, int? page)
        {
            var research = db.Research;

            //--Pagination 10 each
            var researchResult = research.ToList().ToPagedList(page ?? 1, 10);

            //--Search Engine
            if (budgetYear != "-- ปีงบประมาณ --")
            {
                researchResult = db.Research.Where(x => x.budgetYear.StartsWith(budgetYear) || x.budgetYear.Equals(budgetYear)).ToList().ToPagedList(page ?? 1, 10);

                if(researchResult.TotalItemCount == 0)
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

        public ActionResult AllResearch(int? page)
        {
            var research = db.Research;

            //--Pagination 10 each
            var researchResult = research.ToList().ToPagedList(page ?? 1, 10);

            return View(researchResult);
        }

        public ActionResult CreateResearch()
        {
            var modelResearcher = db.Researcher.OrderBy(x => x.name).ToList();
            ViewBag.ResearcherView = (from item in modelResearcher
                                      select new SelectListItem
                                      {
                                          Text = item.name + " " + item.surname,
                                          Value = item.name.ToString() + item.surname.ToString()
                                      });
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

                //if (Session["Id"] != null || Session["Username"] != null)
                //{
                //    research.idOwner = Convert.ToInt32(Session["Id"]);
                //    research.usernameOwner = Session["Username"].ToString();
                //}

                research.views = 0;
                research.workOverview = 00.00m;
                research.date = DateTime.Today;
                db.Research.Add(research);
                db.SaveChanges();
                ModelState.Clear();



                return RedirectToAction("CreateSuccessMessage");
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
            //--Page Visitor
            research.views = research.views + 1;
            db.Entry(research).State = EntityState.Modified;
            db.SaveChanges();
            return View(research);
        }

        public ActionResult EditResearch(int id)
        {
            Research research = db.Research.Find(id);
            if (research == null)
            {
                return RedirectToAction("Index");
            }

            decimal workOverviewValue = Convert.ToDecimal(research.workOverview);
            ViewBag.workOverview = workOverviewValue;

            var modelResearcher = db.Researcher.OrderBy(x => x.name).ToList();
            ViewBag.ResearcherView = (from item in modelResearcher
                                      select new SelectListItem
                                      {
                                          Text = item.name + " " + item.surname,
                                          Value = item.name.ToString() + item.surname.ToString()
                                      });

            return View(research);
        }

        [HttpPost]
        public ActionResult EditResearch(Research research,string workOverview)
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

                //if (Session["Id"] != null || Session["Username"] != null)
                //{
                //    research.idOwner = Convert.ToInt32(Session["Id"]);
                //    research.usernameOwner = Session["Username"].ToString();
                //}

                research.views = 0;
                research.workOverview = Convert.ToDecimal(workOverview);
                research.date = DateTime.Today;
                db.Entry(research).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditSuccessMessage");
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

        [HttpPost, ActionName("DeleteResearch")]
        public ActionResult DeleteResearchConfirm(int id)
        {
            Research research = db.Research.Find(id);
            db.Research.Remove(research);
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