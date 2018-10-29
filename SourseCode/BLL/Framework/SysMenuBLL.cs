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
    /// 菜单 
    /// </summary>
    public partial class SysMenuBLL
    {  /// <summary>
        /// 创建一个菜单
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个菜单</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, SysMenu entity)
        {
            int count = 1;

            foreach (string item in entity.SysOperationId.GetIdSort())
            {
                SysOperation sys = new SysOperation { Id = item };
                db.SysOperation.Attach(sys);
                entity.SysOperation.Add(sys);
                count++;
            }

            repository.Create(db, entity);
            if (count == repository.Save(db))
            {
                //创建后重置菜单编码
                List<int> flags = new List<int>();//层级
                GetMenus2(null, flags);
                db.SaveChanges();
                return true;
            }
            else
            {
                validationErrors.Add("创建出错了");
            }
            return false;
        }
        /// <summary>
        /// 删除菜单集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">主键的菜单</param>
        /// <returns></returns>    
        public bool DeleteCollection(ref ValidationErrors validationErrors, string[] deleteCollection)
        {
            try
            {
                if (deleteCollection != null)
                {

                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        repository.Delete(db, deleteCollection);
                        if (deleteCollection.Length == repository.Save(db))
                        {
                            db.SaveChanges();
                            //要先提交事务，然后修改编码，否则IsLeaf字段得不到更新

                            //在1.2版本中修改 
                            //此方法由eastday(qq:76381028)提供  
                            //删除后重置菜单编码
                            List<int> flags = new List<int>();//层级
                            GetMenus2(null, flags);
                            db.SaveChanges();

                            transactionScope.Complete();
                            return true;
                        }
                        else
                        {
                            Transaction.Current.Rollback();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }
        /// <summary>
        /// 编辑一个菜单
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据上下文</param>
        /// <param name="entity">一个菜单</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, SysMenu entity)
        {  /*                       
                           * 不操作 原有 现有
                           * 增加   原没 现有
                           * 删除   原有 现没
                           */
            if (entity == null)
            {
                return false;
            }
            int count = 1;

            List<string> addSysOperationId = new List<string>();
            List<string> deleteSysOperationId = new List<string>();
            DataOfDiffrent.GetDiffrent(entity.SysOperationId.GetIdSort(), entity.SysOperationIdOld.GetIdSort(), ref addSysOperationId, ref deleteSysOperationId);
            List<SysOperation> listEntitySysOperation = new List<SysOperation>();
            if (deleteSysOperationId != null && deleteSysOperationId.Count() > 0)
            {
                foreach (var item in deleteSysOperationId)
                {
                    SysOperation sys = new SysOperation { Id = item };
                    listEntitySysOperation.Add(sys);
                    entity.SysOperation.Add(sys);
                }
            }

            SysMenu editEntity = repository.Edit(db, entity);


            if (addSysOperationId != null && addSysOperationId.Count() > 0)
            {
                foreach (var item in addSysOperationId)
                {
                    SysOperation sys = new SysOperation { Id = item };
                    db.SysOperation.Attach(sys);
                    editEntity.SysOperation.Add(sys);
                    count++;
                }
            }
            if (deleteSysOperationId != null && deleteSysOperationId.Count() > 0)
            {
                foreach (SysOperation item in listEntitySysOperation)
                {
                    editEntity.SysOperation.Remove(item);
                    count++;
                }
            }

            if (count == repository.Save(db))
            {
                //修改后重置菜单编码
                List<int> flags = new List<int>();//层级
                GetMenus2(null, flags);
                db.SaveChanges();
                return true;
            }
            else
            {
                validationErrors.Add("编辑菜单出错了");
            }
            return false;
        }
        //递归更新
        protected void GetMenus2(string menuId, List<int> flags)
        {
            IQueryable<SysMenu> listTree;
            if (menuId == null)
            {
                listTree = from f in db.SysMenu
                           where (f.ParentId) == null
                           orderby f.Sort
                           select f;
            }
            else
            {
                listTree = from f in db.SysMenu
                           where menuId == (f.ParentId)
                           orderby f.Sort
                           select f;
            }


            if (listTree != null && listTree.Any())
            {
                flags.Add(1000);
                foreach (SysMenu item in listTree)
                {
                    //修改编码
                    item.Remark = string.Join("", flags);
                 
                    if (item.SysMenu1.Any())
                    {
                        item.IsLeaf = "叶子";
                        //非子节点，递归
                        GetMenus2(item.Id, flags);
                        flags.RemoveAt(flags.Count - 1);
                    }
                    else
                    {
                        item.IsLeaf = null;
                    }
                    //值+1
                    flags[flags.Count - 1]++;
                }
            }
        }
         
    }
}

