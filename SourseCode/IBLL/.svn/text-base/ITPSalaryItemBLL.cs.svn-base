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
    ///  描述：薪资明细    接口
    /// </summary>
    public interface ITPSalaryItemBLL : IBaseDAL<TP_SalaryItem>
    {
        List<SalaryItemEntity> GetSalaryItemData(int page, int rows, string search, ref int total);

        List<TP_SalaryItem> GetSalaryItemByItemClass(int itemClass);
    }
}
