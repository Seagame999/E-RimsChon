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
    public class HomeController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();

        public ActionResult Index()
        {
            var publicRelation = db.PublicRelation;
            return View(publicRelation.ToList());
        }

       


    }
}