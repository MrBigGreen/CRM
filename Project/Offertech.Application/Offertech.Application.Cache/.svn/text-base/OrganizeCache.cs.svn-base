using Offertech.Application.Busines.BaseManage;
using Offertech.Application.Entity.BaseManage;
using Offertech.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Offertech.Application.Cache
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016.3.4 9:56
    /// 描 述：组织架构缓存
    /// </summary>
    public class OrganizeCache
    {
        private OrganizeBLL busines = new OrganizeBLL();

        /// <summary>
        /// 组织列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrganizeEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<OrganizeEntity>>(busines.cacheKey);
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
        /// 组织列表
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public OrganizeEntity GetEntity(string organizeId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(organizeId))
            {
                var d = data.Where(t => t.OrganizeId == organizeId).ToList<OrganizeEntity>();
                if (d.Count > 0)
                {
                    return d[0];
                }
            }
            return new OrganizeEntity();
        }
        /// <summary>
        /// 根据组织ID获取所有下级，包含自己
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<OrganizeEntity> GetChildList(string organizeId)
        {
            List<OrganizeEntity> list = new List<OrganizeEntity>();
            var data = this.GetList();
            if (!string.IsNullOrEmpty(organizeId))
            {
                var d = data.Where(t => t.OrganizeId == organizeId).ToList<OrganizeEntity>();
                if (d.Count > 0)
                {
                    list.Add(d[0]);
                    GetChilds(organizeId, ref list);
                }
            }
            return list;
        }
        /// <summary>
        /// 递归获取所有子公司
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public void GetChilds(string organizeId, ref List<OrganizeEntity> ChildList)
        {
            List<OrganizeEntity> list = new List<OrganizeEntity>();
            var data = this.GetList().Where(t => t.ParentId == organizeId).ToList();
            if (data == null)
            {
                return;

            }
            for (int i = 0; i < data.Count(); i++)
            {
                ChildList.Add(data[i]);
                GetChilds(data[i].OrganizeId, ref ChildList);
            }
        }
    }
}
