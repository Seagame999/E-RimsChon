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
            var researchResult = research.OrderByDescending(x => x.date).ToList().ToPagedList(page ?? 1, 10);

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
            var innovationResult = innovation.OrderByDescending(x => x.date).ToList().ToPagedList(page ?? 1, 10);

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
            if (creator != "")
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

            double useNumberofActivities = numberOfActivitiesResearch(id);
            ViewBag.useNumberofActivities = useNumberofActivities;

            var modelStatusActivity = db.StatusActivity.ToList();
            ViewBag.StatusActivityView = (from item in modelStatusActivity
                                      select new SelectListItem
                                      {
                                          Text = item.statusActivity1,
                                          Value = item.statusActivity1
                                      });

            return View(research);
        }

        [HttpPost]
        public ActionResult updateResearchStatus(Research research, double useNumberOfActivities, StatusActivitiesModel statusActivitiesModel)
        {
            double activityValueUse = activityValue(useNumberOfActivities, statusActivitiesModel);

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

        
        public ActionResult updateInnovationStatus(int id)
        {
            Innovation innovation = db.Innovation.Find(id);
            if (innovation == null)
            {
                return RedirectToAction("trackInnovation");
            }

            double useNumberofActivities = numberOfActivitiesInnovation(id);
            ViewBag.useNumberofActivities = useNumberofActivities;

            var modelStatusActivity = db.StatusActivity.ToList();
            ViewBag.StatusActivityView = (from item in modelStatusActivity
                                          select new SelectListItem
                                          {
                                              Text = item.statusActivity1,
                                              Value = item.statusActivity1
                                          });

            return View(innovation);
        }

        [HttpPost]
        public ActionResult updateInnovationStatus(Innovation innovation, double useNumberOfActivities, StatusActivitiesModel statusActivitiesModel)
        {
            double activityValueUse = activityValue(useNumberOfActivities, statusActivitiesModel);

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


        public double activityValue(double useNumberOfActivities, StatusActivitiesModel statusActivitiesModel)
        {
            double statusActivity1Value = 0, statusActivity2Value = 0, statusActivity3Value = 0, statusActivity4Value = 0, statusActivity5Value = 0,
                statusActivity6Value = 0, statusActivity7Value = 0, statusActivity8Value = 0, statusActivity9Value = 0, statusActivity10Value = 0;
            
            if (statusActivitiesModel.statusActivity1 != "ยังไม่ดำเนินการ")
            {
                if (statusActivitiesModel.statusActivity1.Equals("เสร็จสิ้น"))
                {
                    statusActivity1Value = useNumberOfActivities;
                }
                if (statusActivitiesModel.statusActivity1.Equals("ดำเนินการ"))
                {
                    statusActivity1Value = useNumberOfActivities * 0.6;
                }
                if (statusActivitiesModel.statusActivity1.Equals("วางแผน"))
                {
                    statusActivity1Value = useNumberOfActivities * 0.2;
                }
            }

            if (statusActivitiesModel.statusActivity2 != "ยังไม่ดำเนินการ")
            {
                if (statusActivitiesModel.statusActivity2.Equals("เสร็จสิ้น"))
                {
                    statusActivity2Value = useNumberOfActivities;
                }
                if (statusActivitiesModel.statusActivity2.Equals("ดำเนินการ"))
                {
                    statusActivity2Value = useNumberOfActivities * 0.6;
                }
                if (statusActivitiesModel.statusActivity2.Equals("วางแผน"))
                {
                    statusActivity2Value = useNumberOfActivities * 0.2;
                }
            }

            if (statusActivitiesModel.statusActivity3 != "ยังไม่ดำเนินการ")
            {
                if (statusActivitiesModel.statusActivity3.Equals("เสร็จสิ้น"))
                {
                    statusActivity3Value = useNumberOfActivities;
                }
                if (statusActivitiesModel.statusActivity3.Equals("ดำเนินการ"))
                {
                    statusActivity3Value = useNumberOfActivities * 0.6;
                }
                if (statusActivitiesModel.statusActivity3.Equals("วางแผน"))
                {
                    statusActivity3Value = useNumberOfActivities * 0.2;
                }
            }

            if (statusActivitiesModel.statusActivity4 != "ยังไม่ดำเนินการ")
            {
                if (statusActivitiesModel.statusActivity4.Equals("เสร็จสิ้น"))
                {
                    statusActivity4Value = useNumberOfActivities;
                }
                if (statusActivitiesModel.statusActivity4.Equals("ดำเนินการ"))
                {
                    statusActivity4Value = useNumberOfActivities * 0.6;
                }
                if (statusActivitiesModel.statusActivity4.Equals("วางแผน"))
                {
                    statusActivity4Value = useNumberOfActivities * 0.2;
                }
            }

            if (statusActivitiesModel.statusActivity5 != "ยังไม่ดำเนินการ")
            {
                if (statusActivitiesModel.statusActivity5.Equals("เสร็จสิ้น"))
                {
                    statusActivity5Value = useNumberOfActivities;
                }
                if (statusActivitiesModel.statusActivity5.Equals("ดำเนินการ"))
                {
                    statusActivity5Value = useNumberOfActivities * 0.6;
                }
                if (statusActivitiesModel.statusActivity5.Equals("วางแผน"))
                {
                    statusActivity5Value = useNumberOfActivities * 0.2;
                }
            }

            if (statusActivitiesModel.statusActivity6 != "ยังไม่ดำเนินการ")
            {
                if (statusActivitiesModel.statusActivity6.Equals("เสร็จสิ้น"))
                {
                    statusActivity6Value = useNumberOfActivities;
                }
                if (statusActivitiesModel.statusActivity6.Equals("ดำเนินการ"))
                {
                    statusActivity6Value = useNumberOfActivities * 0.6;
                }
                if (statusActivitiesModel.statusActivity6.Equals("วางแผน"))
                {
                    statusActivity6Value = useNumberOfActivities * 0.2;
                }
            }

            if (statusActivitiesModel.statusActivity7 != "ยังไม่ดำเนินการ")
            {
                if (statusActivitiesModel.statusActivity7.Equals("เสร็จสิ้น"))
                {
                    statusActivity7Value = useNumberOfActivities;
                }
                if (statusActivitiesModel.statusActivity7.Equals("ดำเนินการ"))
                {
                    statusActivity7Value = useNumberOfActivities * 0.6;
                }
                if (statusActivitiesModel.statusActivity7.Equals("วางแผน"))
                {
                    statusActivity7Value = useNumberOfActivities * 0.2;
                }
            }

            if (statusActivitiesModel.statusActivity8 != "ยังไม่ดำเนินการ")
            {
                if (statusActivitiesModel.statusActivity8.Equals("เสร็จสิ้น"))
                {
                    statusActivity8Value = useNumberOfActivities;
                }
                if (statusActivitiesModel.statusActivity8.Equals("ดำเนินการ"))
                {
                    statusActivity8Value = useNumberOfActivities * 0.6;
                }
                if (statusActivitiesModel.statusActivity8.Equals("วางแผน"))
                {
                    statusActivity8Value = useNumberOfActivities * 0.2;
                }
            }

            if (statusActivitiesModel.statusActivity9 != "ยังไม่ดำเนินการ")
            {
                if (statusActivitiesModel.statusActivity9.Equals("เสร็จสิ้น"))
                {
                    statusActivity9Value = useNumberOfActivities;
                }
                if (statusActivitiesModel.statusActivity9.Equals("ดำเนินการ"))
                {
                    statusActivity9Value = useNumberOfActivities * 0.6;
                }
                if (statusActivitiesModel.statusActivity9.Equals("วางแผน"))
                {
                    statusActivity9Value = useNumberOfActivities * 0.2;
                }
            }

            if (statusActivitiesModel.statusActivity10 != "ยังไม่ดำเนินการ")
            {
                if (statusActivitiesModel.statusActivity10.Equals("เสร็จสิ้น"))
                {
                    statusActivity10Value = useNumberOfActivities;
                }
                if (statusActivitiesModel.statusActivity10.Equals("ดำเนินการ"))
                {
                    statusActivity10Value = useNumberOfActivities * 0.6;
                }
                if (statusActivitiesModel.statusActivity10.Equals("วางแผน"))
                {
                    statusActivity10Value = useNumberOfActivities * 0.2;
                }
            }

            double sumActivityValue = statusActivity1Value + statusActivity2Value + statusActivity3Value + statusActivity4Value + statusActivity5Value + statusActivity6Value + statusActivity7Value + statusActivity8Value + statusActivity9Value + statusActivity10Value;

            return sumActivityValue;
        }
    }
}