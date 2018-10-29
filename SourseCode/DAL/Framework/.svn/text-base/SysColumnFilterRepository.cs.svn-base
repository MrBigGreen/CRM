using Common;
using CRM.DAL.CommonEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.Framework
{
    public partial class SysColumnFilterRepository
    {
        /// <summary>
        /// 根据SysMenuId，获取所有角色菜单操作数据
        /// </summary>
        /// <param name="id">外键的主键</param>
        /// <returns></returns>
        public List<CustomColumn> GetCustomColumns(DB_CRMEntities db, string SysMenuId, string SysPersonID)
        {
            //兼容oracle
            //return (from s in db.SysColumnFilter where s.SysMenuID == SysMenuId && s.SysPersonID == SysPersonID  && s.IsDelete == false orderby s.IsShow descending, s.Sort select s).AsQueryable();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SysMenuId", SysMenuId);
            param[1] = new SqlParameter("@SysPersonID", SysPersonID);

            var data = db.Database.SqlQuery<CustomColumn>("exec [dbo].[P_SysColumnFilter_S] @SysMenuID,@SysPersonID ", param).ToList();


            return data;
        }

        /// <summary>
        /// 根据SysMenuId，获取所有角色菜单操作数据
        /// </summary>
        /// <param name="id">外键的主键</param>
        /// <returns></returns>
        public IQueryable<SysColumnFilter> GetConditionFields(DB_CRMEntities db, string SysMenuId)
        {
            //兼容oracle
            return (from s in db.SysColumnFilter where s.SysMenuID == SysMenuId && s.IsDelete == false && s.IsCondition == true && s.SysPersonID=="0" orderby s.Sort select s).AsQueryable();

        }



        public void Dispose()
        {
        }
    }
}
