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
    public class TBContractBLL : ITBContractBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;

        /// <summary>
        /// 人员的数据库访问对象
        /// </summary>
        TBContractRepository repository = new TBContractRepository();

        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public TBContractBLL()
        {
            db = new DB_CRMEntities();
        }
        public void Dispose()
        {
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
        /// 获取合同套餐
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public List<SysField> GetPackage(string parm)
        {
            IQueryable<SysField> queryInfo;
            queryInfo = db.SysField.Where(s => s.ParentId.Equals(parm)).OrderBy(t => t.Sort);
            return queryInfo.ToList();
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
                    queryInfo = from m in db.TB_CustomerContact
                                where m.CustomerID == parm
                                orderby m.LastUpdatedTime
                                select new { id = m.CustomerContactID, text = m.ContactName + "," + m.CompanyTel };
                    break;
                default:
                    break;
            }
            return queryInfo;
        }

        /// <summary>
        /// 保存合同信息
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="companyCode"></param>
        /// <param name="cityCode"></param>
        /// <param name="cityName"></param>
        /// <param name="packages"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="packageMoney"></param>
        /// <param name="uploadPackage"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool AddPackageInfo(string companyName, string companyCode, string cityCode, string cityName, string packages, string beginDate, string endDate, string packageMoney, string uploadPackage, string userID)
        {
            int i = repository.AddPackageInfo(db, companyName, companyCode, cityCode, cityName, packages, beginDate, endDate, packageMoney, uploadPackage, userID);
            if (i == 1)
            {
                return true;
            }

            return false;

        }
        /// <summary>
        /// Check公司是否已经签合同
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public bool CheckCompanyContract(string companyCode)
        {
            DateTime? now = DateTime.Parse(DateTime.Now.ToShortDateString());
            var isCheck = true;
            var query = db.TB_Contract.Where(s => s.CustomerID.Equals(companyCode) && s.Deadline < now).Count();
            if (query > 0)
            {
                isCheck = false;
            }
            return isCheck;
        }


        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<ContractModel> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {

            IQueryable<ContractModel> queryData = repository.GetContractInfo(id, db, order, sort, search);
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

        public List<ContractModel> ExportInfo(string userID, string sortOrder, string sortName, string cid, string search)
        {
            IQueryable<ContractModel> queryData = repository.GetExportInfo(userID, db, sortOrder, sortName, cid, search);

            return queryData.ToList();


        }



        /////

        /// <summary>
        /// 创建一个客户联系人
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个客户联系人</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, TB_Contract entity)
        {
            if (entity == null)
            {
                return false;
            }
            int count = 1;


            //TBRecommendSolutionRepository solutionRepository = new TBRecommendSolutionRepository();
            List<string> addSolutionId = entity.RecommendSolutionIDs.ToList();

            if (addSolutionId != null && addSolutionId.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var item in addSolutionId)
                {
                    sb.AppendFormat("insert into dbo.TB_ContractSolution(RecommendSolutionID,ContractID)values('{0}','{1}')", item, entity.ContractID);
                }
                db.Database.ExecuteSqlCommand(sb.ToString());
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
        /// 创建一个客户联系人
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个客户联系人</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, TB_Contract entity)
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
                Transaction.Current.Rollback();
            }

            return false;
        }
        /// <summary>
        ///  创建客户联系人集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">客户联系人集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<TB_Contract> entitys)
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
        ///  创建客户联系人集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">客户联系人集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<TB_Contract> entitys)
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
        /// 编辑一个客户联系人
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据上下文</param>
        /// <param name="entity">一个客户联系人</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, TB_Contract entity)
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

            TB_Contract editEntity = repository.Edit(db, entity);
            if (count == repository.Save(db))
            {
                return true;
            }
            else
            {
                validationErrors.Add("编辑客户联系人出错了");
            }
            return false;
        }
        /// <summary>
        /// 编辑一个客户联系人
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个客户联系人</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, TB_Contract entity)
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
        public List<TB_Contract> GetAll()
        {
            return repository.GetAll(db).ToList();
        }

        /// <summary>
        /// 根据主键获取一个客户联系人
        /// </summary>
        /// <param name="id">客户联系人的主键</param>
        /// <returns>一个客户联系人</returns>
        public TB_Contract GetById(string id)
        {
            var entity = repository.GetById(db, id);
            if (entity != null)
            {
                if (!string.IsNullOrWhiteSpace(entity.CustomerID))
                {

                    string customerID = entity.CustomerID;
                    //获取客户名称
                    entity.CustomerName = (from s in db.TB_Customer where s.CustomerID == customerID select s).SingleOrDefault().CustomerName;

                    //获取客服
                    var pList = (from s in db.TB_CustomerService
                                 join f in db.SysPerson on s.SysPersonID equals f.Id
                                 where s.CustomerID == customerID
                                 select new CustomerServiceEntity
                                 {
                                     CustomerID = s.CustomerID,
                                     SysPersonID = s.SysPersonID,
                                     SysPersonName = f.MyName
                                 }).ToList();
                    for (int i = 0; i < pList.Count; i++)
                    {
                        entity.SysPersonNames += pList[i].SysPersonName + "，";
                        entity.SysPersonID += pList[i].SysPersonID + "，";
                    }

                }
                string SigningCompanyID = entity.SigningCompany;
                var field = db.SysField.SingleOrDefault(s => s.Id == SigningCompanyID);
                if (field != null)
                {
                    entity.SigningCompanyName = field.MyTexts;
                }


                //获取合作服务
                var list = (from s in db.TB_ContractSolution
                            join f in db.SysField on s.RecommendSolutionID equals f.Id
                            where s.ContractID == id
                            select new ContractSolution
                            {
                                ContractID = s.ContractID,
                                RecommendSolutionID = s.RecommendSolutionID,
                                RecommendSolutionName = f.MyTexts


                            }).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    entity.RecommendSolutionIDs += list[i].RecommendSolutionID + "，";
                    entity.RecommendSolutionNames += list[i].RecommendSolutionName + "，";
                }

            }
            return entity;
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
                TB_Contract entity = db.TB_Contract.FirstOrDefault(s => s.ContractID == id);
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
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }

        /// <summary>
        /// 合同查询
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<ContractEntity> GetContractData(string SearchPersonID, string id, string order, string sort, string search, int page, int rows, string contractType, ref int total)
        {
            return repository.GetContractData(db, SearchPersonID, id, order, sort, search, page, rows, contractType, ref total);
        }


        /// <summary>
        /// 合同查询
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<ContractEntity> GetContractByServiceData(string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            return repository.GetContractByServiceData(db, SearchPersonID, id, order, sort, search, page, rows, ref total);
        }
        public List<ContractEntity> GetContractByServiceDataByType(string SearchPersonID, string id, string order, string sort, string search, string CompanyType, int page, int rows, ref int total)
        {
            return repository.GetContractByServiceDataByType(db, SearchPersonID, id, order, sort, search, CompanyType, page, rows, ref total);
        }

        #region 计算合同编号


        /// <summary>
        /// 根据前置码计算最大合同编号
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="PrefixCode"></param>
        /// <param name="MaxContractNO"></param>
        /// <returns></returns>
        public bool GetMaxContractNO(ref ValidationErrors validationErrors, string PrefixCode, ref string MaxContractNO)
        {
            if (string.IsNullOrWhiteSpace(PrefixCode))
            {
                validationErrors.Add("前置码不能空");
                return false;
            }
            PrefixCode = PrefixCode + DateTime.Now.ToString("yyyyMMdd") + "-";


            long MaxNo = repository.GetMaxContractNo(db, PrefixCode);
            if (MaxNo < 0)
            {
                return false;
            }
            MaxNo = MaxNo + 1;
            MaxContractNO = PrefixCode + MaxNo.ToString("0000");

            return true;
        }

        public int GetContractNOCount(string ContractNO)
        {
            return repository.GetContractNOCount(db, ContractNO);
        }

        /// <summary>
        /// 根据合作方案获取最大合同编号
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="SolutionID"></param>
        /// <param name="MaxContractNO"></param>
        /// <returns></returns>
        public bool GetMaxContractNOBySolutionID(ref ValidationErrors validationErrors, string SolutionID, ref string MaxContractNO)
        {
            if (string.IsNullOrWhiteSpace(SolutionID))
            {
                validationErrors.Add("合作方案不能空");
                return false;
            }
            var entity = db.SysField.Where(s => s.Id == SolutionID).SingleOrDefault();
            if (entity == null)
            {
                validationErrors.Add("合作方案不存在");
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.DataValue))
            {
                validationErrors.Add("合作方案的编号规则未找到");
                return false;
            }
            return GetMaxContractNO(ref validationErrors, entity.DataValue, ref MaxContractNO);

        }

        #endregion


        #region 使用SQL语句创建合同 create by chand 2016-09-01
        /// <summary>
        /// 使用SQL语句创建合同
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool SqlCreate(ref ValidationErrors validationErrors, TB_Contract entity)
        {
            List<string> addSolutionId = entity.RecommendSolutionIDs.ToList();
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO TB_Contract( ContractID,CustomerID,CustomerName,ContractMoney,EffectiveDate,Deadline,Package,Annex,Years,ContractCount,ContractNO,SigningCompany,IsDelete,VersionNo,CreatedBy,CreatedTime,LastUpdatedBy,LastUpdatedTime)");
            sb.AppendFormat("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',0,1,'{12}',GETDATE(),'{13}',GETDATE()); ",
                entity.ContractID, entity.CustomerID, entity.CustomerName, entity.ContractMoney, entity.EffectiveDate, entity.Deadline, entity.Package, entity.Annex, entity.Years, entity.ContractCount, entity.ContractNO, entity.SigningCompany, entity.CreatedBy, entity.LastUpdatedBy);
            if (addSolutionId != null && addSolutionId.Count() > 0)
            {
                foreach (var item in addSolutionId)
                {
                    sb.AppendFormat("insert into dbo.TB_ContractSolution(RecommendSolutionID,ContractID)values('{0}','{1}') ; ", item, entity.ContractID);
                }
            }
            try
            {
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
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }


        #endregion

        #region 根据合同ID，获取数据 create by chand 2017-02-08
        /// <summary>
        /// 根据合同ID，获取数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ContractEntity GetEntity(string keyValue)
        {
            return repository.GetEntity(db, keyValue);
        }
        #endregion
    }
}
