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
    using E_RIMS.FileValidation;

    public partial class Research
    {
        public int id { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> date { get; set; }
        public string type { get; set; }
        [Required(ErrorMessage = "��سҡ�͡���ͧҹ")]
        public string name { get; set; }
        [Required(ErrorMessage = "��س����͡������ҹ")]
        public string workGroup { get; set; }
        public string creator { get; set; }
        public string preface { get; set; }
        public string objective { get; set; }
        public string benefit { get; set; }
        public string format { get; set; }
        public string school { get; set; }
        public string timeOfStudy { get; set; }
        public string population { get; set; }
        public string sampleMethod { get; set; }
        public string toolsOfResearch { get; set; }
        public string dataAnalysis { get; set; }
        public string dataMethodCollect { get; set; }
        public string files { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase files2 { get; set; }
        public string activity1 { get; set; }
        public Nullable<bool> isJan1 { get; set; }
        public Nullable<bool> isFeb1 { get; set; }
        public Nullable<bool> isMar1 { get; set; }
        public Nullable<bool> isApr1 { get; set; }
        public Nullable<bool> isMay1 { get; set; }
        public Nullable<bool> isJun1 { get; set; }
        public Nullable<bool> isJul1 { get; set; }
        public Nullable<bool> isAug1 { get; set; }
        public Nullable<bool> isSep1 { get; set; }
        public Nullable<bool> isOct1 { get; set; }
        public Nullable<bool> isNov1 { get; set; }
        public Nullable<bool> isDec1 { get; set; }
        public string activity2 { get; set; }
        public Nullable<bool> isJan2 { get; set; }
        public Nullable<bool> isFeb2 { get; set; }
        public Nullable<bool> isMar2 { get; set; }
        public Nullable<bool> isApr2 { get; set; }
        public Nullable<bool> isMay2 { get; set; }
        public Nullable<bool> isJun2 { get; set; }
        public Nullable<bool> isJul2 { get; set; }
        public Nullable<bool> isAug2 { get; set; }
        public Nullable<bool> isSep2 { get; set; }
        public Nullable<bool> isOct2 { get; set; }
        public Nullable<bool> isNov2 { get; set; }
        public Nullable<bool> isDec2 { get; set; }
        public string activity3 { get; set; }
        public Nullable<bool> isJan3 { get; set; }
        public Nullable<bool> isFeb3 { get; set; }
        public Nullable<bool> isMar3 { get; set; }
        public Nullable<bool> isApr3 { get; set; }
        public Nullable<bool> isMay3 { get; set; }
        public Nullable<bool> isJun3 { get; set; }
        public Nullable<bool> isJul3 { get; set; }
        public Nullable<bool> isAug3 { get; set; }
        public Nullable<bool> isSep3 { get; set; }
        public Nullable<bool> isOct3 { get; set; }
        public Nullable<bool> isNov3 { get; set; }
        public Nullable<bool> isDec3 { get; set; }
        public string activity4 { get; set; }
        public Nullable<bool> isJan4 { get; set; }
        public Nullable<bool> isFeb4 { get; set; }
        public Nullable<bool> isMar4 { get; set; }
        public Nullable<bool> isApr4 { get; set; }
        public Nullable<bool> isMay4 { get; set; }
        public Nullable<bool> isJun4 { get; set; }
        public Nullable<bool> isJul4 { get; set; }
        public Nullable<bool> isAug4 { get; set; }
        public Nullable<bool> isSep4 { get; set; }
        public Nullable<bool> isOct4 { get; set; }
        public Nullable<bool> isNov4 { get; set; }
        public Nullable<bool> isDec4 { get; set; }
        public string activity5 { get; set; }
        public Nullable<bool> isJan5 { get; set; }
        public Nullable<bool> isFeb5 { get; set; }
        public Nullable<bool> isMar5 { get; set; }
        public Nullable<bool> isApr5 { get; set; }
        public Nullable<bool> isMay5 { get; set; }
        public Nullable<bool> isJun5 { get; set; }
        public Nullable<bool> isJul5 { get; set; }
        public Nullable<bool> isAug5 { get; set; }
        public Nullable<bool> isSep5 { get; set; }
        public Nullable<bool> isOct5 { get; set; }
        public Nullable<bool> isNov5 { get; set; }
        public Nullable<bool> isDec5 { get; set; }
        public string activity6 { get; set; }
        public Nullable<bool> isJan6 { get; set; }
        public Nullable<bool> isFeb6 { get; set; }
        public Nullable<bool> isMar6 { get; set; }
        public Nullable<bool> isApr6 { get; set; }
        public Nullable<bool> isMay6 { get; set; }
        public Nullable<bool> isJun6 { get; set; }
        public Nullable<bool> isJul6 { get; set; }
        public Nullable<bool> isAug6 { get; set; }
        public Nullable<bool> isSep6 { get; set; }
        public Nullable<bool> isOct6 { get; set; }
        public Nullable<bool> isNov6 { get; set; }
        public Nullable<bool> isDec6 { get; set; }
        public string activity7 { get; set; }
        public Nullable<bool> isJan7 { get; set; }
        public Nullable<bool> isFeb7 { get; set; }
        public Nullable<bool> isMar7 { get; set; }
        public Nullable<bool> isApr7 { get; set; }
        public Nullable<bool> isMay7 { get; set; }
        public Nullable<bool> isJun7 { get; set; }
        public Nullable<bool> isJul7 { get; set; }
        public Nullable<bool> isAug7 { get; set; }
        public Nullable<bool> isSep7 { get; set; }
        public Nullable<bool> isOct7 { get; set; }
        public Nullable<bool> isNov7 { get; set; }
        public Nullable<bool> isDec7 { get; set; }
        public string activity8 { get; set; }
        public Nullable<bool> isJan8 { get; set; }
        public Nullable<bool> isFeb8 { get; set; }
        public Nullable<bool> isMar8 { get; set; }
        public Nullable<bool> isApr8 { get; set; }
        public Nullable<bool> isMay8 { get; set; }
        public Nullable<bool> isJun8 { get; set; }
        public Nullable<bool> isJul8 { get; set; }
        public Nullable<bool> isAug8 { get; set; }
        public Nullable<bool> isSep8 { get; set; }
        public Nullable<bool> isOct8 { get; set; }
        public Nullable<bool> isNov8 { get; set; }
        public Nullable<bool> isDec8 { get; set; }
        public string activity9 { get; set; }
        public Nullable<bool> isJan9 { get; set; }
        public Nullable<bool> isFeb9 { get; set; }
        public Nullable<bool> isMar9 { get; set; }
        public Nullable<bool> isApr9 { get; set; }
        public Nullable<bool> isMay9 { get; set; }
        public Nullable<bool> isJun9 { get; set; }
        public Nullable<bool> isJul9 { get; set; }
        public Nullable<bool> isAug9 { get; set; }
        public Nullable<bool> isSep9 { get; set; }
        public Nullable<bool> isOct9 { get; set; }
        public Nullable<bool> isNov9 { get; set; }
        public Nullable<bool> isDec9 { get; set; }
        public string activity10 { get; set; }
        public Nullable<bool> isJan10 { get; set; }
        public Nullable<bool> isFeb10 { get; set; }
        public Nullable<bool> isMar10 { get; set; }
        public Nullable<bool> isApr10 { get; set; }
        public Nullable<bool> isMay10 { get; set; }
        public Nullable<bool> isJun10 { get; set; }
        public Nullable<bool> isJul10 { get; set; }
        public Nullable<bool> isAug10 { get; set; }
        public Nullable<bool> isSep10 { get; set; }
        public Nullable<bool> isOct10 { get; set; }
        public Nullable<bool> isNov10 { get; set; }
        public Nullable<bool> isDec10 { get; set; }
        public string budgetYear { get; set; }
        public Nullable<int> idOwner { get; set; }
        public string usernameOwner { get; set; }
        public string statusActivity1 { get; set; }
        public string statusActivity2 { get; set; }
        public string statusActivity3 { get; set; }
        public string statusActivity4 { get; set; }
        public string statusActivity5 { get; set; }
        public string statusActivity6 { get; set; }
        public string statusActivity7 { get; set; }
        public string statusActivity8 { get; set; }
        public string statusActivity9 { get; set; }
        public string statusActivity10 { get; set; }
        public Nullable<decimal> workOverview { get; set; }
        public string planStatusActivity1 { get; set; }
        public string planStatusActivity2 { get; set; }
        public string planStatusActivity3 { get; set; }
        public string planStatusActivity4 { get; set; }
        public string planStatusActivity5 { get; set; }
        public string planStatusActivity6 { get; set; }
        public string planStatusActivity7 { get; set; }
        public string planStatusActivity8 { get; set; }
        public string planStatusActivity9 { get; set; }
        public string planStatusActivity10 { get; set; }
        public string filePlanStatusActivity1 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase filePlanStatusActivity1HttpPost { get; set; }
        public string filePlanStatusActivity2 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase filePlanStatusActivity2HttpPost { get; set; }
        public string filePlanStatusActivity3 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase filePlanStatusActivity3HttpPost { get; set; }
        public string filePlanStatusActivity4 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase filePlanStatusActivity4HttpPost { get; set; }
        public string filePlanStatusActivity5 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase filePlanStatusActivity5HttpPost { get; set; }
        public string filePlanStatusActivity6 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase filePlanStatusActivity6HttpPost { get; set; }
        public string filePlanStatusActivity7 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase filePlanStatusActivity7HttpPost { get; set; }
        public string filePlanStatusActivity8 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase filePlanStatusActivity8HttpPost { get; set; }
        public string filePlanStatusActivity9 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase filePlanStatusActivity9HttpPost { get; set; }
        public string filePlanStatusActivity10 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase filePlanStatusActivity10HttpPost { get; set; }
        public string proceedStatusActivity1 { get; set; }
        public string proceedStatusActivity2 { get; set; }
        public string proceedStatusActivity3 { get; set; }
        public string proceedStatusActivity4 { get; set; }
        public string proceedStatusActivity5 { get; set; }
        public string proceedStatusActivity6 { get; set; }
        public string proceedStatusActivity7 { get; set; }
        public string proceedStatusActivity8 { get; set; }
        public string proceedStatusActivity9 { get; set; }
        public string proceedStatusActivity10 { get; set; }
        public string fileProceedStatusActivity1 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileProceedStatusActivity1HttpPost { get; set; }
        public string fileProceedStatusActivity2 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileProceedStatusActivity2HttpPost { get; set; }
        public string fileProceedStatusActivity3 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileProceedStatusActivity3HttpPost { get; set; }
        public string fileProceedStatusActivity4 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileProceedStatusActivity4HttpPost { get; set; }
        public string fileProceedStatusActivity5 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileProceedStatusActivity5HttpPost { get; set; }
        public string fileProceedStatusActivity6 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileProceedStatusActivity6HttpPost { get; set; }
        public string fileProceedStatusActivity7 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileProceedStatusActivity7HttpPost { get; set; }
        public string fileProceedStatusActivity8 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileProceedStatusActivity8HttpPost { get; set; }
        public string fileProceedStatusActivity9 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileProceedStatusActivity9HttpPost { get; set; }
        public string fileProceedStatusActivity10 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileProceedStatusActivity10HttpPost { get; set; }
        public string finishStatusActivity1 { get; set; }
        public string finishStatusActivity2 { get; set; }
        public string finishStatusActivity3 { get; set; }
        public string finishStatusActivity4 { get; set; }
        public string finishStatusActivity5 { get; set; }
        public string finishStatusActivity6 { get; set; }
        public string finishStatusActivity7 { get; set; }
        public string finishStatusActivity8 { get; set; }
        public string finishStatusActivity9 { get; set; }
        public string finishStatusActivity10 { get; set; }
        public string fileFinishStatusActivity1 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileFinishStatusActivity1HttpPost { get; set; }
        public string fileFinishStatusActivity2 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileFinishStatusActivity2HttpPost { get; set; }
        public string fileFinishStatusActivity3 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileFinishStatusActivity3HttpPost { get; set; }
        public string fileFinishStatusActivity4 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileFinishStatusActivity4HttpPost { get; set; }
        public string fileFinishStatusActivity5 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileFinishStatusActivity5HttpPost { get; set; }
        public string fileFinishStatusActivity6 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileFinishStatusActivity6HttpPost { get; set; }
        public string fileFinishStatusActivity7 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileFinishStatusActivity7HttpPost { get; set; }
        public string fileFinishStatusActivity8 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileFinishStatusActivity8HttpPost { get; set; }
        public string fileFinishStatusActivity9 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileFinishStatusActivity9HttpPost { get; set; }
        public string fileFinishStatusActivity10 { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase fileFinishStatusActivity10HttpPost { get; set; }
        public Nullable<int> views { get; set; }
        public string finalFiles { get; set; }
        [FileExtensionsValidationWorkPiece]
        public HttpPostedFileBase finalFilesHttpPost { get; set; }
        public Nullable<bool> agreePublish { get; set; }

    }

}
