using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_RIMS.Controllers
{
    public class JournalController : Controller
    {
        // GET: Journal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllJournal()
        {
            return View();
        }

        public ActionResult CreateJournal()
        {
            return View();
        }
    }
}