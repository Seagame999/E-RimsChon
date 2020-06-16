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
    public class TreasuryWorkController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: TreasuryWork
        public ActionResult Index()
        {
            return View();
        }

        // GET: TreasuryWork
        public ActionResult ResearchTreasuryWork(int? page)
        {
            var research = db.Research;

            //--Pagination 10 each Select by Condition.
            var researchResult = research.Where(x => x.type.Equals("วิจัย") && x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null)).ToList().ToPagedList(page ?? 1, 10);

            if (researchResult.TotalItemCount == 0)
            {
                ViewBag.Nodata = "ไม่พบงานวิจัยที่เสร็จสิ้น";
            }

            return View(researchResult);

        }

        [HttpPost]
        public ActionResult ResearchTreasuryWork(string budgetYear, string name, string creator, string workGroup, int? page)
        {
            var research = db.Research;

            //--Pagination 10 each Select by Condition.
            var researchResult = research.Where(x => x.type.Equals("วิจัย") && x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null)).ToList().ToPagedList(page ?? 1, 10);

            //--Search Engine
            if (budgetYear != "-- ปีงบประมาณ --")
            {
                researchResult = db.Research.Where(x => x.type.Equals("วิจัย") && x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null) && (x.budgetYear.StartsWith(budgetYear) || x.budgetYear.Equals(budgetYear))
            ).ToList().ToPagedList(page ?? 1, 10);

                if (researchResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิจัย";
                }

                return View(researchResult);
            }
            if (name != "")
            {
                researchResult = db.Research.Where(x => x.type.Equals("วิจัย") && x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null) && (x.name.StartsWith(name) || x.name.Equals(name))
            ).ToList().ToPagedList(page ?? 1, 10);

                if (researchResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิจัย";
                }

                return View(researchResult);
            }
            if (creator != "")
            {
                researchResult = db.Research.Where(x => x.type.Equals("วิจัย") && x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null) && (x.creator.StartsWith(creator) || x.creator.Equals(creator))).ToList().ToPagedList(page ?? 1, 10);

                if (researchResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิจัย";
                }

                return View(researchResult);
            }
            if (workGroup != "-- กลุ่มงาน --")
            {
                researchResult = db.Research.Where(x => x.type.Equals("วิจัย") && x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null) && (x.workGroup.StartsWith(workGroup) || x.workGroup.Equals(workGroup))).ToList().ToPagedList(page ?? 1, 10);

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

        // GET: TreasuryWork
        public ActionResult InnovationTreasuryWork(int? page)
        {
            var innovation = db.Innovation;

            //--Pagination 10 each Select by Condition.
            var innovationResult = innovation.Where(x => x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null)).ToList().ToPagedList(page ?? 1, 10);

            if (innovationResult.TotalItemCount == 0)
            {
                ViewBag.Nodata = "ไม่พบงานนวัตกรรมที่เสร็จสิ้น";
            }

            return View(innovationResult);
        }

        [HttpPost]
        public ActionResult InnovationTreasuryWork(string budgetYear, string name, string creator, string workGroup, int? page)
        {
            var innovation = db.Innovation;

            //--Pagination 10 each Select by Condition.
            var innovationResult = innovation.Where(x => x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null)).ToList().ToPagedList(page ?? 1, 10);


            //--Search Engine
            if (budgetYear != "-- ปีงบประมาณ --")
            {
                innovationResult = db.Innovation.Where(x => x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null) && (x.budgetYear.StartsWith(budgetYear) || x.budgetYear.Equals(budgetYear))
            ).ToList().ToPagedList(page ?? 1, 10);

                if (innovationResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานนวัตกรรม";
                }

                return View(innovationResult);
            }
            if (name != "")
            {
                innovationResult = db.Innovation.Where(x => x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null) && (x.name.StartsWith(name) || x.name.Equals(name))
            ).ToList().ToPagedList(page ?? 1, 10);

                if (innovationResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานนวัตกรรม";
                }

                return View(innovationResult);
            }
            if (creator != "")
            {
                innovationResult = db.Innovation.Where(x => x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null) && (x.creator.StartsWith(creator) || x.creator.Equals(creator))).ToList().ToPagedList(page ?? 1, 10);

                if (innovationResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานนวัตกรรม";
                }

                return View(innovationResult);
            }
            if (workGroup != "-- กลุ่มงาน --")
            {
                innovationResult = db.Innovation.Where(x => x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null) && (x.workGroup.StartsWith(workGroup) || x.workGroup.Equals(workGroup))).ToList().ToPagedList(page ?? 1, 10);

                if (innovationResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานนวัตกรรม";
                }

                return View(innovationResult);
            }
            else
            {
                return View(innovationResult);
            }
        }

        // GET: TreasuryWork
        public ActionResult AcademicTreasuryWork(int? page)
        {
            var academic = db.Research;

            //--Pagination 10 each Select by Condition.
            var academicResult = academic.Where(x => x.type.Equals("วิชาการ") && x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null)).ToList().ToPagedList(page ?? 1, 10);

            if (academicResult.TotalItemCount == 0)
            {
                ViewBag.Nodata = "ไม่พบงานวิชาการที่เสร็จสิ้น";
            }

            return View(academicResult);
        }

        [HttpPost]
        public ActionResult AcademicTreasuryWork(string budgetYear, string name, string creator, string workGroup, int? page)
        {
            var academic = db.Research;

            //--Pagination 10 each Select by Condition.
            var academicResult = academic.Where(x => x.type.Equals("วิชาการ") && x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null)).ToList().ToPagedList(page ?? 1, 10);

            //--Search Engine
            if (budgetYear != "-- ปีงบประมาณ --")
            {
                academicResult = db.Research.Where(x => x.type.Equals("วิชาการ") && x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null) && (x.budgetYear.StartsWith(budgetYear) || x.budgetYear.Equals(budgetYear))
            ).ToList().ToPagedList(page ?? 1, 10);

                if (academicResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิชาการ";
                }

                return View(academicResult);
            }
            if (name != "")
            {
                academicResult = db.Research.Where(x => x.type.Equals("วิชาการ") && x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null) && (x.name.StartsWith(name) || x.name.Equals(name))
            ).ToList().ToPagedList(page ?? 1, 10);

                if (academicResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิชาการ";
                }

                return View(academicResult);
            }
            if (creator != "")
            {
                academicResult = db.Research.Where(x => x.type.Equals("วิชาการ") && x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null) && (x.creator.StartsWith(creator) || x.creator.Equals(creator))).ToList().ToPagedList(page ?? 1, 10);

                if (academicResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิชาการ";
                }

                return View(academicResult);
            }
            if (workGroup != "-- กลุ่มงาน --")
            {
                academicResult = db.Research.Where(x => x.type.Equals("วิชาการ") && x.workOverview == 100.00M && (x.fileFinishStatusActivity1 != null
            || x.fileFinishStatusActivity2 != null
            || x.fileFinishStatusActivity3 != null
            || x.fileFinishStatusActivity4 != null
            || x.fileFinishStatusActivity5 != null
            || x.fileFinishStatusActivity6 != null
            || x.fileFinishStatusActivity7 != null
            || x.fileFinishStatusActivity8 != null
            || x.fileFinishStatusActivity9 != null
            || x.fileFinishStatusActivity10 != null) && (x.finalFiles != null) && (x.workGroup.StartsWith(workGroup) || x.workGroup.Equals(workGroup))).ToList().ToPagedList(page ?? 1, 10);

                if (academicResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิชาการ";
                }

                return View(academicResult);
            }
            else
            {
                return View(academicResult);
            }
        }
    }
}