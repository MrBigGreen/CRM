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
    
    public partial class TB_CustomerContact
    {
    	
    	  public TB_CustomerContact()
        {
    
    			//------------------------------------------------------------------------------
                // <auto-generated>
                //     此代码已从模板生成。  create by chand 2016-04-13
                //
                //     手动更改此文件可能导致应用程序出现意外的行为。
                //     如果重新生成代码，将覆盖对此文件的手动更改。
                // </auto-generated>
                //------------------------------------------------------------------------------
    			this.CustomerContactID = String.Empty;
    				this.CustomerID = String.Empty;
    				this.ContactName = String.Empty;
    				this.Department = String.Empty;
    				this.Post = String.Empty;
    				this.CompanyTel = String.Empty;
    				this.PhoneNumber1 = String.Empty;
    				this.PhoneNumber2 = String.Empty;
    				this.Email1 = String.Empty;
    				this.Email2 = String.Empty;
    				this.QQID = String.Empty;
    				this.BirthDate = String.Empty;
    				this.Remark = String.Empty;
    				this.VersionNo =0;
    				this.IsDelete =false;
    				this.CreatedTime = DateTime.Now;
    				this.CreatedBy = String.Empty;
    				this.LastUpdatedTime = DateTime.Now;
    				this.LastUpdatedBy = String.Empty;
    				this.CompanyTel2 = String.Empty;
    				this.Interested = String.Empty;
    				this.IsKP =false;
    		
    
    
    	}
    	    public string CustomerContactID { get; set; }
        public string CustomerID { get; set; }
        public string ContactName { get; set; }
        public string Department { get; set; }
        public string Post { get; set; }
        public string CompanyTel { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string QQID { get; set; }
        public string BirthDate { get; set; }
        public string Remark { get; set; }
        public Nullable<int> VersionNo { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public string CompanyTel2 { get; set; }
        public string Interested { get; set; }
        public bool IsKP { get; set; }
    
        public virtual TB_Customer TB_Customer { get; set; }
    }
}
