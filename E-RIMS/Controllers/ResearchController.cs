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
            if (Session["Role"] != null)
            {
                var research = db.Research;

                //--Pagination 10 each
                var convertIdOwner = Convert.ToInt32(Session["Id"]);

                var convertUsernameOwner = Session["Username"].ToString();

                var researchResult = research.Where(x => x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).ToList().ToPagedList(page ?? 1, 10);

                return View(researchResult);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(string budgetYear, string name, string creator, string workGroup, int? page)
        {
            var research = db.Research;

            //--Pagination 10 each
            var convertIdOwner = Convert.ToInt32(Session["Id"]);

            var convertUsernameOwner = Session["Username"].ToString();


            var researchResult = research.Where(x => x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).ToList().ToPagedList(page ?? 1, 10);

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
                researchResult = db.Research.Where(x => (x.name.StartsWith(name) || x.name.Equals(name) || x.name.Contains(name))
                && x.idOwner == convertIdOwner && x.usernameOwner == convertUsernameOwner).ToList().ToPagedList(page ?? 1, 10);

                if (researchResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิจัย";
                }

                return View(researchResult);
            }
            if (creator != "")
            {
                researchResult = db.Research.Where(x => (x.creator.StartsWith(creator) || x.creator.Equals(creator) || x.creator.Contains(creator))
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

        public ActionResult AllResearch(int? page)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var research = db.Research;

                    //--Pagination 10 each
                    var researchResult = research.ToList().ToPagedList(page ?? 1, 10);

                    return View(researchResult);
                }
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AllResearch(string budgetYear, string name, string creator, string workGroup, int? page)
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
                researchResult = db.Research.Where(x => x.name.StartsWith(name) || x.name.Equals(name) || x.name.Contains(name)).ToList().ToPagedList(page ?? 1, 10);

                if (researchResult.TotalItemCount == 0)
                {
                    ViewBag.Nodata = "ไม่พบงานวิจัย";
                }

                return View(researchResult);
            }
            if (creator != "")
            {
                researchResult = db.Research.Where(x => x.creator.StartsWith(creator) || x.creator.Equals(creator) || x.creator.Contains(creator)).ToList().ToPagedList(page ?? 1, 10);

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

        public ActionResult CreateResearch()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin") || Session["Role"].Equals("User"))
                {
                    var sessionCreatorToString = Session["Owner"].ToString();
                    ViewBag.ResearcherView = sessionCreatorToString;

                    return View();

                    //var modelResearcher = db.Researcher.OrderBy(x => x.name).ToList();
                    //ViewBag.ResearcherView = (from item in modelResearcher
                    //                          select new SelectListItem
                    //                          {
                    //                              Text = item.name + " " + item.surname,
                    //                              Value = item.name.ToString() + " " + item.surname.ToString()
                    //                          });                   
                }
                return RedirectToAction("Index", "Research");
            }
            else
                return RedirectToAction("Index", "Research");
        }

        [HttpPost]
        public ActionResult CreateResearch(Research research)
        {
            var sessionCreatorToString = Session["Owner"].ToString();

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

                if (research.finalFilesHttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.finalFilesHttpPost.FileName);
                    string extension = Path.GetExtension(research.finalFilesHttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.finalFiles = "/docUploadFinalResearch/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadFinalResearch/"), fileNameDoc);
                    research.finalFilesHttpPost.SaveAs(path);
                }

                //ความเป็นเจ้าของงาน
                if (Session["Id"] != null || Session["Username"] != null)
                {
                    research.idOwner = Convert.ToInt32(Session["Id"]);
                    research.usernameOwner = Session["Username"].ToString();
                }

                research.creator = sessionCreatorToString;
                research.views = 0;
                research.workOverview = 00.00m;
                research.date = DateTime.Today;

                if (research.activity1 != null)
                {
                    research.statusActivity1 = "ยังไม่ดำเนินการ";
                }
                if (research.activity2 != null)
                {
                    research.statusActivity2 = "ยังไม่ดำเนินการ";
                }
                if (research.activity3 != null)
                {
                    research.statusActivity3 = "ยังไม่ดำเนินการ";
                }
                if (research.activity4 != null)
                {
                    research.statusActivity4 = "ยังไม่ดำเนินการ";
                }
                if (research.activity5 != null)
                {
                    research.statusActivity5 = "ยังไม่ดำเนินการ";
                }
                if (research.activity6 != null)
                {
                    research.statusActivity6 = "ยังไม่ดำเนินการ";
                }
                if (research.activity7 != null)
                {
                    research.statusActivity7 = "ยังไม่ดำเนินการ";
                }
                if (research.activity8 != null)
                {
                    research.statusActivity8 = "ยังไม่ดำเนินการ";
                }
                if (research.activity9 != null)
                {
                    research.statusActivity9 = "ยังไม่ดำเนินการ";
                }
                if (research.activity10 != null)
                {
                    research.statusActivity10 = "ยังไม่ดำเนินการ";
                }

                db.Research.Add(research);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("CreateSuccessMessage");
            }

            ViewBag.ResearcherView = sessionCreatorToString;

            //var modelResearcher = db.Researcher.OrderBy(x => x.name).ToList();
            //ViewBag.ResearcherView = (from item in modelResearcher
            //                          select new SelectListItem
            //                          {
            //                              Text = item.name + " " + item.surname,
            //                              Value = item.name.ToString() + " " + item.surname.ToString()
            //                          });

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
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
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
                                                  Value = item.name.ToString() + " " + item.surname.ToString()
                                              });

                    var typeView = new List<string>()
            { "วิจัย", "วิชาการ" };
                    ViewBag.typeView = typeView;

                    var budgetYearView = new List<string>()
            { "2559", "2560", "2561", "2562", "2563", "2564", "2565", "2566", "2567", "2568", "2569", "2570", "2571", "2572", "2573", "2574", "2575", "2576", "2577" , "2578", "2579", "2580"};
                    ViewBag.budgetYearView = budgetYearView;

                    var workGroupView = new List<string>()
            {"งานเภสัชกรรม","บริหารทั่วไป","พัฒนานวัตกรรมและวิจัย และงานควบคุมโรคเขตเมือง","พัฒนาระบบบริหารองค์กร และงานสถาปัตยกรรมข้อมูล","ยุทธศาสตร์ แผนงาน และเครือข่าย","ระบาดวิทยาและตอบโต้ภาวะฉุกเฉินทางด้านสาธารณสุข","โรคติดต่อ","โรคไม่ติดต่อ"
            ,"ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.1 ศรีราชา","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.2 สระแก้ว","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.3 ระยอง","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.4 ตราด","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.5 จันทบุรี","สื่อสารความเสี่ยงโรคและภัยสุขภาพ","หน่วยกามโรคและโรคเอดส์ที่ 6.1 บางละมุง","ห้องปฏิบัติการทางการแพทย์ด้านควบคุมโรค"};
                    ViewBag.workGroupView = workGroupView;

                    var timeOfStudyView = new List<string>()
            {"1 เดือน","2 เดือน","3 เดือน","4 เดือน","5 เดือน","6 เดือน","7 เดือน","8 เดือน","9 เดือน","10 เดือน","11 เดือน","12 เดือน" };
                    ViewBag.timeOfStudyView = timeOfStudyView;

                    return View(research);
                }
                return RedirectToAction("Index", "Research");
            }
            else
                return RedirectToAction("Index", "Research");
        }

        [HttpPost]
        public ActionResult EditResearch(Research research, string workOverview)
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

                if (research.finalFilesHttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(research.finalFilesHttpPost.FileName);
                    string extension = Path.GetExtension(research.finalFilesHttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    research.finalFiles = "/docUploadFinalResearch/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadFinalResearch/"), fileNameDoc);
                    research.finalFilesHttpPost.SaveAs(path);
                }

                research.views = 0;
                research.workOverview = Convert.ToDecimal(workOverview);
                research.date = DateTime.Today;

                db.Research.Attach(research);
                db.Entry(research).Property(x => x.date).IsModified = true;
                db.Entry(research).Property(x => x.type).IsModified = true;
                db.Entry(research).Property(x => x.name).IsModified = true;
                db.Entry(research).Property(x => x.workGroup).IsModified = true;
                //db.Entry(research).Property(x => x.creator).IsModified = true;
                db.Entry(research).Property(x => x.preface).IsModified = true;
                db.Entry(research).Property(x => x.objective).IsModified = true;
                db.Entry(research).Property(x => x.benefit).IsModified = true;
                db.Entry(research).Property(x => x.format).IsModified = true;
                db.Entry(research).Property(x => x.school).IsModified = true;
                db.Entry(research).Property(x => x.timeOfStudy).IsModified = true;
                db.Entry(research).Property(x => x.population).IsModified = true;
                db.Entry(research).Property(x => x.sampleMethod).IsModified = true;
                db.Entry(research).Property(x => x.toolsOfResearch).IsModified = true;
                db.Entry(research).Property(x => x.dataAnalysis).IsModified = true;
                db.Entry(research).Property(x => x.dataMethodCollect).IsModified = true;
                db.Entry(research).Property(x => x.files).IsModified = true;
                db.Entry(research).Property(x => x.activity1).IsModified = true;
                db.Entry(research).Property(x => x.activity2).IsModified = true;
                db.Entry(research).Property(x => x.activity3).IsModified = true;
                db.Entry(research).Property(x => x.activity4).IsModified = true;
                db.Entry(research).Property(x => x.activity5).IsModified = true;
                db.Entry(research).Property(x => x.activity6).IsModified = true;
                db.Entry(research).Property(x => x.activity7).IsModified = true;
                db.Entry(research).Property(x => x.activity8).IsModified = true;
                db.Entry(research).Property(x => x.activity9).IsModified = true;
                db.Entry(research).Property(x => x.activity10).IsModified = true;
                db.Entry(research).Property(x => x.isJan1).IsModified = true;
                db.Entry(research).Property(x => x.isJan2).IsModified = true;
                db.Entry(research).Property(x => x.isJan3).IsModified = true;
                db.Entry(research).Property(x => x.isJan4).IsModified = true;
                db.Entry(research).Property(x => x.isJan5).IsModified = true;
                db.Entry(research).Property(x => x.isJan6).IsModified = true;
                db.Entry(research).Property(x => x.isJan7).IsModified = true;
                db.Entry(research).Property(x => x.isJan8).IsModified = true;
                db.Entry(research).Property(x => x.isJan9).IsModified = true;
                db.Entry(research).Property(x => x.isJan10).IsModified = true;
                db.Entry(research).Property(x => x.isFeb1).IsModified = true;
                db.Entry(research).Property(x => x.isFeb2).IsModified = true;
                db.Entry(research).Property(x => x.isFeb3).IsModified = true;
                db.Entry(research).Property(x => x.isFeb4).IsModified = true;
                db.Entry(research).Property(x => x.isFeb5).IsModified = true;
                db.Entry(research).Property(x => x.isFeb6).IsModified = true;
                db.Entry(research).Property(x => x.isFeb7).IsModified = true;
                db.Entry(research).Property(x => x.isFeb8).IsModified = true;
                db.Entry(research).Property(x => x.isFeb9).IsModified = true;
                db.Entry(research).Property(x => x.isFeb10).IsModified = true;
                db.Entry(research).Property(x => x.isMar1).IsModified = true;
                db.Entry(research).Property(x => x.isMar2).IsModified = true;
                db.Entry(research).Property(x => x.isMar3).IsModified = true;
                db.Entry(research).Property(x => x.isMar4).IsModified = true;
                db.Entry(research).Property(x => x.isMar5).IsModified = true;
                db.Entry(research).Property(x => x.isMar6).IsModified = true;
                db.Entry(research).Property(x => x.isMar7).IsModified = true;
                db.Entry(research).Property(x => x.isMar8).IsModified = true;
                db.Entry(research).Property(x => x.isMar9).IsModified = true;
                db.Entry(research).Property(x => x.isMar10).IsModified = true;
                db.Entry(research).Property(x => x.isApr1).IsModified = true;
                db.Entry(research).Property(x => x.isApr2).IsModified = true;
                db.Entry(research).Property(x => x.isApr3).IsModified = true;
                db.Entry(research).Property(x => x.isApr4).IsModified = true;
                db.Entry(research).Property(x => x.isApr5).IsModified = true;
                db.Entry(research).Property(x => x.isApr6).IsModified = true;
                db.Entry(research).Property(x => x.isApr7).IsModified = true;
                db.Entry(research).Property(x => x.isApr8).IsModified = true;
                db.Entry(research).Property(x => x.isApr9).IsModified = true;
                db.Entry(research).Property(x => x.isApr10).IsModified = true;
                db.Entry(research).Property(x => x.isMay1).IsModified = true;
                db.Entry(research).Property(x => x.isMay2).IsModified = true;
                db.Entry(research).Property(x => x.isMay3).IsModified = true;
                db.Entry(research).Property(x => x.isMay4).IsModified = true;
                db.Entry(research).Property(x => x.isMay5).IsModified = true;
                db.Entry(research).Property(x => x.isMay6).IsModified = true;
                db.Entry(research).Property(x => x.isMay7).IsModified = true;
                db.Entry(research).Property(x => x.isMay8).IsModified = true;
                db.Entry(research).Property(x => x.isMay9).IsModified = true;
                db.Entry(research).Property(x => x.isMay10).IsModified = true;
                db.Entry(research).Property(x => x.isJun1).IsModified = true;
                db.Entry(research).Property(x => x.isJun2).IsModified = true;
                db.Entry(research).Property(x => x.isJun3).IsModified = true;
                db.Entry(research).Property(x => x.isJun4).IsModified = true;
                db.Entry(research).Property(x => x.isJun5).IsModified = true;
                db.Entry(research).Property(x => x.isJun6).IsModified = true;
                db.Entry(research).Property(x => x.isJun7).IsModified = true;
                db.Entry(research).Property(x => x.isJun8).IsModified = true;
                db.Entry(research).Property(x => x.isJun9).IsModified = true;
                db.Entry(research).Property(x => x.isJun10).IsModified = true;
                db.Entry(research).Property(x => x.isJul1).IsModified = true;
                db.Entry(research).Property(x => x.isJul2).IsModified = true;
                db.Entry(research).Property(x => x.isJul3).IsModified = true;
                db.Entry(research).Property(x => x.isJul4).IsModified = true;
                db.Entry(research).Property(x => x.isJul5).IsModified = true;
                db.Entry(research).Property(x => x.isJul6).IsModified = true;
                db.Entry(research).Property(x => x.isJul7).IsModified = true;
                db.Entry(research).Property(x => x.isJul8).IsModified = true;
                db.Entry(research).Property(x => x.isJul9).IsModified = true;
                db.Entry(research).Property(x => x.isJul10).IsModified = true;
                db.Entry(research).Property(x => x.isAug1).IsModified = true;
                db.Entry(research).Property(x => x.isAug2).IsModified = true;
                db.Entry(research).Property(x => x.isAug3).IsModified = true;
                db.Entry(research).Property(x => x.isAug4).IsModified = true;
                db.Entry(research).Property(x => x.isAug5).IsModified = true;
                db.Entry(research).Property(x => x.isAug6).IsModified = true;
                db.Entry(research).Property(x => x.isAug7).IsModified = true;
                db.Entry(research).Property(x => x.isAug8).IsModified = true;
                db.Entry(research).Property(x => x.isAug9).IsModified = true;
                db.Entry(research).Property(x => x.isAug10).IsModified = true;
                db.Entry(research).Property(x => x.isSep1).IsModified = true;
                db.Entry(research).Property(x => x.isSep2).IsModified = true;
                db.Entry(research).Property(x => x.isSep3).IsModified = true;
                db.Entry(research).Property(x => x.isSep4).IsModified = true;
                db.Entry(research).Property(x => x.isSep5).IsModified = true;
                db.Entry(research).Property(x => x.isSep6).IsModified = true;
                db.Entry(research).Property(x => x.isSep7).IsModified = true;
                db.Entry(research).Property(x => x.isSep8).IsModified = true;
                db.Entry(research).Property(x => x.isSep9).IsModified = true;
                db.Entry(research).Property(x => x.isSep10).IsModified = true;
                db.Entry(research).Property(x => x.isOct1).IsModified = true;
                db.Entry(research).Property(x => x.isOct2).IsModified = true;
                db.Entry(research).Property(x => x.isOct3).IsModified = true;
                db.Entry(research).Property(x => x.isOct4).IsModified = true;
                db.Entry(research).Property(x => x.isOct5).IsModified = true;
                db.Entry(research).Property(x => x.isOct6).IsModified = true;
                db.Entry(research).Property(x => x.isOct7).IsModified = true;
                db.Entry(research).Property(x => x.isOct8).IsModified = true;
                db.Entry(research).Property(x => x.isOct9).IsModified = true;
                db.Entry(research).Property(x => x.isOct10).IsModified = true;
                db.Entry(research).Property(x => x.isNov1).IsModified = true;
                db.Entry(research).Property(x => x.isNov2).IsModified = true;
                db.Entry(research).Property(x => x.isNov3).IsModified = true;
                db.Entry(research).Property(x => x.isNov4).IsModified = true;
                db.Entry(research).Property(x => x.isNov5).IsModified = true;
                db.Entry(research).Property(x => x.isNov6).IsModified = true;
                db.Entry(research).Property(x => x.isNov7).IsModified = true;
                db.Entry(research).Property(x => x.isNov8).IsModified = true;
                db.Entry(research).Property(x => x.isNov9).IsModified = true;
                db.Entry(research).Property(x => x.isNov10).IsModified = true;
                db.Entry(research).Property(x => x.isDec1).IsModified = true;
                db.Entry(research).Property(x => x.isDec2).IsModified = true;
                db.Entry(research).Property(x => x.isDec3).IsModified = true;
                db.Entry(research).Property(x => x.isDec4).IsModified = true;
                db.Entry(research).Property(x => x.isDec5).IsModified = true;
                db.Entry(research).Property(x => x.isDec6).IsModified = true;
                db.Entry(research).Property(x => x.isDec7).IsModified = true;
                db.Entry(research).Property(x => x.isDec8).IsModified = true;
                db.Entry(research).Property(x => x.isDec9).IsModified = true;
                db.Entry(research).Property(x => x.isDec10).IsModified = true;
                db.Entry(research).Property(x => x.budgetYear).IsModified = true;
                db.Entry(research).Property(x => x.views).IsModified = true;
                db.Entry(research).Property(x => x.finalFiles).IsModified = true;
                db.Entry(research).Property(x => x.agreePublish).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("EditSuccessMessage");
            }

            var modelResearcher = db.Researcher.OrderBy(x => x.name).ToList();
            ViewBag.ResearcherView = (from item in modelResearcher
                                      select new SelectListItem
                                      {
                                          Text = item.name + " " + item.surname,
                                          Value = item.name.ToString() + " " + item.surname.ToString()
                                      });

            var typeView = new List<string>() { "วิจัย", "วิชาการ" };
            ViewBag.typeView = typeView;

            var budgetYearView = new List<string>()
           { "2559", "2560", "2561", "2562", "2563", "2564", "2565", "2566", "2567", "2568", "2569", "2570", "2571", "2572", "2573", "2574", "2575", "2576", "2577" , "2578", "2579", "2580"};
            ViewBag.budgetYearView = budgetYearView;

            var workGroupView = new List<string>()
            {"งานเภสัชกรรม","บริหารทั่วไป","พัฒนานวัตกรรมและวิจัย และงานควบคุมโรคเขตเมือง","พัฒนาระบบบริหารองค์กร และงานสถาปัตยกรรมข้อมูล","ยุทธศาสตร์ แผนงาน และเครือข่าย","ระบาดวิทยาและตอบโต้ภาวะฉุกเฉินทางด้านสาธารณสุข","โรคติดต่อ","โรคไม่ติดต่อ"
            ,"ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.1 ศรีราชา","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.2 สระแก้ว","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.3 ระยอง","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.4 ตราด","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.5 จันทบุรี","สื่อสารความเสี่ยงโรคและภัยสุขภาพ","หน่วยกามโรคและโรคเอดส์ที่ 6.1 บางละมุง","ห้องปฏิบัติการทางการแพทย์ด้านควบคุมโรค"};
            ViewBag.workGroupView = workGroupView;

            var timeOfStudyView = new List<string>()
            {"1 เดือน","2 เดือน","3 เดือน","4 เดือน","5 เดือน","6 เดือน","7 เดือน","8 เดือน","9 เดือน","10 เดือน","11 เดือน","12 เดือน" };
            ViewBag.timeOfStudyView = timeOfStudyView;

            return View(research);
        }

        public ActionResult DeleteResearch(int id)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    Research research = db.Research.Find(id);
                    if (research == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(research);
                }
                return RedirectToAction("AllResearch", "Research");
            }
            else
                return RedirectToAction("AllResearch", "Research");
        }

        [HttpPost, ActionName("DeleteResearch")]
        public ActionResult DeleteResearchConfirm(int id)
        {
            Research research = db.Research.Find(id);
            db.Research.Remove(research);
            db.SaveChanges();
            return RedirectToAction("AllResearch");
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