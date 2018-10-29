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
    /// 描 述：部门信息缓存
    /// </summary>
    public class DepartmentCache
    {
        private DepartmentBLL busines = new DepartmentBLL();

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<DepartmentEntity>>(busines.cacheKey);
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
        /// 部门列表
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetList(string organizeId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(organizeId))
            {
                data = data.Where(t => t.OrganizeId == organizeId);
            }
            return data;
        }

        public DepartmentEntity GetEntity(string departmentId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(departmentId))
            {
                var d = data.Where(t => t.DepartmentId == departmentId).ToList<DepartmentEntity>();
                if (d.Count > 0)
                {
                    return d[0];
                }
            }
            return new DepartmentEntity();
        }
        /// <summary>
        /// 根据组织ID获取所有下级，包含自己
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<DepartmentEntity> GetChildList(string departmentId)
        {
            List<DepartmentEntity> list = new List<DepartmentEntity>();
            var data = this.GetList();
            if (!string.IsNullOrEmpty(departmentId))
            {
                var d = data.Where(t => t.DepartmentId == departmentId).ToList<DepartmentEntity>();
                if (d.Count > 0)
                {
                    list.Add(d[0]);
                    GetChilds(departmentId, ref list);
                }
            }
            return list;
        }
        /// <summary>
        /// 递归获取所有子公司
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public void GetChilds(string departmentId, ref List<DepartmentEntity> ChildList)
        {
            List<DepartmentEntity> list = new List<DepartmentEntity>();
            var data = this.GetList().Where(t => t.ParentId == departmentId).ToList();
            if (data == null)
            {
                return;

            }
            for (int i = 0; i < data.Count(); i++)
            {
                ChildList.Add(data[i]);
                GetChilds(data[i].DepartmentId, ref ChildList);
            }
        }
    }
}
