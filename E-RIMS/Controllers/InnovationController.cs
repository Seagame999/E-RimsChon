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
        public ActionResult Index(string search,int? page)
        {
            var innovation = db.Innovation;

            //--Pagination 10 each
            var innovationResult = innovation.ToList().ToPagedList(page ?? 1, 10);

            //--Search Engine
            if (search != null)
            {
                if (search != null)
                {
                    innovationResult = db.Innovation.Where(x => x.name.StartsWith(search) || x.name == search).ToList().ToPagedList(page ?? 1, 10); ;
                }
                else if (search != null)
                {
                    innovationResult = db.Innovation.Where(x => x.@abstract.StartsWith(search) || x.@abstract == search).ToList().ToPagedList(page ?? 1, 10); ;
                }
                else if (search != null)
                {
                    innovationResult = db.Innovation.Where(x => x.creator.StartsWith(search) || x.creator == search).ToList().ToPagedList(page ?? 1, 10); ;
                }
            }
            //--

                return View(innovationResult);
        }

        public ActionResult AllInnovation()
        {
            var innovation = db.Innovation;
            return View(innovation.ToList());
        }

        public ActionResult CreateInnovation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateInnovation(Innovation innovation)
        {
            if(ModelState.IsValid)
            {
               if(innovation != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.files2.FileName);
                    string extension = Path.GetExtension(innovation.files2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.files = "~/docUploadInnovation/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadInnovation/"), fileNameDoc);
                    innovation.files2.SaveAs(path);
                }

                db.Innovation.Add(innovation);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }

            return View(innovation);
        }

        public ActionResult DetailInnovation(int id)
        {
            Innovation innovation = db.Innovation.Find(id);
            if(innovation == null)
            {
                return HttpNotFound();
            }
            return View(innovation);
            
        }

        public ActionResult EditInnovation(int id)
        {
            Innovation innovation = db.Innovation.Find(id);
            if (innovation == null)
            {
                return HttpNotFound();
            }
            return View(innovation);
        }

        [HttpPost]
        public ActionResult EditInnovation(Innovation innovation)
        {
            if (ModelState.IsValid)
            {
                if(innovation.files2 != null)
                {
                    var fileNameDoc = Path.GetFileNameWithoutExtension(innovation.files2.FileName);
                    string extension = Path.GetExtension(innovation.files2.FileName);
                    fileNameDoc = fileNameDoc + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovation.files = "~/docUploadInnovation/" + fileNameDoc;
                    var path = Path.Combine(Server.MapPath("~/docUploadInnovation/"), fileNameDoc);
                    innovation.files2.SaveAs(path);
                }

                db.Entry(innovation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(innovation);
        }

        public ActionResult DeleteInnovation(int id)
        {
            Innovation innovation = db.Innovation.Find(id);
            if (innovation == null)
            {
                return HttpNotFound();
            }
            return View(innovation);
        }

        [HttpPost,ActionName("DeleteInnovation")]
        public ActionResult DeleteInnovationConfirm(int id)
        {
            Innovation innovation = db.Innovation.Find(id);
            db.Innovation.Remove(innovation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}