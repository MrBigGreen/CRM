using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2016-03-25
    /// 描述说明： 
    /// </summary>
    [MetadataType(typeof(TP_SalaryDetailsMetadata))]//使用TP_SalaryDetailsMetadata对TP_SalaryDetails进行数据验证

    public partial class TP_SalaryDetails : BaseEntity
    {
        #region 自定义属性，即由数据实体扩展的实体
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public short Gender { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string IDType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string IDCard { get; set; }


        /// <summary>
        /// 银行类型
        /// </summary>
        public string BankType { get; set; }

        /// <summary>
        /// 开户城市
        /// </summary>
        public string BankCity { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankID { get; set; }
        /// <summary>
        /// 联系人部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        #endregion

    }

    public class TP_SalaryDetailsMetadata
    {
    }
}
