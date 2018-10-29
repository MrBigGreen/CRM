using Offertech.Application.Entity.FlowManage;
using Offertech.Application.IService.FlowManage;
using Offertech.Application.Service.FlowManage;
using Offertech.Util.WebControl;
using System;
using System.Data;

namespace Offertech.Application.Busines.FlowManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：陈彬彬
    /// 日 期：2016.03.19 13:57
    /// 描 述：工作流表单操作（支持：SqlServer）
    /// </summary>
    public class WFFrmMainBLL
    {
        private WFFrmMainIService server = new WFFrmMainService();

        #region 获取数据
        /// <summary>
        /// 获取表单列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            return server.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取表单数据ALL(用于下拉框)
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllList()
        {
            return server.GetAllList();
        }
        /// <summary>
        /// 设置表单
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public WFFrmMainEntity GetForm(string keyValue)
        {
            try
            {
                return server.GetForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                server.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="entity">表单模板实体类</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public int SaveForm(string keyValue,WFFrmMainEntity entity)
        {
            try
            {
                return server.SaveForm(keyValue,entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 更新表单模板状态（启用，停用）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="status">状态 1:启用;0.停用</param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                server.UpdateState(keyValue, state);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
