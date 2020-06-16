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
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var docdownload = db.DocumentDownload;

                    return View(docdownload.ToList());
                }
                return RedirectToAction("Index", "DocumentDownload");
            }
            else
                return RedirectToAction("Index", "DocumentDownload");            
        }


        public ActionResult CreateDownloadDocument()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    return View();
                }
                return RedirectToAction("Index", "DocumentDownload");
            }
            else
                return RedirectToAction("Index", "DocumentDownload");          
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

                documentDownload.date = DateTime.Today;
                documentDownload.views = 0;
                db.DocumentDownload.Add(documentDownload);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("CreateSuccessMessage");
            }

                return View(documentDownload);
        }

        public ActionResult DocumentDetail(int id)
        {
            DocumentDownload documentDownload = db.DocumentDownload.Find(id);
            if (documentDownload == null)
            {
                return RedirectToAction("Index");
            }
            //--Page Visitor
            documentDownload.views = documentDownload.views + 1;
            db.Entry(documentDownload).State = EntityState.Modified;
            db.SaveChanges();
            return View(documentDownload);
        }

        public ActionResult EditDownloadDocument(int id)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    DocumentDownload documentDownload = db.DocumentDownload.Find(id);
                    if (documentDownload == null)
                    {
                        return RedirectToAction("Index");
                    }

                    return View(documentDownload);
                }
                return RedirectToAction("Index", "DocumentDownload");
            }
            else
                return RedirectToAction("Index", "DocumentDownload");            
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

                db.DocumentDownload.Attach(documentDownload);
                db.Entry(documentDownload).Property(x => x.name).IsModified = true;
                db.Entry(documentDownload).Property(x => x.description).IsModified = true;
                db.Entry(documentDownload).Property(x => x.docUpload).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("EditSuccessMessage");
            }

            return View(documentDownload);
        }

        public ActionResult DeleteDownloadDocument(int id)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    DocumentDownload documentDownload = db.DocumentDownload.Find(id);
                    if (documentDownload == null)
                    {
                        return RedirectToAction("Index");
                    }

                    return View(documentDownload);
                }
                return RedirectToAction("Index", "DocumentDownload");
            }
            else
                return RedirectToAction("Index", "DocumentDownload");            
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