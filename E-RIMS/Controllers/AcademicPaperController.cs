using E_RIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;

namespace E_RIMS.Controllers
{
    public class AcademicPaperController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: AcademicPaper
        public ActionResult Index()
        {
            var academicPaper = db.AcademicPaper;
            return View(academicPaper);
        }

        public ActionResult AllAcademicPaper()
        {
            var academicPaper = db.AcademicPaper;
            return View(academicPaper);
        }

        public ActionResult CreateAcademicPaper()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAcademicPaper(AcademicPaper academicPaper)
        {
            if (ModelState.IsValid)
            {
                if (academicPaper.files2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(academicPaper.files2.FileName);
                    string extension = Path.GetExtension(academicPaper.files2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    academicPaper.files = "~/docUploadAcademicPaper/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadAcademicPaper/"), fileNameDoc);
                    academicPaper.files2.SaveAs(path);
                }

                db.AcademicPaper.Add(academicPaper);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }
            return View(academicPaper);
        }


        public ActionResult DetailAcademicPaper(int id)
        {
            AcademicPaper academicPaper = db.AcademicPaper.Find(id);
            if (academicPaper == null)
            {
                return HttpNotFound();
            }
            return View(academicPaper);
        }

        public ActionResult EditAcademicPaper(int id)
        {
            AcademicPaper academicPaper = db.AcademicPaper.Find(id);
            if (academicPaper == null)
            {
                return HttpNotFound();
            }
            return View(academicPaper);
        }

        [HttpPost]
        public ActionResult EditAcademicPaper(AcademicPaper academicPaper)
        {
            if (ModelState.IsValid)
            {
                if (academicPaper.files2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(academicPaper.files2.FileName);
                    string extension = Path.GetExtension(academicPaper.files2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    academicPaper.files = "~/docUploadAcademicPaper/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadAcademicPaper/"), fileNameDoc);
                    academicPaper.files2.SaveAs(path);
                }

                db.Entry(academicPaper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(academicPaper);
        }

        public ActionResult DeleteAcademicPaper(int id)
        {
            AcademicPaper academicPaper = db.AcademicPaper.Find(id);
            if (academicPaper == null)
            {
                return HttpNotFound();
            }
            return View(academicPaper);
        }

        [HttpPost,ActionName("DeleteAcademicPaper")]
        public ActionResult DeleteAcademicPaperConfirm(int id)
        {
            AcademicPaper academicPaper = db.AcademicPaper.Find(id);
            db.AcademicPaper.Remove(academicPaper);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}