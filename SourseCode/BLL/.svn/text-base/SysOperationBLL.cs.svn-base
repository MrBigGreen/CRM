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
    /// 操作 
    /// </summary>
    public partial  class SysOperationBLL : IBLL.ISysOperationBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 操作的数据库访问对象
        /// </summary>
        SysOperationRepository repository = new SysOperationRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public SysOperationBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public SysOperationBLL(DB_CRMEntities entities)
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
        public List<SysOperation> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {

            
            if (null != id)
            {
                search += "SysMenu&" + id + "^";
            }
            
            IQueryable<SysOperation> queryData = repository.DaoChuData(db, order, sort, search);
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
                        if (item.SysMenu != null)
                        {
                            item.SysMenuId = string.Empty;
                            foreach (var it in item.SysMenu)
                            {
                                item.SysMenuId += it.Name + ' ';
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
        public List<SysOperation> GetByParam(string id, string order, string sort, string search)
        {
            IQueryable<SysOperation> queryData = repository.DaoChuData(db, order, sort, search);
            
            return queryData.ToList();
        }
        /// <summary>
        /// 创建一个操作
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个操作</param>
        /// <returns></returns>
       public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, SysOperation entity)
        {   
            int count = 1;
        
            foreach (string item in entity.SysMenuId.GetIdSort())
            {
                SysMenu sys = new SysMenu { Id = item };
                db.SysMenu.Attach(sys);
                entity.SysMenu.Add(sys);
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
        /// 创建一个操作
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个操作</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, SysOperation entity)
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
        ///  创建操作集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">操作集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<SysOperation> entitys)
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
        /// 删除一个操作
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一个操作的主键</param>
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
        /// 删除操作集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">主键的操作</param>
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
        ///  创建操作集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">操作集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<SysOperation> entitys)
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
        /// 编辑一个操作
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据上下文</param>
        /// <param name="entity">一个操作</param>
        /// <returns>是否编辑成功</returns>
       public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, SysOperation entity)
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
            
            List<string> addSysMenuId = new List<string>();
            List<string> deleteSysMenuId = new List<string>();
            DataOfDiffrent.GetDiffrent(entity.SysMenuId.GetIdSort(), entity.SysMenuIdOld.GetIdSort(), ref addSysMenuId, ref deleteSysMenuId);
            List<SysMenu> listEntitySysMenu = new List<SysMenu>();
            if (deleteSysMenuId != null && deleteSysMenuId.Count() > 0)
            {                
                foreach (var item in deleteSysMenuId)
                {
                    SysMenu sys = new SysMenu { Id = item };
                    listEntitySysMenu.Add(sys);
                    entity.SysMenu.Add(sys);
                }                
            } 

            SysOperation editEntity = repository.Edit(db, entity);
            
         
            if (addSysMenuId != null && addSysMenuId.Count() > 0)
            {
                foreach (var item in addSysMenuId)
                {
                    SysMenu sys = new SysMenu { Id = item };
                    db.SysMenu.Attach(sys);
                    editEntity.SysMenu.Add(sys);
                    count++;
                }
            }
            if (deleteSysMenuId != null && deleteSysMenuId.Count() > 0)
            { 
                foreach (SysMenu item in listEntitySysMenu)
                {
                    editEntity.SysMenu.Remove(item);
                    count++;
                }
            } 

            if (count == repository.Save(db))
            {
                return true;
            }
            else
            {
                validationErrors.Add("编辑操作出错了");
            }
            return false;
        }
        /// <summary>
        /// 编辑一个操作
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个操作</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, SysOperation entity)
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
        public List<SysOperation> GetAll()
        {            
            return repository.GetAll(db).ToList();          
        }     
        
        /// <summary>
        /// 根据主键获取一个操作
        /// </summary>
        /// <param name="id">操作的主键</param>
        /// <returns>一个操作</returns>
        public SysOperation GetById(string id)
        {          
            return repository.GetById(db, id);           
        }
        
		/// <summary>
        /// 设置一个菜单
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个菜单</param>
        /// <returns>是否设置成功</returns>
        public bool SetSysMenu(ref ValidationErrors validationErrors, SysOperation entity)
        {
            bool bResult = false;
            int count = 0;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    SysOperation editEntity = repository.GetById(db, entity.Id);

                    List<string> addSysMenuId = new List<string>();
                    List<string> deleteSysMenuId = new List<string>();
                    if (entity.SysMenuId != null)
                    {
                        addSysMenuId = entity.SysMenuId.Split(',').ToList();
                    }
                    if (entity.SysMenuIdOld != null)
                    {
                        deleteSysMenuId = entity.SysMenuIdOld.Split(',').ToList();
                    }
                    DataOfDiffrent.GetDiffrent(addSysMenuId, deleteSysMenuId, ref addSysMenuId, ref deleteSysMenuId);

                    if (addSysMenuId != null && addSysMenuId.Count() > 0)
                    {
                        foreach (var item in addSysMenuId)
                        {
                            SysMenu sys = new SysMenu { Id = item };
                            db.SysMenu.Attach(sys);
                            editEntity.SysMenu.Add(sys);
                            count++;
                        }
                    }
                    if (deleteSysMenuId != null && deleteSysMenuId.Count() > 0)
                    {
                        List<SysMenu> listEntity = new List<SysMenu>();
                        foreach (var item in deleteSysMenuId)
                        {
                            SysMenu sys = new SysMenu { Id = item };
                            listEntity.Add(sys);
                            db.SysMenu.Attach(sys);
                        }
                        foreach (SysMenu item in listEntity)
                        {
                            editEntity.SysMenu.Remove(item);//查询数据库
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
        public List<SysMenu> GetRefSysMenu(string id)
        { 
            return repository.GetRefSysMenu(db, id).ToList();
        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public List<SysMenu> GetRefSysMenu()
        { 
            return repository.GetRefSysMenu(db).ToList();
        }

        
        public void Dispose()
        {
           
        }
    }
}

