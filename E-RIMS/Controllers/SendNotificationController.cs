using E_RIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Web.Hosting;
using System.Net.Mail;

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
            int fiscalYear = 2563;
            int currentMonth = 11;

            /*int currentMonth = Int16.Parse(DateTime.Now.Month.ToString());
            int currentYear = Int16.Parse(DateTime.Now.Year.ToString());

            int thaiYear = currentYear + 543;
            int fiscalYear = 0;

            if (currentMonth > 7)
                fiscalYear = thaiYear + 1;
            else
                fiscalYear = thaiYear;*/

            List<DataRow> queryResult = query(currentMonth, fiscalYear);
            generateMail(queryResult, currentMonth);
            return View();
        }

        public string checkMonth(int month)
        {
            string result = "";

            switch (month)
            {
                case 1:
                    result = "isJan";
                    break;
                case 2:
                    result = "isFeb";
                    break;
                case 3:
                    result = "isMar";
                    break;
                case 4:
                    result = "isApr";
                    break;
                case 5:
                    result = "isMay";
                    break;
                case 6:
                    result = "isJun";
                    break;
                case 7:
                    result = "isJul";
                    break;
                case 8:
                    result = "isAug";
                    break;
                case 9:
                    result = "isSep";
                    break;
                case 10:
                    result = "isOct";
                    break;
                case 11:
                    result = "isNov";
                    break;
                case 12:
                    result = "isDec";
                    break;
            }
            return result;
        }

        public List<DataRow> query(int currentMonth, int fiscalYear)
        {
            //System.Diagnostics.Debug.WriteLine(checkMonth(currentMonth));
            //System.Diagnostics.Debug.WriteLine(checkMonth(currentMonth - 1));

            string monthFixedQuery = "((res.thisMonth1 = '1' and res.beforeMonth1 = '0') or " +
                "(res.thisMonth2 = '1' and res.beforeMonth2 = '0') " +
                "or (res.thisMonth3 = '1' and res.beforeMonth3 = '0') " +
                "or (res.thisMonth4 = '1' and res.beforeMonth4 = '0') or (res.thisMonth5 = '1' and res.beforeMonth5 = '0') " +
                "or (res.thisMonth6 = '1' and res.beforeMonth6 = '0') " +
                "or (res.thisMonth7 = '1' and res.beforeMonth7 = '0') or (res.thisMonth8 = '1' and res.beforeMonth8 = '0') or " +
                "(res.thisMonth9 = '1' and res.beforeMonth9 = '0') " +
                "or (res.thisMonth10 = '1' and res.beforeMonth10 = '0'))";

            string monthQuery = "";
            monthQuery = monthFixedQuery.Replace("thisMonth", checkMonth(currentMonth));
            monthQuery = monthQuery.Replace("beforeMonth", checkMonth(currentMonth - 1));

            string connectionString = @"Data Source=BCJ-RyzenPC;Initial Catalog=ERIMS;User ID=sa;Password=jingjang";
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();

            string sql = "SELECT res.id, res.date, res.type, res.name researchname, res.workGroup, res.creator, " +
                        "res.activity1, res.isJan1, res.isFeb1, res.isMar1, res.isApr1, res.isMay1, res.isJun1, res.isJul1, res.isAug1, res.isSep1, res.isOct1, res.isNov1, res.isDec1, " +
                        "res.activity2, res.isJan2, res.isFeb2, res.isMar2, res.isApr2, res.isMay2, res.isJun2, res.isJul2, res.isAug2, res.isSep2, res.isOct2, res.isNov2, res.isDec2, " +
                        "res.activity3, res.isJan3, res.isFeb3, res.isMar3, res.isApr3, res.isMay3, res.isJun3, res.isJul3, res.isAug3, res.isSep3, res.isOct3, res.isNov3, res.isDec3, " +
                        "res.activity4, res.isJan4, res.isFeb4, res.isMar4, res.isApr4, res.isMay4, res.isJun4, res.isJul4, res.isAug4, res.isSep4, res.isOct4, res.isNov4, res.isDec4, " +
                        "res.activity5, res.isJan5, res.isFeb5, res.isMar5, res.isApr5, res.isMay5, res.isJun5, res.isJul5, res.isAug5, res.isSep5, res.isOct5, res.isNov5, res.isDec5, " +
                        "res.activity6, res.isJan6, res.isFeb6, res.isMar6, res.isApr6, res.isMay6, res.isJun6, res.isJul6, res.isAug6, res.isSep6, res.isOct6, res.isNov6, res.isDec6, " +
                        "res.activity7, res.isJan7, res.isFeb7, res.isMar7, res.isApr7, res.isMay7, res.isJun7, res.isJul7, res.isAug7, res.isSep7, res.isOct7, res.isNov7, res.isDec7, " +
                        "res.activity8, res.isJan8, res.isFeb8, res.isMar8, res.isApr8, res.isMay8, res.isJun8, res.isJul8, res.isAug8, res.isSep8, res.isOct8, res.isNov8, res.isDec8, " +
                        "res.activity9, res.isJan9, res.isFeb9, res.isMar9, res.isApr9, res.isMay9, res.isJun9, res.isJul9, res.isAug9, res.isSep9, res.isOct9, res.isNov9, res.isDec9, " +
                        "res.activity10, res.isJan10, res.isFeb10, res.isMar10, res.isApr10, res.isMay10, res.isJun10, res.isJul10, res.isAug10, res.isSep10, res.isOct10, res.isNov10, res.isDec10, " +
                        "res.budgetYear, res.idOwner, " +
                        "researcher.nameTitle, researcher.name researchername, researcher.surname, researcher.Email " +
                        "FROM ERIMS.dbo.Research res " +
                        "INNER JOIN ERIMS.dbo.Researcher researcher ON res.idOwner = researcher.id " +
                        "WHERE " + monthQuery + " " +
                        "AND res.budgetYear = '" + fiscalYear + "'";

            var cmd = new SqlCommand(sql, cnn);

            SqlDataReader rdr = cmd.ExecuteReader();
            System.Diagnostics.Debug.Write(sql);
            System.Diagnostics.Debug.WriteLine(rdr);

            var dt = new DataTable();
            dt.Load(rdr);
            List<DataRow> dr = dt.AsEnumerable().ToList();
            cnn.Close();

            return dr;
        }

        public void generateMail(List<DataRow> queryResult, int currentMonth)
        {
            string monthString = checkMonth(currentMonth);

            foreach (var row in queryResult)
            {
                System.Diagnostics.Debug.WriteLine(row["type"]);
                System.Diagnostics.Debug.WriteLine(row["researchname"]);
                System.Diagnostics.Debug.WriteLine(row["nameTitle"]);
                System.Diagnostics.Debug.WriteLine(row["researchername"]);
                System.Diagnostics.Debug.WriteLine(row["surname"]);
                System.Diagnostics.Debug.WriteLine(row["workGroup"]);
                System.Diagnostics.Debug.WriteLine(row["Email"]);

                string activityName = "";

                if (bool.Parse(row[monthString + "1"].ToString()))
                {
                    activityName = row["activity1"].ToString();
                }
                else if (bool.Parse(row[monthString + "2"].ToString()))
                {
                    activityName = row["activity2"].ToString();
                }
                else if (bool.Parse(row[monthString + "3"].ToString()))
                {
                    activityName = row["activity3"].ToString();
                }
                else if (bool.Parse(row[monthString + "4"].ToString()))
                {
                    activityName = row["activity4"].ToString();
                }
                else if (bool.Parse(row[monthString + "5"].ToString()))
                {
                    activityName = row["activity5"].ToString();
                }
                else if (bool.Parse(row[monthString + "6"].ToString()))
                {
                    activityName = row["activity6"].ToString();
                }
                else if (bool.Parse(row[monthString + "7"].ToString()))
                {
                    activityName = row["activity7"].ToString();
                }
                else if (bool.Parse(row[monthString + "8"].ToString()))
                {
                    activityName = row["activity8"].ToString();
                }
                else if (bool.Parse(row[monthString + "9"].ToString()))
                {
                    activityName = row["activity9"].ToString();
                }
                else if (bool.Parse(row[monthString + "10"].ToString()))
                {
                    activityName = row["activity10"].ToString();
                }

                System.Diagnostics.Debug.WriteLine(activityName);
                sendMail(row["type"].ToString(), row["researchname"].ToString(), row["nameTitle"].ToString(), row["researchername"].ToString(), row["surname"].ToString(), row["workGroup"].ToString(), activityName, row["Email"].ToString());

            }
        }

        private void sendMail(string type, string researchName, string title, string researcherName, string surname, string workGroup, string activityName, string email)
        {
            using (StreamReader sr = new StreamReader(VirtualPathProvider.OpenFile("/Content/mailtemplate.html")))
            {
                string content = sr.ReadToEnd();
                content = content.Replace("{title}", title);
                content = content.Replace("{name}", researcherName);
                content = content.Replace("{surname}", surname);
                content = content.Replace("{workgroup}", workGroup);
                content = content.Replace("{type}", type);
                content = content.Replace("{researchname}", researchName);
                content = content.Replace("{activityname}", activityName);
                System.Diagnostics.Debug.WriteLine(content);

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                MailAddress from = new MailAddress("academicdpc06@gmail.com", "ERIMS ODPC6 Chonburi");
                MailAddress to = new MailAddress(email);

                MailMessage mail = new MailMessage(from, to);
                mail.To.Add("boonchoo.ji@gmail.com");
                mail.Subject = "[ERIMS] แจ้งเตือนเข้าสู่กิจกรรมในโครงการ" + researchName ;
                mail.Body = content;
                mail.IsBodyHtml = true;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("academicdpc06@gmail.com", "aca12345");

                //SmtpServer.Credentials = new System.Net.NetworkCredential("choojai777@gmail.com", "clegiedgppxsaulw");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                System.Diagnostics.Debug.WriteLine("Mail Sent");

            }
        }
    }
}