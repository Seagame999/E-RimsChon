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

        //!!!!!หน้านี้ยังเหลือการเก็บค่าเดือนเป็น bool ไว้ใน DB ถ้าไม่ทำหาย
        public ActionResult updateResearchStatus(int id)
        {
            Research research = db.Research.Find(id);

            if (research == null)
            {
                return RedirectToAction("trackResearch");
            }

            double useNumberofActivities = numberOfActivitiesResearch(id);
            ViewBag.useNumberofActivities = useNumberofActivities;

            return View(research);
        }

        [HttpPost]
        public ActionResult updateResearchStatus(Research research, double useNumberOfActivities, string statusActivity1, string statusActivity2,
            string statusActivity3, string statusActivity4, string statusActivity5, string statusActivity6, string statusActivity7, string statusActivity8
            , string statusActivity9, string statusActivity10)
        {
            double activityValueUse = activityValue(useNumberOfActivities, statusActivity1, statusActivity2, statusActivity3, statusActivity4, statusActivity5, statusActivity6, statusActivity7, statusActivity8, statusActivity9, statusActivity10);

            if (ModelState.IsValid)
            {
                research.workOverview = Convert.ToDecimal(activityValueUse);
                research.date = DateTime.Today;
                db.Entry(research).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("trackResearch");
            }
            return View(research);
        }

        //!!!!!หน้านี้ยังเหลือการเก็บค่าเดือนเป็น bool ไว้ใน DB ถ้าไม่ทำหาย
        public ActionResult updateInnovationStatus(int id)
        {
            Innovation innovation = db.Innovation.Find(id);
            if (innovation == null)
            {
                return RedirectToAction("trackInnovation");
            }

            double useNumberofActivities = numberOfActivitiesInnovation(id);
            ViewBag.useNumberofActivities = useNumberofActivities;

            return View(innovation);
        }

        [HttpPost]
        public ActionResult updateInnovationStatus(Innovation innovation, double useNumberOfActivities, string statusActivity1, string statusActivity2,
            string statusActivity3, string statusActivity4, string statusActivity5, string statusActivity6, string statusActivity7, string statusActivity8
            , string statusActivity9, string statusActivity10)
        {
            double activityValueUse = activityValue(useNumberOfActivities, statusActivity1, statusActivity2, statusActivity3, statusActivity4, statusActivity5, statusActivity6, statusActivity7, statusActivity8, statusActivity9, statusActivity10);

            if (ModelState.IsValid)
            {
                innovation.workOverview = Convert.ToDecimal(activityValueUse);
                innovation.date = DateTime.Today;
                db.Entry(innovation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("trackInnovation");
            }
            return View(innovation);
        }


        public double numberOfActivitiesResearch(int id)
        {
            Research research = db.Research.Find(id);

            //numberOfActivities
            double numberOfActivities = 0;
            double percentPerActivity = 0;

            if (research.activity1 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (research.activity2 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (research.activity3 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (research.activity4 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (research.activity5 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (research.activity6 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (research.activity7 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (research.activity8 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (research.activity9 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (research.activity10 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }

            percentPerActivity = 100.00 / numberOfActivities;

            return percentPerActivity;

        }

        public double numberOfActivitiesInnovation(int id)
        {
            Innovation innovation = db.Innovation.Find(id);

            //numberOfActivities
            double numberOfActivities = 0;
            double percentPerActivity = 0;

            if (innovation.activity1 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (innovation.activity2 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (innovation.activity3 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (innovation.activity4 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (innovation.activity5 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (innovation.activity6 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (innovation.activity7 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (innovation.activity8 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (innovation.activity9 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }
            if (innovation.activity10 != null)
            {
                numberOfActivities = numberOfActivities + 1;
            }

            percentPerActivity = 100.00 / numberOfActivities;

            return percentPerActivity;

        }


        public double activityValue(double useNumberOfActivities, string statusActivity1, string statusActivity2,
            string statusActivity3, string statusActivity4, string statusActivity5, string statusActivity6, string statusActivity7, string statusActivity8
            , string statusActivity9, string statusActivity10)
        {
            double statusActivity1Value = 0, statusActivity2Value = 0, statusActivity3Value = 0, statusActivity4Value = 0, statusActivity5Value = 0,
                statusActivity6Value = 0, statusActivity7Value = 0, statusActivity8Value = 0, statusActivity9Value = 0, statusActivity10Value = 0;

            if (statusActivity1.Equals("เสร็จสิ้นแล้ว"))
            {
                statusActivity1Value = useNumberOfActivities;
            }
            if (statusActivity2.Equals("เสร็จสิ้นแล้ว"))
            {
                statusActivity2Value = useNumberOfActivities;
            }
            if (statusActivity3.Equals("เสร็จสิ้นแล้ว"))
            {
                statusActivity3Value = useNumberOfActivities;
            }
            if (statusActivity4.Equals("เสร็จสิ้นแล้ว"))
            {
                statusActivity4Value = useNumberOfActivities;
            }
            if (statusActivity5.Equals("เสร็จสิ้นแล้ว"))
            {
                statusActivity5Value = useNumberOfActivities;
            }
            if (statusActivity6.Equals("เสร็จสิ้นแล้ว"))
            {
                statusActivity6Value = useNumberOfActivities;
            }
            if (statusActivity7.Equals("เสร็จสิ้นแล้ว"))
            {
                statusActivity7Value = useNumberOfActivities;
            }
            if (statusActivity8.Equals("เสร็จสิ้นแล้ว"))
            {
                statusActivity8Value = useNumberOfActivities;
            }
            if (statusActivity9.Equals("เสร็จสิ้นแล้ว"))
            {
                statusActivity9Value = useNumberOfActivities;
            }
            if (statusActivity10.Equals("เสร็จสิ้นแล้ว"))
            {
                statusActivity10Value = useNumberOfActivities;
            }

            double sumActivityValue = statusActivity1Value + statusActivity2Value + statusActivity3Value + statusActivity4Value + statusActivity5Value + statusActivity6Value + statusActivity7Value + statusActivity8Value + statusActivity9Value + statusActivity10Value;

            return sumActivityValue;
        }
    }
}