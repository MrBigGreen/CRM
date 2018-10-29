using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.CommonEntity
{
    /// <summary>
    /// 
    /// 创建人：chand<br/>
    /// 创建时间：2015-01-19<br/>
    /// 描述说明：自定义客户分享实体
    /// </summary>
    public class CustomerShareEntity
    {
        public long CustomerShareLogID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string SysPersonIDFrom { get; set; }
        public string SysPersonIDTo { get; set; }
        public string ShareRemark { get; set; }
        public DateTime LastUpdatedTime { get; set; }

    }
}
