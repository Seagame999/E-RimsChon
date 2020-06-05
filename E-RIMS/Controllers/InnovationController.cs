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
    public class InnovationController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: Innovation
        public ActionResult Index(int? page)
        {
            var innovation = db.Innovation;

            //--Pagination 10 each
            var innovationResult = innovation.ToList().ToPagedList(page ?? 1, 10);

            return View(innovationResult);
        }

        [HttpPost]
        public ActionResult Index(string budgetYear, string name, string creator, string workGroup, int? page)
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

        public ActionResult AllInnovation(int? page)
        {
            var innovation = db.Innovation;

            //--Pagination 10 each
            var innovationResult = innovation.ToList().ToPagedList(page ?? 1, 10);

            return View(innovationResult);
        }

        public ActionResult CreateInnovation()
        {
            var modelInnovator = db.Innovator.OrderBy(x => x.name).ToList();
            ViewBag.InnovatorView = (from item in modelInnovator
                                     select new SelectListItem
                                     {
                                         Text = item.name + " " + item.surname,
                                         Value = item.name.ToString() + " " + item.surname.ToString()
                                     });
            return View();
        }

        [HttpPost]
        public ActionResult CreateInnovation(Innovation innovation)
        {
            if (ModelState.IsValid)
            {
                if (innovation.files2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.files2.FileName);
                    string extension = Path.GetExtension(innovation.files2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.files = "/docUploadInnovation/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadInnovation/"), fileNameDoc);
                    innovation.files2.SaveAs(path);
                }

                if (innovation.finalFilesHttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.finalFilesHttpPost.FileName);
                    string extension = Path.GetExtension(innovation.finalFilesHttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.finalFiles = "/docUploadFinalInnovation/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadFinalInnovation/"), fileNameDoc);
                    innovation.finalFilesHttpPost.SaveAs(path);
                }

                //if (Session["Id"] != null || Session["Username"] != null)
                //{
                //    innovation.idOwner = Convert.ToInt32(Session["Id"]);
                //    innovation.usernameOwner = Session["Username"].ToString();
                //}
                innovation.views = 0;
                innovation.workOverview = 00.00m;
                innovation.date = DateTime.Today;

                if (innovation.activity1 != null)
                {
                    innovation.statusActivity1 = "ยังไม่ดำเนินการ";
                }
                if (innovation.activity2 != null)
                {
                    innovation.statusActivity2 = "ยังไม่ดำเนินการ";
                }
                if (innovation.activity3 != null)
                {
                    innovation.statusActivity3 = "ยังไม่ดำเนินการ";
                }
                if (innovation.activity4 != null)
                {
                    innovation.statusActivity4 = "ยังไม่ดำเนินการ";
                }
                if (innovation.activity5 != null)
                {
                    innovation.statusActivity5 = "ยังไม่ดำเนินการ";
                }
                if (innovation.activity6 != null)
                {
                    innovation.statusActivity6 = "ยังไม่ดำเนินการ";
                }
                if (innovation.activity7 != null)
                {
                    innovation.statusActivity7 = "ยังไม่ดำเนินการ";
                }
                if (innovation.activity8 != null)
                {
                    innovation.statusActivity8 = "ยังไม่ดำเนินการ";
                }
                if (innovation.activity9 != null)
                {
                    innovation.statusActivity9 = "ยังไม่ดำเนินการ";
                }
                if (innovation.activity10 != null)
                {
                    innovation.statusActivity10 = "ยังไม่ดำเนินการ";
                }

                db.Innovation.Add(innovation);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("CreateSuccessMessage");
            }

            var modelInnovator = db.Innovator.OrderBy(x => x.name).ToList();
            ViewBag.InnovatorView = (from item in modelInnovator
                                     select new SelectListItem
                                     {
                                         Text = item.name + " " + item.surname,
                                         Value = item.name.ToString() + " " + item.surname.ToString()
                                     });

            return View(innovation);
        }

        public ActionResult DetailInnovation(int id)
        {
            Innovation innovation = db.Innovation.Find(id);
            if (innovation == null)
            {
                return RedirectToAction("Index");
            }
            //--Page Visitor
            innovation.views = innovation.views + 1;
            db.Entry(innovation).State = EntityState.Modified;
            db.SaveChanges();
            return View(innovation);

        }

        public ActionResult EditInnovation(int id)
        {
            Innovation innovation = db.Innovation.Find(id);
            if (innovation == null)
            {
                return RedirectToAction("Index");
            }

            decimal workOverviewValue = Convert.ToDecimal(innovation.workOverview);
            ViewBag.workOverview = workOverviewValue;

            var modelInnovator = db.Innovator.OrderBy(x => x.name).ToList();
            ViewBag.InnovatorView = (from item in modelInnovator
                                     select new SelectListItem
                                     {
                                         Text = item.name + " " + item.surname,
                                         Value = item.name.ToString()+ " " + item.surname.ToString()
                                     });

            var budgetYearView = new List<string>()
            { "2559", "2560", "2561", "2562", "2563", "2564", "2565" };
            ViewBag.budgetYearView = budgetYearView;

            var typeView = new List<string>()
            {"Product","Process","Service","Policy/Strategic"};
            ViewBag.typeView = typeView;

            var workGroupView = new List<string>()
            {"งานเภสัชกรรม","บริหารทั่วไป","พัฒนานวัตกรรมและวิจัย และงานควบคุมโรคเขตเมือง","พัฒนาระบบบริหารองค์กร และงานสถาปัตยกรรมข้อมูล","ยุทธศาสตร์ แผนงาน และเครือข่าย","ระบาดวิทยาและตอบโต้ภาวะฉุกเฉินทางด้านสาธารณสุข","โรคติดต่อ","โรคไม่ติดต่อ"
            ,"ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.1 ศรีราชา","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.2 สระแก้ว","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.3 ระยอง","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.4 ตราด","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.5 จันทบุรี","สื่อสารความเสี่ยงโรคและภัยสุขภาพ","หน่วยกามโรคและโรคเอดส์ที่ 6.1 บางละมุง","ห้องปฏิบัติการทางการแพทย์ด้านควบคุมโรค"};
            ViewBag.workGroupView = workGroupView;

            return View(innovation);
        }

        [HttpPost]
        public ActionResult EditInnovation(Innovation innovation, string workOverview)
        {
            if (ModelState.IsValid)
            {
                if (innovation.files2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.files2.FileName);
                    string extension = Path.GetExtension(innovation.files2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.files = "/docUploadInnovation/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadInnovation/"), fileNameDoc);
                    innovation.files2.SaveAs(path);
                }

                if (innovation.finalFilesHttpPost != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.finalFilesHttpPost.FileName);
                    string extension = Path.GetExtension(innovation.finalFilesHttpPost.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.finalFiles = "/docUploadFinalInnovation/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadFinalInnovation/"), fileNameDoc);
                    innovation.finalFilesHttpPost.SaveAs(path);
                }

                //if (Session["Id"] != null || Session["Username"] != null)
                //{
                //    innovation.idOwner = Convert.ToInt32(Session["Id"]);
                //    innovation.usernameOwner = Session["Username"].ToString();
                //}

                innovation.views = 0;
                innovation.workOverview = Convert.ToDecimal(workOverview);
                innovation.date = DateTime.Today;

                db.Innovation.Attach(innovation);
                db.Entry(innovation).Property(x => x.date).IsModified = true;
                db.Entry(innovation).Property(x => x.type).IsModified = true;
                db.Entry(innovation).Property(x => x.name).IsModified = true;
                db.Entry(innovation).Property(x => x.workGroup).IsModified = true;
                db.Entry(innovation).Property(x => x.creator).IsModified = true;
                db.Entry(innovation).Property(x => x.backgroudAndImportance).IsModified = true;
                db.Entry(innovation).Property(x => x.objective).IsModified = true;
                db.Entry(innovation).Property(x => x.benefit).IsModified = true;
                db.Entry(innovation).Property(x => x.targetGroup).IsModified = true;
                db.Entry(innovation).Property(x => x.files).IsModified = true;
                db.Entry(innovation).Property(x => x.activity1).IsModified = true;
                db.Entry(innovation).Property(x => x.activity2).IsModified = true;
                db.Entry(innovation).Property(x => x.activity3).IsModified = true;
                db.Entry(innovation).Property(x => x.activity4).IsModified = true;
                db.Entry(innovation).Property(x => x.activity5).IsModified = true;
                db.Entry(innovation).Property(x => x.activity6).IsModified = true;
                db.Entry(innovation).Property(x => x.activity7).IsModified = true;
                db.Entry(innovation).Property(x => x.activity8).IsModified = true;
                db.Entry(innovation).Property(x => x.activity9).IsModified = true;
                db.Entry(innovation).Property(x => x.activity10).IsModified = true;
                db.Entry(innovation).Property(x => x.isJan1).IsModified = true;
                db.Entry(innovation).Property(x => x.isJan2).IsModified = true;
                db.Entry(innovation).Property(x => x.isJan3).IsModified = true;
                db.Entry(innovation).Property(x => x.isJan4).IsModified = true;
                db.Entry(innovation).Property(x => x.isJan5).IsModified = true;
                db.Entry(innovation).Property(x => x.isJan6).IsModified = true;
                db.Entry(innovation).Property(x => x.isJan7).IsModified = true;
                db.Entry(innovation).Property(x => x.isJan8).IsModified = true;
                db.Entry(innovation).Property(x => x.isJan9).IsModified = true;
                db.Entry(innovation).Property(x => x.isJan10).IsModified = true;
                db.Entry(innovation).Property(x => x.isFeb1).IsModified = true;
                db.Entry(innovation).Property(x => x.isFeb2).IsModified = true;
                db.Entry(innovation).Property(x => x.isFeb3).IsModified = true;
                db.Entry(innovation).Property(x => x.isFeb4).IsModified = true;
                db.Entry(innovation).Property(x => x.isFeb5).IsModified = true;
                db.Entry(innovation).Property(x => x.isFeb6).IsModified = true;
                db.Entry(innovation).Property(x => x.isFeb7).IsModified = true;
                db.Entry(innovation).Property(x => x.isFeb8).IsModified = true;
                db.Entry(innovation).Property(x => x.isFeb9).IsModified = true;
                db.Entry(innovation).Property(x => x.isFeb10).IsModified = true;
                db.Entry(innovation).Property(x => x.isMar1).IsModified = true;
                db.Entry(innovation).Property(x => x.isMar2).IsModified = true;
                db.Entry(innovation).Property(x => x.isMar3).IsModified = true;
                db.Entry(innovation).Property(x => x.isMar4).IsModified = true;
                db.Entry(innovation).Property(x => x.isMar5).IsModified = true;
                db.Entry(innovation).Property(x => x.isMar6).IsModified = true;
                db.Entry(innovation).Property(x => x.isMar7).IsModified = true;
                db.Entry(innovation).Property(x => x.isMar8).IsModified = true;
                db.Entry(innovation).Property(x => x.isMar9).IsModified = true;
                db.Entry(innovation).Property(x => x.isMar10).IsModified = true;
                db.Entry(innovation).Property(x => x.isApr1).IsModified = true;
                db.Entry(innovation).Property(x => x.isApr2).IsModified = true;
                db.Entry(innovation).Property(x => x.isApr3).IsModified = true;
                db.Entry(innovation).Property(x => x.isApr4).IsModified = true;
                db.Entry(innovation).Property(x => x.isApr5).IsModified = true;
                db.Entry(innovation).Property(x => x.isApr6).IsModified = true;
                db.Entry(innovation).Property(x => x.isApr7).IsModified = true;
                db.Entry(innovation).Property(x => x.isApr8).IsModified = true;
                db.Entry(innovation).Property(x => x.isApr9).IsModified = true;
                db.Entry(innovation).Property(x => x.isApr10).IsModified = true;
                db.Entry(innovation).Property(x => x.isMay1).IsModified = true;
                db.Entry(innovation).Property(x => x.isMay2).IsModified = true;
                db.Entry(innovation).Property(x => x.isMay3).IsModified = true;
                db.Entry(innovation).Property(x => x.isMay4).IsModified = true;
                db.Entry(innovation).Property(x => x.isMay5).IsModified = true;
                db.Entry(innovation).Property(x => x.isMay6).IsModified = true;
                db.Entry(innovation).Property(x => x.isMay7).IsModified = true;
                db.Entry(innovation).Property(x => x.isMay8).IsModified = true;
                db.Entry(innovation).Property(x => x.isMay9).IsModified = true;
                db.Entry(innovation).Property(x => x.isMay10).IsModified = true;
                db.Entry(innovation).Property(x => x.isJun1).IsModified = true;
                db.Entry(innovation).Property(x => x.isJun2).IsModified = true;
                db.Entry(innovation).Property(x => x.isJun3).IsModified = true;
                db.Entry(innovation).Property(x => x.isJun4).IsModified = true;
                db.Entry(innovation).Property(x => x.isJun5).IsModified = true;
                db.Entry(innovation).Property(x => x.isJun6).IsModified = true;
                db.Entry(innovation).Property(x => x.isJun7).IsModified = true;
                db.Entry(innovation).Property(x => x.isJun8).IsModified = true;
                db.Entry(innovation).Property(x => x.isJun9).IsModified = true;
                db.Entry(innovation).Property(x => x.isJun10).IsModified = true;
                db.Entry(innovation).Property(x => x.isJul1).IsModified = true;
                db.Entry(innovation).Property(x => x.isJul2).IsModified = true;
                db.Entry(innovation).Property(x => x.isJul3).IsModified = true;
                db.Entry(innovation).Property(x => x.isJul4).IsModified = true;
                db.Entry(innovation).Property(x => x.isJul5).IsModified = true;
                db.Entry(innovation).Property(x => x.isJul6).IsModified = true;
                db.Entry(innovation).Property(x => x.isJul7).IsModified = true;
                db.Entry(innovation).Property(x => x.isJul8).IsModified = true;
                db.Entry(innovation).Property(x => x.isJul9).IsModified = true;
                db.Entry(innovation).Property(x => x.isJul10).IsModified = true;
                db.Entry(innovation).Property(x => x.isAug1).IsModified = true;
                db.Entry(innovation).Property(x => x.isAug2).IsModified = true;
                db.Entry(innovation).Property(x => x.isAug3).IsModified = true;
                db.Entry(innovation).Property(x => x.isAug4).IsModified = true;
                db.Entry(innovation).Property(x => x.isAug5).IsModified = true;
                db.Entry(innovation).Property(x => x.isAug6).IsModified = true;
                db.Entry(innovation).Property(x => x.isAug7).IsModified = true;
                db.Entry(innovation).Property(x => x.isAug8).IsModified = true;
                db.Entry(innovation).Property(x => x.isAug9).IsModified = true;
                db.Entry(innovation).Property(x => x.isAug10).IsModified = true;
                db.Entry(innovation).Property(x => x.isSep1).IsModified = true;
                db.Entry(innovation).Property(x => x.isSep2).IsModified = true;
                db.Entry(innovation).Property(x => x.isSep3).IsModified = true;
                db.Entry(innovation).Property(x => x.isSep4).IsModified = true;
                db.Entry(innovation).Property(x => x.isSep5).IsModified = true;
                db.Entry(innovation).Property(x => x.isSep6).IsModified = true;
                db.Entry(innovation).Property(x => x.isSep7).IsModified = true;
                db.Entry(innovation).Property(x => x.isSep8).IsModified = true;
                db.Entry(innovation).Property(x => x.isSep9).IsModified = true;
                db.Entry(innovation).Property(x => x.isSep10).IsModified = true;
                db.Entry(innovation).Property(x => x.isOct1).IsModified = true;
                db.Entry(innovation).Property(x => x.isOct2).IsModified = true;
                db.Entry(innovation).Property(x => x.isOct3).IsModified = true;
                db.Entry(innovation).Property(x => x.isOct4).IsModified = true;
                db.Entry(innovation).Property(x => x.isOct5).IsModified = true;
                db.Entry(innovation).Property(x => x.isOct6).IsModified = true;
                db.Entry(innovation).Property(x => x.isOct7).IsModified = true;
                db.Entry(innovation).Property(x => x.isOct8).IsModified = true;
                db.Entry(innovation).Property(x => x.isOct9).IsModified = true;
                db.Entry(innovation).Property(x => x.isOct10).IsModified = true;
                db.Entry(innovation).Property(x => x.isNov1).IsModified = true;
                db.Entry(innovation).Property(x => x.isNov2).IsModified = true;
                db.Entry(innovation).Property(x => x.isNov3).IsModified = true;
                db.Entry(innovation).Property(x => x.isNov4).IsModified = true;
                db.Entry(innovation).Property(x => x.isNov5).IsModified = true;
                db.Entry(innovation).Property(x => x.isNov6).IsModified = true;
                db.Entry(innovation).Property(x => x.isNov7).IsModified = true;
                db.Entry(innovation).Property(x => x.isNov8).IsModified = true;
                db.Entry(innovation).Property(x => x.isNov9).IsModified = true;
                db.Entry(innovation).Property(x => x.isNov10).IsModified = true;
                db.Entry(innovation).Property(x => x.isDec1).IsModified = true;
                db.Entry(innovation).Property(x => x.isDec2).IsModified = true;
                db.Entry(innovation).Property(x => x.isDec3).IsModified = true;
                db.Entry(innovation).Property(x => x.isDec4).IsModified = true;
                db.Entry(innovation).Property(x => x.isDec5).IsModified = true;
                db.Entry(innovation).Property(x => x.isDec6).IsModified = true;
                db.Entry(innovation).Property(x => x.isDec7).IsModified = true;
                db.Entry(innovation).Property(x => x.isDec8).IsModified = true;
                db.Entry(innovation).Property(x => x.isDec9).IsModified = true;
                db.Entry(innovation).Property(x => x.isDec10).IsModified = true;
                db.Entry(innovation).Property(x => x.budgetYear).IsModified = true;
                db.Entry(innovation).Property(x => x.usernameOwner).IsModified = true;
                db.Entry(innovation).Property(x => x.views).IsModified = true;
                db.Entry(innovation).Property(x => x.finalFiles).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("EditSuccessMessage");
            }

            var modelInnovator = db.Innovator.OrderBy(x => x.name).ToList();
            ViewBag.InnovatorView = (from item in modelInnovator
                                     select new SelectListItem
                                     {
                                         Text = item.name + " " + item.surname,
                                         Value = item.name.ToString() + " " + item.surname.ToString()
                                     });

            var budgetYearView = new List<string>()
            { "2559", "2560", "2561", "2562", "2563", "2564", "2565" };
            ViewBag.budgetYearView = budgetYearView;

            var typeView = new List<string>()
            {"Product","Process","Service","Policy/Strategic"};
            ViewBag.typeView = typeView;

            var workGroupView = new List<string>()
            {"งานเภสัชกรรม","บริหารทั่วไป","พัฒนานวัตกรรมและวิจัย และงานควบคุมโรคเขตเมือง","พัฒนาระบบบริหารองค์กร และงานสถาปัตยกรรมข้อมูล","ยุทธศาสตร์ แผนงาน และเครือข่าย","ระบาดวิทยาและตอบโต้ภาวะฉุกเฉินทางด้านสาธารณสุข","โรคติดต่อ","โรคไม่ติดต่อ"
            ,"ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.1 ศรีราชา","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.2 สระแก้ว","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.3 ระยอง","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.4 ตราด","ศูนย์ควบคุมโรคติดต่อนำโดยแมลงที่ 6.5 จันทบุรี","สื่อสารความเสี่ยงโรคและภัยสุขภาพ","หน่วยกามโรคและโรคเอดส์ที่ 6.1 บางละมุง","ห้องปฏิบัติการทางการแพทย์ด้านควบคุมโรค"};
            ViewBag.workGroupView = workGroupView;

            return View(innovation);
        }

        public ActionResult DeleteInnovation(int id)
        {
            Innovation innovation = db.Innovation.Find(id);
            if (innovation == null)
            {
                return RedirectToAction("Index");
            }
            return View(innovation);
        }

        [HttpPost, ActionName("DeleteInnovation")]
        public ActionResult DeleteInnovationConfirm(int id)
        {
            Innovation innovation = db.Innovation.Find(id);
            db.Innovation.Remove(innovation);
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