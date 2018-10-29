
using Offertech.Application.Busines.SystemManage;
using Offertech.Application.Entity.SystemManage;
using Offertech.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offertech.Application.Cache
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016.9.26
    /// 描 述：行政区域缓存
    /// </summary>
    public class AreaCache
    {
        private AreaBLL busines = new AreaBLL();

        /// <summary>
        /// 行政区域列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AreaEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<AreaEntity>>(busines.cacheKey);
            if (cacheList == null)
            {
                var data = busines.GetList();
                CacheFactory.Cache().WriteCache(data, busines.cacheKey);
                return data;
            }
            else
            {
                return cacheList;
            }
        }
        /// <summary>
        /// 行政区域列表
        /// </summary>
        /// <param name="AreaId">区域Id</param>
        /// <returns></returns>
        public AreaEntity GetEntity(string areaId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(areaId))
            {
                var d = data.Where(t => t.AreaId == areaId).ToList<AreaEntity>();
                if (d.Count > 0)
                {
                    return d[0];
                }
            }
            return new AreaEntity();
        }

        /// <summary>
        /// 区域列表（主要是给绑定数据源提供的）
        /// </summary>
        /// <param name="parentId">节点Id</param>
        /// <returns></returns>
        public IEnumerable<AreaEntity> GetAreaList(string parentId)
        {
            return this.GetList().Where(t => t.EnabledMark == 1 && t.ParentId == parentId);
        }
        /// <summary>
        /// 获取城市
        /// </summary>
        /// <param name="parentId">节点Id</param>
        /// <returns></returns>
        public IEnumerable<AreaEntity> GetCityList(string parentId="")
        {
            if (string.IsNullOrWhiteSpace(parentId))
            {
                return this.GetList().Where(t => t.EnabledMark == 1 && t.Layer == 2);
            }
            else
            {
                return this.GetList().Where(t => t.EnabledMark == 1 && t.ParentId == parentId);
            }

        }
    }
}
