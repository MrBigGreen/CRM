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
    
    public partial class TB_Address
    {
    	
    	  public TB_Address()
        {
    
    			//------------------------------------------------------------------------------
                // <auto-generated>
                //     此代码已从模板生成。  create by chand 2016-04-13
                //
                //     手动更改此文件可能导致应用程序出现意外的行为。
                //     如果重新生成代码，将覆盖对此文件的手动更改。
                // </auto-generated>
                //------------------------------------------------------------------------------
    			this.AddressID = String.Empty;
    				this.ProvinceCode = String.Empty;
    				this.CityCode = String.Empty;
    				this.DistrictCode = String.Empty;
    				this.AddressDetails = String.Empty;
    				this.VersionNo =0;
    				this.TransactionID = String.Empty;
    				this.CreatedTime = DateTime.Now;
    				this.CreatedBy = String.Empty;
    				this.LastUpdatedTime = DateTime.Now;
    				this.LastUpdatedBy = String.Empty;
    				this.Lng =0;
    				this.Lat =0;
    		
    
    
    	}
    	    public string AddressID { get; set; }
        public string ProvinceCode { get; set; }
        public string CityCode { get; set; }
        public string DistrictCode { get; set; }
        public string AddressDetails { get; set; }
        public Nullable<int> VersionNo { get; set; }
        public string TransactionID { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<decimal> Lng { get; set; }
        public Nullable<decimal> Lat { get; set; }
    }
}
