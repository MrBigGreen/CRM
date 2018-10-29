﻿using Offertech.Application.Code;
using Offertech.Application.Entity.FlowManage;
using Offertech.Application.Entity.SystemManage;
using Offertech.Application.IService.FlowManage;
using Offertech.Application.IService.SystemManage;
using Offertech.Application.Service.SystemManage;
using Offertech.Data;
using Offertech.Data.Repository;
using Offertech.Util;
using Offertech.Util.FlowWork;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Linq;
using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.Service.CustomerManage;
using Offertech.Application.IService.CustomerManage;


namespace Offertech.Application.Service.FlowManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 15:54
    /// 描 述：工作流实例表操作
    /// </summary>
    public class WFProcessInstanceService : RepositoryFactory, WFProcessInstanceIService
    {
        private IDataBaseLinkService dataBaseLinkService = new DataBaseLinkService();
        private ICodeRuleService coderuleService = new CodeRuleService();

        #region 获取数据
        /// <summary>
        /// 获取流程监控数据（用于流程监控）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                w.Id,
                                    w.Code,
                                    w.SchemeName,
                                    w.CustomName,
                                    w.wfLevel,
	                                w.ActivityId,
	                                w.ActivityName,
	                                w.ActivityType,
	                                w.ProcessSchemeId,
                                    w.SchemeType,
                                    t2.ItemName AS SchemeTypeName,
                                    w.EnabledMark,
	                                w.CreateDate,
	                                w.CreateUserId,
	                                w.CreateUserName,
                                    ISNULL(w.Description,'') as Description,
                                    w.isFinish
                                FROM
	                                WF_ProcessInstance w
                                LEFT JOIN
                                    WF_ProcessScheme w1 ON w1.Id = w.ProcessSchemeId
                                LEFT JOIN
	                                Base_DataItemDetail t2 ON t2.ItemDetailId = w.SchemeType
                                WHERE w.EnabledMark != 3 ");//3表示草稿
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                if (!queryParam["WFSchemeInfoId"].IsEmpty())
                {
                    strSql.Append(" AND w1.WFSchemeInfoId = @WFSchemeInfoId ");
                    parameter.Add(DbParameters.CreateDbParameter("@WFSchemeInfoId", queryParam["WFSchemeInfoId"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.Code LIKE @keyword 
                                        or w.CustomName LIKE @keyword 
                                        or w.CreateUserName LIKE @keyword 
                    )");
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取流程实例分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="type">3草稿</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson, string type)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                w.Id,
                                    w.Code,
                                    w.SchemeName,
                                    w.CustomName,
                                    w.wfLevel,
	                                w.ActivityId,
	                                w.ActivityName,
	                                w.ActivityType,
	                                w.ProcessSchemeId,
                                    w.SchemeType,
                                    t2.ItemName AS SchemeTypeName,
                                    w.EnabledMark,
	                                w.CreateDate,
	                                w.CreateUserId,
	                                w.CreateUserName,
                                    ISNULL(w.Description,'') as Description,
                                    w.isFinish
                                FROM
	                                WF_ProcessInstance w
                                LEFT JOIN 
	                                Base_DataItemDetail t2 ON t2.ItemDetailId = w.SchemeType");//3表示草稿
                if (type == "3")
                {
                    strSql.Append(@" WHERE w.EnabledMark = 3 AND w.isFinish != 2 ");
                }
                else
                {
                    strSql.Append(@" WHERE w.EnabledMark != 3 AND w.isFinish != 2 ");
                }

                if (!OperatorProvider.Provider.Current().LoginInfo.IsSystem)
                {
                    strSql.Append(string.Format(" AND w.CreateUserId = '{0}' ", OperatorProvider.Provider.Current().LoginInfo.UserId));
                }

                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                if (!queryParam["SchemeType"].IsEmpty())
                {
                    strSql.Append(" AND w.SchemeType = @SchemeType ");
                    parameter.Add(DbParameters.CreateDbParameter("@SchemeType", queryParam["SchemeType"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.Code LIKE @keyword 
                                        or w.CustomName LIKE @keyword 
                                        or w.CreateUserName LIKE @keyword 
                    )");
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取登录者需要处理的流程
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetToMeBeforePageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                w.Id,
                                    w.Code,
                                    w.SchemeName,
									w.CustomName,
	                                w.ActivityId,
	                                w.ActivityName,
	                                w.ActivityType,
	                                w.ProcessSchemeId,
                                    w.SchemeType,
                                    t2.ItemName AS SchemeTypeName,
                                    w.MakerList,
                                    w.EnabledMark,
	                                w.CreateDate,
	                                w.CreateUserId,
	                                w.CreateUserName,
                                    w.isFinish,
                                    ISNULL(w.Description,'') as Description,
                                    w.wfLevel,
                                    p.WFSchemeInfoId
                                FROM
	                                WF_ProcessInstance w
                                LEFT JOIN 
	                                Base_DataItemDetail t2 ON t2.ItemDetailId = w.SchemeType
                                    LEFT JOIN WF_ProcessScheme p ON w.ProcessSchemeId=p.Id
                                WHERE w.EnabledMark = 1 AND w.isFinish = 0 ");
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();

                if (!queryParam["SchemeType"].IsEmpty())
                {
                    strSql.Append(" AND w.SchemeType = @SchemeType ");
                    parameter.Add(DbParameters.CreateDbParameter("@SchemeType", queryParam["SchemeType"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.Code LIKE @keyword 
                                        or w.CustomName LIKE @keyword 
                                        or w.CreateUserName LIKE @keyword 
                    )");

                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                strSql.Append(string.Format(@" AND ( w.MakerList LIKE '{0}' or  w.MakerList = '1' ", OperatorProvider.Provider.Current().LoginInfo.UserId));
                foreach (var objectid in OperatorProvider.Provider.Current().LoginInfo.ObjectId.Split(','))
                {
                    strSql.Append(string.Format(@" or w.MakerList LIKE '{0}' ", objectid));
                }
                strSql.Append(")");
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取登录者需要处理的流程
        /// </summary>
        /// <returns></returns>
        public int GetToMeBeforeCount()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT  COUNT(*)
                                FROM
	                                WF_ProcessInstance w
                                LEFT JOIN 
	                                Base_DataItemDetail t2 ON t2.ItemDetailId = w.SchemeType
                                    LEFT JOIN WF_ProcessScheme p ON w.ProcessSchemeId=p.Id
                                WHERE w.EnabledMark = 1 AND w.isFinish = 0 ");
                var parameter = new List<DbParameter>();

                strSql.Append(string.Format(@" AND ( w.MakerList LIKE '{0}' or  w.MakerList = '1' ", OperatorProvider.Provider.Current().LoginInfo.UserId));
                foreach (var objectid in OperatorProvider.Provider.Current().LoginInfo.ObjectId.Split(','))
                {
                    strSql.Append(string.Format(@" or w.MakerList LIKE '{0}' ", objectid));
                }
                strSql.Append(")");
                return this.BaseRepository().FindObject(strSql.ToString(), parameter.ToArray()).ToInt();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 获取登录者已经处理的流程
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetToMeAfterPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(string.Format(@"SELECT
	                                w.Id,
	                                w.Code,
                                    w.SchemeName,
									w.CustomName,
	                                w.ActivityId,
	                                w.ActivityName,
	                                w.ActivityType,
	                                w.ProcessSchemeId,
	                                w.SchemeType,
	                                t2.ItemName AS SchemeTypeName,
	                                w.MakerList,
	                                w.EnabledMark,
	                                w.CreateDate,
	                                w.CreateUserId,
	                                w.CreateUserName,
	                                w2.Content,
                                    w.isFinish,
                                    ISNULL(w.Description,'') as Description,
                                    w.wfLevel,
                                    p.WFSchemeInfoId
                                FROM
	                                WF_ProcessInstance w
                                LEFT JOIN Base_DataItemDetail t2 ON t2.ItemDetailId = w.SchemeType
                                LEFT JOIN WF_ProcessOperationHistory w2 ON w2.ProcessId = w.Id
                                LEFT JOIN WF_ProcessScheme p ON w.ProcessSchemeId=p.Id
                                WHERE
	                                w.EnabledMark = 1 AND w2.CreateUserId = '{0}' And w.CreateUserId != '{0}' ", OperatorProvider.Provider.Current().LoginInfo.UserId));
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();

                if (!queryParam["SchemeType"].IsEmpty())
                {
                    strSql.Append(" AND w.SchemeType = @SchemeType ");
                    parameter.Add(DbParameters.CreateDbParameter("@SchemeType", queryParam["SchemeType"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.Code LIKE @keyword 
                                        or w.CustomName LIKE @keyword 
                                        or w.CreateUserName LIKE @keyword 
                    )");

                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取实例进程信息实体
        /// </summary>
        /// <param name="keyVlaue"></param>
        /// <returns></returns>
        public WFProcessInstanceEntity GetEntity(string keyVlaue)
        {
            try
            {
                return this.BaseRepository().FindEntity<WFProcessInstanceEntity>(keyVlaue);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 存储工作流实例进程(编辑草稿用)
        /// </summary>
        /// <param name="processInstanceEntity"></param>
        /// <param name="processSchemeEntity"></param>
        /// <param name="wfOperationHistoryEntity"></param>
        /// <returns></returns>
        public int SaveProcess(string processId, WFProcessInstanceEntity processInstanceEntity, WFProcessSchemeEntity processSchemeEntity, WFProcessOperationHistoryEntity wfOperationHistoryEntity = null)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                //草稿不需要流程编号
                processInstanceEntity.Code = "";
                if (string.IsNullOrEmpty(processInstanceEntity.Id))
                {
                    processSchemeEntity.Create();
                    db.Insert(processSchemeEntity);

                    processInstanceEntity.Create();
                    processInstanceEntity.Id = processId;
                    processInstanceEntity.ProcessSchemeId = processSchemeEntity.Id;
                    db.Insert(processInstanceEntity);
                }
                else
                {
                    processInstanceEntity.Modify(processId);
                    db.Update(processInstanceEntity);

                    processSchemeEntity.Modify(processInstanceEntity.ProcessSchemeId);
                    db.Update(processSchemeEntity);
                }
                if (wfOperationHistoryEntity != null)
                {
                    wfOperationHistoryEntity.Create();
                    wfOperationHistoryEntity.ProcessId = processId;
                    db.Insert(wfOperationHistoryEntity);
                }
                db.Commit();
                return 1;
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 存储工作流实例进程(创建实例进程)
        /// </summary>
        /// <param name="wfRuntimeModel"></param>
        /// <param name="processInstanceEntity"></param>
        /// <param name="processSchemeEntity"></param>
        /// <param name="processOperationHistoryEntity"></param>
        /// <param name="delegateRecordEntity"></param>
        /// <returns></returns>
        public int SaveProcess(WF_RuntimeModel wfRuntimeModel, WFProcessInstanceEntity processInstanceEntity, WFProcessSchemeEntity processSchemeEntity, WFProcessOperationHistoryEntity processOperationHistoryEntity, WFProcessTransitionHistoryEntity processTransitionHistoryEntity, List<WFDelegateRecordEntity> delegateRecordEntityList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                //占用单据号
                coderuleService.UseRuleSeed(OperatorProvider.Provider.Current().LoginInfo.UserId, "", "10007", db);

                if (string.IsNullOrEmpty(processInstanceEntity.Id))
                {
                    processSchemeEntity.Create();
                    db.Insert(processSchemeEntity);

                    processInstanceEntity.Create();
                    processInstanceEntity.Id = wfRuntimeModel.processId;
                    processInstanceEntity.ProcessSchemeId = processSchemeEntity.Id;
                    db.Insert(processInstanceEntity);
                }
                else
                {
                    processInstanceEntity.Modify(processInstanceEntity.Id);
                    db.Update(processSchemeEntity);
                    db.Update(processInstanceEntity);
                }
                processOperationHistoryEntity.Create();
                processOperationHistoryEntity.ProcessId = processInstanceEntity.Id;
                db.Insert(processOperationHistoryEntity);

                if (processTransitionHistoryEntity != null)
                {
                    processTransitionHistoryEntity.Create();
                    processTransitionHistoryEntity.ProcessId = processInstanceEntity.Id;
                    db.Insert(processTransitionHistoryEntity);
                }
                foreach (var item in delegateRecordEntityList)
                {
                    item.Create();
                    item.ProcessId = processInstanceEntity.Id;
                    db.Insert(item);
                }
                if (processInstanceEntity.FrmType == 0)
                {
                    DataBaseLinkEntity dataBaseLinkEntity = dataBaseLinkService.GetEntity(wfRuntimeModel.schemeContentJson.Frm.FrmDbId.Value);//获取
                    if (wfRuntimeModel.schemeContentJson.Frm.isSystemTable.Value != 0)
                    {
                        //是否执行插入数据  外包员工薪资申请流程处理  create by chand 2016-11-29
                        if (processSchemeEntity.WFSchemeInfoId != "3416863b-d5c1-4b31-b59f-e5fd8cd2d5f1")
                        {
                            this.BaseRepository(dataBaseLinkEntity.DbConnection).ExecuteBySql(wfRuntimeModel.sqlFrm);
                        }
                    }
                }
                db.Commit();
                return 1;
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 存储工作流实例进程（审核驳回重新提交）
        /// </summary>
        /// <param name="processInstanceEntity"></param>
        /// <param name="processSchemeEntity"></param>
        /// <param name="processOperationHistoryEntity"></param>
        /// <param name="processTransitionHistoryEntity"></param>
        /// <returns></returns>
        public int SaveProcess(WFProcessInstanceEntity processInstanceEntity, WFProcessSchemeEntity processSchemeEntity, WFProcessOperationHistoryEntity processOperationHistoryEntity, List<WFDelegateRecordEntity> delegateRecordEntityList, WFProcessTransitionHistoryEntity processTransitionHistoryEntity = null)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {

                processInstanceEntity.Modify(processInstanceEntity.Id);
                db.Update(processSchemeEntity);
                db.Update(processInstanceEntity);

                processOperationHistoryEntity.Create();
                processOperationHistoryEntity.ProcessId = processInstanceEntity.Id;
                db.Insert(processOperationHistoryEntity);

                if (processTransitionHistoryEntity != null)
                {
                    processTransitionHistoryEntity.Create();
                    processTransitionHistoryEntity.ProcessId = processInstanceEntity.Id;
                    db.Insert(processTransitionHistoryEntity);
                }
                if (delegateRecordEntityList != null)
                {
                    foreach (var item in delegateRecordEntityList)
                    {
                        item.Create();
                        item.ProcessId = processInstanceEntity.Id;
                        db.Insert(item);
                    }
                }
                db.Commit();
                return 1;
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        ///  更新流程实例 审核节点用
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbbaseId"></param>
        /// <param name="processInstanceEntity"></param>
        /// <param name="processSchemeEntity"></param>
        /// <param name="processOperationHistoryEntity"></param>
        /// <param name="delegateRecordEntityList"></param>
        /// <param name="processTransitionHistoryEntity"></param>
        /// <returns></returns>
        public int SaveProcess(string sql, string dbbaseId, WFProcessInstanceEntity processInstanceEntity, WFProcessSchemeEntity processSchemeEntity, WFProcessOperationHistoryEntity processOperationHistoryEntity, List<WFDelegateRecordEntity> delegateRecordEntityList, WFProcessTransitionHistoryEntity processTransitionHistoryEntity = null)
        {

            #region 这里才是正常流程处理

            IRepository db2 = this.BaseRepository().BeginTrans();
            try
            {
                processInstanceEntity.Modify(processInstanceEntity.Id);
                db2.Update(processSchemeEntity);
                db2.Update(processInstanceEntity);

                processOperationHistoryEntity.Create();
                processOperationHistoryEntity.ProcessId = processInstanceEntity.Id;
                db2.Insert(processOperationHistoryEntity);

                if (processTransitionHistoryEntity != null)
                {
                    processTransitionHistoryEntity.Create();
                    processTransitionHistoryEntity.ProcessId = processInstanceEntity.Id;
                    db2.Insert(processTransitionHistoryEntity);
                }
                if (delegateRecordEntityList != null)
                {
                    foreach (var item in delegateRecordEntityList)
                    {
                        item.Create();
                        item.ProcessId = processInstanceEntity.Id;
                        db2.Insert(item);
                    }
                }
                db2.Commit();
                return 1;
            }
            catch
            {
                db2.Rollback();
                throw;
            }

            #endregion

        }
        /// <summary>
        ///  更新流程实例 审核节点用
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbbaseId"></param>
        /// <param name="processInstanceEntity"></param>
        /// <param name="processSchemeEntity"></param>
        /// <param name="processOperationHistoryEntity"></param>
        /// <param name="delegateRecordEntityList"></param>
        /// <param name="processTransitionHistoryEntity"></param>
        /// <returns></returns>
        public int SaveProcess2(string sql, string dbbaseId, WFProcessInstanceEntity processInstanceEntity, WFProcessSchemeEntity processSchemeEntity, WFProcessOperationHistoryEntity processOperationHistoryEntity, List<WFDelegateRecordEntity> delegateRecordEntityList, WFProcessTransitionHistoryEntity processTransitionHistoryEntity = null)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {

                processInstanceEntity.Modify(processInstanceEntity.Id);
                db.Update(processSchemeEntity);
                db.Update(processInstanceEntity);

                processOperationHistoryEntity.Create();
                processOperationHistoryEntity.ProcessId = processInstanceEntity.Id;
                db.Insert(processOperationHistoryEntity);

                if (processTransitionHistoryEntity != null)
                {
                    processTransitionHistoryEntity.Create();
                    processTransitionHistoryEntity.ProcessId = processInstanceEntity.Id;
                    db.Insert(processTransitionHistoryEntity);
                }
                if (delegateRecordEntityList != null)
                {
                    foreach (var item in delegateRecordEntityList)
                    {
                        item.Create();
                        item.ProcessId = processInstanceEntity.Id;
                        db.Insert(item);
                    }
                }
                
                db.Commit();
                return 1;
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 保存工作流进程实例
        /// </summary>
        /// <param name="processInstanceEntity"></param>
        /// <returns></returns>
        public int SaveProcess(WFProcessInstanceEntity processInstanceEntity)
        {
            try
            {
                int num;
                WFProcessInstanceEntity isExistEntity = this.BaseRepository().FindEntity<WFProcessInstanceEntity>(processInstanceEntity.Id);
                if (isExistEntity == null)
                {
                    processInstanceEntity.Create();
                    num = this.BaseRepository().Insert(processInstanceEntity);
                }
                else
                {
                    processInstanceEntity.Modify(processInstanceEntity.Id);
                    num = this.BaseRepository().Update(processInstanceEntity);
                }
                return num;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除工作流实例进程(删除草稿使用)
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public int DeleteProcess(string keyValue)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                WFProcessInstanceEntity entity = this.BaseRepository().FindEntity<WFProcessInstanceEntity>(keyValue);
                db.Delete<WFProcessSchemeEntity>(entity.ProcessSchemeId);
                db.Delete<WFProcessInstanceEntity>(keyValue);
                db.Commit();
                return 1;
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 虚拟操作实例
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="state">0暂停,1启用,2取消（召回）</param>
        /// <returns></returns>
        public int OperateVirtualProcess(string keyValue, int state)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                WFProcessInstanceEntity entity = this.BaseRepository().FindEntity<WFProcessInstanceEntity>(keyValue);
                if (entity.isFinish == 1)
                {
                    throw new Exception("实例已经审核完成,操作失败");
                }
                else if (entity.isFinish == 2)
                {
                    throw new Exception("实例已经取消,操作失败");
                }
                /// 流程是否完成(0运行中,1运行结束,2被召回,3不同意,4表示被驳回)
                string content = "";
                switch (state)
                {
                    case 0:
                        if (entity.EnabledMark == 0)
                        {
                            return 1;
                        }
                        entity.EnabledMark = 0;
                        content = "【暂停】" + OperatorProvider.Provider.Current().LoginInfo.UserName + "暂停了一个流程进程【" + entity.Code + "/" + entity.CustomName + "】";
                        break;
                    case 1:
                        if (entity.EnabledMark == 1)
                        {
                            return 1;
                        }
                        entity.EnabledMark = 1;
                        content = "【启用】" + OperatorProvider.Provider.Current().LoginInfo.UserName + "启用了一个流程进程【" + entity.Code + "/" + entity.CustomName + "】";
                        break;
                    case 2:
                        entity.isFinish = 2;
                        content = "【召回】" + OperatorProvider.Provider.Current().LoginInfo.UserName + "召回了一个流程进程【" + entity.Code + "/" + entity.CustomName + "】";
                        break;
                }

                


                db.Update<WFProcessInstanceEntity>(entity);
                WFProcessOperationHistoryEntity processOperationHistoryEntity = new WFProcessOperationHistoryEntity();
                processOperationHistoryEntity.Create();
                processOperationHistoryEntity.ProcessId = entity.Id;
                processOperationHistoryEntity.Content = content;
                db.Insert(processOperationHistoryEntity);
                db.Commit();
                return 1;
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 流程指派
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="makeLists"></param>
        public void DesignateProcess(string processId, string makeLists)
        {
            try
            {
                WFProcessInstanceEntity entity = new WFProcessInstanceEntity();
                entity.Id = processId;
                entity.MakerList = makeLists;
                this.BaseRepository().Update(entity);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
