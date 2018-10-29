using Offertech.Application.Entity.WeChatManage;
using Offertech.Application.IService.WeChatManage;
using Offertech.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Offertech.Application.Service.WeChatManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号成员
    /// </summary>
    public class WeChatUserService : RepositoryFactory<WeChatUserRelationEntity>, IWeChatUserService
    {
        #region 获取数据
        /// <summary>
        /// 成员列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WeChatUserRelationEntity> GetList()
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 成员实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public WeChatUserRelationEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 成员（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="weChatUserRelationEntity">用户实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, WeChatUserRelationEntity weChatUserRelationEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                weChatUserRelationEntity.Modify(keyValue);
                this.BaseRepository().Update(weChatUserRelationEntity);
            }
            else
            {
                weChatUserRelationEntity.Create();
                this.BaseRepository().Insert(weChatUserRelationEntity);
            }
        }
        #endregion
    }
}
