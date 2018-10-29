using Offertech.Application.Busines.BaseManage;
using Offertech.Application.Entity.BaseManage;
using Offertech.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offertech.Application.Cache
{
    public class CompanyCache
    {
        private CompanyBLL busines = new CompanyBLL();

        /// <summary>
        /// 组织列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CompanyEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<CompanyEntity>>(busines.cacheKey);
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
        /// <param name="CompanyId">公司Id</param>
        /// <returns></returns>
        public CompanyEntity GetEntity(string CompanyId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(CompanyId))
            {
                var d = data.Where(t => t.CompanyId == CompanyId).ToList<CompanyEntity>();
                if (d.Count > 0)
                {
                    return d[0];
                }
            }
            return new CompanyEntity();
        }
        /// <summary>
        /// 根据组织ID获取所有下级，包含自己
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public List<CompanyEntity> GetChildList(string CompanyId)
        {
            List<CompanyEntity> list = new List<CompanyEntity>();
            var data = this.GetList();
            if (!string.IsNullOrEmpty(CompanyId))
            {
                var d = data.Where(t => t.CompanyId == CompanyId).ToList<CompanyEntity>();
                if (d.Count > 0)
                {
                    list.Add(d[0]);
                    GetChilds(CompanyId, ref list);
                }
            }
            return list;
        }
        /// <summary>
        /// 递归获取所有子公司
        /// </summary>
        /// <param name="CompanyId">公司Id</param>
        /// <returns></returns>
        public void GetChilds(string CompanyId, ref List<CompanyEntity> ChildList)
        {
            List<CompanyEntity> list = new List<CompanyEntity>();
            var data = this.GetList().Where(t => t.ParentId == CompanyId).ToList();
            if (data == null)
            {
                return;

            }
            for (int i = 0; i < data.Count(); i++)
            {
                ChildList.Add(data[i]);
                GetChilds(data[i].CompanyId, ref ChildList);
            }
        }
    }
}
