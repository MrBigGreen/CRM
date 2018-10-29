using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    /// <summary>
    /// Name:跟进任务实体
    /// Author：Jonny
    /// Date:2015-6-11
    public class ContractModel
    {
        public string ContractID { get; set; }
        public string CustomerID { get; set; }
        public string ContractMoney { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public System.DateTime Deadline { get; set; }
        public string Package { get; set; }
        public string Annex { get; set; }
        public string Years { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public string CustomerName { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }

        public string VocationCode { get; set; }

        public string SysPersonID { get; set; }

        public string PackageName { get; set; }

        public Nullable<int> IsNew { get; set; }
        public string IsNewName { get; set; }

        public string PayType { get; set; }

        public string CreatedByName { get; set; }

        public string Remark { get; set; }
    }



    /// <summary>
    /// 
    /// </summary>
    public class ContractEntity
    {
        public string ContractID { get; set; }
        public string ContractNO { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ContractMoney { get; set; }
        public string RecommendSolutionName { get; set; }
        public string SysPersonName { get; set; }
        public System.DateTime EffectiveDate { get; set; }
        public System.DateTime Deadline { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedTime { get; set; }

        /// <summary>
        /// 签约公司编号
        /// </summary>
        public string SigningCompanyID { get; set; }

        /// <summary>
        /// 签约公司
        /// </summary>
        public string SigningCompanyName { get; set; }

        public string PhoneNumber { get; set; }
        public string ProjectLeader { get; set; }
        public string oldContractNO { get; set; }

        public string DepartmentName { get; set; }

        public string ContractName { get; set; }

        public Nullable<int> IsNew { get; set; }
        public string IsNewName { get; set; }

        public string PayType { get; set; }

        public string CreatedByName { get; set; }

        public string Remark { get; set; }
    }
}
