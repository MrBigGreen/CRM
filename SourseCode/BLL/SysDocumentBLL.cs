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
    /// 文档管理 
    /// </summary>
    public partial  class SysDocumentBLL : IBLL.ISysDocumentBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 文档管理的数据库访问对象
        /// </summary>
        SysDocumentRepository repository = new SysDocumentRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public SysDocumentBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public SysDocumentBLL(DB_CRMEntities entities)
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
        public List<SysDocument> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {

            
            if (null != id)
            {
                search += "SysDepartment&" + id + "^";
            }
            
            IQueryable<SysDocument> queryData = repository.DaoChuData(db, order, sort, search);
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
                                item.SysPersonId += it.Name + ' ';
                            }                         
                        } 
 
                        if (item.SysDepartment != null)
                        {
                            item.SysDepartmentId = string.Empty;
                            foreach (var it in item.SysDepartment)
                            {
                                item.SysDepartmentId += it.Name + ' ';
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
        public List<SysDocument> GetByParam(string id, string order, string sort, string search)
        {
            IQueryable<SysDocument> queryData = repository.DaoChuData(db, order, sort, search);
            
            return queryData.ToList();
        }
        /// <summary>
        /// 创建一个文档管理
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个文档管理</param>
        /// <returns></returns>
       public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, SysDocument entity)
        {   
            int count = 1;
        
            foreach (string item in entity.SysPersonId.GetIdSort())
            {
                SysPerson sys = new SysPerson { Id = item };
                db.SysPerson.Attach(sys);
                entity.SysPerson.Add(sys);
                count++;
            }

            foreach (string item in entity.SysDepartmentId.GetIdSort())
            {
                SysDepartment sys = new SysDepartment { Id = item };
                db.SysDepartment.Attach(sys);
                entity.SysDepartment.Add(sys);
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
        /// 创建一个文档管理
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个文档管理</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, SysDocument entity)
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
        ///  创建文档管理集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">文档管理集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<SysDocument> entitys)
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
        /// 删除一个文档管理
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一个文档管理的主键</param>
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
        /// 删除文档管理集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">主键的文档管理</param>
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
        ///  创建文档管理集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">文档管理集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<SysDocument> entitys)
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
        /// 编辑一个文档管理
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据上下文</param>
        /// <param name="entity">一个文档管理</param>
        /// <returns>是否编辑成功</returns>
       public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, SysDocument entity)
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

            List<string> addSysDepartmentId = new List<string>();
            List<string> deleteSysDepartmentId = new List<string>();
            DataOfDiffrent.GetDiffrent(entity.SysDepartmentId.GetIdSort(), entity.SysDepartmentIdOld.GetIdSort(), ref addSysDepartmentId, ref deleteSysDepartmentId);
            List<SysDepartment> listEntitySysDepartment = new List<SysDepartment>();
            if (deleteSysDepartmentId != null && deleteSysDepartmentId.Count() > 0)
            {                
                foreach (var item in deleteSysDepartmentId)
                {
                    SysDepartment sys = new SysDepartment { Id = item };
                    listEntitySysDepartment.Add(sys);
                    entity.SysDepartment.Add(sys);
                }                
            } 

            SysDocument editEntity = repository.Edit(db, entity);
            
         
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

         
            if (addSysDepartmentId != null && addSysDepartmentId.Count() > 0)
            {
                foreach (var item in addSysDepartmentId)
                {
                    SysDepartment sys = new SysDepartment { Id = item };
                    db.SysDepartment.Attach(sys);
                    editEntity.SysDepartment.Add(sys);
                    count++;
                }
            }
            if (deleteSysDepartmentId != null && deleteSysDepartmentId.Count() > 0)
            { 
                foreach (SysDepartment item in listEntitySysDepartment)
                {
                    editEntity.SysDepartment.Remove(item);
                    count++;
                }
            } 

            if (count == repository.Save(db))
            {
                return true;
            }
            else
            {
                validationErrors.Add("编辑文档管理出错了");
            }
            return false;
        }
        /// <summary>
        /// 编辑一个文档管理
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个文档管理</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, SysDocument entity)
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
        public List<SysDocument> GetAll()
        {            
            return repository.GetAll(db).ToList();          
        }     
        
        /// <summary>
        /// 根据主键获取一个文档管理
        /// </summary>
        /// <param name="id">文档管理的主键</param>
        /// <returns>一个文档管理</returns>
        public SysDocument GetById(string id)
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

		/// <summary>
        /// 设置一个部门
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个部门</param>
        /// <returns>是否设置成功</returns>
        public bool SetSysDepartment(ref ValidationErrors validationErrors, SysDocument entity)
        {
            bool bResult = false;
            int count = 0;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    SysDocument editEntity = repository.GetById(db, entity.Id);

                    List<string> addSysDepartmentId = new List<string>();
                    List<string> deleteSysDepartmentId = new List<string>();
                    if (entity.SysDepartmentId != null)
                    {
                        addSysDepartmentId = entity.SysDepartmentId.Split(',').ToList();
                    }
                    if (entity.SysDepartmentIdOld != null)
                    {
                        deleteSysDepartmentId = entity.SysDepartmentIdOld.Split(',').ToList();
                    }
                    DataOfDiffrent.GetDiffrent(addSysDepartmentId, deleteSysDepartmentId, ref addSysDepartmentId, ref deleteSysDepartmentId);

                    if (addSysDepartmentId != null && addSysDepartmentId.Count() > 0)
                    {
                        foreach (var item in addSysDepartmentId)
                        {
                            SysDepartment sys = new SysDepartment { Id = item };
                            db.SysDepartment.Attach(sys);
                            editEntity.SysDepartment.Add(sys);
                            count++;
                        }
                    }
                    if (deleteSysDepartmentId != null && deleteSysDepartmentId.Count() > 0)
                    {
                        List<SysDepartment> listEntity = new List<SysDepartment>();
                        foreach (var item in deleteSysDepartmentId)
                        {
                            SysDepartment sys = new SysDepartment { Id = item };
                            listEntity.Add(sys);
                            db.SysDepartment.Attach(sys);
                        }
                        foreach (SysDepartment item in listEntity)
                        {
                            editEntity.SysDepartment.Remove(item);//查询数据库
                            count++;
                        }
                    } 

                    if (count > 0 && count == repository.Save(db))
                    {
                       transactionScope.Complete();
                       bResult = true;
                    }
                    else if(count == 0 )
                    {
                        validationErrors.Add("数据没有改变");
                    }
                }
                catch (Exception ex)
                {
                    Transaction.Current.Rollback();                    
                    ExceptionsHander.WriteExceptions(ex);
                    validationErrors.Add("编辑出错了。原因"+ex.Message);
                }
            }
            
            return bResult;
        }

        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public List<SysDepartment> GetRefSysDepartment(string id)
        { 
            return repository.GetRefSysDepartment(db, id).ToList();
        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public List<SysDepartment> GetRefSysDepartment()
        { 
            return repository.GetRefSysDepartment(db).ToList();
        }

        
        public void Dispose()
        {
           
        }
    }
}

