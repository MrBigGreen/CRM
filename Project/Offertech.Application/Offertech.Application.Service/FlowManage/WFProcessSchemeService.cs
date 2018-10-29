using Offertech.Application.Entity.FlowManage;
using Offertech.Application.IService.FlowManage;
using Offertech.Data.Repository;

namespace Offertech.Application.Service.FlowManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 15:54
    /// 描 述：工作流实例模板内容表操作（支持：SqlServer）
    /// </summary>
    public class WFProcessSchemeService:RepositoryFactory, WFProcessSchemeIService
    {
        #region 获取数据
        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public WFProcessSchemeEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<WFProcessSchemeEntity>(keyValue);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public void SaveEntity(string keyValue,WFProcessSchemeEntity entity)
        {
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    this.BaseRepository().Insert<WFProcessSchemeEntity>(entity);
                }
                else {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update<WFProcessSchemeEntity>(entity);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
