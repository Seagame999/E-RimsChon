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
    public class InnovatorsController : Controller
    {
        ERIMSEntities db = new ERIMSEntities();
        
        // GET: Innovators
        public ActionResult Index(int? page)
        {
            var innovator = db.Innovator;

            //--Pagination 6 cards
            var innovatorResult = innovator.ToList().ToPagedList(page ?? 1, 6);

            return View(innovatorResult);
        }

        public ActionResult InnovatorList(int? page)
        {
            var innovator = db.Innovator;

            //--Pagination 6 cards
            var innovatorResult = innovator.ToList().ToPagedList(page ?? 1, 6);

            return View(innovatorResult);
        }

        public ActionResult CreateInnovator()
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

        [HttpPost]
        public ActionResult CreateInnovator(Innovator innovator)
        {
            if (ModelState.IsValid)
            {
                if(innovator.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(innovator.image2.FileName);
                    string extension = Path.GetExtension(innovator.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovator.image = "/ImageInnovator/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageInnovator/"), fileName);
                    innovator.image2.SaveAs(fileName);
                }

                db.Innovator.Add(innovator);
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

            return View(innovator);
        }

        public ActionResult DetailInnovator(int id)
        {
            Innovator innovator = db.Innovator.Find(id);
            if(innovator == null)
            {
                return RedirectToAction("Index");
            }
            return View(innovator);
        }

        public ActionResult EditInnovator(int id)
        {
            Innovator innovator = db.Innovator.Find(id);
            if (innovator == null)
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

            return View(innovator);
        }

        [HttpPost]
        public ActionResult EditInnovator(Innovator innovator)
        {
            if (ModelState.IsValid)
            {
                if(innovator.image2 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(innovator.image2.FileName);
                    string extension = Path.GetExtension(innovator.image2.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + extension;
                    innovator.image = "/ImageInnovator/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ImageInnovator/"), fileName);
                    innovator.image2.SaveAs(fileName);
                }

                db.Entry(innovator).State = EntityState.Modified;
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

            return View(innovator);
        }

        public ActionResult DeleteInnovator(int id)
        {
            Innovator innovator = db.Innovator.Find(id);
            if (innovator == null)
            {
                return HttpNotFound();
            }
            return View(innovator);
        }

        [HttpPost, ActionName("DeleteInnovator")]
        public ActionResult DeleteInnovatorConfirm(int id)
        {
            Innovator innovator = db.Innovator.Find(id);
            db.Innovator.Remove(innovator);
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