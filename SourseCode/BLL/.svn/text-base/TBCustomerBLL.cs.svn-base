using Common;
using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRM.BLL
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-06-10
    /// 描述说明：客户信息-业务逻辑
    /// </summary>
    public partial class TBCustomerBLL : IBLL.ITBCustomerBLL, IDisposable
    {

        #region 初始化

        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 客户的数据库访问对象
        /// </summary>
        TBCustomerRepository repository = new TBCustomerRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public TBCustomerBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public TBCustomerBLL(DB_CRMEntities entities)
        {
            db = entities;
        }

        #endregion

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
        public List<CustomerModel> GetData(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            List<CustomerModel> queryData = repository.GetData(db, SearchPersonID, id, order, sort, search, page, rows, ref   total);
            return queryData;
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
        public List<CustomerModel> GetCustomerData_KA(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            List<CustomerModel> queryData = repository.GetCustomerData_KA(db, SearchPersonID, id, order, sort, search, page, rows, ref   total);
            return queryData;
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
        public List<CustomerModel> GetCustomerData_Self(string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            List<CustomerModel> queryData = repository.GetCustomerData_Self(db, SearchPersonID, id, order, sort, search, page, rows, ref   total);
            return queryData;
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
        public List<CustomerModel> GetPublicData(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            List<CustomerModel> queryData = repository.GetPublicData(db, id, order, sort, search, page, rows, ref   total);
            return queryData;
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
        public List<TB_Customer> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            IQueryable<TB_Customer> queryData = repository.DaoChuData(db, order, sort, search);
            total = queryData.Count();
            List<TB_Customer> list = new List<TB_Customer>(); ;
            if (total > 0)
            {
                //if (page <= 1)
                //{
                //    list = queryData.Take(rows).ToList();
                //}
                //else
                //{
                //    list = queryData.Skip((page - 1) * rows).Take(rows).ToList();
                //}
                //foreach (var item in list)
                //{
                //    item.SysPersonName = (from s in db.SysPerson where s.Id == item.SysPersonID select s.MyName).SingleOrDefault<string>();
                //}
            }
            return list;
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
        public List<TB_Customer> GetByParam(string id, string order, string sort, string search)
        {
            IQueryable<TB_Customer> queryData = repository.DaoChuData(db, order, sort, search);

            return queryData.ToList();
        }
        /// <summary>
        /// 创建一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个客户</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, TB_Customer entity)
        {
            int count = 1;
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
        /// 创建一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个客户</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, TB_Customer entity)
        {
            bool flag = false;
            try
            {
                //检查客户名是否存在
                // -1不可以创建  0可以创建    1 需要验证
                int result = 0;

                var q = db.TB_Customer.FirstOrDefault(s => s.CustomerName == entity.CustomerName);
                if (q != null)
                {
                    if (q.CreatedBy == entity.CreatedBy)
                    {
                        validationErrors.Add("您已创建过此客户，不能重复创建！");
                        return false;
                    }
                    else
                    {
                        validationErrors.Add("客户已存在不能创建！");
                        return false;
                    }
                }
                //modify by chand 2016-02-23 
                //var q = GetCustomerRepeat(entity.CustomerName, "", ref result);
                //if (result == -1)
                //{
                //    validationErrors.Add("客户已存在不能创建！");
                //    return false;
                //}


                //---modify by chand 2015-10-19
                if (result == 1)
                {
                    //待审核状态
                    entity.AuditStatus = 2;
                }
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    if (Create(ref validationErrors, db, entity))
                    {
                        transactionScope.Complete();
                        flag = true;
                    }
                    else
                    {
                        Transaction.Current.Rollback();
                    }
                }
                if (flag)
                {
                    #region 增加默认归属记录
                    TB_CustomerAttributionChange item = new TB_CustomerAttributionChange
                               {
                                   CustomerAttributionChangeID = Result.GetNewId(),
                                   CustomerID = entity.CustomerID,
                                   CreatedBy = entity.CreatedBy,
                                   CreatedTime = entity.CreatedTime,
                                   IsDelete = false,
                                   LastUpdatedBy = entity.LastUpdatedBy,
                                   LastUpdatedTime = entity.LastUpdatedTime,
                                   StartDate = entity.CreatedTime,
                                   SysPersonID = entity.SysPersonID,
                                   VersionNo = 1
                               };
                    new TBCustomerAttributionChangeBLL().Create(ref validationErrors, item);
                    #endregion
                    #region  增加主联系人

                    TB_CustomerContact contact = new TB_CustomerContact();
                    contact.CustomerContactID = Result.GetNewId();
                    contact.CustomerID = entity.CustomerID;
                    contact.ContactName = entity.ContactPerson;
                    contact.CompanyTel = entity.ContactTel;
                    contact.PhoneNumber1 = entity.ContactPhone;
                    contact.Post = entity.Post;
                    contact.Department = entity.Department;
                    contact.IsKP = true;
                    contact.IsDelete = false;
                    contact.CreatedBy = entity.CreatedBy;
                    contact.CreatedTime = entity.CreatedTime;
                    contact.LastUpdatedBy = entity.LastUpdatedBy;
                    contact.LastUpdatedTime = entity.LastUpdatedTime;
                    contact.VersionNo = 1;
                    new TBCustomerContactBLL().Create(ref validationErrors, contact);

                    #endregion

                }

            }
            catch (Exception ex)
            {
                flag = false;
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return flag;
        }

        /// <summary>
        ///  创建客户集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">客户集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<TB_Customer> entitys)
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
        /// 删除一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一个客户的主键</param>
        /// <returns></returns>  
        public bool Delete(ref ValidationErrors validationErrors, string id)
        {
            try
            {
                TB_Customer entity = db.TB_Customer.FirstOrDefault(s => s.CustomerID == id);
                if (entity != null)
                {
                    entity.IsDelete = true;

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
                //return repository.Delete(id) == 1;
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }
        /// <summary>
        /// 删除客户集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">主键的客户</param>
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
        ///  创建客户集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">客户集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<TB_Customer> entitys)
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
        /// 编辑一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据上下文</param>
        /// <param name="entity">一个客户</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, TB_Customer entity)
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

            TB_Customer editEntity = repository.Edit(db, entity);
            if (count == repository.Save(db))
            {
                return true;
            }
            else
            {
                validationErrors.Add("编辑客户出错了");
            }
            return false;
        }
        /// <summary>
        /// 编辑一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个客户</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, TB_Customer entity)
        {
            //检查用户是否有编辑分享权限
            int authority = GetCustomerAuthority(entity.CustomerID, entity.LastUpdatedBy);
            if (authority == -1 || authority == 1)
            {
                validationErrors.Add("您没有编辑客户权限");
                return false;
            }
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

        public bool Edit(ref ValidationErrors validationErrors, TB_Customer entity, bool isVerify)
        {
            //检查用户是否有编辑分享权限
            if (isVerify)
            {
                int authority = GetCustomerAuthority(entity.CustomerID, entity.LastUpdatedBy);
                if (authority == -1 || authority == 1)
                {
                    validationErrors.Add("您没有编辑客户权限");
                    return false;
                }
            }
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

        public List<TB_Customer> GetAll()
        {
            return repository.GetAll(db).ToList();
        }

        /// <summary>
        /// 根据主键获取一个客户
        /// </summary>
        /// <param name="id">客户的主键</param>
        /// <returns>一个客户</returns>
        public TB_Customer GetById(string id)
        {
            return repository.GetById(db, id);
        }

        #region 查重验证 create by chand 201-07-24


        /// <summary>
        /// 检查相似的客户
        /// </summary>
        /// <param name="CustomerName"></param>
        /// <param name="result"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerRepeatEntity> GetCustomerRepeat(string CustomerNewName, string CustomerID, ref  int result)
        {
            return new TBCustomerVerificationRepository().GetCustomerRepeat(db, CustomerNewName, CustomerID, ref result);
        }

        /// <summary>
        /// 发送验证码给相关客户归属人
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="CustomerNewName"></param>
        /// <param name="CurrentPersonID"></param>
        /// <returns></returns>
        public bool SendVerification(ref ValidationErrors validationErrors, string CustomerNewName, string CurrentPersonID)
        {
            int result = 0;
            TBCustomerVerificationRepository vRepository = new TBCustomerVerificationRepository();
            List<CustomerRepeatEntity> list = vRepository.GetCustomerRepeat(db, CustomerNewName, "", ref result);
            if (result > 0)
            {
                int count = 0;
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    db.Database.ExecuteSqlCommand("update dbo.TB_CustomerVerification set IsDelete=1,VersionNo=VersionNo+1 where  CustomerNewName='" + CustomerNewName + "'");
                    for (int i = 0; i < list.Count; i++)
                    {
                        try
                        {
                            //创建客户验证记录
                            TB_CustomerVerification entity = new TB_CustomerVerification();
                            entity.CustomerVerificationID = Result.GetNewId();
                            entity.CreatedBy = CurrentPersonID;
                            entity.CreatedTime = DateTime.Now;
                            entity.CustomerID = list[i].CustomerID;
                            entity.CustomerNewName = CustomerNewName;
                            entity.IsDelete = false;
                            entity.IsSuccess = false;
                            entity.LastUpdatedBy = CurrentPersonID;
                            entity.LastUpdatedTime = DateTime.Now;
                            entity.VerificationCode = entity.CustomerVerificationID.Substring(entity.CustomerVerificationID.Length - 10, 10).ToUpper();
                            entity.VersionNo = 1;
                            vRepository.Create(db, entity);
                            if (repository.Save(db) == 1)
                            {
                                count++;
                                //MailUtility.SendMail(list[i].EmailAddress, "客户重复验证", "新建客户名称“" + CustomerNewName + "”与您的客户名称“" + list[i].CustomerName + "”相似，同意创建验证码：" + entity.VerificationCode, "");
                                NetSendMail.SendMail(db, list[i].EmailAddress, "客户查重验证【" + CustomerNewName + "】", string.Format("新建客户名称“{0}”与您的客户名称“{1}”相似，同意创建验证码：{2}", CustomerNewName, list[i].CustomerName, entity.VerificationCode), "", CurrentPersonID);
                            }
                        }
                        catch (Exception ex)
                        {
                            validationErrors.Add(ex.Message);
                        }
                    }
                    if (count == list.Count)
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
            return false;
        }

        /// <summary>
        /// 进行查重验证 create by chand 2015-07-24
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="CustomerNewName"></param>
        /// <param name="VerificationCode"></param>
        /// <returns></returns>
        public bool GetVerification(ref ValidationErrors validationErrors, string CustomerNewName, string VerificationCode)
        {
            return new TBCustomerVerificationRepository().GetVerification(db, CustomerNewName, VerificationCode);
        }
        #endregion

        #region 是否可以创建客户 create by chand 2015-08-14
        public bool IsCreate(string CurrentPersonID)
        {
            bool flag = false;
            var personEntity = db.SysPerson.SingleOrDefault(s => s.Id == CurrentPersonID && s.IsDelete == false && s.State == "开启");
            if (personEntity != null && personEntity.CustomerCeiling != null)
            {
                int qty = db.TB_Customer.Count(s => s.SysPersonID == CurrentPersonID && s.IsDelete == false && s.IsFrozen == false);
                if (personEntity.CustomerCeiling > qty)
                {
                    flag = true;
                }
            }
            return flag;
        }

        #endregion


        /// <summary>
        /// 审核客户管理
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        public List<CustomerModel> GetCustomerAuditData(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            List<CustomerModel> queryData = repository.GetCustomerAuditData(db, id, order, sort, search, page, rows, ref   total);
            return queryData;
        }

        #region 客户审核
        /// <summary>
        /// 客户审核
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="isPass"></param>
        /// <param name="CustomerID"></param>
        /// <param name="Reason"></param>
        /// <param name="AuditPerson"></param>
        /// <returns></returns>
        public bool GetAuditCustomer(ref ValidationErrors validationErrors, bool isPass, string CustomerID, string Reason, string AuditPerson)
        {
            string sql = string.Format("update TB_Customer set AuditStatus={0},AuditPerson='{1}',AuditTime=getdate(),Reason='{2}' where CustomerID='{3}'", (isPass ? "1" : "3"), AuditPerson, Reason, CustomerID);
            int result = db.Database.ExecuteSqlCommand(sql);
            if (result == 1)
            {
                if (!isPass)
                {
                    var obj = db.Database.SqlQuery<CustomerRepeatEntity>("select cst.CustomerID,cst.CustomerName,cst.SysPersonID,p.MyName,p.EmailAddress from dbo.TB_Customer cst inner join SysPerson p on cst.CreatedBy=p.Id where cst.CustomerID='" + CustomerID + "'").ToList();
                    if (obj != null && obj.Count > 0)
                    {
                        //审核拒绝发送邮件 
                        NetSendMail.SendMail(db, obj[0].EmailAddress, "新建客户【" + obj[0].CustomerName + "】,审核失败", string.Format("新建客户名称“{0}”拒绝创建，原因：{1}", obj[0].CustomerName, Reason), "", AuditPerson);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 获取客户操作权限  create by chand 2015-10-23
        /// <summary>
        /// 获取客户操作权限
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CustomerID"></param>
        /// <param name="SysPersonID"></param>
        /// <returns>
        /// -1没有操作权限 
        /// 0 客户最大操作权限 
        /// 1 只读 
        /// 2 可以编辑权限
        /// </returns>
        public int GetCustomerAuthority(string CustomerID, string SysPersonID)
        {
            return repository.GetCustomerAuthority(db, CustomerID, SysPersonID);
        }

        #endregion


        #region 客户释放
        /// <summary>
        /// 客户释放
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="CustomerID"></param>
        /// <param name="CurrentPersonID"></param>
        /// <returns></returns>
        public bool GetCustomerRelease(ref ValidationErrors validationErrors, string CustomerID, string CurrentPersonID)
        {
            try
            {
                if (repository.GetCustomerRelease(db, CustomerID, CurrentPersonID) > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            //检查用户是否有编辑分享权限
            //int authority = GetCustomerAuthority(CustomerID, CurrentPersonID);
            //if (authority != 0)
            //{
            //    validationErrors.Add("您没有释放客户权限");
            //    return false;
            //}
            //try
            //{
            //    using (TransactionScope transactionScope = new TransactionScope())
            //    {
            //        int flag = 0;
            //        string sql = string.Format("UPDATE TB_Customer SET BelongingDate=NULL,SysPersonID='',IsFrozen=1,VersionNo=ISNULL(VersionNo,1)+1,LastUpdatedBy='{0}',LastUpdatedTime=GETDATE() WHERE CustomerID='{1}' and IsFrozen=0 ", CurrentPersonID, CustomerID);
            //        flag = db.Database.ExecuteSqlCommand(sql);
            //        sql = string.Format("UPDATE TB_CustomerAttributionChange SET EndDate =GETDATE(),VersionNo=ISNULL(VersionNo,1)+1 WHERE CustomerID='{0}' AND EndDate IS NULL AND IsDelete=0", CustomerID);
            //        flag += db.Database.ExecuteSqlCommand(sql);
            //        if (flag == 2)
            //        {
            //            transactionScope.Complete();
            //            return true;
            //        }
            //        else
            //        {
            //            Transaction.Current.Rollback();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    validationErrors.Add(ex.Message);
            //    ExceptionsHander.WriteExceptions(ex);
            //}
            return false;
        }

        #endregion

        #region 客户提取
        /// <summary>
        /// 客户提取
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="CustomerID"></param>
        /// <param name="CurrentPersonID"></param>
        /// <returns></returns>
        public bool GetCustomerExtraction(ref ValidationErrors validationErrors, string CustomerID, string CurrentPersonID)
        {
            try
            {

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    int flag = 0;
                    string sql = string.Format("UPDATE TB_Customer SET SysPersonID='{0}',BelongingDate=GETDATE(),Source=3,RelieveDate=DATEADD(MONTH,6,GETDATE()) ,IsFrozen=0,VersionNo=ISNULL(VersionNo,1)+1,LastUpdatedBy='{0}',LastUpdatedTime=GETDATE() WHERE CustomerID='{1}' and IsFrozen=1 ", CurrentPersonID, CustomerID);
                    flag = db.Database.ExecuteSqlCommand(sql);
                    sql = string.Format("INSERT INTO TB_CustomerAttributionChange(CustomerAttributionChangeID,CustomerID,SysPersonID,StartDate,Source,VersionNo,IsDelete,CreatedTime,CreatedBy,LastUpdatedTime,LastUpdatedBy)VALUES('{0}','{1}','{2}',GETDATE(),3,1,0,GETDATE(),'{2}',GETDATE(),'{2}') ", Result.GetNewId(), CustomerID, CurrentPersonID);
                    flag += db.Database.ExecuteSqlCommand(sql);
                    if (flag == 2)
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

        #region  修改客户名称 create by chand  2015-12-02
        /// <summary>
        /// 修改客户名称
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="CustomerID"></param>
        /// <param name="CustomerName"></param>
        /// <param name="CurrentPersonID"></param>
        /// <returns></returns>
        public bool GetUpdateCustomerName(ref ValidationErrors validationErrors, string CustomerID, string CustomerName, string CurrentPersonID)
        {
            int flag = 0;
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    string sql = string.Format("UPDATE TB_Customer SET CustomerName='{0}',LastUpdatedBy='{1}',LastUpdatedTime=GETDATE() WHERE CustomerID='{2}'", CustomerName, CurrentPersonID, CustomerID);
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
            return false;
        }
        #endregion


        #region 客户进程统计 create by chand 2015-12-07
        /// <summary>
        /// 客户进程统计
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerFunnel> GetFunnelStatistics(string SearchPersonID, string id, string search)
        {
            List<CustomerFunnel> queryData = repository.GetFunnelStatistics(db, SearchPersonID, id, search);
            return queryData;
        }
        #endregion


        #region 获取客户长时间未联系数量 create by chand 2015-12-08
        /// <summary>
        /// 获取客户长时间未联系数量
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public long GetCustomerNoContactQty(string SearchPersonID, string id, string search)
        {
            return repository.GetCustomerNoContactQty(db, SearchPersonID, id, search);
        }
        #endregion

        #region 客户报表  create  by chand 2016-2-21
        /// <summary>
        /// 客户汇总数据
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerSummaryModel> GetCustomerSummary(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            List<CustomerSummaryModel> queryData = repository.GetCustomerSummary(db, SearchPersonID, id, order, sort, search, page, rows, ref   total);
            return queryData;
        }

        public List<CustomerSummaryModel> GetCustomerSummaryDept(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            List<CustomerSummaryModel> queryData = repository.GetCustomerSummaryDept(db, id, order, sort, search, page, rows, ref   total);
            return queryData;
        }


        /// <summary>
        /// 客户进程数据
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerFollowReportModel> GetCustomerFollow_Report(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            List<CustomerFollowReportModel> queryData = repository.GetCustomerFollow_Report(db, SearchPersonID, id, order, sort, search, page, rows, ref   total);
            return queryData;
        }

        #endregion


        #region 四张报表 by Jonny
        public List<CustomerVisitReportModel> GetCustomerVisit_Report(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            List<CustomerVisitReportModel> queryData = repository.GetCustomerVisit_Report(db, SearchPersonID, id, order, sort, search, page, rows, ref   total);
            return queryData;
        }

        public List<CustomerNewFollowReportModel> GetCustomerNewFollow_Report(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            List<CustomerNewFollowReportModel> queryData = repository.GetCustomerNewFollow_Report(db, SearchPersonID, id, order, sort, search, page, rows, ref   total);
            return queryData;
        }

        public List<CustomerProcessReportModel> GetCustomerProcess_Report(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            List<CustomerProcessReportModel> queryData = repository.GetCustomerProcess_Report(db, SearchPersonID, id, order, sort, search, page, rows, ref   total);
            return queryData;
        }

        public List<ContractEntity> GetCustomerContract_Report(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            //List<CustomerContractReportModel> queryData = repository.GetCustomerContract_Report(db, SearchPersonID, id, order, sort, search, page, rows, ref   total);
            //return queryData;

            return new TBContractRepository().GetContractData(db, SearchPersonID, id, order, sort, search, page, rows,"", ref total);
        }
        /// <summary>
        /// 客户释放报表
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerReleaseReportModel> GetCustomerRelease_Report(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            List<CustomerReleaseReportModel> queryData = repository.GetCustomerRelease_Report(db, SearchPersonID, id, order, sort, search, page, rows, ref   total);
            return queryData;
        }


        /// <summary>
        /// 客户提取报表
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerExtractReportModel> GetCustomerExtract_Report(string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            List<CustomerExtractReportModel> queryData = repository.GetCustomerExtract_Report(db, id, order, sort, search, page, rows, ref   total);
            return queryData;
        }
        #endregion


        #region  获取无效客户

        /// <summary>
        /// 获取无效客户
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        public List<CustomerModel> GetLogoffData(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            List<CustomerModel> queryData = repository.GetLogoffData(db, id, order, sort, search, page, rows, ref   total);
            return queryData;
        }


        #endregion

        #region 客户从无效客户中激活 create by chand 2016-02-25

        /// <summary>
        /// 激活客户
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GetActivating(ref ValidationErrors validationErrors, string id)
        {
            try
            {
                TB_Customer entity = db.TB_Customer.FirstOrDefault(s => s.CustomerID == id);
                if (entity != null)
                {
                    entity.IsDelete = false;
                    entity.IsFrozen = true;
                    //entity.SysPersonID = "";
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

            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.ToString());
            }
            return false;
        }

        #endregion


        #region 获取是薪资客户的信息
        /// <summary>
        /// 获取是薪资客户的信息
        /// </summary>
        /// <returns></returns>
        public List<TB_Customer> GetSalaryCustomer()
        {
            return db.TB_Customer.Where(s => s.IsSalary == true).ToList();

        }
        #endregion

        #region 根据客户名称获取 create by chand 2016-04-07

        /// <summary>
        /// 根据客户名称获取
        /// </summary>
        /// <param name="CustomerName"></param>
        /// <returns></returns>
        public TB_Customer GetByName(string CustomerName)
        {

            return repository.GetByName(db, CustomerName);
        }
        #endregion

        #region 客户导入汇总报表  create by chand 2016-08-17

        /// <summary>
        /// 客户导入汇总报表
        /// </summary>
        /// <returns></returns>
        public List<ImportSummaryReportModel> GetImportSummary_Report(string sort, string order)
        {
            return repository.GetImportSummary_Report(db, sort, order);
        }
        #endregion
        public void Dispose()
        {

        }


        public List<CustomerServiceModel> GetCustomerServiceList(int page, int rows, string order, string sort, string search, ref int total)
        {
            return repository.GetCustomerServiceList(db, page, rows, order, sort, search, ref total);
        }

        public List<CustomerServiceModel> GetCustomerSelectedList(int page, int rows, string order, string sort, string search, ref int total)
        {
            return repository.GetCustomerSelectedList(db, page, rows, order, sort, search, ref total);
        }

        
        public List<HRListEntity> GetHRListInfo(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total)
        {
           return repository.GetHRListInfo(db, SearchPersonID, id, order, sort, search, page, rows, "", ref total);
        }

    }

}
