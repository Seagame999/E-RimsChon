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

        public ActionResult Login()
        {
            if (Session["Role"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var inputPassword = GetMD5(password);
                var data = db.Member.Where(s => s.username.Equals(username) && s.password.Equals(inputPassword)).ToList();

                //var data = db.Member.Where(s => s.username.Equals(username) && s.password.Equals(password)).ToList();

                if (data.Count() > 0)
                {
                    //Create Session
                    Session["Username"] = data.FirstOrDefault().username.ToString();
                    Session["Email"] = data.FirstOrDefault().email.ToString();
                    Session["Id"] = data.FirstOrDefault().id.ToString();
                    Session["Role"] = data.FirstOrDefault().role.ToString();

                    //Session Creator
                    if (data.FirstOrDefault().researcherOwner != null)
                    {
                        Session["Owner"] = data.FirstOrDefault().researcherOwner.ToString();
                    }
                    if (data.FirstOrDefault().innovatorOwner != null)
                    {
                        Session["Owner"] = data.FirstOrDefault().innovatorOwner.ToString();
                    }
                    if (data.FirstOrDefault().researcherOwner == null && data.FirstOrDefault().innovatorOwner == null)
                    {
                        Session["Owner"] = "ผู้ดูแลระบบ";
                    }

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    return RedirectToAction("LoginFailed");
                }

            }
            return View();
        }

        //Login Failed
        public ActionResult LoginFailed()
        {
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