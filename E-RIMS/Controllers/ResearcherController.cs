using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_RIMS.Controllers
{
    public class ResearcherController : Controller
    {
        // GET: Researcher
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetailResearcher()
        {
            return View();
        }

        public ActionResult CreateResearcher()
        {
            return View();
        }
    }
}