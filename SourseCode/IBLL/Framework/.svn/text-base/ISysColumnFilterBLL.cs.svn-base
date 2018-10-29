using Common;
using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IBLL.Framework
{
    /// <summary>
    /// 接口名：
    /// 创建人：chand
    /// 创建日期：2016-04-10
    /// </summary>
    public interface ISysColumnFilterBLL
    {
        List<CustomColumn> GetCustomColumns(string SysMenuId, string SysPersonID);

        bool Eidt(ref ValidationErrors validationErrors, SysColumnFilter entity);

        bool Eidt(ref ValidationErrors validationErrors, List<SysColumnFilter> list, string SysPersonID);

        bool CreateDefault(ref ValidationErrors validationErrors, string SysMenuID, string SysPersonID);


        List<SysColumnFilter> GetConditionFields(string SysMenuId);


        bool SetCustomColumn(ref ValidationErrors validationErrors, string SysMenuID, string SysPersonID, List<string> columns);
    }
}
