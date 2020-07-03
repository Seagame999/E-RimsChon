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
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var member = db.Member;
                    return View(member.ToList());
                }
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateRegister()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var modelResearcher = db.Researcher.OrderBy(x => x.name).ToList();
                    ViewBag.ResearcherView = (from item in modelResearcher
                                              select new SelectListItem
                                              {
                                                  Text = item.name + " " + item.surname,
                                                  Value = item.name.ToString() + " " + item.surname.ToString()
                                              });

                    var modelInnovator = db.Innovator.OrderBy(x => x.name).ToList();
                    ViewBag.InnovatorView = (from item in modelInnovator
                                             select new SelectListItem
                                             {
                                                 Text = item.name + " " + item.surname,
                                                 Value = item.name.ToString() + " " + item.surname.ToString()
                                             });

                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CreateRegister(Member member)
        {
            if (ModelState.IsValid)
            {
                //member.password = GetMD5(member.password);
                member.role = "User";
                db.Member.Add(member);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("CreateSuccessMessage");
            }

            var modelResearcher = db.Researcher.OrderBy(x => x.name).ToList();
            ViewBag.ResearcherView = (from item in modelResearcher
                                      select new SelectListItem
                                      {
                                          Text = item.name + " " + item.surname,
                                          Value = item.name.ToString() + " " + item.surname.ToString()
                                      });

            var modelInnovator = db.Innovator.OrderBy(x => x.name).ToList();
            ViewBag.InnovatorView = (from item in modelInnovator
                                     select new SelectListItem
                                     {
                                         Text = item.name + " " + item.surname,
                                         Value = item.name.ToString() + " " + item.surname.ToString()
                                     });

            return View(member);
        }

        public ActionResult DetailRegister(int id)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    Member member = db.Member.Find(id);
                    if (member == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(member);
                }
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        //Re-New Password if forget password.
        public ActionResult EditRegister(int id)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    Member member = db.Member.Find(id);
                    if (member == null)
                    {
                        return RedirectToAction("Index");
                    }

                    var modelResearcher = db.Researcher.OrderBy(x => x.name).ToList();
                    ViewBag.ResearcherView = (from item in modelResearcher
                                              select new SelectListItem
                                              {
                                                  Text = item.name + " " + item.surname,
                                                  Value = item.name.ToString() + " " + item.surname.ToString()
                                              });

                    var modelInnovator = db.Innovator.OrderBy(x => x.name).ToList();
                    ViewBag.InnovatorView = (from item in modelInnovator
                                             select new SelectListItem
                                             {
                                                 Text = item.name + " " + item.surname,
                                                 Value = item.name.ToString() + " " + item.surname.ToString()
                                             });

                    return View(member);
                }
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditRegister(Member member)
        {
            if (ModelState.IsValid)
            {
                //member.password = GetMD5(member.password);
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditSuccessMessage");
            }

            var modelResearcher = db.Researcher.OrderBy(x => x.name).ToList();
            ViewBag.ResearcherView = (from item in modelResearcher
                                      select new SelectListItem
                                      {
                                          Text = item.name + " " + item.surname,
                                          Value = item.name.ToString() + " " + item.surname.ToString()
                                      });

            var modelInnovator = db.Innovator.OrderBy(x => x.name).ToList();
            ViewBag.InnovatorView = (from item in modelInnovator
                                     select new SelectListItem
                                     {
                                         Text = item.name + " " + item.surname,
                                         Value = item.name.ToString() + " " + item.surname.ToString()
                                     });

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