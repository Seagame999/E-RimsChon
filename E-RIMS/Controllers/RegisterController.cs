using E_RIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace E_RIMS.Controllers
{
    public class RegisterController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: Register
        public ActionResult Index()
        {
            var member = db.Member;
            return View(member.ToList());
        }

        public ActionResult CreateRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRegister(Member member)
        {
            if (ModelState.IsValid)
            {
                db.Member.Add(member);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }

            return View(member);
        }

        public ActionResult DetailRegister(int id)
        {
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        public ActionResult EditRegister(int id)
        {
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        [HttpPost]
        public ActionResult EditRegister(Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }
    }
}