using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using CRM.DAL;
using Common;
using CRM.DAL.CommonEntity;

namespace CRM.BLL
{
    /// <summary>
    /// 人员 
    /// </summary>
    public partial class SysPersonBLL : IBLL.ISysPersonBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 人员的数据库访问对象
        /// </summary>
        SysPersonRepository repository = new SysPersonRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public SysPersonBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public SysPersonBLL(DB_CRMEntities entities)
        {
            db = entities;
        }
        /// <summary>
        /// 保存皮肤
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="id"></param>
        public bool SaveTheme(string theme, string id)
        {
            SysPerson p = this.GetById(id);
            p.PageStyle = theme;
            repository.Edit(db, p);
            int count = repository.Save(db);
            return count > 0;
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
        public List<SysPerson> GetByParam(string id, int page, int rows, string order, string sort, string search, string sSysDepartmentId, ref int total)
        {


            IQueryable<SysPerson> queryData = repository.DaoChuData(db, order, sort, search, sSysDepartmentId);
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
                    if (item.SysDepartmentId != null && item.SysDepartment != null)
                    {
                        item.SysDepartmentIdOld = item.SysDepartment.Name.GetString();//                            
                    }

                    if (item.SysRole != null)
                    {
                        item.SysRoleName = string.Empty;
                        foreach (var it in item.SysRole)
                        {
                            item.SysRoleName += it.Name + ' ';
                        }
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
        public List<SysPerson> GetByParam(string id, string order, string sort, string search, string sSysDepartmentId)
        {
            IQueryable<SysPerson> queryData = repository.DaoChuData(db, order, sort, search, sSysDepartmentId);

            return queryData.ToList();
        }
        /// <summary>
        /// 创建一个人员
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个人员</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, SysPerson entity)
        {
            int count = 1;
            //密码加密
            entity.Password = EncryptAndDecrypte.EncryptString(entity.Password);
            entity.SurePassword = EncryptAndDecrypte.EncryptString(entity.SurePassword);


            if (!string.IsNullOrWhiteSpace(entity.SysRoleId))
            {
                SysRole sys = new SysRole { Id = entity.SysRoleId };
                db.SysRole.Attach(sys);
                entity.SysRole.Add(sys);
                count++;
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
        /// 创建一个人员
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个人员</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, SysPerson entity)
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
        ///  创建人员集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">人员集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<SysPerson> entitys)
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
        /// 删除一个人员
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一个人员的主键</param>
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
        /// 删除人员集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">主键的人员</param>
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
        ///  创建人员集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">人员集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<SysPerson> entitys)
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
        /// 编辑一个人员
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据上下文</param>
        /// <param name="entity">一个人员</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, SysPerson entity)
        {  /*                       
                           * 不操作 原有 现有
                           * 增加   原没 现有
                           * 删除   原有 现没
                           */
            if (entity == null)
            {
                return false;
            }
            //密码加密
            entity.Password = EncryptAndDecrypte.EncryptString(entity.Password);
            entity.SurePassword = EncryptAndDecrypte.EncryptString(entity.SurePassword);
            int count = 1;



            List<string> addSysRoleId = new List<string>();
            List<string> deleteSysRoleId = new List<string>();
            List<SysRole> listEntitySysRole = new List<SysRole>();


            if (entity.SysRoleId != entity.SysRoleIdOld)
            {
                if (!string.IsNullOrWhiteSpace(entity.SysRoleIdOld))
                {
                    SysRole sys = new SysRole { Id = entity.SysRoleIdOld };
                    listEntitySysRole.Add(sys);
                    entity.SysRole.Add(sys);
                }
            }

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

            SysPerson editEntity = repository.Edit(db, entity);

            if (entity.SysRoleId != entity.SysRoleIdOld)
            {
                if (!string.IsNullOrWhiteSpace(entity.SysRoleId))
                {
                    SysRole sys = new SysRole { Id = entity.SysRoleId };
                    db.SysRole.Attach(sys);
                    editEntity.SysRole.Add(sys);
                    count++;
                }
                if (!string.IsNullOrWhiteSpace(entity.SysRoleIdOld))
                {
                    foreach (SysRole item in listEntitySysRole)
                    {
                        editEntity.SysRole.Remove(item);
                        count++;
                    }
                }
            }

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
                validationErrors.Add("编辑人员出错了");
            }
            return false;
        }
        /// <summary>
        /// 编辑一个人员
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个人员</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, SysPerson entity)
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
        public List<SysPerson> GetAll()
        {
            return repository.GetAll(db).ToList();
        }

        /// <summary>
        /// 根据主键获取一个人员
        /// </summary>
        /// <param name="id">人员的主键</param>
        /// <returns>一个人员</returns>
        public SysPerson GetById(string id)
        {
            SysPerson entity = repository.GetById(db, id);
            //密码解密
            entity.Password = EncryptAndDecrypte.DecrypteString(entity.Password);
            entity.SurePassword = EncryptAndDecrypte.DecrypteString(entity.SurePassword);
            if (entity.SysRole != null)
            {
                var role = entity.SysRole.SingleOrDefault();
                if (role != null)
                {
                    entity.SysRoleId = role.Id;
                }

                entity.SysRoleIdOld = entity.SysRoleId ?? "";
            }
            return entity;
        }

        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public List<SysRole> GetRefSysRole(string id)
        {
            return repository.GetRefSysRole(db, id).ToList();
        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public List<SysRole> GetRefSysRole()
        {
            return repository.GetRefSysRole(db).ToList();
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


        /// <summary>
        /// 根据SysDepartmentIdId，获取所有人员数据
        /// </summary>
        /// <param name="id">外键的主键</param>
        /// <returns></returns>
        public List<SysPerson> GetByRefSysDepartmentId(string id)
        {
            return repository.GetByRefSysDepartmentId(db, id).ToList();
        }

        #region 获取账号能见度 create by chand 2015-08-19
        /// <summary>
        /// 获取账号能见度
        /// create by chand 2015-08-19
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PersonEntity> GetPersonVisibility(string id)
        {
            return repository.GetPersonVisibility(db, id);
        }

        #endregion


        #region 设置薪资主体  create by chand 2016-04-12
        /// <summary>
        /// 设置薪资主体
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="SysPersonID"></param>
        /// <param name="SalarySubjectIDList"></param>
        /// <returns></returns>
        public bool SetSalarySubjectSysPerson(ref ValidationErrors validationErrors, string SysPersonID, List<string> SalarySubjectIDList)
        {
            if (string.IsNullOrWhiteSpace(SysPersonID) || SalarySubjectIDList == null || SalarySubjectIDList.Count < 1)
            {
                validationErrors.Add("信息不能为空");
                return false;
            }

            var entity = db.SysPerson.Where(s => s.Id == SysPersonID).SingleOrDefault();
            if (entity == null)
            {
                validationErrors.Add("人员信息不存在");
                return false;

            }
            int flag = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE TP_SalarySubjectSysPerson WHERE SysPersonID='{0}'", SysPersonID);
            flag = 1;
            for (int i = 0; i < SalarySubjectIDList.Count; i++)
            {
                sb.AppendFormat("INSERT INTO TP_SalarySubjectSysPerson (SysPersonID,SalarySubjectID) VALUES('{0}','{1}');", SysPersonID, SalarySubjectIDList[i]);
                flag++;
            }

            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    flag = db.Database.ExecuteSqlCommand(sb.ToString());
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

            return false;
        }
        #endregion

        #region 获取薪资主体的权限   create by chand 2016-04-12
        /// <summary>
        /// 获取薪资主体的权限
        /// </summary>
        /// <param name="SysPersonID"></param>
        /// <returns></returns>
        public List<string> GetSalarySubjectSysPerson(string SysPersonID)
        {
            return repository.GetSalarySubjectSysPerson(db, SysPersonID);
        }
        #endregion



        public void Dispose()
        {

        }
    }
}

