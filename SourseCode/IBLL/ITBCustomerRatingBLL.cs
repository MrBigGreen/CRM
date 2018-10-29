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
    ///  创建时间：2016-03-31
    ///  描述：接口
    /// </summary>
    public interface ITBCustomerRatingBLL : IBaseDAL<TB_CustomerRating>
    {
        List<CustomerRatingEntity> GetCustomerRatingData(int page, int rows, string search, ref int total);

        bool GetCalcRatingBefore(TB_Customer entity);

        bool GetCalcRatingBefore(List<string> ItemList, ref string RatingScore);
    }
}
