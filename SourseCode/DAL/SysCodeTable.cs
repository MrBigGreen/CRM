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
    
    public partial class SysCodeTable
    {
    	
    	  public SysCodeTable()
        {
    
    			//------------------------------------------------------------------------------
                // <auto-generated>
                //     此代码已从模板生成。  create by chand 2016-04-13
                //
                //     手动更改此文件可能导致应用程序出现意外的行为。
                //     如果重新生成代码，将覆盖对此文件的手动更改。
                // </auto-generated>
                //------------------------------------------------------------------------------
    			this.CodeID = String.Empty;
    				this.Code = String.Empty;
    				this.CodeCategory = String.Empty;
    				this.CodeValue = String.Empty;
    				this.CodeSeq =0;
    				this.VersionNo =0;
    				this.TransactionID = String.Empty;
    				this.CreatedBy = String.Empty;
    				this.LastUpdatedBy = String.Empty;
    		
    
    
    	}
    	    public string CodeID { get; set; }
        public string Code { get; set; }
        public string CodeCategory { get; set; }
        public string CodeValue { get; set; }
        public System.DateTime EffectDateFrom { get; set; }
        public System.DateTime EffectDateEnd { get; set; }
        public Nullable<int> CodeSeq { get; set; }
        public int VersionNo { get; set; }
        public string TransactionID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public System.DateTime LastUpdatedTime { get; set; }
    }
}