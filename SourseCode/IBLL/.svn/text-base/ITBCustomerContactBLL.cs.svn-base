using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IBLL
{
    /// <summary>
    ///  创建人：chand
    ///  创建时间：2015-06-12
    ///  描述：客户联系人   接口
    /// </summary>
    public interface ITBCustomerContactBLL : IBaseDAL<TB_CustomerContact>
    {


        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        List<CustomerContactModel> GetCustomerContactData(string id, string order, string sort, string search, int page, int rows, ref int total);

    }

}
