using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.CommonEntity
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-08-19
    /// 描述说明：职位列表实体
    /// </summary>
    public class PersonEntity
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public string SysPersonID { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string SysPersonName { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        public string SysDepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string SysDepartmentName { get; set; }



    }
}
