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
    public class ExpertController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: Expert

        public ActionResult Index(int? page)
        {
            var expert = db.Expert;

            //--Pagination 6 cards
            var expertResult = expert.ToList().ToPagedList(page ?? 1, 6);

            return View(expertResult);
        }

        public ActionResult ListExpert(int? page)
        {

            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var expert = db.Expert;

                    //--Pagination 6 cards
                    var expertResult = expert.ToList().ToPagedList(page ?? 1, 6);

                    return View(expertResult);
                }
                return RedirectToAction("Index", "Expert");
            }
            else
                return RedirectToAction("Index", "Expert");
            
        }

        public ActionResult CreateExpert()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var modelNameTitle = db.NameTitle.ToList();
                    ViewBag.nameTitleView = (from item in modelNameTitle
                                             select new SelectListItem
                                             {
                                                 Text = item.nameTitle1,
                                                 Value = item.nameTitle1.ToString()
                                             });

                    var modelPosition = db.Position.ToList();
                    ViewBag.PositionView = (from item in modelPosition
                                            select new SelectListItem
                                            {
                                                Text = item.position1,
                                                Value = item.position1.ToString()
                                            });

                    var modelLevel = db.Level.ToList();
                    ViewBag.LevelView = (from item in modelLevel
                                         select new SelectListItem
                                         {
                                             Text = item.levels,
                                             Value = item.levels.ToString()
                                         });

                    var proviceView = new List<string>()
            { "กรุงเทพมหานคร","กระบี่","กาญจนบุรี","กาฬสินธุ์","กำแพงเพชร","ขอนแก่น","จันทบุรี","ฉะเชิงเทรา","ชลบุรี","ชัยนาท","ชัยภูมิ",
            "ชุมพร","เชียงราย","เชียงใหม่","ตรัง","ตราด","ตาก","นครนายก","นครปฐม","นครพนม","นครราชสีมา","นครศรีธรรมราช",
            "นครสวรรค์","นนทบุรี","นราธิวาส","น่าน","บุรีรัมย์","ปทุมธานี","ประจวบคีรีขันธ์","ปราจีนบุรี","ปัตตานี","พระนครศรีอยุธยา","พะเยา",
            "พังงา","พัทลุง","พิจิตร","พิษณุโลก","เพชรบุรี","เพชรบูรณ์","แพร่","ภูเก็ต","มหาสารคาม","มุกดาหาร","แม่ฮ่องสอน",
            "ยโสธร","ยะลา","ร้อยเอ็ด","ระนอง","ระยอง","ราชบุรี","ลพบุรี","ลำปาง","ลำพูน","เลย","ศรีสะเกษ",
            "สกลนคร","สงขลา","สตูล","สมุทรปราการ","สมุทรสงคราม","สมุทรสาคร","สระแก้ว","สระบุรี","สิงห์บุรี","สุโขทัย","สุพรรณบุรี",
            "สุราษฎร์ธานี","สุรินทร์","หนองคาย","หนองบัวลำภู","อ่างทอง","อำนาจเจริญ","อุดรธานี","อุตรดิตถ์","อุทัยธานี","อุบลราชธานี","บึงกาฬ"};
                    ViewBag.proviceView = proviceView;

                    return View();
                }
                return RedirectToAction("Index", "Expert");
            }
            else
                return RedirectToAction("Index", "Expert");

           
        }

        [HttpPost]
        public ActionResult CreateExpert(Expert expert)
        {
            if (ModelState.IsValid)
            {
                if(expert.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(expert.image2.FileName);
                    string extension = Path.GetExtension(expert.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    expert.image = "/ImageExpert/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageExpert/"), fileName);
                    expert.image2.SaveAs(fileName);
                }

                db.Expert.Add(expert);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("CreateSuccessMessage");
            }

            var modelNameTitle = db.NameTitle.ToList();
            ViewBag.nameTitleView = (from item in modelNameTitle
                                     select new SelectListItem
                                     {
                                         Text = item.nameTitle1,
                                         Value = item.nameTitle1.ToString()
                                     });

            var modelPosition = db.Position.ToList();
            ViewBag.PositionView = (from item in modelPosition
                                    select new SelectListItem
                                    {
                                        Text = item.position1,
                                        Value = item.position1.ToString()
                                    });

            var modelLevel = db.Level.ToList();
            ViewBag.LevelView = (from item in modelLevel
                                 select new SelectListItem
                                 {
                                     Text = item.levels,
                                     Value = item.levels.ToString()
                                 });

            var proviceView = new List<string>()
            { "กรุงเทพมหานคร","กระบี่","กาญจนบุรี","กาฬสินธุ์","กำแพงเพชร","ขอนแก่น","จันทบุรี","ฉะเชิงเทรา","ชลบุรี","ชัยนาท","ชัยภูมิ",
            "ชุมพร","เชียงราย","เชียงใหม่","ตรัง","ตราด","ตาก","นครนายก","นครปฐม","นครพนม","นครราชสีมา","นครศรีธรรมราช",
            "นครสวรรค์","นนทบุรี","นราธิวาส","น่าน","บุรีรัมย์","ปทุมธานี","ประจวบคีรีขันธ์","ปราจีนบุรี","ปัตตานี","พระนครศรีอยุธยา","พะเยา",
            "พังงา","พัทลุง","พิจิตร","พิษณุโลก","เพชรบุรี","เพชรบูรณ์","แพร่","ภูเก็ต","มหาสารคาม","มุกดาหาร","แม่ฮ่องสอน",
            "ยโสธร","ยะลา","ร้อยเอ็ด","ระนอง","ระยอง","ราชบุรี","ลพบุรี","ลำปาง","ลำพูน","เลย","ศรีสะเกษ",
            "สกลนคร","สงขลา","สตูล","สมุทรปราการ","สมุทรสงคราม","สมุทรสาคร","สระแก้ว","สระบุรี","สิงห์บุรี","สุโขทัย","สุพรรณบุรี",
            "สุราษฎร์ธานี","สุรินทร์","หนองคาย","หนองบัวลำภู","อ่างทอง","อำนาจเจริญ","อุดรธานี","อุตรดิตถ์","อุทัยธานี","อุบลราชธานี","บึงกาฬ"};
            ViewBag.proviceView = proviceView;

            return View(expert);
        }

        public ActionResult ExpertDetails(int id)
        {
            Expert expert = db.Expert.Find(id);
            if(expert == null)
            {
                return RedirectToAction("Index");
            }

            if(expert.IsSpecialExpertInfection == true)
            {
                var infection = Convert.ToString(expert.IsSpecialExpertInfection);
                infection = "โรคติดต่อ";
                ViewBag.infection = infection;
            }
            else
            {
                ViewBag.infection = "*";
            }
            if (expert.IsSpecialExpertNonInfection == true)
            {
                var nonInfection = Convert.ToString(expert.IsSpecialExpertNonInfection);
                nonInfection = "โรคไม่ติดต่อ";
                ViewBag.nonInfection = nonInfection;
            }
            else
            {
                ViewBag.nonInfection = "*";
            }
            if (expert.IsSpecialExpertDiseaseExposure == true)
            {
                var diseaseExposure = Convert.ToString(expert.IsSpecialExpertDiseaseExposure);
                diseaseExposure = "โรคจากการสัมผัส";
                ViewBag.diseaseExposure = diseaseExposure;
            }
            else
            {
                ViewBag.diseaseExposure = "*";
            }
            if (expert.IsSpecialExpertOccupationAndEnvironDiseases == true)
            {
                var occupationAndEnvironDiseases = Convert.ToString(expert.IsSpecialExpertOccupationAndEnvironDiseases);
                occupationAndEnvironDiseases = "โรคจากการประกอบอาชีพและสิ่งแวดล้อม";
                ViewBag.occupationAndEnvironDiseases = occupationAndEnvironDiseases;
            }
            else
            {
                ViewBag.occupationAndEnvironDiseases = "*";
            }
            if (expert.IsSpecialExpertEpidemiology == true)
            {
                var epidemiology = Convert.ToString(expert.IsSpecialExpertEpidemiology);
                epidemiology = "ระบาดวิทยา";
                ViewBag.epidemiology = epidemiology;
            }
            else
            {
                ViewBag.epidemiology = "*";
            }
            if (expert.IsSpecialExpertLaboratory == true)
            {
                var expertLaboratory = Convert.ToString(expert.IsSpecialExpertLaboratory);
                expertLaboratory = "ห้องปฎิบัติการ";
                ViewBag.expertLaboratory = expertLaboratory;
            }
            else
            {
                ViewBag.expertLaboratory = "*";
            }
            if (expert.IsSpecialExpertEvaluate == true)
            {
                var evaluate = Convert.ToString(expert.IsSpecialExpertEvaluate);
                evaluate = "ประเมินผล";
                ViewBag.evaluate = evaluate;
            }
            else
            {
                ViewBag.evaluate = "*";
            }
            if (expert.IsSpecialExpertStatistics == true)
            {
                var statistics = Convert.ToString(expert.IsSpecialExpertStatistics);
                statistics = "สถิติ";
                ViewBag.statistics = statistics;
            }
            else
            {
                ViewBag.statistics = "*";
            }
            if (expert.IsSpecialExpertManage == true)
            {
                var manage = Convert.ToString(expert.IsSpecialExpertManage);
                manage = "บริหารจัดการ";
                ViewBag.manage = manage;
            }
            else
            {
                ViewBag.manage = "*";
            }
            if (expert.IsSpecialExpertInnovation == true)
            {
                var innovation = Convert.ToString(expert.IsSpecialExpertInnovation);
                innovation = "นวัตกรรม";
                ViewBag.innovation = innovation;
            }
            else
            {
                ViewBag.innovation = "*";
            }

            return View(expert);
        }

        public ActionResult EditExpert(int id)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    Expert expert = db.Expert.Find(id);
                    if (expert == null)
                    {
                        return RedirectToAction("Index");
                    }

                    var modelNameTitle = db.NameTitle.ToList();
                    ViewBag.nameTitleView = (from item in modelNameTitle
                                             select new SelectListItem
                                             {
                                                 Text = item.nameTitle1,
                                                 Value = item.nameTitle1.ToString()
                                             });

                    var modelPosition = db.Position.ToList();
                    ViewBag.PositionView = (from item in modelPosition
                                            select new SelectListItem
                                            {
                                                Text = item.position1,
                                                Value = item.position1.ToString()
                                            });

                    var modelLevel = db.Level.ToList();
                    ViewBag.LevelView = (from item in modelLevel
                                         select new SelectListItem
                                         {
                                             Text = item.levels,
                                             Value = item.levels.ToString()
                                         });

                    var proviceView = new List<string>()
            { "กรุงเทพมหานคร","กระบี่","กาญจนบุรี","กาฬสินธุ์","กำแพงเพชร","ขอนแก่น","จันทบุรี","ฉะเชิงเทรา","ชลบุรี","ชัยนาท","ชัยภูมิ",
            "ชุมพร","เชียงราย","เชียงใหม่","ตรัง","ตราด","ตาก","นครนายก","นครปฐม","นครพนม","นครราชสีมา","นครศรีธรรมราช",
            "นครสวรรค์","นนทบุรี","นราธิวาส","น่าน","บุรีรัมย์","ปทุมธานี","ประจวบคีรีขันธ์","ปราจีนบุรี","ปัตตานี","พระนครศรีอยุธยา","พะเยา",
            "พังงา","พัทลุง","พิจิตร","พิษณุโลก","เพชรบุรี","เพชรบูรณ์","แพร่","ภูเก็ต","มหาสารคาม","มุกดาหาร","แม่ฮ่องสอน",
            "ยโสธร","ยะลา","ร้อยเอ็ด","ระนอง","ระยอง","ราชบุรี","ลพบุรี","ลำปาง","ลำพูน","เลย","ศรีสะเกษ",
            "สกลนคร","สงขลา","สตูล","สมุทรปราการ","สมุทรสงคราม","สมุทรสาคร","สระแก้ว","สระบุรี","สิงห์บุรี","สุโขทัย","สุพรรณบุรี",
            "สุราษฎร์ธานี","สุรินทร์","หนองคาย","หนองบัวลำภู","อ่างทอง","อำนาจเจริญ","อุดรธานี","อุตรดิตถ์","อุทัยธานี","อุบลราชธานี","บึงกาฬ"};
                    ViewBag.proviceView = proviceView;

                    return View(expert);
                }
                return RedirectToAction("Index", "Expert");
            }
            else
                return RedirectToAction("Index", "Expert");            
        }

        [HttpPost]
        public ActionResult EditExpert(Expert expert)
        {
            if (ModelState.IsValid)
            {
                if (expert.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(expert.image2.FileName);
                    string extension = Path.GetExtension(expert.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    expert.image = "/ImageExpert/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageExpert/"), fileName);
                    expert.image2.SaveAs(fileName);
                }

                db.Entry(expert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditSuccessMessage");
            }

            var modelNameTitle = db.NameTitle.ToList();
            ViewBag.nameTitleView = (from item in modelNameTitle
                                     select new SelectListItem
                                     {
                                         Text = item.nameTitle1,
                                         Value = item.nameTitle1.ToString()
                                     });

            var modelPosition = db.Position.ToList();
            ViewBag.PositionView = (from item in modelPosition
                                    select new SelectListItem
                                    {
                                        Text = item.position1,
                                        Value = item.position1.ToString()
                                    });

            var modelLevel = db.Level.ToList();
            ViewBag.LevelView = (from item in modelLevel
                                 select new SelectListItem
                                 {
                                     Text = item.levels,
                                     Value = item.levels.ToString()
                                 });

            var proviceView = new List<string>()
            { "กรุงเทพมหานคร","กระบี่","กาญจนบุรี","กาฬสินธุ์","กำแพงเพชร","ขอนแก่น","จันทบุรี","ฉะเชิงเทรา","ชลบุรี","ชัยนาท","ชัยภูมิ",
            "ชุมพร","เชียงราย","เชียงใหม่","ตรัง","ตราด","ตาก","นครนายก","นครปฐม","นครพนม","นครราชสีมา","นครศรีธรรมราช",
            "นครสวรรค์","นนทบุรี","นราธิวาส","น่าน","บุรีรัมย์","ปทุมธานี","ประจวบคีรีขันธ์","ปราจีนบุรี","ปัตตานี","พระนครศรีอยุธยา","พะเยา",
            "พังงา","พัทลุง","พิจิตร","พิษณุโลก","เพชรบุรี","เพชรบูรณ์","แพร่","ภูเก็ต","มหาสารคาม","มุกดาหาร","แม่ฮ่องสอน",
            "ยโสธร","ยะลา","ร้อยเอ็ด","ระนอง","ระยอง","ราชบุรี","ลพบุรี","ลำปาง","ลำพูน","เลย","ศรีสะเกษ",
            "สกลนคร","สงขลา","สตูล","สมุทรปราการ","สมุทรสงคราม","สมุทรสาคร","สระแก้ว","สระบุรี","สิงห์บุรี","สุโขทัย","สุพรรณบุรี",
            "สุราษฎร์ธานี","สุรินทร์","หนองคาย","หนองบัวลำภู","อ่างทอง","อำนาจเจริญ","อุดรธานี","อุตรดิตถ์","อุทัยธานี","อุบลราชธานี","บึงกาฬ"};
            ViewBag.proviceView = proviceView;


            return View(expert);
            
        }

        public ActionResult DeleteExpert(int id)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    Expert expert = db.Expert.Find(id);
                    if (expert == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(expert);
                }
                return RedirectToAction("Index", "Expert");
            }
            else
                return RedirectToAction("Index", "Expert");           
        }
        
        [HttpPost,ActionName("DeleteExpert")]
        public ActionResult DeleteExpertConfirm(int id)
        {
            Expert expert = db.Expert.Find(id);
            db.Expert.Remove(expert);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateSuccessMessage()
        {
            return View();
        }

        public ActionResult EditSuccessMessage()
        {
            return View();
        }
    }
}