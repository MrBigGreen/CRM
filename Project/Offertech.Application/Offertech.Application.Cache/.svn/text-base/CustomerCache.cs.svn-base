using Offertech.Application.Busines.CustomerManage;
using Offertech.Application.Entity.CustomerManage;
using Offertech.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offertech.Application.Cache
{
    public class CustomerCache
    {
        private CustomerBLL busines = new CustomerBLL();

        /// <summary>
        /// 组织列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<CustomerEntity>>(busines.cacheKey);
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
    }
}
