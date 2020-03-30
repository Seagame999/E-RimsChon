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
    public class PublicRelationController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: PublicRelation

        public ActionResult AllNews()
        {
            var publicRelation = db.PublicRelation;
            return View(publicRelation.ToList());
            //Test Github
            //Test 2 2
           
        }

        public ActionResult CreateNews()
        {
            return View();
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
                    fileName = fileName +"_"+ DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    publicRelation.image = "~/ImagePublicRelation/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImagePublicRelation/"), fileName);
                    publicRelation.image2.SaveAs(fileName);
                }

                if (publicRelation.docUpload2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(publicRelation.docUpload2.FileName);
                    string extension2 = Path.GetExtension(publicRelation.docUpload2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension2;
                    publicRelation.docUpload = "~/docUploadPublicRelation/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadPublicRelation/"), fileNameDoc);
                    publicRelation.docUpload2.SaveAs(path);
                }

                db.PublicRelation.Add(publicRelation);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("AllNews");
            }

            return View(publicRelation);
        }



        public ActionResult News(int id)
        {
            PublicRelation publicRelation = db.PublicRelation.Find(id);
            if (publicRelation == null)
            {
                return HttpNotFound();
            }
            return View(publicRelation);
        }


        public ActionResult EditNews(int id)
        {
            PublicRelation publicRelation = db.PublicRelation.Find(id);
            if (publicRelation == null)
            {
                return HttpNotFound();
            }
            return View(publicRelation);
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
                    publicRelation.image = "~/ImagePublicRelation/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImagePublicRelation/"), fileName);
                    publicRelation.image2.SaveAs(fileName);
                }

                if (publicRelation.docUpload2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(publicRelation.docUpload2.FileName);
                    string extension2 = Path.GetExtension(publicRelation.docUpload2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension2;
                    publicRelation.docUpload = "~/docUploadPublicRelation/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadPublicRelation/"), fileNameDoc);
                    publicRelation.docUpload2.SaveAs(path);
                }
                db.Entry(publicRelation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllNews");

            }
            return View(publicRelation);
        }

        public ActionResult DeleteNews(int id)
        {
            PublicRelation publicRelation = db.PublicRelation.Find(id);

            if (publicRelation == null)
            {
                return HttpNotFound();
            }
            return View(publicRelation);
        }

        [HttpPost, ActionName("DeleteNews")]
        public ActionResult DeleteNewsConfirm(int id)
        {
            PublicRelation publicRelation = db.PublicRelation.Find(id);
            db.PublicRelation.Remove(publicRelation);
            db.SaveChanges();
            return RedirectToAction("AllNews");
        }
        
    }
}
