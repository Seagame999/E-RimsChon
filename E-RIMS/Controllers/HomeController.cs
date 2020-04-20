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
    public class HomeController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();

        public ActionResult Index(int? page)
        {
            var publicRelation = db.PublicRelation;

            //--Pagination 6 cards
            var publicRelationResult = publicRelation.ToList().ToPagedList(page ?? 1, 6);

            return View(publicRelationResult);
        }

       


    }
}