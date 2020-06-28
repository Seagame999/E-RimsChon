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
    public class ResearcherController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        // GET: Researcher
        public ActionResult Index(int? page)
        {
            var researcher = db.Researcher;

            //--Pagination 6 cards
            var researcherResult = researcher.ToList().ToPagedList(page ?? 1, 6);
            return View(researcherResult);
        }

        public ActionResult ListResearcher(int? page)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    var researcher = db.Researcher;

                    //--Pagination 6 cards
                    var researcherResult = researcher.ToList().ToPagedList(page ?? 1, 6);
                    return View(researcherResult);
                }
                return RedirectToAction("Index", "Researcher");
            }
            else
                return RedirectToAction("Index", "Researcher");          
        }

        public ActionResult CreateResearcher()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
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
                return RedirectToAction("Index", "Researcher");
            }
            else
                return RedirectToAction("Index", "Researcher");            
        }

        [HttpPost]
        public ActionResult CreateResearcher(Researcher researcher)
        {
            if (ModelState.IsValid)
            {
                if (researcher.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(researcher.image2.FileName);
                    string extension = Path.GetExtension(researcher.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    researcher.image = "/ImageResearcher/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageResearcher/"), fileName);
                    researcher.image2.SaveAs(fileName);

                }

                db.Researcher.Add(researcher);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("CreateSuccessMessage");
            }

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

            return View(researcher);
        }

        public ActionResult DetailResearcher(int id)
        {
            Researcher researcher = db.Researcher.Find(id);
            if (researcher == null)
            {
                return RedirectToAction("Index");
            }
            return View(researcher);
        }
        
        public ActionResult EditResearcher(int id)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    Researcher researcher = db.Researcher.Find(id);
                    if (researcher == null)
                    {
                        return RedirectToAction("Index");
                    }

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

                    return View(researcher);
                }
                return RedirectToAction("Index", "Researcher");
            }
            else
                return RedirectToAction("Index", "Researcher");            
        }

        [HttpPost]
        public ActionResult EditResearcher(Researcher researcher)
        {
            if (ModelState.IsValid)
            {
                if (researcher.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(researcher.image2.FileName);
                    string extension = Path.GetExtension(researcher.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    researcher.image = "/ImageResearcher/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageResearcher/"), fileName);
                    researcher.image2.SaveAs(fileName);
                }

                db.Entry(researcher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditSuccessMessage");
            }

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

            return View(researcher);
        }

        public ActionResult DeleteResearcher(int id)
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    Researcher researcher = db.Researcher.Find(id);
                    if (researcher == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(researcher);
                }
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");           
        }

        [HttpPost, ActionName("DeleteResearcher")]
        public ActionResult DeleteResearcherConfirm(int id)
        {
            Researcher researcher = db.Researcher.Find(id);
            db.Researcher.Remove(researcher);
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