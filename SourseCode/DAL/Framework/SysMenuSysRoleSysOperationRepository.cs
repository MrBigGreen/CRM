using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Data;
namespace CRM.DAL
{
    /// <summary>
    /// 角色菜单操作
    /// </summary>
    public partial class SysMenuSysRoleSysOperationRepository  
    {
       
        /// <summary>
        /// 根据SysMenuId，获取所有角色菜单操作数据
        /// </summary>
        /// <param name="id">外键的主键</param>
        /// <returns></returns>
        public IQueryable<SysOperation> GetByRefSysMenuIdAndSysRoleId(DB_CRMEntities db, string id, List<string> sysRoleIds)
        {
            //兼容oracle
            return
                  ( from o in db.SysOperation
                   from m in db.SysMenuSysRoleSysOperation.Where(c => sysRoleIds.Any(a => a == c.SysRoleId) && c.SysMenuId == id && c.SysOperationId != null).Select(s => s.SysOperationId)
                   where o.Id == m                    
                   select o).Distinct().OrderBy(o=>o.Sort).AsQueryable()
                     ;

        }
        public void Dispose()
        {
        }
    }
}

