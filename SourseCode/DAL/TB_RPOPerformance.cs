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
    
    public partial class TB_RPOPerformance
    {
    	
    	  public TB_RPOPerformance()
        {
    
    			//------------------------------------------------------------------------------
                // <auto-generated>
                //     此代码已从模板生成。  create by chand 2016-04-13
                //
                //     手动更改此文件可能导致应用程序出现意外的行为。
                //     如果重新生成代码，将覆盖对此文件的手动更改。
                // </auto-generated>
                //------------------------------------------------------------------------------
    			this.RPOPerformanceID = String.Empty;
    				this.CustomerProjectID = String.Empty;
    				this.SysPersonID = String.Empty;
    				this.DownLoadQty =0;
    				this.ContactPersonQty =0;
    				this.AppInterviewQty =0;
    				this.InterviewQty =0;
    				this.OfferQty =0;
    				this.OnBoardQty =0;
    				this.OverProbationQty =0;
    				this.IsEnd =false;
    				this.IsDelete =false;
    				this.VersionNo =0;
    				this.CreatedBy = String.Empty;
    				this.CreatedTime = DateTime.Now;
    				this.LastUpdatedBy = String.Empty;
    				this.LastUpdatedTime = DateTime.Now;
    		
    
    
    	}
    	    public string RPOPerformanceID { get; set; }
        public string CustomerProjectID { get; set; }
        public string SysPersonID { get; set; }
        public System.DateTime CurrentDate { get; set; }
        public int DownLoadQty { get; set; }
        public int ContactPersonQty { get; set; }
        public int AppInterviewQty { get; set; }
        public int InterviewQty { get; set; }
        public int OfferQty { get; set; }
        public int OnBoardQty { get; set; }
        public int OverProbationQty { get; set; }
        public bool IsEnd { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<int> VersionNo { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedTime { get; set; }
    }
}