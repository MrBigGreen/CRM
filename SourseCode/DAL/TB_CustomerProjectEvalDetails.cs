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
    
    public partial class TB_CustomerProjectEvalDetails
    {
    	
    	  public TB_CustomerProjectEvalDetails()
        {
    
    			//------------------------------------------------------------------------------
                // <auto-generated>
                //     此代码已从模板生成。  create by chand 2016-04-13
                //
                //     手动更改此文件可能导致应用程序出现意外的行为。
                //     如果重新生成代码，将覆盖对此文件的手动更改。
                // </auto-generated>
                //------------------------------------------------------------------------------
    			this.PrjEavlDetailsID = String.Empty;
    				this.CustomerProjectID = String.Empty;
    				this.EvaluationDesc = String.Empty;
    				this.EvaluationResult = String.Empty;
    				this.EvaluationPerson = String.Empty;
    				this.EvaluationTime = DateTime.Now;
    				this.IsDelete =false;
    				this.VersionNo =0;
    				this.CreatedTime = DateTime.Now;
    				this.CreatedBy = String.Empty;
    				this.LastUpdatedTime = DateTime.Now;
    				this.LastUpdatedBy = String.Empty;
    		
    
    
    	}
    	    public string PrjEavlDetailsID { get; set; }
        public string CustomerProjectID { get; set; }
        public string EvaluationDesc { get; set; }
        public string EvaluationResult { get; set; }
        public string EvaluationPerson { get; set; }
        public Nullable<System.DateTime> EvaluationTime { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> VersionNo { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
