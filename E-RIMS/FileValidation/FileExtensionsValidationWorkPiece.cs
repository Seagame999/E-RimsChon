using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.IO;

namespace E_RIMS.FileValidation
{
    public class FileExtensionsValidationWorkPiece : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                HttpPostedFileWrapper file = (HttpPostedFileWrapper)value;
                string extention = Path.GetExtension(file.FileName);
                string txt1 = ".txt", txt2 = ".TXT";
                string doc1 = ".doc", doc2 = ".DOC", doc3 = ".docx", doc4 = ".DOCX";
                string pdf1 = ".pdf", pdf2 = ".PDF";
                string xls1 = ".xls", xls2 = ".XLS", xls3 = ".xlsx", xls4 = ".XLSX";
                string ppt1 = ".ppt", ppt2 = ".PPT", ppt3 = ".pptx", ppt4 = ".PPTX";
                string rar1 = ".rar", rar2 = ".RAR";
                string zip1 = ".zip", zip2 = ".zip2";


                if (string.Equals(extention, txt1) == true || string.Equals(extention, txt2) == true
                   || string.Equals(extention, doc1) == true || string.Equals(extention, doc2) == true || string.Equals(extention, doc3) == true || string.Equals(extention, doc4) == true ||
                   string.Equals(extention, pdf1) == true || string.Equals(extention, pdf2) == true ||
                   string.Equals(extention, xls1) == true || string.Equals(extention, xls2) == true || string.Equals(extention, xls3) == true || string.Equals(extention, xls4) == true ||
                   string.Equals(extention, ppt1) == true || string.Equals(extention, ppt2) == true || string.Equals(extention, ppt3) == true || string.Equals(extention, ppt4) == true ||
                   string.Equals(extention, rar1) == true || string.Equals(extention, rar2) == true || string.Equals(extention, zip1) == true || string.Equals(extention, zip2) == true
                   )
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("ขออภัยไฟล์ไม่สนับสนุน");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}