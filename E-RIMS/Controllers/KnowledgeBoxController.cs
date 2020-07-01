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
    public class KnowledgeBoxController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();

        // GET: KnowledgeBox
        public ActionResult Index(int? page)
        {
            var knowledgebox = db.KnowledgeBox;

            //--Pagination 10 each
            var knowledgeboxResult = knowledgebox.ToList().ToPagedList(page ?? 1, 10);

            return View(knowledgeboxResult);
        }

        public ActionResult CreateKnowledgeBox()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    return View();
                }
                return RedirectToAction("Index", "KnowledgeBox");
            }
            else
                return RedirectToAction("Index", "KnowledgeBox");
        }

        [HttpPost]
        public ActionResult CreateKnowledgeBox(KnowledgeBox knowledgeBox)
        {
            if (ModelState.IsValid)
            {
                if (knowledgeBox.docUpload2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(knowledgeBox.docUpload2.FileName);
                    string extension2 = Path.GetExtension(knowledgeBox.docUpload2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension2;
                    knowledgeBox.docUpload = "/docUploadKnowledgeBox/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadKnowledgeBox/"), fileNameDoc);
                    knowledgeBox.docUpload2.SaveAs(path);
                }

                knowledgeBox.date = DateTime.Today;
                knowledgeBox.views = 0;
                db.KnowledgeBox.Add(knowledgeBox);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("CreateSuccessMessage");
            }

            return View(knowledgeBox);
        }

        public ActionResult KnowledgeBoxDetail(int id)
        {
            KnowledgeBox knowledgeBox = db.KnowledgeBox.Find(id);
            if (knowledgeBox == null)
            {
                return RedirectToAction("Index");
            }

            //--Page Visitor
            knowledgeBox.views = knowledgeBox.views + 1;
            db.Entry(knowledgeBox).State = EntityState.Modified;
            db.SaveChanges();

            return View(knowledgeBox);
        }

        public ActionResult EditKnowledgeBox(int id)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    KnowledgeBox knowledgeBox = db.KnowledgeBox.Find(id);
                    if (knowledgeBox == null)
                    {
                        return RedirectToAction("Index");
                    }

                    return View(knowledgeBox);
                }
                return RedirectToAction("Index", "KnowledgeBox");
            }
            else
                return RedirectToAction("Index", "KnowledgeBox");
        }

        [HttpPost]
        public ActionResult EditKnowledgeBox(KnowledgeBox knowledgeBox)
        {
            if (ModelState.IsValid)
            {
                if (knowledgeBox.docUpload2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(knowledgeBox.docUpload2.FileName);
                    string extension2 = Path.GetExtension(knowledgeBox.docUpload2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension2;
                    knowledgeBox.docUpload = "/docUploadKnowledgeBox/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadKnowledgeBox/"), fileNameDoc);
                    knowledgeBox.docUpload2.SaveAs(path);
                }

                db.KnowledgeBox.Attach(knowledgeBox);
                db.Entry(knowledgeBox).Property(x => x.name).IsModified = true;
                db.Entry(knowledgeBox).Property(x => x.description).IsModified = true;
                db.Entry(knowledgeBox).Property(x => x.docUpload).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("EditSuccessMessage");
            }

            return View(knowledgeBox);
        }

        public ActionResult DeleteKnowledgeBox(int id)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    KnowledgeBox knowledgeBox = db.KnowledgeBox.Find(id);
                    if (knowledgeBox == null)
                    {
                        return RedirectToAction("Index");
                    }

                    return View(knowledgeBox);
                }

                return RedirectToAction("Index", "KnowledgeBox");
            }
            else
                return RedirectToAction("Index", "KnowledgeBox");
        }

        [HttpPost, ActionName("DeleteKnowledgeBox")]
        public ActionResult DeleteKnowledgeBoxConfirm(int id)
        {
            KnowledgeBox knowledgeBox = db.KnowledgeBox.Find(id);
            db.KnowledgeBox.Remove(knowledgeBox);
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