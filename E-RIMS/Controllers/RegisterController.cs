using E_RIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;

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
                member.password = GetMD5(member.password);
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
                return RedirectToAction("Index");
            }
            return View(member);
        }

        public ActionResult EditRegister(int id)
        {
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return RedirectToAction("Index");
            }
            return View(member);
        }

        [HttpPost]
        public ActionResult EditRegister(Member member)
        {
            if (ModelState.IsValid)
            {
                member.password = GetMD5(member.password);
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }


        //Password Hash MD5
        public string GetMD5(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(password);
            byte[] targetData = md5.ComputeHash(fromData);

            string byteToString = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byteToString += targetData[i].ToString("x2"); 
            }
            return byteToString;

        }
    }
}