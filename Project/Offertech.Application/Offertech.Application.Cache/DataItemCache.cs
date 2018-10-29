using Offertech.Application.Busines;
using Offertech.Application.Busines.SystemManage;
using Offertech.Application.Entity.SystemManage.ViewModel;
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
    /// 日 期：2015.12.29 9:56
    /// 描 述：数据字典缓存
    /// </summary>
    public class DataItemCache
    {
        private DataItemDetailBLL busines = new DataItemDetailBLL();

        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<DataItemModel>>(busines.cacheKey);
            if (cacheList == null)
            {
                var data = busines.GetDataItemList();
                CacheFactory.Cache().WriteCache(data, busines.cacheKey);
                return data;
            }
            else
            {
                return cacheList;
            }
        }
        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <param name="EnCode">分类代码</param>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList(string EnCode)
        {
            return this.GetDataItemList().Where(t => t.EnCode == EnCode);
        }
        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <param name="EnCode">分类代码</param>
        /// <param name="ItemValue">项目值</param>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetSubDataItemList(string EnCode, string ItemValue)
        {
            var data = this.GetDataItemList().Where(t => t.EnCode == EnCode);
            string ItemDetailId = data.First(t => t.ItemValue == ItemValue).ItemDetailId;
            return data.Where(t => t.ParentId == ItemDetailId);
        }
        /// <summary>
        /// 项目值转换名称
        /// </summary>
        /// <param name="EnCode">分类代码</param>
        /// <param name="ItemValue">项目值</param>
        /// <returns></returns>
        public string ToItemName(string EnCode, string ItemValue)
        {
            var data = this.GetDataItemList().Where(t => t.EnCode == EnCode);
            return data.First(t => t.ItemValue == ItemValue).ItemName;
        }
        /// <summary>
        /// 根据详细Id获取项目值
        /// </summary>
        /// <param name="itemDetailId">项目Id</param>
        /// <returns></returns>
        public string GetDataItemValue(string itemDetailId)
        {
            var data = this.GetDataItemList().FirstOrDefault(t => t.ItemDetailId == itemDetailId);
            if (data != null)
            {
                return data.ItemValue;
            }
            return string.Empty;
        }
        /// <summary>
        /// 根据详细Id获取项目
        /// </summary>
        /// <param name="itemDetailId">项目Id</param>
        /// <returns></returns>
        public DataItemModel GetDataItem(string itemDetailId)
        {
            return this.GetDataItemList().FirstOrDefault(t => t.ItemDetailId == itemDetailId);

        }
    }
}
