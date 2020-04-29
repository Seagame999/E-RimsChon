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

    public partial class Innovation
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string workGroup { get; set; }
        public string creator { get; set; }
        public string backgroudAndImportance { get; set; }
        public string objective { get; set; }
        public string targetGroup { get; set; }
        public string benefit { get; set; }
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
    }
}
