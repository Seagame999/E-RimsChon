using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_RIMS.Controllers
{
    public class ResearchAndInnovationController : Controller
    {
        // GET: ResearchAndInnovation
        public ActionResult Index()
        {
            if (Session["Role"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }
    }
}