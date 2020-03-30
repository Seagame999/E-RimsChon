using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_RIMS.Controllers
{
    public class AcademicPaperController : Controller
    {
        // GET: AcademicPaper
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllAcademicPaper()
        {
            return View();
        }

        public ActionResult CreateAcademicPaper()
        {
            return View();
        }
    }
}