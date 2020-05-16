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
    public class DocumentDownloadController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: DocumentDownload
        public ActionResult Index(int? page)
        {
            var docdownload = db.DocumentDownload;

            //--Pagination 10 each
            var docdownloadResult = docdownload.ToList().ToPagedList(page ?? 1, 10);

            return View(docdownloadResult);
        }

        public ActionResult AllDocumentDownload()
        {
            var docdownload = db.DocumentDownload;

            return View(docdownload.ToList());
        }


        public ActionResult CreateDownloadDocument()
        {
            return View();
        }
        

        [HttpPost]
        public ActionResult CreateDownloadDocument(DocumentDownload documentDownload)
        {
            if (ModelState.IsValid)
            {
                if (documentDownload.docUpload2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(documentDownload.docUpload2.FileName);
                    string extension2 = Path.GetExtension(documentDownload.docUpload2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension2;
                    documentDownload.docUpload = "/docUploadDocumentDownload/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadDocumentDownload/"), fileNameDoc);
                    documentDownload.docUpload2.SaveAs(path);
                }

                db.DocumentDownload.Add(documentDownload);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("CreateSuccessMessage");
            }

                return View(documentDownload);
        }

        public ActionResult EditDownloadDocument(int id)
        {
            DocumentDownload documentDownload = db.DocumentDownload.Find(id);
            if(documentDownload == null)
            {
                return RedirectToAction("Index");
            }

            return View(documentDownload);
        }

        [HttpPost]
        public ActionResult EditDownloadDocument(DocumentDownload documentDownload)
        {
            if (ModelState.IsValid)
            {
                if (documentDownload.docUpload2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(documentDownload.docUpload2.FileName);
                    string extension2 = Path.GetExtension(documentDownload.docUpload2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension2;
                    documentDownload.docUpload = "/docUploadDocumentDownload/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadDocumentDownload/"), fileNameDoc);
                    documentDownload.docUpload2.SaveAs(path);
                }

                db.Entry(documentDownload).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditSuccessMessage");
            }

            return View(documentDownload);
        }

        public ActionResult DeleteDownloadDocument(int id)
        {
            DocumentDownload documentDownload = db.DocumentDownload.Find(id);
            if (documentDownload == null)
            {
                return RedirectToAction("Index");
            }

            return View(documentDownload);
        }

        [HttpPost, ActionName("DeleteDownloadDocument")]
        public ActionResult DeleteDownloadDocumentConfirm(int id)
        {
            DocumentDownload documentDownload = db.DocumentDownload.Find(id);
            db.DocumentDownload.Remove(documentDownload);
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