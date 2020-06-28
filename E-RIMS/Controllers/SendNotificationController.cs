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
    public class SendNotificationController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();

        // GET: SendNotification
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendNotification()
        {
            var research = db.Research;

            int currentMonth = Int16.Parse(DateTime.Now.Month.ToString());
            int currentYear = Int16.Parse(DateTime.Now.Year.ToString());

            int thaiYear = currentYear + 543;
            int fiscalYear = 0;

            if (currentMonth > 7)
                fiscalYear = thaiYear + 1;
            else
                fiscalYear = thaiYear;

            var researchResult = db.Research.SqlQuery("").ToList<Research>();

            foreach (Research researchDetail in researchResult){

                var researchActivity1 = researchDetail.activity1.ToString();
                var researchActivity2 = researchDetail.activity2.ToString();
                var researchActivity3 = researchDetail.activity3.ToString();
                var researchActivity4 = researchDetail.activity4.ToString();
                var researchActivity5 = researchDetail.activity5.ToString();
                var researchActivity6 = researchDetail.activity6.ToString();
                var researchActivity7 = researchDetail.activity7.ToString();
                var researchActivity8 = researchDetail.activity8.ToString();
                var researchActivity9 = researchDetail.activity9.ToString();
                var researchActivity1o = researchDetail.activity10.ToString();

            }
            
            return View();
        }

        public int getColumnMonth(string month)
        {
            if (month.IndexOf("isJan") != -1)
            {
                return 1;
            }
            else if (month.IndexOf("isFeb") != -1)
            {
                return 2;
            }
            else if (month.IndexOf("isMar") != -1)
            {
                return 3;
            }
            else if (month.IndexOf("isApr") != -1)
            {
                return 4;
            }
            else if (month.IndexOf("isMay") != -1)
            {
                return 5;
            }
            else if (month.IndexOf("isJun") != -1)
            {
                return 6;
            }
            else if (month.IndexOf("isJul") != -1)
            {
                return 7;
            }
            else if (month.IndexOf("isAug") != -1)
            {
                return 8;
            }
            else if (month.IndexOf("isSep") != -1)
            {
                return 9;
            }
            else if (month.IndexOf("isOct") != -1)
            {
                return 10;
            }
            else if (month.IndexOf("isNov") != -1)
            {
                return 11;
            }
            else if (month.IndexOf("isDec") != -1)
            {
                return 12;
            }
            else
                return 0;
        }

        public bool checkFirstMonthActivity(bool first, bool second)
        {
            if (second == true && first == false)
                return true;
            else
                return false;
        }
    }
}