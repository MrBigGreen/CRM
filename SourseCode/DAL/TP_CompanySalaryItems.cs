//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRM.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TP_CompanySalaryItems
    {
        public string CompanyID { get; set; }
        public string ItemCode { get; set; }
        public string ItemNewName { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> VersionNo { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedTime { get; set; }
    }
}
