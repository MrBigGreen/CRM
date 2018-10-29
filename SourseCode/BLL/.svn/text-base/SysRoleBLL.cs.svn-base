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
    /// 角色 
    /// </summary>
    public partial class SysRoleBLL : IBLL.ISysRoleBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 角色的数据库访问对象
        /// </summary>
        SysRoleRepository repository = new SysRoleRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public SysRoleBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public SysRoleBLL(DB_CRMEntities entities)
        {
            db = entities;
        }
        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        public List<SysRole> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {


            IQueryable<SysRole> queryData = repository.DaoChuData(db, order, sort, search);
            total = queryData.Count();
            if (total > 0)
            {
                if (page <= 1)
                {
                    queryData = queryData.Take(rows);
                }
                else
                {
                    queryData = queryData.Skip((page - 1) * rows).Take(rows);
                }

                foreach (var item in queryData)
                {
                    if (item.SysPerson != null)
                    {
                        item.SysPersonId = string.Empty;
                        foreach (var it in item.SysPerson)
                        {
                            item.SysPersonId += it.MyName + '、';
                        }
                    }

                }

            }
            return queryData.ToList();
        }
        /// <summary>
        /// 查询的数据 /*在6.0版本中 新增*/
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        public List<SysRole> GetByParam(string id, string order, string sort, string search)
        {
            IQueryable<SysRole> queryData = repository.DaoChuData(db, order, sort, search);

            return queryData.ToList();
        }
        /// <summary>
        /// 创建一个角色
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个角色</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, SysRole entity)
        {
            int count = 1;

            foreach (string item in entity.SysPersonId.GetIdSort())
            {
                SysPerson sys = new SysPerson { Id = item };
                db.SysPerson.Attach(sys);
                entity.SysPerson.Add(sys);
                count++;
            }

            repository.Create(db, entity);
            if (count == repository.Save(db))
            {
                return true;
            }
            else
            {
                validationErrors.Add("创建出错了");
            }
            return false;
        }
        /// <summary>
        /// 创建一个角色
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个角色</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, SysRole entity)
        {
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    if (Create(ref validationErrors, db, entity))
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
            return false;
        }
        /// <summary>
        ///  创建角色集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">角色集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<SysRole> entitys)
        {
            try
            {
                if (entitys != null)
                {
                    int flag = 0, count = entitys.Count();
                    if (count > 0)
                    {
                        using (TransactionScope transactionScope = new TransactionScope())
                        {
                            foreach (var entity in entitys)
                            {
                                if (Create(ref validationErrors, db, entity))
                                {
                                    flag++;
                                }
                                else
                                {
                                    Transaction.Current.Rollback();
                                    return false;
                                }
                            }
                            if (count == flag)
                            {
                                transactionScope.Complete();
                                return true;
                            }
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
        /// 删除一个角色
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一个角色的主键</param>
        /// <returns></returns>  
        public bool Delete(ref ValidationErrors validationErrors, string id)
        {
            try
            {
                return repository.Delete(id) == 1;
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }
        /// <summary>
        /// 删除角色集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">主键的角色</param>
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
        ///  创建角色集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">角色集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<SysRole> entitys)
        {
            if (entitys != null)
            {
                try
                {
                    int flag = 0, count = entitys.Count();
                    if (count > 0)
                    {
                        using (TransactionScope transactionScope = new TransactionScope())
                        {
                            foreach (var entity in entitys)
                            {
                                if (Edit(ref validationErrors, db, entity))
                                {
                                    flag++;
                                }
                                else
                                {
                                    Transaction.Current.Rollback();
                                    return false;
                                }
                            }
                            if (count == flag)
                            {
                                transactionScope.Complete();
                                return true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    validationErrors.Add(ex.Message);
                    ExceptionsHander.WriteExceptions(ex);
                }
            }
            return false;
        }
        /// <summary>
        /// 编辑一个角色
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据上下文</param>
        /// <param name="entity">一个角色</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, SysRole entity)
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

            List<string> addSysPersonId = new List<string>();
            List<string> deleteSysPersonId = new List<string>();
            DataOfDiffrent.GetDiffrent(entity.SysPersonId.GetIdSort(), entity.SysPersonIdOld.GetIdSort(), ref addSysPersonId, ref deleteSysPersonId);
            List<SysPerson> listEntitySysPerson = new List<SysPerson>();
            if (deleteSysPersonId != null && deleteSysPersonId.Count() > 0)
            {
                foreach (var item in deleteSysPersonId)
                {
                    SysPerson sys = new SysPerson { Id = item };
                    listEntitySysPerson.Add(sys);
                    entity.SysPerson.Add(sys);
                }
            }

            SysRole editEntity = repository.Edit(db, entity);


            if (addSysPersonId != null && addSysPersonId.Count() > 0)
            {
                foreach (var item in addSysPersonId)
                {
                    SysPerson sys = new SysPerson { Id = item };
                    db.SysPerson.Attach(sys);
                    editEntity.SysPerson.Add(sys);
                    count++;
                }
            }
            if (deleteSysPersonId != null && deleteSysPersonId.Count() > 0)
            {
                foreach (SysPerson item in listEntitySysPerson)
                {
                    editEntity.SysPerson.Remove(item);
                    count++;
                }
            }

            if (count == repository.Save(db))
            {
                return true;
            }
            else
            {
                validationErrors.Add("编辑角色出错了");
            }
            return false;
        }
        /// <summary>
        /// 编辑一个角色
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个角色</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, SysRole entity)
        {
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    if (Edit(ref validationErrors, db, entity))
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
            return false;
        }
        public List<SysRole> GetAllDisplay()
        {
            return repository.GetAllDisplay(db).ToList();
        }

        public List<SysRole> GetAll()
        {
            return repository.GetAll(db).ToList();
        }

        /// <summary>
        /// 根据主键获取一个角色
        /// </summary>
        /// <param name="id">角色的主键</param>
        /// <returns>一个角色</returns>
        public SysRole GetById(string id)
        {
            return repository.GetById(db, id);
        }

        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public List<SysPerson> GetRefSysPerson(string id)
        {
            return repository.GetRefSysPerson(db, id).ToList();
        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public List<SysPerson> GetRefSysPerson()
        {
            return repository.GetRefSysPerson(db).ToList();
        }


        public void Dispose()
        {

        }



        public bool SaveCollection(ref ValidationErrors validationErrors, string[] ids, string id)
        {
            char split = '^';
            var data = (
                from f in ids
                where f.Contains(split)
                select f.Substring(0, f.IndexOf(split))
                        ).Union(
                from f in ids
                where !string.IsNullOrWhiteSpace(f) && !f.Contains(split)
                select f);

            using (TransactionScope transactionScope = new TransactionScope())
            {
                //利用编码机制，查询出所有的菜单
                var codes = db.SysMenu.Where(w => data.Contains(w.Id)).Select(s => s.Remark).Distinct();
                List<string> ls = new List<string>();
                foreach (var item in codes)
                {
                    if (item == null)
                    {
                        continue;
                    }
                    for (int i = 0; i < item.Length / 4; i++)
                    {

                        //在1.2版本中修改
                        string num = System.Numerics.BigInteger.Divide(System.Numerics.BigInteger.Parse(item), System.Numerics.BigInteger.Pow(10000, i)).ToString();

                        if (!ls.Contains(num))
                        {
                            ls.Add(num);
                        }
                    }
                }
                var SysMenusIds = from f in db.SysMenu
                                  where ls.Contains(f.Remark)
                                  select f.Id; //现在所有的菜单

                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("DELETE FROM  [SysMenuSysRoleSysOperation] WHERE [SysRoleId] = '{0}';", id);//删除该角色的所有的菜单和操作
                foreach (var item in SysMenusIds)
                {//插入菜单
                    builder.AppendFormat("INSERT INTO [SysMenuSysRoleSysOperation]([Id],[SysRoleId],[SysMenuId])VALUES('{0}','{1}','{2}');"
                        , Common.Result.GetNewId(), id, item);
                }

                foreach (var item in ids)
                {//插入操作
                    if (item.Contains(split))
                        builder.AppendFormat("INSERT INTO [SysMenuSysRoleSysOperation]([Id],[SysRoleId],[SysMenuId],[SysOperationId])VALUES('{0}','{1}','{2}','{3}');"
                       , Common.Result.GetNewId(), id, item.Substring(0, item.IndexOf(split)), item.Substring(item.IndexOf(split) + 1));
                }

                db.Database.ExecuteSqlCommand(builder.ToString());
                db.SaveChanges();
                transactionScope.Complete();
                return true;

            }
        }
    }
}

