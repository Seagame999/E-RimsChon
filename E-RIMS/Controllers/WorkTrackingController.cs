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
            if (Session["Role"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------//
        //--รายงานผลโดยรวมของ วิจัย , วิชาการ
        public ActionResult trackResearch(int? page)
        {
            if (Session["Role"] != null)
            {
                var research = db.Research;

                //--Pagination 10 each

                var convertIdOwner = Convert.ToInt32(Session["Id"]);

                var convertUsernameOwner = Session["Username"].ToString();

                var researchResult = research.Where(x => x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).OrderByDescending(x => x.date).ToList().ToPagedList(page ?? 1, 10);

                return View(researchResult);
            }
            else
                return RedirectToAction("Index", "Home");           
        }

        [HttpPost]
        public ActionResult trackResearch(string budgetYear, string workOverview, string name, string creator, string workGroup, int? page)
        {
            var research = db.Research;

            //--Pagination 10 each          
            var convertIdOwner = Convert.ToInt32(Session["Id"]);

            var convertUsernameOwner = Session["Username"].ToString();

            var researchResult = research.Where(x => x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).OrderByDescending(x => x.date).ToList().ToPagedList(page ?? 1, 10);

            //--Search Engine
            if (budgetYear != "-- ปีงบประมาณ --")
            {
                researchResult = db.Research.Where(x => (x.budgetYear.StartsWith(budgetYear) || x.budgetYear.Equals(budgetYear))
                && x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).ToList().ToPagedList(page ?? 1, 10);

                if (researchResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิจัย";
                }

                return View(researchResult);
            }

            if (name != "")
            {
                researchResult = db.Research.Where(x => (x.name.StartsWith(name) || x.name.Equals(name))
                && x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).ToList().ToPagedList(page ?? 1, 10);

                if (researchResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิจัย";
                }

                return View(researchResult);
            }
            if (creator != "")
            {
                researchResult = db.Research.Where(x => (x.creator.StartsWith(creator) || x.creator.Equals(creator))
                && x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).ToList().ToPagedList(page ?? 1, 10);

                if (researchResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิจัย";
                }

                return View(researchResult);
            }
            if (workGroup != "-- กลุ่มงาน --")
            {
                researchResult = db.Research.Where(x => (x.workGroup.StartsWith(workGroup) || x.workGroup.Equals(workGroup))
                && x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).ToList().ToPagedList(page ?? 1, 10);

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

        public ActionResult trackResearchAll(int? page)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var research = db.Research;

                    //--Pagination 10 each
                    var researchResult = research.OrderByDescending(x => x.date).ToList().ToPagedList(page ?? 1, 10);

                    return View(researchResult);
                }
                return RedirectToAction("Index", "WorkTracking");
            }
            else
                return RedirectToAction("Index", "WorkTracking");
        }

        [HttpPost]
        public ActionResult trackResearchAll(string budgetYear, string workOverview, string name, string creator, string workGroup, int? page)
        {
            var research = db.Research;

            //--Pagination 10 each
            var researchResult = research.OrderByDescending(x => x.date).ToList().ToPagedList(page ?? 1, 10);

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

        //---------------------------------------------------------------------------------------------------------------------------------------------//
        //--รายงานผลโดยรวมของ นวัตกรรม
        public ActionResult trackInnovation(int? page)
        {
            if (Session["Role"] != null)
            {
                var innovation = db.Innovation;

                //--Pagination 10 each
                var convertIdOwner = Convert.ToInt32(Session["Id"]);

                var convertUsernameOwner = Session["Username"].ToString();

                var innovationResult = innovation.Where(x => x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).OrderByDescending(x => x.date).ToList().ToPagedList(page ?? 1, 10);

                return View(innovationResult);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult trackInnovation(string budgetYear, string workOverview, string name, string creator, string workGroup, int? page)
        {
            var innovation = db.Innovation;

            //--Pagination 10 each
            var convertIdOwner = Convert.ToInt32(Session["Id"]);

            var convertUsernameOwner = Session["Username"].ToString();

            var innovationResult = innovation.Where(x => x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).OrderByDescending(x => x.date).ToList().ToPagedList(page ?? 1, 10);

            //--Search Engine
            if (budgetYear != "-- ปีงบประมาณ --")
            {
                innovationResult = db.Innovation.Where(x => (x.budgetYear.StartsWith(budgetYear) || x.budgetYear.Equals(budgetYear))
                && x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).ToList().ToPagedList(page ?? 1, 10);

                if (innovationResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบนวัตกรรม";
                }

                return View(innovationResult);
            }

            if (name != "")
            {
                innovationResult = db.Innovation.Where(x => (x.name.StartsWith(name) || x.name.Equals(name))
                && x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).ToList().ToPagedList(page ?? 1, 10);

                if (innovationResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบนวัตกรรม";
                }

                return View(innovationResult);
            }
            if (creator != "")
            {
                innovationResult = db.Innovation.Where(x => (x.creator.StartsWith(creator) || x.creator.Equals(creator))
                && x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).ToList().ToPagedList(page ?? 1, 10);

                if (innovationResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบนวัตกรรม";
                }

                return View(innovationResult);
            }
            if (workGroup != "-- กลุ่มงาน --")
            {

                innovationResult = db.Innovation.Where(x => (x.workGroup.StartsWith(workGroup) || x.workGroup.Equals(workGroup))
                && x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).ToList().ToPagedList(page ?? 1, 10);

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

        public ActionResult trackInnovationAll(int? page)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var innovation = db.Innovation;

                    //--Pagination 10 each

                    var innovationResult = innovation.OrderByDescending(x => x.date).ToList().ToPagedList(page ?? 1, 10);

                    return View(innovationResult);
                }
                return RedirectToAction("Index", "WorkTracking");
            }
            else
                return RedirectToAction("Index", "WorkTracking");
        }

        [HttpPost]
        public ActionResult trackInnovationAll(string budgetYear, string workOverview, string name, string creator, string workGroup, int? page)
        {
            var innovation = db.Innovation;

            //--Pagination 10 each
            var innovationResult = innovation.OrderByDescending(x => x.date).ToList().ToPagedList(page ?? 1, 10);

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

        //---------------------------------------------------------------------------------------------------------------------------------------------//
        //--อัพเดทรายงานวิจัย , วิชาการ
        public ActionResult updateResearchStatus(int id)
        {
            Research research = db.Research.Find(id);

            if (research == null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    return RedirectToAction("trackResearchAll");
                }
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

                db.Research.Attach(research);
                db.Entry(research).Property(x => x.date).IsModified = true;
                db.Entry(research).Property(x => x.workOverview).IsModified = true;
                db.Entry(research).Property(x => x.statusActivity1).IsModified = true;
                db.Entry(research).Property(x => x.statusActivity2).IsModified = true;
                db.Entry(research).Property(x => x.statusActivity3).IsModified = true;
                db.Entry(research).Property(x => x.statusActivity4).IsModified = true;
                db.Entry(research).Property(x => x.statusActivity5).IsModified = true;
                db.Entry(research).Property(x => x.statusActivity6).IsModified = true;
                db.Entry(research).Property(x => x.statusActivity7).IsModified = true;
                db.Entry(research).Property(x => x.statusActivity8).IsModified = true;
                db.Entry(research).Property(x => x.statusActivity9).IsModified = true;
                db.Entry(research).Property(x => x.statusActivity10).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("UpdateSuccessMessageResearch");
            }
            return View(research);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------//
        //--อัพเดทรายงานนวัตกรรม
        public ActionResult updateInnovationStatus(int id)
        {
            Innovation innovation = db.Innovation.Find(id);
            if (innovation == null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    return RedirectToAction("trackInnovationAll");
                }
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
                db.Innovation.Attach(innovation);
                db.Entry(innovation).Property(x => x.date).IsModified = true;
                db.Entry(innovation).Property(x => x.workOverview).IsModified = true;
                db.Entry(innovation).Property(x => x.statusActivity1).IsModified = true;
                db.Entry(innovation).Property(x => x.statusActivity2).IsModified = true;
                db.Entry(innovation).Property(x => x.statusActivity3).IsModified = true;
                db.Entry(innovation).Property(x => x.statusActivity4).IsModified = true;
                db.Entry(innovation).Property(x => x.statusActivity5).IsModified = true;
                db.Entry(innovation).Property(x => x.statusActivity6).IsModified = true;
                db.Entry(innovation).Property(x => x.statusActivity7).IsModified = true;
                db.Entry(innovation).Property(x => x.statusActivity8).IsModified = true;
                db.Entry(innovation).Property(x => x.statusActivity9).IsModified = true;
                db.Entry(innovation).Property(x => x.statusActivity10).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("UpdateSuccessMessageInnovation");
            }
            return View(innovation);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------//

        //--คำนวณจำนวนกิจกรรมทั้งหมด
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

        //--คำนวณเปอร์เซ็นต์งาน
        public double activityValue(double useNumberOfActivities, StatusActivitiesModel statusActivitiesModel)
        {
            double statusActivity1Value = 0, statusActivity2Value = 0, statusActivity3Value = 0, statusActivity4Value = 0, statusActivity5Value = 0,
                statusActivity6Value = 0, statusActivity7Value = 0, statusActivity8Value = 0, statusActivity9Value = 0, statusActivity10Value = 0;

            if (statusActivitiesModel.statusActivity1 != null)
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

            if (statusActivitiesModel.statusActivity2 != null)
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

            if (statusActivitiesModel.statusActivity3 != null)
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

            if (statusActivitiesModel.statusActivity4 != null)
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

            if (statusActivitiesModel.statusActivity5 != null)
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

            if (statusActivitiesModel.statusActivity6 != null)
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

            if (statusActivitiesModel.statusActivity7 != null)
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

            if (statusActivitiesModel.statusActivity8 != null)
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

            if (statusActivitiesModel.statusActivity9 != null)
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

            if (statusActivitiesModel.statusActivity10 != null)
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

            if(sumActivityValue >= 99)
            {
                sumActivityValue = 100.00;
            }

            return sumActivityValue;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------//

        //--การแจ้งเตือนการกระทำ
        public ActionResult UpdateSuccessMessageResearch()
        {
            return View();
        }

        public ActionResult UpdateSuccessMessageInnovation()
        {
            return View();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------//

        //--อัพเดทรายกิจกรรมวิจัย , วิชาการ

        public ActionResult updateResearchActivityStatus1(int id)
        {
            Research research = db.Research.Find(id);

            if (research == null)
            {
                return RedirectToAction("trackResearch");
            }

            return View(research);
        }

        [HttpPost]
        public ActionResult updateResearchActivityStatus1(Research research,int id)
        {

            if (ModelState.IsValid)
            {
                if (research.filePlanStatusActivity1HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.filePlanStatusActivity1HttpPost.FileName);
                    string extension = Path.GetExtension(research.filePlanStatusActivity1HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.filePlanStatusActivity1 = "/filePlanStatusActivityAll/filePlanStatusActivity1/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity1/"), fileNameDoc);
                    research.filePlanStatusActivity1HttpPost.SaveAs(path);
                }

                if (research.fileProceedStatusActivity1HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileProceedStatusActivity1HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileProceedStatusActivity1HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileProceedStatusActivity1 = "/fileProceedStatusActivityAll/fileProceedStatusActivity1/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity1/"), fileNameDoc);
                    research.fileProceedStatusActivity1HttpPost.SaveAs(path);
                }

                if (research.fileFinishStatusActivity1HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileFinishStatusActivity1HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileFinishStatusActivity1HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileFinishStatusActivity1 = "/fileFinishStatusActivityAll/fileFinishStatusActivity1/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity1/"), fileNameDoc);
                    research.fileFinishStatusActivity1HttpPost.SaveAs(path);
                }

                if (research.planStatusActivity1 == null && research.proceedStatusActivity1 == null && research.fileFinishStatusActivity1 == null)
                {
                    research.statusActivity1 = "ยังไม่ดำเนินการ";
                }

                if (research.planStatusActivity1 != null && research.filePlanStatusActivity1 != null)
                {
                    research.statusActivity1 = "วางแผน";
                }
                if (research.planStatusActivity1 != null && research.filePlanStatusActivity1 != null
                    && research.proceedStatusActivity1 != null && research.fileProceedStatusActivity1 != null)
                {
                    research.statusActivity1 = "ดำเนินการ";
                }
                if (research.planStatusActivity1 != null && research.filePlanStatusActivity1 != null
                    && research.proceedStatusActivity1 != null && research.fileProceedStatusActivity1 != null
                    && research.finishStatusActivity1 != null && research.fileFinishStatusActivity1 != null)
                {
                    research.statusActivity1 = "เสร็จสิ้น";
                }

                db.Research.Attach(research);
                db.Entry(research).Property(x => x.statusActivity1).IsModified = true;
                db.Entry(research).Property(x => x.planStatusActivity1).IsModified = true;
                db.Entry(research).Property(x => x.proceedStatusActivity1).IsModified = true;
                db.Entry(research).Property(x => x.finishStatusActivity1).IsModified = true;
                db.Entry(research).Property(x => x.filePlanStatusActivity1).IsModified = true;
                db.Entry(research).Property(x => x.fileProceedStatusActivity1).IsModified = true;
                db.Entry(research).Property(x => x.fileFinishStatusActivity1).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateResearchStatus", new { id });
            }

            return View(research);
        }


        public ActionResult updateResearchActivityStatus2(int id)
        {
            Research research = db.Research.Find(id);

            if (research == null)
            {
                return RedirectToAction("trackResearch");
            }

            return View(research);
        }

        [HttpPost]
        public ActionResult updateResearchActivityStatus2(Research research, int id)
        {

            if (ModelState.IsValid)
            {
                if (research.filePlanStatusActivity2HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.filePlanStatusActivity2HttpPost.FileName);
                    string extension = Path.GetExtension(research.filePlanStatusActivity2HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.filePlanStatusActivity2 = "/filePlanStatusActivityAll/filePlanStatusActivity2/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity2/"), fileNameDoc);
                    research.filePlanStatusActivity2HttpPost.SaveAs(path);
                }

                if (research.fileProceedStatusActivity2HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileProceedStatusActivity2HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileProceedStatusActivity2HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileProceedStatusActivity2 = "/fileProceedStatusActivityAll/fileProceedStatusActivity2/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity2/"), fileNameDoc);
                    research.fileProceedStatusActivity2HttpPost.SaveAs(path);
                }

                if (research.fileFinishStatusActivity2HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileFinishStatusActivity2HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileFinishStatusActivity2HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileFinishStatusActivity2 = "/fileFinishStatusActivityAll/fileFinishStatusActivity2/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity2/"), fileNameDoc);
                    research.fileFinishStatusActivity2HttpPost.SaveAs(path);
                }

                if (research.planStatusActivity2 == null && research.proceedStatusActivity2 == null && research.fileFinishStatusActivity2 == null)
                {
                    research.statusActivity2 = "ยังไม่ดำเนินการ";
                }

                if (research.planStatusActivity2 != null && research.filePlanStatusActivity2 != null)
                {
                    research.statusActivity2 = "วางแผน";
                }
                if (research.planStatusActivity2 != null && research.filePlanStatusActivity2 != null
                    && research.proceedStatusActivity2 != null && research.fileProceedStatusActivity2 != null)
                {
                    research.statusActivity2 = "ดำเนินการ";
                }
                if (research.planStatusActivity2 != null && research.filePlanStatusActivity2 != null
                    && research.proceedStatusActivity2 != null && research.fileProceedStatusActivity2 != null
                    && research.finishStatusActivity2 != null && research.fileFinishStatusActivity2 != null)
                {
                    research.statusActivity2 = "เสร็จสิ้น";
                }

                db.Research.Attach(research);
                db.Entry(research).Property(x => x.statusActivity2).IsModified = true;
                db.Entry(research).Property(x => x.planStatusActivity2).IsModified = true;
                db.Entry(research).Property(x => x.proceedStatusActivity2).IsModified = true;
                db.Entry(research).Property(x => x.finishStatusActivity2).IsModified = true;
                db.Entry(research).Property(x => x.filePlanStatusActivity2).IsModified = true;
                db.Entry(research).Property(x => x.fileProceedStatusActivity2).IsModified = true;
                db.Entry(research).Property(x => x.fileFinishStatusActivity2).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateResearchStatus", new { id });
            }

            return View(research);
        }

        public ActionResult updateResearchActivityStatus3(int id)
        {
            Research research = db.Research.Find(id);

            if (research == null)
            {
                return RedirectToAction("trackResearch");
            }

            return View(research);
        }

        [HttpPost]
        public ActionResult updateResearchActivityStatus3(Research research, int id)
        {
            if (ModelState.IsValid)
            {
                if (research.filePlanStatusActivity3HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.filePlanStatusActivity3HttpPost.FileName);
                    string extension = Path.GetExtension(research.filePlanStatusActivity3HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.filePlanStatusActivity3 = "/filePlanStatusActivityAll/filePlanStatusActivity3/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity3/"), fileNameDoc);
                    research.filePlanStatusActivity3HttpPost.SaveAs(path);
                }

                if (research.fileProceedStatusActivity3HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileProceedStatusActivity3HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileProceedStatusActivity3HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileProceedStatusActivity3 = "/fileProceedStatusActivityAll/fileProceedStatusActivity3/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity3/"), fileNameDoc);
                    research.fileProceedStatusActivity3HttpPost.SaveAs(path);
                }

                if (research.fileFinishStatusActivity3HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileFinishStatusActivity3HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileFinishStatusActivity3HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileFinishStatusActivity3 = "/fileFinishStatusActivityAll/fileFinishStatusActivity3/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity3/"), fileNameDoc);
                    research.fileFinishStatusActivity3HttpPost.SaveAs(path);
                }

                if (research.planStatusActivity3 == null && research.proceedStatusActivity3 == null && research.fileFinishStatusActivity3 == null)
                {
                    research.statusActivity3 = "ยังไม่ดำเนินการ";
                }

                if (research.planStatusActivity3 != null && research.filePlanStatusActivity3 != null)
                {
                    research.statusActivity3 = "วางแผน";
                }
                if (research.planStatusActivity3 != null && research.filePlanStatusActivity3 != null
                    && research.proceedStatusActivity3 != null && research.fileProceedStatusActivity3 != null)
                {
                    research.statusActivity3 = "ดำเนินการ";
                }
                if (research.planStatusActivity3 != null && research.filePlanStatusActivity3 != null
                    && research.proceedStatusActivity3 != null && research.fileProceedStatusActivity3 != null
                    && research.finishStatusActivity3 != null && research.fileFinishStatusActivity3 != null)
                {
                    research.statusActivity3 = "เสร็จสิ้น";
                }

                db.Research.Attach(research);
                db.Entry(research).Property(x => x.statusActivity3).IsModified = true;
                db.Entry(research).Property(x => x.planStatusActivity3).IsModified = true;
                db.Entry(research).Property(x => x.proceedStatusActivity3).IsModified = true;
                db.Entry(research).Property(x => x.finishStatusActivity3).IsModified = true;
                db.Entry(research).Property(x => x.filePlanStatusActivity3).IsModified = true;
                db.Entry(research).Property(x => x.fileProceedStatusActivity3).IsModified = true;
                db.Entry(research).Property(x => x.fileFinishStatusActivity3).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateResearchStatus", new { id });
            }

            return View(research);
        }

        public ActionResult updateResearchActivityStatus4(int id)
        {
            Research research = db.Research.Find(id);

            if (research == null)
            {
                return RedirectToAction("trackResearch");
            }

            return View(research);
        }

        [HttpPost]
        public ActionResult updateResearchActivityStatus4(Research research, int id)
        {

            if (ModelState.IsValid)
            {
                if (research.filePlanStatusActivity4HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.filePlanStatusActivity4HttpPost.FileName);
                    string extension = Path.GetExtension(research.filePlanStatusActivity4HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.filePlanStatusActivity4 = "/filePlanStatusActivityAll/filePlanStatusActivity4/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity4/"), fileNameDoc);
                    research.filePlanStatusActivity4HttpPost.SaveAs(path);
                }

                if (research.fileProceedStatusActivity4HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileProceedStatusActivity4HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileProceedStatusActivity4HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileProceedStatusActivity4 = "/fileProceedStatusActivityAll/fileProceedStatusActivity4/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity4/"), fileNameDoc);
                    research.fileProceedStatusActivity4HttpPost.SaveAs(path);
                }

                if (research.fileFinishStatusActivity4HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileFinishStatusActivity4HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileFinishStatusActivity4HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileFinishStatusActivity4 = "/fileFinishStatusActivityAll/fileFinishStatusActivity4/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity4/"), fileNameDoc);
                    research.fileFinishStatusActivity4HttpPost.SaveAs(path);
                }

                if (research.planStatusActivity4 == null && research.proceedStatusActivity4 == null && research.fileFinishStatusActivity4 == null)
                {
                    research.statusActivity4 = "ยังไม่ดำเนินการ";
                }

                if (research.planStatusActivity4 != null && research.filePlanStatusActivity4 != null)
                {
                    research.statusActivity4 = "วางแผน";
                }
                if (research.planStatusActivity4 != null && research.filePlanStatusActivity4 != null
                    && research.proceedStatusActivity4 != null && research.fileProceedStatusActivity4 != null)
                {
                    research.statusActivity4 = "ดำเนินการ";
                }
                if (research.planStatusActivity4 != null && research.filePlanStatusActivity4 != null
                    && research.proceedStatusActivity4 != null && research.fileProceedStatusActivity4 != null
                    && research.finishStatusActivity4 != null && research.fileFinishStatusActivity4 != null)
                {
                    research.statusActivity4 = "เสร็จสิ้น";
                }

                db.Research.Attach(research);
                db.Entry(research).Property(x => x.statusActivity4).IsModified = true;
                db.Entry(research).Property(x => x.planStatusActivity4).IsModified = true;
                db.Entry(research).Property(x => x.proceedStatusActivity4).IsModified = true;
                db.Entry(research).Property(x => x.finishStatusActivity4).IsModified = true;
                db.Entry(research).Property(x => x.filePlanStatusActivity4).IsModified = true;
                db.Entry(research).Property(x => x.fileProceedStatusActivity4).IsModified = true;
                db.Entry(research).Property(x => x.fileFinishStatusActivity4).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateResearchStatus", new { id });
            }

            return View(research);
        }

        public ActionResult updateResearchActivityStatus5(int id)
        {
            Research research = db.Research.Find(id);

            if (research == null)
            {
                return RedirectToAction("trackResearch");
            }

            return View(research);
        }

        [HttpPost]
        public ActionResult updateResearchActivityStatus5(Research research, int id)
        {

            if (ModelState.IsValid)
            {
                if (research.filePlanStatusActivity5HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.filePlanStatusActivity5HttpPost.FileName);
                    string extension = Path.GetExtension(research.filePlanStatusActivity5HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.filePlanStatusActivity5 = "/filePlanStatusActivityAll/filePlanStatusActivity5/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity5/"), fileNameDoc);
                    research.filePlanStatusActivity5HttpPost.SaveAs(path);
                }

                if (research.fileProceedStatusActivity5HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileProceedStatusActivity5HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileProceedStatusActivity5HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileProceedStatusActivity5 = "/fileProceedStatusActivityAll/fileProceedStatusActivity5/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity5/"), fileNameDoc);
                    research.fileProceedStatusActivity5HttpPost.SaveAs(path);
                }

                if (research.fileFinishStatusActivity5HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileFinishStatusActivity5HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileFinishStatusActivity5HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileFinishStatusActivity5 = "/fileFinishStatusActivityAll/fileFinishStatusActivity5/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity5/"), fileNameDoc);
                    research.fileFinishStatusActivity5HttpPost.SaveAs(path);
                }

                if (research.planStatusActivity5 == null && research.proceedStatusActivity5 == null && research.fileFinishStatusActivity5 == null)
                {
                    research.statusActivity5 = "ยังไม่ดำเนินการ";
                }

                if (research.planStatusActivity5 != null && research.filePlanStatusActivity5 != null)
                {
                    research.statusActivity5 = "วางแผน";
                }
                if (research.planStatusActivity5 != null && research.filePlanStatusActivity5 != null
                    && research.proceedStatusActivity5 != null && research.fileProceedStatusActivity5 != null)
                {
                    research.statusActivity5 = "ดำเนินการ";
                }
                if (research.planStatusActivity5 != null && research.filePlanStatusActivity5 != null
                    && research.proceedStatusActivity5 != null && research.fileProceedStatusActivity5 != null
                    && research.finishStatusActivity5 != null && research.fileFinishStatusActivity5 != null)
                {
                    research.statusActivity5 = "เสร็จสิ้น";
                }

                db.Research.Attach(research);
                db.Entry(research).Property(x => x.statusActivity5).IsModified = true;
                db.Entry(research).Property(x => x.planStatusActivity5).IsModified = true;
                db.Entry(research).Property(x => x.proceedStatusActivity5).IsModified = true;
                db.Entry(research).Property(x => x.finishStatusActivity5).IsModified = true;
                db.Entry(research).Property(x => x.filePlanStatusActivity5).IsModified = true;
                db.Entry(research).Property(x => x.fileProceedStatusActivity5).IsModified = true;
                db.Entry(research).Property(x => x.fileFinishStatusActivity5).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateResearchStatus", new { id });
            }

            return View(research);
        }

        public ActionResult updateResearchActivityStatus6(int id)
        {
            Research research = db.Research.Find(id);

            if (research == null)
            {
                return RedirectToAction("trackResearch");
            }

            return View(research);
        }

        [HttpPost]
        public ActionResult updateResearchActivityStatus6(Research research, int id)
        {

            if (ModelState.IsValid)
            {
                if (research.filePlanStatusActivity6HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.filePlanStatusActivity6HttpPost.FileName);
                    string extension = Path.GetExtension(research.filePlanStatusActivity6HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.filePlanStatusActivity6 = "/filePlanStatusActivityAll/filePlanStatusActivity6/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity6/"), fileNameDoc);
                    research.filePlanStatusActivity6HttpPost.SaveAs(path);
                }

                if (research.fileProceedStatusActivity6HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileProceedStatusActivity6HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileProceedStatusActivity6HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileProceedStatusActivity6 = "/fileProceedStatusActivityAll/fileProceedStatusActivity6/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity6/"), fileNameDoc);
                    research.fileProceedStatusActivity6HttpPost.SaveAs(path);
                }

                if (research.fileFinishStatusActivity6HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileFinishStatusActivity6HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileFinishStatusActivity6HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileFinishStatusActivity6 = "/fileFinishStatusActivityAll/fileFinishStatusActivity6/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity6/"), fileNameDoc);
                    research.fileFinishStatusActivity6HttpPost.SaveAs(path);
                }

                if (research.planStatusActivity6 == null && research.proceedStatusActivity6 == null && research.fileFinishStatusActivity6 == null)
                {
                    research.statusActivity6 = "ยังไม่ดำเนินการ";
                }

                if (research.planStatusActivity6 != null && research.filePlanStatusActivity6 != null)
                {
                    research.statusActivity6 = "วางแผน";
                }
                if (research.planStatusActivity6 != null && research.filePlanStatusActivity6 != null
                    && research.proceedStatusActivity6 != null && research.fileProceedStatusActivity6 != null)
                {
                    research.statusActivity6 = "ดำเนินการ";
                }
                if (research.planStatusActivity6 != null && research.filePlanStatusActivity6 != null
                    && research.proceedStatusActivity6 != null && research.fileProceedStatusActivity6 != null
                    && research.finishStatusActivity6 != null && research.fileFinishStatusActivity6 != null)
                {
                    research.statusActivity6 = "เสร็จสิ้น";
                }

                db.Research.Attach(research);
                db.Entry(research).Property(x => x.statusActivity6).IsModified = true;
                db.Entry(research).Property(x => x.planStatusActivity6).IsModified = true;
                db.Entry(research).Property(x => x.proceedStatusActivity6).IsModified = true;
                db.Entry(research).Property(x => x.finishStatusActivity6).IsModified = true;
                db.Entry(research).Property(x => x.filePlanStatusActivity6).IsModified = true;
                db.Entry(research).Property(x => x.fileProceedStatusActivity6).IsModified = true;
                db.Entry(research).Property(x => x.fileFinishStatusActivity6).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateResearchStatus", new { id });
            }

            return View(research);
        }

        public ActionResult updateResearchActivityStatus7(int id)
        {
            Research research = db.Research.Find(id);

            if (research == null)
            {
                return RedirectToAction("trackResearch");
            }

            return View(research);
        }

        [HttpPost]
        public ActionResult updateResearchActivityStatus7(Research research, int id)
        {

            if (ModelState.IsValid)
            {
                if (research.filePlanStatusActivity7HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.filePlanStatusActivity7HttpPost.FileName);
                    string extension = Path.GetExtension(research.filePlanStatusActivity7HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.filePlanStatusActivity7 = "/filePlanStatusActivityAll/filePlanStatusActivity7/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity7/"), fileNameDoc);
                    research.filePlanStatusActivity7HttpPost.SaveAs(path);
                }

                if (research.fileProceedStatusActivity7HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileProceedStatusActivity7HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileProceedStatusActivity7HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileProceedStatusActivity7 = "/fileProceedStatusActivityAll/fileProceedStatusActivity7/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity7/"), fileNameDoc);
                    research.fileProceedStatusActivity7HttpPost.SaveAs(path);
                }

                if (research.fileFinishStatusActivity7HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileFinishStatusActivity7HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileFinishStatusActivity7HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileFinishStatusActivity7 = "/fileFinishStatusActivityAll/fileFinishStatusActivity7/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity7/"), fileNameDoc);
                    research.fileFinishStatusActivity7HttpPost.SaveAs(path);
                }

                if (research.planStatusActivity7 == null && research.proceedStatusActivity7 == null && research.fileFinishStatusActivity7 == null)
                {
                    research.statusActivity7 = "ยังไม่ดำเนินการ";
                }

                if (research.planStatusActivity7 != null && research.filePlanStatusActivity7 != null)
                {
                    research.statusActivity7 = "วางแผน";
                }
                if (research.planStatusActivity7 != null && research.filePlanStatusActivity7 != null
                    && research.proceedStatusActivity7 != null && research.fileProceedStatusActivity7 != null)
                {
                    research.statusActivity7 = "ดำเนินการ";
                }
                if (research.planStatusActivity7 != null && research.filePlanStatusActivity7 != null
                    && research.proceedStatusActivity7 != null && research.fileProceedStatusActivity7 != null
                    && research.finishStatusActivity7 != null && research.fileFinishStatusActivity7 != null)
                {
                    research.statusActivity7 = "เสร็จสิ้น";
                }

                db.Research.Attach(research);
                db.Entry(research).Property(x => x.statusActivity7).IsModified = true;
                db.Entry(research).Property(x => x.planStatusActivity7).IsModified = true;
                db.Entry(research).Property(x => x.proceedStatusActivity7).IsModified = true;
                db.Entry(research).Property(x => x.finishStatusActivity7).IsModified = true;
                db.Entry(research).Property(x => x.filePlanStatusActivity7).IsModified = true;
                db.Entry(research).Property(x => x.fileProceedStatusActivity7).IsModified = true;
                db.Entry(research).Property(x => x.fileFinishStatusActivity7).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateResearchStatus", new { id });
            }

            return View(research);
        }

        public ActionResult updateResearchActivityStatus8(int id)
        {
            Research research = db.Research.Find(id);

            if (research == null)
            {
                return RedirectToAction("trackResearch");
            }

            return View(research);
        }

        [HttpPost]
        public ActionResult updateResearchActivityStatus8(Research research, int id)
        {

            if (ModelState.IsValid)
            {
                if (research.filePlanStatusActivity8HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.filePlanStatusActivity8HttpPost.FileName);
                    string extension = Path.GetExtension(research.filePlanStatusActivity8HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.filePlanStatusActivity8 = "/filePlanStatusActivityAll/filePlanStatusActivity8/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity8/"), fileNameDoc);
                    research.filePlanStatusActivity8HttpPost.SaveAs(path);
                }

                if (research.fileProceedStatusActivity8HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileProceedStatusActivity8HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileProceedStatusActivity8HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileProceedStatusActivity8 = "/fileProceedStatusActivityAll/fileProceedStatusActivity8/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity8/"), fileNameDoc);
                    research.fileProceedStatusActivity8HttpPost.SaveAs(path);
                }

                if (research.fileFinishStatusActivity8HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileFinishStatusActivity8HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileFinishStatusActivity8HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileFinishStatusActivity8 = "/fileFinishStatusActivityAll/fileFinishStatusActivity8/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity8/"), fileNameDoc);
                    research.fileFinishStatusActivity8HttpPost.SaveAs(path);
                }

                if (research.planStatusActivity8 == null && research.proceedStatusActivity8 == null && research.fileFinishStatusActivity8 == null)
                {
                    research.statusActivity8 = "ยังไม่ดำเนินการ";
                }

                if (research.planStatusActivity8 != null && research.filePlanStatusActivity8 != null)
                {
                    research.statusActivity8 = "วางแผน";
                }
                if (research.planStatusActivity8 != null && research.filePlanStatusActivity8 != null
                    && research.proceedStatusActivity8 != null && research.fileProceedStatusActivity8 != null)
                {
                    research.statusActivity8 = "ดำเนินการ";
                }
                if (research.planStatusActivity8 != null && research.filePlanStatusActivity8 != null
                    && research.proceedStatusActivity8 != null && research.fileProceedStatusActivity8 != null
                    && research.finishStatusActivity8 != null && research.fileFinishStatusActivity8 != null)
                {
                    research.statusActivity8 = "เสร็จสิ้น";
                }

                db.Research.Attach(research);
                db.Entry(research).Property(x => x.statusActivity8).IsModified = true;
                db.Entry(research).Property(x => x.planStatusActivity8).IsModified = true;
                db.Entry(research).Property(x => x.proceedStatusActivity8).IsModified = true;
                db.Entry(research).Property(x => x.finishStatusActivity8).IsModified = true;
                db.Entry(research).Property(x => x.filePlanStatusActivity8).IsModified = true;
                db.Entry(research).Property(x => x.fileProceedStatusActivity8).IsModified = true;
                db.Entry(research).Property(x => x.fileFinishStatusActivity8).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateResearchStatus", new { id });
            }

            return View(research);
        }

        public ActionResult updateResearchActivityStatus9(int id)
        {
            Research research = db.Research.Find(id);

            if (research == null)
            {
                return RedirectToAction("trackResearch");
            }

            return View(research);
        }

        [HttpPost]
        public ActionResult updateResearchActivityStatus9(Research research, int id)
        {

            if (ModelState.IsValid)
            {
                if (research.filePlanStatusActivity9HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.filePlanStatusActivity9HttpPost.FileName);
                    string extension = Path.GetExtension(research.filePlanStatusActivity9HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.filePlanStatusActivity9 = "/filePlanStatusActivityAll/filePlanStatusActivity9/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity9/"), fileNameDoc);
                    research.filePlanStatusActivity9HttpPost.SaveAs(path);
                }

                if (research.fileProceedStatusActivity9HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileProceedStatusActivity9HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileProceedStatusActivity9HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileProceedStatusActivity9 = "/fileProceedStatusActivityAll/fileProceedStatusActivity9/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity9/"), fileNameDoc);
                    research.fileProceedStatusActivity9HttpPost.SaveAs(path);
                }

                if (research.fileFinishStatusActivity9HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileFinishStatusActivity9HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileFinishStatusActivity9HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileFinishStatusActivity9 = "/fileFinishStatusActivityAll/fileFinishStatusActivity9/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity9/"), fileNameDoc);
                    research.fileFinishStatusActivity9HttpPost.SaveAs(path);
                }

                if (research.planStatusActivity9 == null && research.proceedStatusActivity9 == null && research.fileFinishStatusActivity9 == null)
                {
                    research.statusActivity9 = "ยังไม่ดำเนินการ";
                }

                if (research.planStatusActivity9 != null && research.filePlanStatusActivity9 != null)
                {
                    research.statusActivity9 = "วางแผน";
                }
                if (research.planStatusActivity9 != null && research.filePlanStatusActivity9 != null
                    && research.proceedStatusActivity9 != null && research.fileProceedStatusActivity9 != null)
                {
                    research.statusActivity9 = "ดำเนินการ";
                }
                if (research.planStatusActivity9 != null && research.filePlanStatusActivity9 != null
                    && research.proceedStatusActivity9 != null && research.fileProceedStatusActivity9 != null
                    && research.finishStatusActivity9 != null && research.fileFinishStatusActivity9 != null)
                {
                    research.statusActivity9 = "เสร็จสิ้น";
                }

                db.Research.Attach(research);
                db.Entry(research).Property(x => x.statusActivity9).IsModified = true;
                db.Entry(research).Property(x => x.planStatusActivity9).IsModified = true;
                db.Entry(research).Property(x => x.proceedStatusActivity9).IsModified = true;
                db.Entry(research).Property(x => x.finishStatusActivity9).IsModified = true;
                db.Entry(research).Property(x => x.filePlanStatusActivity9).IsModified = true;
                db.Entry(research).Property(x => x.fileProceedStatusActivity9).IsModified = true;
                db.Entry(research).Property(x => x.fileFinishStatusActivity9).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateResearchStatus", new { id });
            }

            return View(research);
        }

        public ActionResult updateResearchActivityStatus10(int id)
        {
            Research research = db.Research.Find(id);

            if (research == null)
            {
                return RedirectToAction("trackResearch");
            }

            return View(research);
        }

        [HttpPost]
        public ActionResult updateResearchActivityStatus10(Research research, int id)
        {

            if (ModelState.IsValid)
            {
                if (research.filePlanStatusActivity10HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.filePlanStatusActivity10HttpPost.FileName);
                    string extension = Path.GetExtension(research.filePlanStatusActivity10HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.filePlanStatusActivity10 = "/filePlanStatusActivityAll/filePlanStatusActivity10/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity10/"), fileNameDoc);
                    research.filePlanStatusActivity10HttpPost.SaveAs(path);
                }

                if (research.fileProceedStatusActivity10HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileProceedStatusActivity10HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileProceedStatusActivity10HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileProceedStatusActivity10 = "/fileProceedStatusActivityAll/fileProceedStatusActivity10/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity10/"), fileNameDoc);
                    research.fileProceedStatusActivity10HttpPost.SaveAs(path);
                }

                if (research.fileFinishStatusActivity10HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.fileFinishStatusActivity10HttpPost.FileName);
                    string extension = Path.GetExtension(research.fileFinishStatusActivity10HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.fileFinishStatusActivity10 = "/fileFinishStatusActivityAll/fileFinishStatusActivity10/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity10/"), fileNameDoc);
                    research.fileFinishStatusActivity10HttpPost.SaveAs(path);
                }

                if (research.planStatusActivity10 == null && research.proceedStatusActivity10 == null && research.fileFinishStatusActivity10 == null)
                {
                    research.statusActivity10 = "ยังไม่ดำเนินการ";
                }

                if (research.planStatusActivity10 != null && research.filePlanStatusActivity10 != null)
                {
                    research.statusActivity10 = "วางแผน";
                }
                if (research.planStatusActivity10 != null && research.filePlanStatusActivity10 != null
                    && research.proceedStatusActivity10 != null && research.fileProceedStatusActivity10 != null)
                {
                    research.statusActivity10 = "ดำเนินการ";
                }
                if (research.planStatusActivity10 != null && research.filePlanStatusActivity10 != null
                    && research.proceedStatusActivity10 != null && research.fileProceedStatusActivity10 != null
                    && research.finishStatusActivity10 != null && research.fileFinishStatusActivity10 != null)
                {
                    research.statusActivity10 = "เสร็จสิ้น";
                }

                db.Research.Attach(research);
                db.Entry(research).Property(x => x.statusActivity10).IsModified = true;
                db.Entry(research).Property(x => x.planStatusActivity10).IsModified = true;
                db.Entry(research).Property(x => x.proceedStatusActivity10).IsModified = true;
                db.Entry(research).Property(x => x.finishStatusActivity10).IsModified = true;
                db.Entry(research).Property(x => x.filePlanStatusActivity10).IsModified = true;
                db.Entry(research).Property(x => x.fileProceedStatusActivity10).IsModified = true;
                db.Entry(research).Property(x => x.fileFinishStatusActivity10).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateResearchStatus", new { id });
            }

            return View(research);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------//

        //--อัพเดทรายกิจกรรมนวัตกรรม
        public ActionResult updateInnovationActivityStatus1(int id)
        {
            Innovation innovation = db.Innovation.Find(id);

            if (innovation == null)
            {
                return RedirectToAction("trackInnovation");
            }

            return View(innovation);
        }

        [HttpPost]
        public ActionResult updateInnovationActivityStatus1(Innovation innovation, int id)
        {
            if (ModelState.IsValid)
            {
                if (innovation.filePlanStatusActivity1HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.filePlanStatusActivity1HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.filePlanStatusActivity1HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.filePlanStatusActivity1 = "/filePlanStatusActivityAll/filePlanStatusActivity1/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity1/"), fileNameDoc);
                    innovation.filePlanStatusActivity1HttpPost.SaveAs(path);
                }

                if (innovation.fileProceedStatusActivity1HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileProceedStatusActivity1HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileProceedStatusActivity1HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileProceedStatusActivity1 = "/fileProceedStatusActivityAll/fileProceedStatusActivity1/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity1/"), fileNameDoc);
                    innovation.fileProceedStatusActivity1HttpPost.SaveAs(path);
                }

                if (innovation.fileFinishStatusActivity1HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileFinishStatusActivity1HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileFinishStatusActivity1HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileFinishStatusActivity1 = "/fileFinishStatusActivityAll/fileFinishStatusActivity1/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity1/"), fileNameDoc);
                    innovation.fileFinishStatusActivity1HttpPost.SaveAs(path);
                }

                if (innovation.planStatusActivity1 == null && innovation.proceedStatusActivity1 == null && innovation.fileFinishStatusActivity1 == null)
                {
                    innovation.statusActivity1 = "ยังไม่ดำเนินการ";
                }

                if (innovation.planStatusActivity1 != null && innovation.filePlanStatusActivity1 != null)
                {
                    innovation.statusActivity1 = "วางแผน";
                }
                if (innovation.planStatusActivity1 != null && innovation.filePlanStatusActivity1 != null
                    && innovation.proceedStatusActivity1 != null && innovation.fileProceedStatusActivity1 != null)
                {
                    innovation.statusActivity1 = "ดำเนินการ";
                }
                if (innovation.planStatusActivity1 != null && innovation.filePlanStatusActivity1 != null
                    && innovation.proceedStatusActivity1 != null && innovation.fileProceedStatusActivity1 != null
                    && innovation.finishStatusActivity1 != null && innovation.fileFinishStatusActivity1 != null)
                {
                    innovation.statusActivity1 = "เสร็จสิ้น";
                }

                db.Innovation.Attach(innovation);
                db.Entry(innovation).Property(x => x.statusActivity1).IsModified = true;
                db.Entry(innovation).Property(x => x.planStatusActivity1).IsModified = true;
                db.Entry(innovation).Property(x => x.proceedStatusActivity1).IsModified = true;
                db.Entry(innovation).Property(x => x.finishStatusActivity1).IsModified = true;
                db.Entry(innovation).Property(x => x.filePlanStatusActivity1).IsModified = true;
                db.Entry(innovation).Property(x => x.fileProceedStatusActivity1).IsModified = true;
                db.Entry(innovation).Property(x => x.fileFinishStatusActivity1).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateInnovationStatus", new { id });
            }

            return View(innovation);
        }

        public ActionResult updateInnovationActivityStatus2(int id)
        {
            Innovation innovation = db.Innovation.Find(id);

            if (innovation == null)
            {
                return RedirectToAction("trackInnovation");
            }

            return View(innovation);
        }

        [HttpPost]
        public ActionResult updateInnovationActivityStatus2(Innovation innovation, int id)
        {

            if (ModelState.IsValid)
            {
                if (innovation.filePlanStatusActivity2HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.filePlanStatusActivity2HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.filePlanStatusActivity2HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.filePlanStatusActivity2 = "/filePlanStatusActivityAll/filePlanStatusActivity2/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity2/"), fileNameDoc);
                    innovation.filePlanStatusActivity2HttpPost.SaveAs(path);
                }

                if (innovation.fileProceedStatusActivity2HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileProceedStatusActivity2HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileProceedStatusActivity2HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileProceedStatusActivity2 = "/fileProceedStatusActivityAll/fileProceedStatusActivity2/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity2/"), fileNameDoc);
                    innovation.fileProceedStatusActivity2HttpPost.SaveAs(path);
                }

                if (innovation.fileFinishStatusActivity2HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileFinishStatusActivity2HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileFinishStatusActivity2HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileFinishStatusActivity2 = "/fileFinishStatusActivityAll/fileFinishStatusActivity2/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity2/"), fileNameDoc);
                    innovation.fileFinishStatusActivity2HttpPost.SaveAs(path);
                }

                if (innovation.planStatusActivity2 == null && innovation.proceedStatusActivity2 == null && innovation.fileFinishStatusActivity2 == null)
                {
                    innovation.statusActivity2 = "ยังไม่ดำเนินการ";
                }

                if (innovation.planStatusActivity2 != null && innovation.filePlanStatusActivity2 != null)
                {
                    innovation.statusActivity2 = "วางแผน";
                }
                if (innovation.planStatusActivity2 != null && innovation.filePlanStatusActivity2 != null
                    && innovation.proceedStatusActivity2 != null && innovation.fileProceedStatusActivity2 != null)
                {
                    innovation.statusActivity2 = "ดำเนินการ";
                }
                if (innovation.planStatusActivity2 != null && innovation.filePlanStatusActivity2 != null
                    && innovation.proceedStatusActivity2 != null && innovation.fileProceedStatusActivity2 != null
                    && innovation.finishStatusActivity2 != null && innovation.fileFinishStatusActivity2 != null)
                {
                    innovation.statusActivity2 = "เสร็จสิ้น";
                }

                db.Innovation.Attach(innovation);
                db.Entry(innovation).Property(x => x.statusActivity2).IsModified = true;
                db.Entry(innovation).Property(x => x.planStatusActivity2).IsModified = true;
                db.Entry(innovation).Property(x => x.proceedStatusActivity2).IsModified = true;
                db.Entry(innovation).Property(x => x.finishStatusActivity2).IsModified = true;
                db.Entry(innovation).Property(x => x.filePlanStatusActivity2).IsModified = true;
                db.Entry(innovation).Property(x => x.fileProceedStatusActivity2).IsModified = true;
                db.Entry(innovation).Property(x => x.fileFinishStatusActivity2).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateInnovationStatus", new { id });
            }

            return View(innovation);
        }

        public ActionResult updateInnovationActivityStatus3(int id)
        {
            Innovation innovation = db.Innovation.Find(id);

            if (innovation == null)
            {
                return RedirectToAction("trackInnovation");
            }

            return View(innovation);
        }

        [HttpPost]
        public ActionResult updateInnovationActivityStatus3(Innovation innovation, int id)
        {
            if (ModelState.IsValid)
            {
                if (innovation.filePlanStatusActivity3HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.filePlanStatusActivity3HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.filePlanStatusActivity3HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.filePlanStatusActivity3 = "/filePlanStatusActivityAll/filePlanStatusActivity3/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity3/"), fileNameDoc);
                    innovation.filePlanStatusActivity3HttpPost.SaveAs(path);
                }

                if (innovation.fileProceedStatusActivity3HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileProceedStatusActivity3HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileProceedStatusActivity3HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileProceedStatusActivity3 = "/fileProceedStatusActivityAll/fileProceedStatusActivity3/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity3/"), fileNameDoc);
                    innovation.fileProceedStatusActivity3HttpPost.SaveAs(path);
                }

                if (innovation.fileFinishStatusActivity3HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileFinishStatusActivity3HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileFinishStatusActivity3HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileFinishStatusActivity3 = "/fileFinishStatusActivityAll/fileFinishStatusActivity3/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity3/"), fileNameDoc);
                    innovation.fileFinishStatusActivity3HttpPost.SaveAs(path);
                }

                if (innovation.planStatusActivity3 == null && innovation.proceedStatusActivity3 == null && innovation.fileFinishStatusActivity3 == null)
                {
                    innovation.statusActivity3 = "ยังไม่ดำเนินการ";
                }

                if (innovation.planStatusActivity3 != null && innovation.filePlanStatusActivity3 != null)
                {
                    innovation.statusActivity3 = "วางแผน";
                }
                if (innovation.planStatusActivity3 != null && innovation.filePlanStatusActivity3 != null
                    && innovation.proceedStatusActivity3 != null && innovation.fileProceedStatusActivity3 != null)
                {
                    innovation.statusActivity3 = "ดำเนินการ";
                }
                if (innovation.planStatusActivity3 != null && innovation.filePlanStatusActivity3 != null
                    && innovation.proceedStatusActivity3 != null && innovation.fileProceedStatusActivity3 != null
                    && innovation.finishStatusActivity3 != null && innovation.fileFinishStatusActivity3 != null)
                {
                    innovation.statusActivity3 = "เสร็จสิ้น";
                }

                db.Innovation.Attach(innovation);
                db.Entry(innovation).Property(x => x.statusActivity3).IsModified = true;
                db.Entry(innovation).Property(x => x.planStatusActivity3).IsModified = true;
                db.Entry(innovation).Property(x => x.proceedStatusActivity3).IsModified = true;
                db.Entry(innovation).Property(x => x.finishStatusActivity3).IsModified = true;
                db.Entry(innovation).Property(x => x.filePlanStatusActivity3).IsModified = true;
                db.Entry(innovation).Property(x => x.fileProceedStatusActivity3).IsModified = true;
                db.Entry(innovation).Property(x => x.fileFinishStatusActivity3).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateInnovationStatus", new { id });
            }

            return View(innovation);
        }

        public ActionResult updateInnovationActivityStatus4(int id)
        {
            Innovation innovation = db.Innovation.Find(id);

            if (innovation == null)
            {
                return RedirectToAction("trackInnovation");
            }

            return View(innovation);
        }

        [HttpPost]
        public ActionResult updateInnovationActivityStatus4(Innovation innovation, int id)
        {

            if (ModelState.IsValid)
            {
                if (innovation.filePlanStatusActivity4HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.filePlanStatusActivity4HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.filePlanStatusActivity4HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.filePlanStatusActivity4 = "/filePlanStatusActivityAll/filePlanStatusActivity4/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity4/"), fileNameDoc);
                    innovation.filePlanStatusActivity4HttpPost.SaveAs(path);
                }

                if (innovation.fileProceedStatusActivity4HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileProceedStatusActivity4HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileProceedStatusActivity4HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileProceedStatusActivity4 = "/fileProceedStatusActivityAll/fileProceedStatusActivity4/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity4/"), fileNameDoc);
                    innovation.fileProceedStatusActivity4HttpPost.SaveAs(path);
                }

                if (innovation.fileFinishStatusActivity4HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileFinishStatusActivity4HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileFinishStatusActivity4HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileFinishStatusActivity4 = "/fileFinishStatusActivityAll/fileFinishStatusActivity4/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity4/"), fileNameDoc);
                    innovation.fileFinishStatusActivity4HttpPost.SaveAs(path);
                }

                if (innovation.planStatusActivity4 == null && innovation.proceedStatusActivity4 == null && innovation.fileFinishStatusActivity4 == null)
                {
                    innovation.statusActivity4 = "ยังไม่ดำเนินการ";
                }

                if (innovation.planStatusActivity4 != null && innovation.filePlanStatusActivity4 != null)
                {
                    innovation.statusActivity4 = "วางแผน";
                }
                if (innovation.planStatusActivity4 != null && innovation.filePlanStatusActivity4 != null
                    && innovation.proceedStatusActivity4 != null && innovation.fileProceedStatusActivity4 != null)
                {
                    innovation.statusActivity4 = "ดำเนินการ";
                }
                if (innovation.planStatusActivity4 != null && innovation.filePlanStatusActivity4 != null
                    && innovation.proceedStatusActivity4 != null && innovation.fileProceedStatusActivity4 != null
                    && innovation.finishStatusActivity4 != null && innovation.fileFinishStatusActivity4 != null)
                {
                    innovation.statusActivity4 = "เสร็จสิ้น";
                }

                db.Innovation.Attach(innovation);
                db.Entry(innovation).Property(x => x.statusActivity4).IsModified = true;
                db.Entry(innovation).Property(x => x.planStatusActivity4).IsModified = true;
                db.Entry(innovation).Property(x => x.proceedStatusActivity4).IsModified = true;
                db.Entry(innovation).Property(x => x.finishStatusActivity4).IsModified = true;
                db.Entry(innovation).Property(x => x.filePlanStatusActivity4).IsModified = true;
                db.Entry(innovation).Property(x => x.fileProceedStatusActivity4).IsModified = true;
                db.Entry(innovation).Property(x => x.fileFinishStatusActivity4).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateInnovationStatus", new { id });
            }

            return View(innovation);
        }

        public ActionResult updateInnovationActivityStatus5(int id)
        {
            Innovation innovation = db.Innovation.Find(id);

            if (innovation == null)
            {
                return RedirectToAction("trackInnovation");
            }

            return View(innovation);
        }

        [HttpPost]
        public ActionResult updateInnovationActivityStatus5(Innovation innovation, int id)
        {

            if (ModelState.IsValid)
            {
                if (innovation.filePlanStatusActivity5HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.filePlanStatusActivity5HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.filePlanStatusActivity5HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.filePlanStatusActivity5 = "/filePlanStatusActivityAll/filePlanStatusActivity5/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity5/"), fileNameDoc);
                    innovation.filePlanStatusActivity5HttpPost.SaveAs(path);
                }

                if (innovation.fileProceedStatusActivity5HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileProceedStatusActivity5HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileProceedStatusActivity5HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileProceedStatusActivity5 = "/fileProceedStatusActivityAll/fileProceedStatusActivity5/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity5/"), fileNameDoc);
                    innovation.fileProceedStatusActivity5HttpPost.SaveAs(path);
                }

                if (innovation.fileFinishStatusActivity5HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileFinishStatusActivity5HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileFinishStatusActivity5HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileFinishStatusActivity5 = "/fileFinishStatusActivityAll/fileFinishStatusActivity5/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity5/"), fileNameDoc);
                    innovation.fileFinishStatusActivity5HttpPost.SaveAs(path);
                }

                if (innovation.planStatusActivity5 == null && innovation.proceedStatusActivity5 == null && innovation.fileFinishStatusActivity5 == null)
                {
                    innovation.statusActivity5 = "ยังไม่ดำเนินการ";
                }

                if (innovation.planStatusActivity5 != null && innovation.filePlanStatusActivity5 != null)
                {
                    innovation.statusActivity5 = "วางแผน";
                }
                if (innovation.planStatusActivity5 != null && innovation.filePlanStatusActivity5 != null
                    && innovation.proceedStatusActivity5 != null && innovation.fileProceedStatusActivity5 != null)
                {
                    innovation.statusActivity5 = "ดำเนินการ";
                }
                if (innovation.planStatusActivity5 != null && innovation.filePlanStatusActivity5 != null
                    && innovation.proceedStatusActivity5 != null && innovation.fileProceedStatusActivity5 != null
                    && innovation.finishStatusActivity5 != null && innovation.fileFinishStatusActivity5 != null)
                {
                    innovation.statusActivity5 = "เสร็จสิ้น";
                }

                db.Innovation.Attach(innovation);
                db.Entry(innovation).Property(x => x.statusActivity5).IsModified = true;
                db.Entry(innovation).Property(x => x.planStatusActivity5).IsModified = true;
                db.Entry(innovation).Property(x => x.proceedStatusActivity5).IsModified = true;
                db.Entry(innovation).Property(x => x.finishStatusActivity5).IsModified = true;
                db.Entry(innovation).Property(x => x.filePlanStatusActivity5).IsModified = true;
                db.Entry(innovation).Property(x => x.fileProceedStatusActivity5).IsModified = true;
                db.Entry(innovation).Property(x => x.fileFinishStatusActivity5).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateInnovationStatus", new { id });
            }

            return View(innovation);
        }

        public ActionResult updateInnovationActivityStatus6(int id)
        {
            Innovation innovation = db.Innovation.Find(id);

            if (innovation == null)
            {
                return RedirectToAction("trackInnovation");
            }

            return View(innovation);
        }

        [HttpPost]
        public ActionResult updateInnovationActivityStatus6(Innovation innovation, int id)
        {

            if (ModelState.IsValid)
            {
                if (innovation.filePlanStatusActivity6HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.filePlanStatusActivity6HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.filePlanStatusActivity6HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.filePlanStatusActivity6 = "/filePlanStatusActivityAll/filePlanStatusActivity6/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity6/"), fileNameDoc);
                    innovation.filePlanStatusActivity6HttpPost.SaveAs(path);
                }

                if (innovation.fileProceedStatusActivity6HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileProceedStatusActivity6HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileProceedStatusActivity6HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileProceedStatusActivity6 = "/fileProceedStatusActivityAll/fileProceedStatusActivity6/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity6/"), fileNameDoc);
                    innovation.fileProceedStatusActivity6HttpPost.SaveAs(path);
                }

                if (innovation.fileFinishStatusActivity6HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileFinishStatusActivity6HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileFinishStatusActivity6HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileFinishStatusActivity6 = "/fileFinishStatusActivityAll/fileFinishStatusActivity6/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity6/"), fileNameDoc);
                    innovation.fileFinishStatusActivity6HttpPost.SaveAs(path);
                }

                if (innovation.planStatusActivity6 == null && innovation.proceedStatusActivity6 == null && innovation.fileFinishStatusActivity6 == null)
                {
                    innovation.statusActivity6 = "ยังไม่ดำเนินการ";
                }

                if (innovation.planStatusActivity6 != null && innovation.filePlanStatusActivity6 != null)
                {
                    innovation.statusActivity6 = "วางแผน";
                }
                if (innovation.planStatusActivity6 != null && innovation.filePlanStatusActivity6 != null
                    && innovation.proceedStatusActivity6 != null && innovation.fileProceedStatusActivity6 != null)
                {
                    innovation.statusActivity6 = "ดำเนินการ";
                }
                if (innovation.planStatusActivity6 != null && innovation.filePlanStatusActivity6 != null
                    && innovation.proceedStatusActivity6 != null && innovation.fileProceedStatusActivity6 != null
                    && innovation.finishStatusActivity6 != null && innovation.fileFinishStatusActivity6 != null)
                {
                    innovation.statusActivity6 = "เสร็จสิ้น";
                }

                db.Innovation.Attach(innovation);
                db.Entry(innovation).Property(x => x.statusActivity6).IsModified = true;
                db.Entry(innovation).Property(x => x.planStatusActivity6).IsModified = true;
                db.Entry(innovation).Property(x => x.proceedStatusActivity6).IsModified = true;
                db.Entry(innovation).Property(x => x.finishStatusActivity6).IsModified = true;
                db.Entry(innovation).Property(x => x.filePlanStatusActivity6).IsModified = true;
                db.Entry(innovation).Property(x => x.fileProceedStatusActivity6).IsModified = true;
                db.Entry(innovation).Property(x => x.fileFinishStatusActivity6).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateInnovationStatus", new { id });
            }

            return View(innovation);
        }

        public ActionResult updateInnovationActivityStatus7(int id)
        {
            Innovation innovation = db.Innovation.Find(id);

            if (innovation == null)
            {
                return RedirectToAction("trackInnovation");
            }

            return View(innovation);
        }

        [HttpPost]
        public ActionResult updateInnovationActivityStatus7(Innovation innovation, int id)
        {

            if (ModelState.IsValid)
            {
                if (innovation.filePlanStatusActivity7HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.filePlanStatusActivity7HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.filePlanStatusActivity7HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.filePlanStatusActivity7 = "/filePlanStatusActivityAll/filePlanStatusActivity7/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity7/"), fileNameDoc);
                    innovation.filePlanStatusActivity7HttpPost.SaveAs(path);
                }

                if (innovation.fileProceedStatusActivity7HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileProceedStatusActivity7HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileProceedStatusActivity7HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileProceedStatusActivity7 = "/fileProceedStatusActivityAll/fileProceedStatusActivity7/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity7/"), fileNameDoc);
                    innovation.fileProceedStatusActivity7HttpPost.SaveAs(path);
                }

                if (innovation.fileFinishStatusActivity7HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileFinishStatusActivity7HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileFinishStatusActivity7HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileFinishStatusActivity7 = "/fileFinishStatusActivityAll/fileFinishStatusActivity7/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity7/"), fileNameDoc);
                    innovation.fileFinishStatusActivity7HttpPost.SaveAs(path);
                }

                if (innovation.planStatusActivity7 == null && innovation.proceedStatusActivity7 == null && innovation.fileFinishStatusActivity7 == null)
                {
                    innovation.statusActivity7 = "ยังไม่ดำเนินการ";
                }

                if (innovation.planStatusActivity7 != null && innovation.filePlanStatusActivity7 != null)
                {
                    innovation.statusActivity7 = "วางแผน";
                }
                if (innovation.planStatusActivity7 != null && innovation.filePlanStatusActivity7 != null
                    && innovation.proceedStatusActivity7 != null && innovation.fileProceedStatusActivity7 != null)
                {
                    innovation.statusActivity7 = "ดำเนินการ";
                }
                if (innovation.planStatusActivity7 != null && innovation.filePlanStatusActivity7 != null
                    && innovation.proceedStatusActivity7 != null && innovation.fileProceedStatusActivity7 != null
                    && innovation.finishStatusActivity7 != null && innovation.fileFinishStatusActivity7 != null)
                {
                    innovation.statusActivity7 = "เสร็จสิ้น";
                }

                db.Innovation.Attach(innovation);
                db.Entry(innovation).Property(x => x.statusActivity7).IsModified = true;
                db.Entry(innovation).Property(x => x.planStatusActivity7).IsModified = true;
                db.Entry(innovation).Property(x => x.proceedStatusActivity7).IsModified = true;
                db.Entry(innovation).Property(x => x.finishStatusActivity7).IsModified = true;
                db.Entry(innovation).Property(x => x.filePlanStatusActivity7).IsModified = true;
                db.Entry(innovation).Property(x => x.fileProceedStatusActivity7).IsModified = true;
                db.Entry(innovation).Property(x => x.fileFinishStatusActivity7).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateInnovationStatus", new { id });
            }

            return View(innovation);
        }

        public ActionResult updateInnovationActivityStatus8(int id)
        {
            Innovation innovation = db.Innovation.Find(id);

            if (innovation == null)
            {
                return RedirectToAction("trackInnovation");
            }

            return View(innovation);
        }

        [HttpPost]
        public ActionResult updateInnovationActivityStatus8(Innovation innovation, int id)
        {

            if (ModelState.IsValid)
            {
                if (innovation.filePlanStatusActivity8HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.filePlanStatusActivity8HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.filePlanStatusActivity8HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.filePlanStatusActivity8 = "/filePlanStatusActivityAll/filePlanStatusActivity8/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity8/"), fileNameDoc);
                    innovation.filePlanStatusActivity8HttpPost.SaveAs(path);
                }

                if (innovation.fileProceedStatusActivity8HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileProceedStatusActivity8HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileProceedStatusActivity8HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileProceedStatusActivity8 = "/fileProceedStatusActivityAll/fileProceedStatusActivity8/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity8/"), fileNameDoc);
                    innovation.fileProceedStatusActivity8HttpPost.SaveAs(path);
                }

                if (innovation.fileFinishStatusActivity8HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileFinishStatusActivity8HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileFinishStatusActivity8HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileFinishStatusActivity8 = "/fileFinishStatusActivityAll/fileFinishStatusActivity8/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity8/"), fileNameDoc);
                    innovation.fileFinishStatusActivity8HttpPost.SaveAs(path);
                }

                if (innovation.planStatusActivity8 == null && innovation.proceedStatusActivity8 == null && innovation.fileFinishStatusActivity8 == null)
                {
                    innovation.statusActivity8 = "ยังไม่ดำเนินการ";
                }

                if (innovation.planStatusActivity8 != null && innovation.filePlanStatusActivity8 != null)
                {
                    innovation.statusActivity8 = "วางแผน";
                }
                if (innovation.planStatusActivity8 != null && innovation.filePlanStatusActivity8 != null
                    && innovation.proceedStatusActivity8 != null && innovation.fileProceedStatusActivity8 != null)
                {
                    innovation.statusActivity8 = "ดำเนินการ";
                }
                if (innovation.planStatusActivity8 != null && innovation.filePlanStatusActivity8 != null
                    && innovation.proceedStatusActivity8 != null && innovation.fileProceedStatusActivity8 != null
                    && innovation.finishStatusActivity8 != null && innovation.fileFinishStatusActivity8 != null)
                {
                    innovation.statusActivity8 = "เสร็จสิ้น";
                }

                db.Innovation.Attach(innovation);
                db.Entry(innovation).Property(x => x.statusActivity8).IsModified = true;
                db.Entry(innovation).Property(x => x.planStatusActivity8).IsModified = true;
                db.Entry(innovation).Property(x => x.proceedStatusActivity8).IsModified = true;
                db.Entry(innovation).Property(x => x.finishStatusActivity8).IsModified = true;
                db.Entry(innovation).Property(x => x.filePlanStatusActivity8).IsModified = true;
                db.Entry(innovation).Property(x => x.fileProceedStatusActivity8).IsModified = true;
                db.Entry(innovation).Property(x => x.fileFinishStatusActivity8).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateInnovationStatus", new { id });
            }

            return View(innovation);
        }

        public ActionResult updateInnovationActivityStatus9(int id)
        {
            Innovation innovation = db.Innovation.Find(id);

            if (innovation == null)
            {
                return RedirectToAction("trackInnovation");
            }

            return View(innovation);
        }

        [HttpPost]
        public ActionResult updateInnovationActivityStatus9(Innovation innovation, int id)
        {

            if (ModelState.IsValid)
            {
                if (innovation.filePlanStatusActivity9HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.filePlanStatusActivity9HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.filePlanStatusActivity9HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.filePlanStatusActivity9 = "/filePlanStatusActivityAll/filePlanStatusActivity9/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity9/"), fileNameDoc);
                    innovation.filePlanStatusActivity9HttpPost.SaveAs(path);
                }

                if (innovation.fileProceedStatusActivity9HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileProceedStatusActivity9HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileProceedStatusActivity9HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileProceedStatusActivity9 = "/fileProceedStatusActivityAll/fileProceedStatusActivity9/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity9/"), fileNameDoc);
                    innovation.fileProceedStatusActivity9HttpPost.SaveAs(path);
                }

                if (innovation.fileFinishStatusActivity9HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileFinishStatusActivity9HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileFinishStatusActivity9HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileFinishStatusActivity9 = "/fileFinishStatusActivityAll/fileFinishStatusActivity9/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity9/"), fileNameDoc);
                    innovation.fileFinishStatusActivity9HttpPost.SaveAs(path);
                }

                if (innovation.planStatusActivity9 == null && innovation.proceedStatusActivity9 == null && innovation.fileFinishStatusActivity9 == null)
                {
                    innovation.statusActivity9 = "ยังไม่ดำเนินการ";
                }

                if (innovation.planStatusActivity9 != null && innovation.filePlanStatusActivity9 != null)
                {
                    innovation.statusActivity9 = "วางแผน";
                }
                if (innovation.planStatusActivity9 != null && innovation.filePlanStatusActivity9 != null
                    && innovation.proceedStatusActivity9 != null && innovation.fileProceedStatusActivity9 != null)
                {
                    innovation.statusActivity9 = "ดำเนินการ";
                }
                if (innovation.planStatusActivity9 != null && innovation.filePlanStatusActivity9 != null
                    && innovation.proceedStatusActivity9 != null && innovation.fileProceedStatusActivity9 != null
                    && innovation.finishStatusActivity9 != null && innovation.fileFinishStatusActivity9 != null)
                {
                    innovation.statusActivity9 = "เสร็จสิ้น";
                }

                db.Innovation.Attach(innovation);
                db.Entry(innovation).Property(x => x.statusActivity9).IsModified = true;
                db.Entry(innovation).Property(x => x.planStatusActivity9).IsModified = true;
                db.Entry(innovation).Property(x => x.proceedStatusActivity9).IsModified = true;
                db.Entry(innovation).Property(x => x.finishStatusActivity9).IsModified = true;
                db.Entry(innovation).Property(x => x.filePlanStatusActivity9).IsModified = true;
                db.Entry(innovation).Property(x => x.fileProceedStatusActivity9).IsModified = true;
                db.Entry(innovation).Property(x => x.fileFinishStatusActivity9).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateInnovationStatus", new { id });
            }

            return View(innovation);
        }

        public ActionResult updateInnovationActivityStatus10(int id)
        {
            Innovation innovation = db.Innovation.Find(id);

            if (innovation == null)
            {
                return RedirectToAction("trackInnovation");
            }

            return View(innovation);
        }

        [HttpPost]
        public ActionResult updateInnovationActivityStatus10(Innovation innovation, int id)
        {

            if (ModelState.IsValid)
            {
                if (innovation.filePlanStatusActivity10HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.filePlanStatusActivity10HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.filePlanStatusActivity10HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.filePlanStatusActivity10 = "/filePlanStatusActivityAll/filePlanStatusActivity10/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/filePlanStatusActivityAll/filePlanStatusActivity10/"), fileNameDoc);
                    innovation.filePlanStatusActivity10HttpPost.SaveAs(path);
                }

                if (innovation.fileProceedStatusActivity10HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileProceedStatusActivity10HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileProceedStatusActivity10HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileProceedStatusActivity10 = "/fileProceedStatusActivityAll/fileProceedStatusActivity10/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileProceedStatusActivityAll/fileProceedStatusActivity10/"), fileNameDoc);
                    innovation.fileProceedStatusActivity10HttpPost.SaveAs(path);
                }

                if (innovation.fileFinishStatusActivity10HttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.fileFinishStatusActivity10HttpPost.FileName);
                    string extension = Path.GetExtension(innovation.fileFinishStatusActivity10HttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.fileFinishStatusActivity10 = "/fileFinishStatusActivityAll/fileFinishStatusActivity10/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/fileFinishStatusActivityAll/fileFinishStatusActivity10/"), fileNameDoc);
                    innovation.fileFinishStatusActivity10HttpPost.SaveAs(path);
                }

                if (innovation.planStatusActivity10 == null && innovation.proceedStatusActivity10 == null && innovation.fileFinishStatusActivity10 == null)
                {
                    innovation.statusActivity10 = "ยังไม่ดำเนินการ";
                }

                if (innovation.planStatusActivity10 != null && innovation.filePlanStatusActivity10 != null)
                {
                    innovation.statusActivity10 = "วางแผน";
                }
                if (innovation.planStatusActivity10 != null && innovation.filePlanStatusActivity10 != null
                    && innovation.proceedStatusActivity10 != null && innovation.fileProceedStatusActivity10 != null)
                {
                    innovation.statusActivity10 = "ดำเนินการ";
                }
                if (innovation.planStatusActivity10 != null && innovation.filePlanStatusActivity10 != null
                    && innovation.proceedStatusActivity10 != null && innovation.fileProceedStatusActivity10 != null
                    && innovation.finishStatusActivity10 != null && innovation.fileFinishStatusActivity10 != null)
                {
                    innovation.statusActivity10 = "เสร็จสิ้น";
                }

                db.Innovation.Attach(innovation);
                db.Entry(innovation).Property(x => x.statusActivity10).IsModified = true;
                db.Entry(innovation).Property(x => x.planStatusActivity10).IsModified = true;
                db.Entry(innovation).Property(x => x.proceedStatusActivity10).IsModified = true;
                db.Entry(innovation).Property(x => x.finishStatusActivity10).IsModified = true;
                db.Entry(innovation).Property(x => x.filePlanStatusActivity10).IsModified = true;
                db.Entry(innovation).Property(x => x.fileProceedStatusActivity10).IsModified = true;
                db.Entry(innovation).Property(x => x.fileFinishStatusActivity10).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("updateInnovationStatus", new { id });
            }

            return View(innovation);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------//

        public ActionResult updateActivityStatus()
        {
            return View();
        }
    }
}