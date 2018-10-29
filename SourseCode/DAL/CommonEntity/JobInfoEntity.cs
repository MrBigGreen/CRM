using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.CommonEntity
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-07-14
    /// 描述说明：职位列表实体
    /// </summary>
    public class JobInfoEntity : BaseEntity
    {

        public int CompanyInterviewJobID { get; set; }

        public int CompanyID { get; set; }

        public string CustomerID { get; set; }

        public string WorkAddress { get; set; }
        public string JobTitle { get; set; }
        public string JobTypeCode { get; set; }
        public string JobTypeText { get; set; }
        public string WorkExperenceCode { get; set; }
        public string WorkExperenceText { get; set; }
        public string JobCheckStatusCode { get; set; }
        public string JobCheckStatusText { get; set; }
        public string CompanyName { get; set; }
        public int ReceiveQty { get; set; }
        public int RecommendQty { get; set; }
        public string CustomerLevel { get; set; }
        public string CustomerLevelText { get; set; }
        public string CustomerBelongingID { get; set; }
        public string CustomerBelongingName { get; set; }



    }
}
