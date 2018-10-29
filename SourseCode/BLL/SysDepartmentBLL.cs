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
    /// 部门 
    /// </summary>
    public partial class SysDepartmentBLL : IBLL.ISysDepartmentBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 部门的数据库访问对象
        /// </summary>
        SysDepartmentRepository repository = new SysDepartmentRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public SysDepartmentBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public SysDepartmentBLL(DB_CRMEntities entities)
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
        public List<SysDepartment> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {


            IQueryable<SysDepartment> queryData = repository.DaoChuData(db, order, sort, search);
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
                    if (item.ParentId != null && item.SysDepartment2 != null)
                    {
                        item.ParentIdOld = item.SysDepartment2.Name.GetString();//                            
                    }

                    if (item.SysDocument != null)
                    {
                        item.SysDocumentId = string.Empty;
                        foreach (var it in item.SysDocument)
                        {
                            item.SysDocumentId += it.Name + ' ';
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
        public List<SysDepartment> GetByParam(string id, string order, string sort, string search)
        {
            IQueryable<SysDepartment> queryData = repository.DaoChuData(db, order, sort, search);

            return queryData.ToList();
        }
        /// <summary>
        /// 创建一个部门
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个部门</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, SysDepartment entity)
        {
            int count = 1;
            if (string.IsNullOrEmpty(entity.ParentId))
            {
                //获取最大的职位级别编码 add by chand 2015-01-16
                entity.PositionLevel = (repository.GetMaxPositionLevel(db, "") + 1).ToString();
            }
            else
            {
                var parent = repository.GetById(entity.ParentId);
                entity.PositionLevel = (repository.GetMaxChildPositionLevel(db, parent.PositionLevel) + 1).ToString();
            }
            foreach (string item in entity.SysDocumentId.GetIdSort())
            {
                SysDocument sys = new SysDocument { Id = item };
                db.SysDocument.Attach(sys);
                entity.SysDocument.Add(sys);
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
        /// 创建一个部门
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个部门</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, SysDepartment entity)
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
        ///  创建部门集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">部门集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<SysDepartment> entitys)
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
        /// 删除一个部门
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一个部门的主键</param>
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
        /// 删除部门集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">主键的部门</param>
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
        ///  创建部门集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">部门集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<SysDepartment> entitys)
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
        /// 编辑一个部门
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据上下文</param>
        /// <param name="entity">一个部门</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, SysDepartment entity)
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

            List<string> addSysDocumentId = new List<string>();
            List<string> deleteSysDocumentId = new List<string>();
            DataOfDiffrent.GetDiffrent(entity.SysDocumentId.GetIdSort(), entity.SysDocumentIdOld.GetIdSort(), ref addSysDocumentId, ref deleteSysDocumentId);
            List<SysDocument> listEntitySysDocument = new List<SysDocument>();
            if (deleteSysDocumentId != null && deleteSysDocumentId.Count() > 0)
            {
                foreach (var item in deleteSysDocumentId)
                {
                    SysDocument sys = new SysDocument { Id = item };
                    listEntitySysDocument.Add(sys);
                    entity.SysDocument.Add(sys);
                }
            }

            SysDepartment editEntity = repository.Edit(db, entity);


            if (addSysDocumentId != null && addSysDocumentId.Count() > 0)
            {
                foreach (var item in addSysDocumentId)
                {
                    SysDocument sys = new SysDocument { Id = item };
                    db.SysDocument.Attach(sys);
                    editEntity.SysDocument.Add(sys);
                    count++;
                }
            }
            if (deleteSysDocumentId != null && deleteSysDocumentId.Count() > 0)
            {
                foreach (SysDocument item in listEntitySysDocument)
                {
                    editEntity.SysDocument.Remove(item);
                    count++;
                }
            }

            if (count == repository.Save(db))
            {
                return true;
            }
            else
            {
                validationErrors.Add("编辑部门出错了");
            }
            return false;
        }
        /// <summary>
        /// 编辑一个部门
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个部门</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, SysDepartment entity)
        {
            try
            {
                if (string.IsNullOrEmpty(entity.ParentId))
                {
                    //获取最大的职位级别编码 add by chand 2015-01-16
                    entity.PositionLevel = (repository.GetMaxPositionLevel(db, "") + 1).ToString();
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    if (entity.ParentIdOld != entity.ParentId)
                    {
                        //父级职位发生改变，需要修改所有当前下级的职位级别编码
                        string oldLevel = entity.PositionLevel;

                        //算出新的职位编码
                        var parent = repository.GetById(entity.ParentId);
                        string newLevel = (repository.GetMaxChildPositionLevel(db, parent.PositionLevel) + 1).ToString();


                        sb.AppendFormat("UPDATE SysDepartment SET Name='{0}',ParentId='{1}',[Address]='{2}',Sort='{3}',Remark='{4}',PositionLevel='{5}' WHERE Id='{6}';",
                            entity.Name, entity.ParentId, entity.Address, entity.Sort, entity.Remark, newLevel, entity.Id);

                        sb.AppendFormat("UPDATE dbo.SysDepartment SET PositionLevel= '{0}'+SUBSTRING(PositionLevel,{1},100)  WHERE LEFT(PositionLevel,{2})='{3}'", newLevel, oldLevel.Length + 1, oldLevel.Length, oldLevel);

                    }
                    else
                    {
                        sb.AppendFormat("UPDATE SysDepartment SET Name='{0}',[Address]='{1}',Sort='{2}',Remark='{3}' WHERE Id='{4}';",
                           entity.Name, entity.Address, entity.Sort, entity.Remark, entity.Id);
                    }

                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        if (db.Database.ExecuteSqlCommand(sb.ToString()) > 0)
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
        public List<SysDepartment> GetAll()
        {
            return repository.GetAll(db).ToList();
        }

        /// <summary>
        /// 获取自连接树形列表数据
        /// </summary>
        /// <returns>自定义的树形结构</returns>

        public IQueryable<SysDepartment> GetAllMetadata(string id)
        {
            if (id == null)
            {
                return db.SysDepartment.Where(w => w.ParentId == null).AsQueryable();
            }
            else
            {
                return db.SysDepartment.Where(w => w.ParentId == id).AsQueryable();
            }
        }


        /// <summary>
        /// 根据主键获取一个部门
        /// </summary>
        /// <param name="id">部门的主键</param>
        /// <returns>一个部门</returns>
        public SysDepartment GetById(string id)
        {
            return repository.GetById(db, id);
        }

        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public List<SysDocument> GetRefSysDocument(string id)
        {
            return repository.GetRefSysDocument(db, id).ToList();
        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public List<SysDocument> GetRefSysDocument()
        {
            return repository.GetRefSysDocument(db).ToList();
        }


        public void Dispose()
        {

        }
    }
}

