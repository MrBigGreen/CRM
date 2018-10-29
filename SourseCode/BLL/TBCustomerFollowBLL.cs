using Common;
using CRM.DAL;
using CRM.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRM.BLL
{
    /// <summary>
    /// Name:跟进任务Bll
    /// Author：Jonny
    /// Date:2015-6-11
    public class TBCustomerFollowBLL : ITBCustomerFollowBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;

        /// <summary>
        /// 人员的数据库访问对象
        /// </summary>
        TBCustomerFollowRepository repository = new TBCustomerFollowRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public TBCustomerFollowBLL()
        {
            db = new DB_CRMEntities();
            // offerdb = new OfferEntities();
        }



        public List<TB_CustomerFollow> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            IQueryable<TB_CustomerFollow> queryData = repository.DaoChuData(db, id, order, sort, search);
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

            }
            var list = queryData.ToList();
            foreach (var item in list)
            {
                if (!string.IsNullOrWhiteSpace(item.FollowUpMode))
                {
                    item.FollowUpMode = (from s in db.SysField where s.Id == item.FollowUpMode select s.MyTexts).SingleOrDefault();
                }
                if (!string.IsNullOrWhiteSpace(item.FollowUpCategory))
                {
                    item.FollowUpCategory = (from s in db.SysField where s.Id == item.FollowUpCategory select s.MyTexts).SingleOrDefault();
                }
                if (!string.IsNullOrWhiteSpace(item.OtherLevel))
                {
                    item.OtherLevel = (from s in db.SysField where s.Id == item.OtherLevel select s.MyTexts).SingleOrDefault();
                }
                if (!string.IsNullOrWhiteSpace(item.FollowUpPurpose))
                {
                    item.FollowUpPurpose = (from s in db.SysField where s.Id == item.FollowUpPurpose select s.MyTexts).SingleOrDefault();
                }
                if (!string.IsNullOrWhiteSpace(item.CustomerFeedback))
                {
                    item.CustomerFeedback = (from s in db.SysField where s.Id == item.CustomerFeedback select s.MyTexts).SingleOrDefault();
                }
                if (!string.IsNullOrWhiteSpace(item.CustomerFunnel))
                {
                    item.CustomerFunnel = (from s in db.SysField where s.Id == item.CustomerFunnel select s.MyTexts).SingleOrDefault();
                }
                if (!string.IsNullOrWhiteSpace(item.ProcessMode))
                {
                    item.ProcessMode = (from s in db.SysField where s.Id == item.ProcessMode select s.MyTexts).SingleOrDefault();
                }
                if (!string.IsNullOrWhiteSpace(item.Opportunities))
                {
                    item.Opportunities = (from s in db.SysField where s.Id == item.Opportunities select s.MyTexts).SingleOrDefault();
                }
                if (!string.IsNullOrWhiteSpace(item.NextFollowUpMode))
                {
                    item.NextFollowUpMode = (from s in db.SysField where s.Id == item.NextFollowUpMode select s.MyTexts).SingleOrDefault();
                }
                if (!string.IsNullOrWhiteSpace(item.SysPersonID))
                {
                    item.SysPersonName = (from s in db.SysPerson where s.Id == item.SysPersonID select s.MyName).SingleOrDefault();
                }
                if (!string.IsNullOrWhiteSpace(item.CustomerContactName))
                {
                    item.ContactName = item.CustomerContactName;//(from s in db.TB_CustomerContact where s.CustomerContactID == item.CustomerContactID select s.ContactName).SingleOrDefault();
                }
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
        public List<TB_CustomerFollow> GetByParam(string id, string order, string sort, string search)
        {
            // IQueryable<TB_CustomerFollow> queryData = repository.DaoChuData(db, order, sort, search);

            //return queryData.ToList();
            return null;
        }


        /// <summary>
        /// 下拉框
        /// </summary>
        /// <param name="type">0：SysField表查询；1:联系人</param>
        /// <param name="parm">参数</param>
        /// <returns></returns>
        public IQueryable GetCombox(int type, string parm)
        {
            IQueryable queryInfo = null;
            switch (type)
            {

                case 0:
                    queryInfo = from m in db.SysField
                                where m.ParentId == parm
                                orderby m.Sort
                                select new { id = m.Id, text = m.MyTexts };
                    break;
                case 1:

                    //queryInfo = (from c in db.TB_Customer where c.CustomerID == parm select new { id = c.ContactPerson + "," + c.ContactPhone, text = c.ContactPerson + "(" + c.ContactPhone + ")" })
                    //            .Union(from c in db.TB_Customer where c.CustomerID == parm select new { id = c.ContactPerson + "," + c.ContactTel, text = c.ContactPerson + "(" + c.ContactTel + ")" })
                    //            .Union(from m in db.TB_CustomerContact where m.CustomerID == parm && m.CompanyTel != null select new { id = m.ContactName + "," + m.CompanyTel, text = m.ContactName + "(" + m.CompanyTel + ")" })
                    //            .Union(from n in db.TB_CustomerContact where n.CustomerID == parm && n.CompanyTel2 != null select new { id = n.ContactName + "," + n.CompanyTel2, text = n.ContactName + "(" + n.CompanyTel2 + ")" })
                    //            .Union(from a in db.TB_CustomerContact where a.CustomerID == parm && a.PhoneNumber1 != null select new { id = a.ContactName + "," + a.PhoneNumber1, text = a.ContactName + "(" + a.PhoneNumber1 + ")" })
                    //            .Union(from b in db.TB_CustomerContact where b.CustomerID == parm && b.PhoneNumber2 != null select new { id = b.ContactName + "," + b.PhoneNumber2, text = b.ContactName + "(" + b.PhoneNumber2 + ")" });

                    queryInfo = (from c in db.TB_Customer where c.CustomerID == parm && c.ContactPhone != null select new { id = c.ContactPhone, text = c.ContactPerson })
                                .Union(from c in db.TB_Customer where c.CustomerID == parm && c.ContactTel != null select new { id = c.ContactTel, text = c.ContactPerson })
                                .Union(from m in db.TB_CustomerContact where m.CustomerID == parm && m.CompanyTel != null select new { id = m.CompanyTel, text = m.ContactName })
                                .Union(from n in db.TB_CustomerContact where n.CustomerID == parm && n.CompanyTel2 != null select new { id = n.CompanyTel2, text = n.ContactName })
                                .Union(from a in db.TB_CustomerContact where a.CustomerID == parm && a.PhoneNumber1 != null select new { id = a.PhoneNumber1, text = a.ContactName })
                                .Union(from b in db.TB_CustomerContact where b.CustomerID == parm && b.PhoneNumber2 != null select new { id = b.PhoneNumber2, text = b.ContactName });

                    //queryInfo = (from c in db.TB_Customer where c.CustomerID == parm select new { id = c.ContactPhone, text = c.ContactPerson + "(" + c.ContactPhone + ")" })
                    //            .Union(from c in db.TB_Customer where c.CustomerID == parm select new { id = c.ContactTel, text = c.ContactPerson + "(" + c.ContactTel + ")" })
                    //            .Union(from m in db.TB_CustomerContact where m.CustomerID == parm && m.CompanyTel != null select new { id = m.CompanyTel, text = m.ContactName + "(" + m.CompanyTel + ")" })
                    //            .Union(from n in db.TB_CustomerContact where n.CustomerID == parm && n.CompanyTel2 != null select new { id = n.CompanyTel2, text = n.ContactName + "(" + n.CompanyTel2 + ")" })
                    //            .Union(from a in db.TB_CustomerContact where a.CustomerID == parm && a.PhoneNumber1 != null select new { id = a.PhoneNumber1, text = a.ContactName + "(" + a.PhoneNumber1 + ")" })
                    //            .Union(from b in db.TB_CustomerContact where b.CustomerID == parm && b.PhoneNumber2 != null select new { id = b.PhoneNumber2, text = b.ContactName + "(" + b.PhoneNumber2 + ")" });


                    var ss = queryInfo.ToString();
                    break;
                default:
                    break;
            }
            return queryInfo;
        }
        /// <summary>
        /// 获取组员
        /// </summary>
        /// <returns></returns>
        public List<SysPerson> GetTeamPersons(string parm)
        {
            IQueryable<SysPerson> queryInfo;
            queryInfo = (from m in db.SysPerson
                         join n in db.SysDepartment on m.SysDepartmentId equals n.ParentId
                         join x in db.SysPerson on n.Id equals x.SysDepartmentId
                         where x.Id == parm || (x.State == "开启" && m.Id == parm)
                         orderby m.MyName
                         select x
                        );
            return queryInfo.ToList();

        }

        /// <summary>
        /// 获取跟进方式
        /// </summary>
        /// <returns></returns>
        public List<SysField> GetFollowMode()
        {
            IQueryable<SysField> queryInfo;
            queryInfo = (from m in db.SysField
                         where m.ParentId == "1506251032089851135c18a57f429"
                         orderby m.Sort
                         select m
                        );
            return queryInfo.ToList();

        }



        /// <summary>
        /// 获取联系人
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public List<TB_CustomerContact> GetContactsInfo(string CustomerID)
        {
            IQueryable<TB_CustomerContact> queryInfo;
            queryInfo = from m in db.TB_CustomerContact
                        where m.CustomerID == CustomerID
                        orderby m.LastUpdatedTime
                        select m;
            return queryInfo.ToList();
        }

        public List<TB_Customer> GetCustomerInfo(string CustomerID)
        {
            IQueryable<TB_Customer> queryInfo;
            queryInfo = from m in db.TB_Customer
                        where m.CustomerID == CustomerID
                        orderby m.LastUpdatedTime
                        select m;
            return queryInfo.ToList();
        }

        /// <summary>
        /// 我的任务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerFollowModel> GetMyTask(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            IQueryable<CustomerFollowModel> queryData = repository.MyTask(id, db, order, sort, search);
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

            }
            List<CustomerFollowModel> rim = new List<CustomerFollowModel>();
            List<CustomerFollowModel> list = queryData.ToList();
            foreach (var item in list)
            {
                item.CustomerLevel = (from e in db.SysField
                                      where e.Id == item.CustomerLevel
                                      select e.MyTexts).SingleOrDefault<string>();

                rim.Add(item);
            }
            return rim;


        }

        public List<CustomerFollowModel> GetCustomerFollowTask(string SysPersonID, int FollowType, string order, string sort, string search, int page, int rows, ref int total)
        {
            return repository.GetCustomerFollowTask(db, SysPersonID, FollowType, order, sort, search, page, rows, ref   total);
        }

        /// <summary>
        /// 跟进记录
        /// </summary>
        /// <param name="customerFollowID"></param>
        /// <param name="isLine"></param>
        /// <param name="isNextType"></param>
        /// <param name="customerPhone"></param>
        /// <param name="customerLevel"></param>
        /// <param name="customerFeedback"></param>
        /// <param name="customerFunnel"></param>
        /// <param name="customerOffer"></param>
        /// <param name="customerHandle"></param>
        /// <param name="callDate"></param>
        /// <param name="remark"></param>
        /// <param name="userID"></param>
        /// <param name="customerID"></param>
        /// <param name="cityName"></param>
        /// <param name="cityCode"></param>
        /// <param name="customerPurpose"></param>
        /// <param name="addressDetails"></param>
        /// <param name="provinceName"></param>
        /// <param name="provinceCode"></param>
        /// <param name="districtName"></param>
        /// <param name="districtCode"></param>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        //public bool AddFollowUpInfo(string customerFollowID, string isLine, string isNextType, string customerPhone, string customerLevel, string customerFeedback, string customerFunnel, string customerOffer, string customerHandle, string callDate, string remark, string userID, string customerID, string cityName, string cityCode, string customerPurpose, string addressDetails, string provinceName, string provinceCode, string districtName, string districtCode, string fileUrl)
        public bool AddFollowUpInfo(string customerFollowID, string isLine, string isNextType, string customerPhone, string customerLevel, string customerFunnel, string customerOffer, string callDate, string remark, string userID, string customerID, string cityName, string cityCode, string customerPurpose, string addressDetails, string provinceName, string provinceCode, string districtName, string districtCode, string fileUrl)
        {
            int i = repository.AddFollowUpInfo(db, customerFollowID, isLine, isNextType, customerPhone, customerLevel,
                         customerFunnel, customerOffer, callDate, remark, userID, customerID, cityName, cityCode, customerPurpose, addressDetails, provinceName, provinceCode, districtName, districtCode, fileUrl);
            if (i == 1)
            {
                return true;
            }

            return false;

        }
        /// <summary>
        /// 电话联系历史
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerFollowModel> GetCallHistroy(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            IQueryable<CustomerFollowModel> queryData = repository.CallHistroy(id, db, order, sort, search);
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

            }

            return queryData.ToList();

        }

        /// <summary>
        /// 见面约谈历史
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerFollowModel> GetMeetingHistroy(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            IQueryable<CustomerFollowModel> queryData = repository.MeetingHistroy(id, db, order, sort, search);
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

            }
            List<CustomerFollowModel> rim = new List<CustomerFollowModel>();
            List<CustomerFollowModel> list = queryData.ToList();
            foreach (var item in list)
            {
                item.CustomerLevel = (from e in db.SysField
                                      where e.Id == item.CustomerLevel
                                      select e.MyTexts).SingleOrDefault<string>();
                item.Opportunities = (from e in db.SysField
                                      where e.Id == item.Opportunities
                                      select e.MyTexts).SingleOrDefault<string>();
                item.CustomerFunnel = (from e in db.SysField
                                       where e.Id == item.CustomerFunnel
                                       select e.MyTexts).SingleOrDefault<string>();
                rim.Add(item);
            }
            return rim;

        }


        /// <summary>
        ///  导出
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="sortOrder"></param>
        /// <param name="sortName"></param>
        /// <param name="cid"></param>
        /// <param name="search"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<CustomerFollowModel> ExportInfo(string userID, string sortOrder, string sortName, string cid, string search, string type)
        {
            IQueryable<CustomerFollowModel> queryData = repository.GetExportInfos(userID, db, sortOrder, sortName, cid, search, type);

            return queryData.ToList();
        }

        #region 是否已联系

        /// <summary>
        /// 是否已联系
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public bool IsContact(string CustomerID, string CurrentPersonID, int FollowType)
        {
            var m = db.TB_CustomerFollow.Where(s => s.CustomerID == CustomerID && s.ContactState == 0 && s.IsDelete == false && s.CreatedBy == CurrentPersonID && s.FollowType == FollowType).ToList();
            if (m == null || m.Count < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region 基本操作
        /// <summary>
        /// 创建一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个客户</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, TB_CustomerFollow entity)
        {

            if (!IsContact(entity.CustomerID, entity.CreatedBy, (int)entity.FollowType))
            {
                validationErrors.Add("创建失败，已有跟进未完成");
                return false;
            }

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
        public bool Create(ref ValidationErrors validationErrors, TB_CustomerFollow entity)
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
        ///  创建客户集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">客户集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<TB_CustomerFollow> entitys)
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
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<TB_CustomerFollow> entitys)
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
        public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, TB_CustomerFollow entity)
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





            //entity.TB_RecommendSolution.Remove(item)
            //entity.RecommendSolutionID


            TB_CustomerFollow editEntity = repository.Edit(db, entity);


            TBRecommendSolutionRepository solutionRepository = new TBRecommendSolutionRepository();
            List<string> addSolutionId = new List<string>();
            List<string> deleteSolutionId = new List<string>();
            List<TB_RecommendSolution> listEntitySysRole = new List<TB_RecommendSolution>();
            DataOfDiffrent.GetDiffrent(entity.RecommendSolutionIDNew.ToList(), entity.RecommendSolutionIDOld.ToList(), ref addSolutionId, ref deleteSolutionId);

            if (deleteSolutionId != null && deleteSolutionId.Count() > 0)
            {
                foreach (var item in deleteSolutionId)
                {
                    solutionRepository.Delete(item);
                }
            }

            if (addSolutionId != null && addSolutionId.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("delete TB_RecommendSolution where CustomerID='{0}'", entity.CustomerID);
                foreach (var item in addSolutionId)
                {
                    sb.AppendFormat("insert into dbo.TB_RecommendSolution(RecommendSolutionID,CustomerID)values('{0}','{1}')", item, entity.CustomerID);
                    sb.AppendFormat("insert into TB_CustomerFollowSolution(RecommendSolutionID,CustomerFollowID)values('{0}','{1}')", item, entity.CustomerFollowID);
                }
                db.Database.ExecuteSqlCommand(sb.ToString());
            }
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
        public bool Edit(ref ValidationErrors validationErrors, TB_CustomerFollow entity)
        {
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    if (Edit(ref validationErrors, db, entity))
                    {

                        transactionScope.Complete();

                        //TBRecommendSolutionRepository solutionRepository = new TBRecommendSolutionRepository();
                        //List<string> addSolutionId = new List<string>();
                        //List<string> deleteSolutionId = new List<string>();
                        //List<TB_RecommendSolution> listEntitySysRole = new List<TB_RecommendSolution>();
                        //DataOfDiffrent.GetDiffrent(entity.RecommendSolutionIDNew.ToList(), entity.RecommendSolutionIDOld.ToList(), ref addSolutionId, ref deleteSolutionId);

                        //if (deleteSolutionId != null && deleteSolutionId.Count() > 0)
                        //{
                        //    foreach (var item in deleteSolutionId)
                        //    {
                        //        solutionRepository.Delete(item);
                        //    }
                        //}

                        //if (addSolutionId != null && addSolutionId.Count() > 0)
                        //{
                        //    foreach (var item in addSolutionId)
                        //    {
                        //        TB_RecommendSolution newEntity = new TB_RecommendSolution();
                        //        newEntity.CustomerFollowID = entity.CustomerFollowID;
                        //        newEntity.RecommendSolutionID = item;
                        //        newEntity.TB_CustomerFollow = entity;
                        //        solutionRepository.Create(db, newEntity);
                        //        entity.TB_RecommendSolution.Add(newEntity);
                        //        solutionRepository.Save(db);
                        //    }
                        //}


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
        public List<TB_CustomerFollow> GetAll()
        {
            return repository.GetAll(db).ToList();
        }

        /// <summary>
        /// 根据主键获取一个客户
        /// </summary>
        /// <param name="id">客户的主键</param>
        /// <returns>一个客户</returns>
        public TB_CustomerFollow GetById(string id)
        {
            return repository.GetById(db, id);
        }

        #endregion

        #region 跟进历史

        /// <summary>
        /// 跟进历史
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerFollowModel> GetFollowBackHistoryByCustomerID(string id, int FollowType, int page, int rows, string search, ref int total)
        {
            return repository.GetFollowBackHistoryByCustomerID(db, id, FollowType, page, rows, search, ref total);
        }

        /// <summary>
        /// 跟进历史
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerFollowModel> GetFollowBackHistory(int page, int rows, string search, ref int total)
        {
            return repository.GetFollowBackHistory(db, page, rows, search, ref total);
        }
        #endregion

        #region 最后一次跟进未反馈记录

        /// <summary>
        /// 最后一次跟进未反馈记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public TB_CustomerFollow GetLastCustomerFollow(string id)
        {

            return repository.GetLastCustomerFollow(db, id);
        }

        #endregion

        #region 获取客户最后一次联系人
        /// <summary>
        /// 获取客户最后一次联系人
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ContactName"></param>
        /// <param name="ContactPhone"></param>
        public TB_CustomerFollow GetLastContactInfo(string CustomerID, string SysPersonID)
        {
            return repository.GetLastContactInfo(db, CustomerID, SysPersonID);
        }

        #endregion


        public void Dispose()
        {
        }

    }
}
