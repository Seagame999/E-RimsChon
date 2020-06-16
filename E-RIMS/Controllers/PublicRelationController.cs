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
using System.Data.Entity.Validation;

namespace E_RIMS.Controllers
{
    public class PublicRelationController : Controller
    {

        ERIMSEntities db = new ERIMSEntities();
        // GET: PublicRelation

        public ActionResult AllNews(int? page)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var publicRelation = db.PublicRelation;
                    var publicRelationResult = publicRelation.OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 10);
                    return View(publicRelationResult);
                }
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateNews()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult CreateNews(PublicRelation publicRelation)
        {
            if (ModelState.IsValid)
            {
                if (publicRelation.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(publicRelation.image2.FileName);
                    string extension = Path.GetExtension(publicRelation.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    publicRelation.image = "/ImagePublicRelation/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImagePublicRelation/"), fileName);
                    publicRelation.image2.SaveAs(fileName);
                }

                if (publicRelation.docUpload2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(publicRelation.docUpload2.FileName);
                    string extension2 = Path.GetExtension(publicRelation.docUpload2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension2;
                    publicRelation.docUpload = "/docUploadPublicRelation/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadPublicRelation/"), fileNameDoc);
                    publicRelation.docUpload2.SaveAs(path);
                }

                publicRelation.views = 0;
                db.PublicRelation.Add(publicRelation);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("CreateSuccessMessage");
            }

            return View(publicRelation);
        }

        public ActionResult News(int id)
        {
            PublicRelation publicRelation = db.PublicRelation.Find(id);
            if (publicRelation == null)
            {
                return RedirectToAction("AllNews");
            }
            //--Page Visitor
            publicRelation.views = publicRelation.views + 1;
            db.Entry(publicRelation).State = EntityState.Modified;
            db.SaveChanges();
            return View(publicRelation);
        }

        public ActionResult EditNews(int id)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    PublicRelation publicRelation = db.PublicRelation.Find(id);
                    if (publicRelation == null)
                    {
                        return RedirectToAction("AllNews");
                    }
                    return View(publicRelation);
                }
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");           
        }

        [HttpPost]
        public ActionResult EditNews(PublicRelation publicRelation)
        {
            if (ModelState.IsValid)
            {
                if (publicRelation.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(publicRelation.image2.FileName);
                    string extension = Path.GetExtension(publicRelation.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    publicRelation.image = "/ImagePublicRelation/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImagePublicRelation/"), fileName);
                    publicRelation.image2.SaveAs(fileName);
                }

                if (publicRelation.docUpload2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(publicRelation.docUpload2.FileName);
                    string extension2 = Path.GetExtension(publicRelation.docUpload2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension2;
                    publicRelation.docUpload = "/docUploadPublicRelation/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadPublicRelation/"), fileNameDoc);
                    publicRelation.docUpload2.SaveAs(path);
                }
                publicRelation.views = 0;
                db.Entry(publicRelation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditSuccessMessage");

            }
            return View(publicRelation);
        }

        public ActionResult DeleteNews(int id)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    PublicRelation publicRelation = db.PublicRelation.Find(id);

                    if (publicRelation == null)
                    {
                        return RedirectToAction("AllNews");
                    }
                    return View(publicRelation);
                }
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("DeleteNews")]
        public ActionResult DeleteNewsConfirm(int id)
        {
            PublicRelation publicRelation = db.PublicRelation.Find(id);
            db.PublicRelation.Remove(publicRelation);
            db.SaveChanges();
            return RedirectToAction("AllNews");
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
