using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_RIMS.Controllers
{
    public class InnovationController : Controller
    {
        // GET: Innovation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllInnovation()
        {
            return View();
        }

        public ActionResult CreateInnovation()
        {
            return View();
        }
    }
}