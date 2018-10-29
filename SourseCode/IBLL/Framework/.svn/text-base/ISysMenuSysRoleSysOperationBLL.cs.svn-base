using System;
using System.Collections.Generic;
using System.Linq;

using Common;
using CRM.DAL;
using System.ServiceModel;

namespace CRM.IBLL
{
    /// <summary>
    /// 角色菜单操作 接口
    /// </summary>
 
    public partial interface ISysMenuSysRoleSysOperationBLL
    {
        /// <summary>
        /// 根据SysMenuIdId，获取所有角色菜单操作数据
        /// </summary>
        /// <param name="id">外键的主键</param>
        /// <returns></returns>
        List<SysOperation> GetByRefSysMenuIdAndSysRoleId(string id, List<string> sysRoleIds);
        
    }
}

