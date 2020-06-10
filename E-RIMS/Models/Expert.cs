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
    using System.Web.Mvc;

    public partial class Expert
    {
        public int id { get; set; }
        public string image { get; set; }
        [FileExtensionsValidationImage]
        public HttpPostedFileBase image2 { get; set; }
        public string nameTitle { get; set; }
        [Required(ErrorMessage = "��سҡ�͡���ͼ������Ǫҭ")]
        public string name { get; set; }
        [Required(ErrorMessage = "��سҡ�͡���ʡ�ż������Ǫҭ")]
        public string surname { get; set; }
        [Required(ErrorMessage = "��سҡ�͡�Ţ�ѵû�Шӵ�ǻ�ЪҪ�")]
        public string identificationNumber { get; set; }
        [Required(ErrorMessage = "��س����͡���˹�")]
        public string position { get; set; }
        [Required(ErrorMessage = "��س����͡�дѺ")]
        public string levels { get; set; }
        public string houseNumberHome { get; set; }
        public string villageNumberHome { get; set; }
        public string alleyHome { get; set; }
        public string roadHome { get; set; }
        public string subDistricHome { get; set; }
        public string districHome { get; set; }
        public string provinceHome { get; set; }
        public string postalHome { get; set; }
        [Required(ErrorMessage = "��سҡ�͡�������Ѿ����Ͷ��")]
        public string moblieNumber { get; set; }
        public string houseNumberOffice { get; set; }
        public string villageNumberOffice { get; set; }
        public string alleyOffice { get; set; }
        public string roadOffice { get; set; }
        public string subDistrictOffice { get; set; }
        public string districtOffice { get; set; }
        public string provinceOffice { get; set; }
        public string postalCodeOffice { get; set; }
        public string officePhoneNumber { get; set; }
        public string officeFax { get; set; }
        public string officeMobileNumber { get; set; }
        public string educationLevel { get; set; }
        public string graduationYear { get; set; }
        public string schoolName { get; set; }
        public string subjectQualification { get; set; }
        public string specialExpertise { get; set; }
        public Nullable<System.DateTime> dateWorkFrom { get; set; }
        public Nullable<System.DateTime> dateWorkTo { get; set; }
        public string workplace { get; set; }
        public string positionWork { get; set; }
        public string academicWorkYear { get; set; }
        public string joinDevelopmentTeam { get; set; }
        public string nameAcademic { get; set; }
        public string publication { get; set; }
        [Required(ErrorMessage = "��سҡ�͡������")]
        [EmailAddress(ErrorMessage = "�ٻẺ������Դ��Ҵ")]
        public string email { get; set; }
        public string educationLevel2 { get; set; }
        public string educationLevel3 { get; set; }
        public string workplace2 { get; set; }
        public string workplace3 { get; set; }
        public Nullable<bool> IsSpecialExpertInfection { get; set; }
        public Nullable<bool> IsSpecialExpertNonInfection { get; set; }
        public Nullable<bool> IsSpecialExpertDiseaseExposure { get; set; }
        public Nullable<bool> IsSpecialExpertOccupationAndEnvironDiseases { get; set; }
        public Nullable<bool> IsSpecialExpertEpidemiology { get; set; }
        public Nullable<bool> IsSpecialExpertLaboratory { get; set; }
        public Nullable<bool> IsSpecialExpertEvaluate { get; set; }
        public Nullable<bool> IsSpecialExpertStatistics { get; set; }
        public Nullable<bool> IsSpecialExpertManage { get; set; }
        public Nullable<bool> IsSpecialExpertInnovation { get; set; }
        public Nullable<bool> IsSpecialExpertEtc { get; set; }
    }
}