using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_RIMS.Controllers
{
    public class DocumentDownloadController : Controller
    {
        // GET: DocumentDownload
        public ActionResult Index()
        {
            return View();
        }

        // GET: DocumentDownload
        public ActionResult CreateDownloadDocument()
        {
            return View();
        }
    }
}