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
    public class LoginController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username , string password)
        {
            if (ModelState.IsValid)
            {
                var inputPassword = GetMD5(password);
                var data = db.Member.Where(s => s.username.Equals(username) && s.password.Equals(inputPassword)).ToList();

                if(data.Count() > 0)
                {
                    //Create Session
                    Session["Username"] = data.FirstOrDefault().username;
                    Session["Email"] = data.FirstOrDefault().email;
                    Session["Id"] = data.FirstOrDefault().id;

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ViewBag.Error = "ไม่สามารถเข้าสู่ระบบได้ หากท่านลืมรหัสผ่านติดต่อ ADMIN เว็บไซต์เพื่อกำหนดรหัสผ่านใหม่";

                    return RedirectToAction("Login");
                }

            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
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