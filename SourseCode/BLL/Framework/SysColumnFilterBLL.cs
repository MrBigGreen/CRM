using Common;
using CRM.DAL;
using CRM.DAL.Framework;
using CRM.IBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRM.BLL.Framework
{
    /// <summary>
    /// 类名：数据列表的列操作
    /// 创建人：chand
    /// 创建时间：2016-04-10
    /// </summary>
    public partial class SysColumnFilterBLL : ISysColumnFilterBLL
    {

        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 菜单角色操作的数据库访问对象
        /// </summary>
        SysColumnFilterRepository repository = new SysColumnFilterRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public SysColumnFilterBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public SysColumnFilterBLL(DB_CRMEntities entities)
        {
            db = entities;
        }
        /// <summary>
        /// 根据SysMenuIdId，获取所有角色菜单操作数据
        /// </summary>
        /// <param name="id">外键的主键</param>
        /// <returns></returns>
        public List<CustomColumn> GetCustomColumns(string SysMenuId, string SysPersonID)
        {
            //获取本人自定义的列
            return repository.GetCustomColumns(db, SysMenuId, SysPersonID);
        }


        #region 创建默认定制菜单

        public bool CreateDefault(ref ValidationErrors validationErrors, string SysMenuID, string SysPersonID)
        {


            #region 首次创建定制菜单
            try
            {

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    int flag = 0;
                    string sql = string.Format("DELETE SysColumnFilter WHERE SysMenuID='{0}' AND SysPersonID='{1}'; INSERT INTO SysColumnFilter(SysMenuID,SysPersonID,ColumnName,ColumnText,ColumnWidth,IsShow,Sort,IsCondition,IsDelete,VersionNo,CreatedBy,CreatedTime,LastUpdatedBy,LastUpdatedTime) SELECT SysMenuID,'{1}' AS SysPersonID,ColumnName,ColumnText,ColumnWidth,IsShow,Sort,IsCondition,IsDelete,VersionNo,'{2}' AS CreatedBy,GETDATE(), '{2}' AS  LastUpdatedBy,GETDATE() FROM SysColumnFilter 	 WHERE SysMenuID='{0}' AND SysPersonID='0' AND IsDelete=0", SysMenuID, SysPersonID, SysPersonID);
                    flag = db.Database.ExecuteSqlCommand(sql);


                    if (flag > 0)
                    {
                        transactionScope.Complete();
                        return true;
                    }
                    else
                    {
                        Transaction.Current.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            #endregion


            return false;
        }


        #endregion

        /// <summary>
        /// 修改定制列
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eidt(ref ValidationErrors validationErrors, SysColumnFilter entity)
        {

            var q = db.SysColumnFilter.Where(s => s.SysMenuID == entity.SysMenuID && s.SysPersonID == entity.SysPersonID && s.ColumnName == entity.ColumnName).SingleOrDefault();
            if (q == null)
            {
                //首次创建定制菜单
                if (!CreateDefault(ref validationErrors, entity.SysMenuID, entity.SysPersonID))
                {

                    return false;
                }

            }

            #region 修改菜单属性
            try
            {

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    int flag = 0;
                    string sql = string.Format("UPDATE SysColumnFilter SET ColumnWidth='{0}',IsShow='{1}',Sort='{2}',LastUpdatedBy='{3}',LastUpdatedTime=GETDATE()   WHERE SysMenuID='{4}' AND SysPersonID='{5}' AND ColumnName='{6}'",
                        entity.ColumnWidth, (entity.IsShow ? 1 : 0), entity.Sort, entity.CreatedBy, entity.SysMenuID, entity.SysPersonID, entity.ColumnName);
                    flag = db.Database.ExecuteSqlCommand(sql);
                    if (flag == 1)
                    {
                        transactionScope.Complete();
                        return true;
                    }
                    else
                    {
                        Transaction.Current.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }

            #endregion


            return false;
        }

        /// <summary>
        /// 修改定制列
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Eidt(ref ValidationErrors validationErrors, List<SysColumnFilter> list, string SysPersonID)
        {
            if (list == null || list.Count < 1)
            {
                validationErrors.Add("修改定制列不能为空");
                return false;
            }
            string ColumnName = list[0].ColumnName;
            string SysMenuID = list[0].SysMenuID;
            var q = db.SysColumnFilter.Where(s => s.SysMenuID == SysMenuID && s.SysPersonID == SysPersonID && s.ColumnName == ColumnName).FirstOrDefault();
            if (q == null)
            {
                //首次创建定制菜单
                if (!CreateDefault(ref validationErrors, SysMenuID, SysPersonID))
                {

                    return false;
                }

            }

            #region 修改菜单属性
            try
            {

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    int flag = 0;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < list.Count; i++)
                    {
                        sb.AppendFormat("UPDATE SysColumnFilter SET ColumnWidth='{0}',IsShow='{1}',Sort='{2}',LastUpdatedBy='{3}',LastUpdatedTime=GETDATE()   WHERE SysMenuID='{4}' AND SysPersonID='{5}' AND ColumnName='{6}'",
                        list[i].ColumnWidth, (list[i].IsShow ? 1 : 0), list[i].Sort, list[i].CreatedBy, list[i].SysMenuID, SysPersonID, list[i].ColumnName);

                    }

                    flag = db.Database.ExecuteSqlCommand(sb.ToString());
                    if (flag == list.Count)
                    {
                        transactionScope.Complete();
                        return true;
                    }
                    else
                    {
                        Transaction.Current.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }

            #endregion


            return false;
        }

        /// <summary>
        /// 根据SysMenuId，获取查询字段
        /// </summary>
        /// <param name="SysMenuId"></param>
        /// <returns></returns>
        public List<SysColumnFilter> GetConditionFields(string SysMenuId)
        {
            return repository.GetConditionFields(db, SysMenuId).ToList();
        }

        #region 创建默认定制菜单

        public bool SetCustomColumn(ref ValidationErrors validationErrors, string SysMenuID, string SysPersonID, List<string> columns)
        {

            if (string.IsNullOrWhiteSpace(SysMenuID))
            {

                validationErrors.Add("菜单信息有误");
                return false;
            }
            if (string.IsNullOrWhiteSpace(SysPersonID))
            {

                validationErrors.Add("人员信息有误");
                return false;
            }

            if (columns.Count < 1)
            {
                validationErrors.Add("自定义列不能为空！");
                return false;
            }
            StringBuilder sb = new StringBuilder();


            #region 首次创建定制菜单
            try
            {

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    int flag = 0;
                    string sql = string.Format("DELETE SysColumnFilter WHERE SysMenuID='{0}' AND SysPersonID='{1}'; ", SysMenuID, SysPersonID);
                    flag = db.Database.ExecuteSqlCommand(sql);
                    for (int i = 0; i < columns.Count; i++)
                    {
                        if (columns[i].Length < 1)
                        {
                            continue;
                        }
                        sb.Append(" INSERT INTO SysColumnFilter(SysMenuID,SysPersonID,ColumnName,ColumnText,ColumnWidth,IsShow,Sort,IsCondition,IsDelete,VersionNo,CreatedBy,CreatedTime,LastUpdatedBy,LastUpdatedTime) ");
                        sb.AppendFormat("SELECT SysMenuID,'{1}' AS SysPersonID,ColumnName,ColumnText,ColumnWidth,IsShow,{3},IsCondition,IsDelete,VersionNo,'{1}' AS CreatedBy,GETDATE(), '{1}' AS  LastUpdatedBy,GETDATE()  FROM SysColumnFilter  WHERE SysMenuID='{0}' AND SysPersonID='0' AND IsDelete=0 AND ColumnName='{2}'",

                            SysMenuID, SysPersonID, columns[i], i);
                    }


                    flag = db.Database.ExecuteSqlCommand(sb.ToString());

                    if (flag == columns.Count)
                    {
                        transactionScope.Complete();
                        return true;
                    }
                    else
                    {
                        Transaction.Current.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            #endregion


            return false;
        }


        #endregion

    }
}
