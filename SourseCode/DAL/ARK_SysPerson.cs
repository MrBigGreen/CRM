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
    
    public partial class ARK_SysPerson
    {
    	
    	  public ARK_SysPerson()
        {
    
    			//------------------------------------------------------------------------------
                // <auto-generated>
                //     此代码已从模板生成。  create by chand 2016-04-13
                //
                //     手动更改此文件可能导致应用程序出现意外的行为。
                //     如果重新生成代码，将覆盖对此文件的手动更改。
                // </auto-generated>
                //------------------------------------------------------------------------------
    			this.Id = String.Empty;
    				this.Name = String.Empty;
    				this.MyName = String.Empty;
    				this.Password = String.Empty;
    				this.SurePassword = String.Empty;
    				this.Sex = String.Empty;
    				this.SysDepartmentId = String.Empty;
    				this.Position = String.Empty;
    				this.MobilePhoneNumber = String.Empty;
    				this.CompanyQQ = String.Empty;
    				this.Province = String.Empty;
    				this.City = String.Empty;
    				this.Village = String.Empty;
    				this.EnglishName = String.Empty;
    				this.EmailAddress = String.Empty;
    				this.Remark = String.Empty;
    				this.State = String.Empty;
    				this.CreateTime = DateTime.Now;
    				this.CreatePerson = String.Empty;
    				this.UpdateTime = DateTime.Now;
    				this.LogonNum =0;
    				this.LogonTime = DateTime.Now;
    				this.LogonIP = String.Empty;
    				this.LastLogonTime = DateTime.Now;
    				this.LastLogonIP = String.Empty;
    				this.PageStyle = String.Empty;
    				this.UpdatePerson = String.Empty;
    				this.HDpic = String.Empty;
    		
    
    
    	}
    	    public string Id { get; set; }
        public string Name { get; set; }
        public string MyName { get; set; }
        public string Password { get; set; }
        public string SurePassword { get; set; }
        public string Sex { get; set; }
        public string SysDepartmentId { get; set; }
        public string Position { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string CompanyQQ { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Village { get; set; }
        public string EnglishName { get; set; }
        public string EmailAddress { get; set; }
        public string Remark { get; set; }
        public string State { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreatePerson { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> LogonNum { get; set; }
        public Nullable<System.DateTime> LogonTime { get; set; }
        public string LogonIP { get; set; }
        public Nullable<System.DateTime> LastLogonTime { get; set; }
        public string LastLogonIP { get; set; }
        public string PageStyle { get; set; }
        public string UpdatePerson { get; set; }
        public byte[] Version { get; set; }
        public string HDpic { get; set; }
    }
}