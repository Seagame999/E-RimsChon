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
    public class JournalController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: Journal
        public ActionResult Index(int? page)
        {
            var journal = db.Journal;

            //--Pagination 10 each
            var journalResult = journal.ToList().ToPagedList(page ?? 1, 10);

            return View(journalResult);
        }

        public ActionResult AllJournal()
        {
            var journal = db.Journal;

            return View(journal.ToList());
        }

        public ActionResult CreateJournal()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateJournal(Journal journal)
        {
            if (ModelState.IsValid)
            {
                if(journal.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(journal.image2.FileName);
                    string extension = Path.GetExtension(journal.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    journal.image = "/ImageJournal/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageJournal/"), fileName);
                    journal.image2.SaveAs(fileName);
                }

                if (journal.files2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(journal.files2.FileName);
                    string extension2 = Path.GetExtension(journal.files2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension2;
                    journal.files = "/docUploadJournal/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadJournal/"), fileNameDoc);
                    journal.files2.SaveAs(path);
                }

                db.Journal.Add(journal);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }

            return View(journal);
        }

        public ActionResult DetailJournal(int id)
        {
            Journal journal = db.Journal.Find(id);
            if(journal == null)
            {
                return RedirectToAction("Index");
            }

            return View(journal);
        }

        public ActionResult EditJournal(int id)
        {
            Journal journal = db.Journal.Find(id);
            if (journal == null)
            {
                return RedirectToAction("Index");
            }

            return View(journal);
        }

        [HttpPost]
        public ActionResult EditJournal(Journal journal)
        {
            if (ModelState.IsValid)
            {
                if (journal.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(journal.image2.FileName);
                    string extension = Path.GetExtension(journal.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    journal.image = "/ImageJournal/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageJournal/"), fileName);
                    journal.image2.SaveAs(fileName);
                }

                if (journal.files2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(journal.files2.FileName);
                    string extension2 = Path.GetExtension(journal.files2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension2;
                    journal.files = "/docUploadJournal/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadJournal/"), fileNameDoc);
                    journal.files2.SaveAs(path);
                }

                db.Entry(journal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(journal);
        }

        public ActionResult DeleteJournal(int id)
        {
            Journal journal = db.Journal.Find(id);
            if (journal == null)
            {
                return RedirectToAction("Index");
            }

            return View(journal);
        }

        [HttpPost,ActionName("DeleteJournal")]
        public ActionResult DeleteJournalConfirm(int id)
        {
            Journal journal = db.Journal.Find(id);
            db.Journal.Remove(journal);
            db.SaveChanges();
            return RedirectToAction("AllJournal");
        }
    }
}