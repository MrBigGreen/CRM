using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using CRM.DAL;
using Common;

namespace CRM.BLL
{
    /// <summary>
    /// 角色菜单操作 
    /// </summary>
    public partial class SysMenuSysRoleSysOperationBLL 
    {
     
        /// <summary>
        /// 根据SysMenuIdId，获取所有角色菜单操作数据
        /// </summary>
        /// <param name="id">外键的主键</param>
        /// <returns></returns>
        public List<SysOperation> GetByRefSysMenuIdAndSysRoleId(string id, List<string> sysRoleIds)
        {
            return repository.GetByRefSysMenuIdAndSysRoleId(db, id,sysRoleIds).ToList();
        }
        
    }
}

