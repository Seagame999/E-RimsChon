using E_RIMS.FileValidation;
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_RIMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class PublicRelation
    {
        public int id { get; set; }
        public string newsName { get; set; }
        public string image { get; set; }
        [Required(ErrorMessage ="��س�����ٻ�Ҿ��Сͺ����")]
        [FileExtensionsValidationImage]
        public HttpPostedFileBase image2 { get; set; }
        public string description { get; set; }
        public string htmlLink { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> views { get; set; }
        [FileExtensionsValidationDoc]
        public HttpPostedFileBase docUpload2 { get; set; }
        public string docUpload { get; set; }
    }
}