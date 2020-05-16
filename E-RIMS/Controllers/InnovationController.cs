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
                                         Value = item.name.ToString() + item.surname.ToString()
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

                //if (Session["Id"] != null || Session["Username"] != null)
                //{
                //    innovation.idOwner = Convert.ToInt32(Session["Id"]);
                //    innovation.usernameOwner = Session["Username"].ToString();
                //}

                innovation.workOverview = 00.00m;
                innovation.date = DateTime.Today;
                db.Innovation.Add(innovation);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("CreateSuccessMessage");
            }

            return View(innovation);
        }

        public ActionResult DetailInnovation(int id)
        {
            Innovation innovation = db.Innovation.Find(id);
            if (innovation == null)
            {
                return RedirectToAction("Index");
            }
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
                                         Value = item.name.ToString() + item.surname.ToString()
                                     });

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

                //if (Session["Id"] != null || Session["Username"] != null)
                //{
                //    innovation.idOwner = Convert.ToInt32(Session["Id"]);
                //    innovation.usernameOwner = Session["Username"].ToString();
                //}

                innovation.workOverview = Convert.ToDecimal(workOverview);
                innovation.date = DateTime.Today;
                db.Entry(innovation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditSuccessMessage");
            }
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