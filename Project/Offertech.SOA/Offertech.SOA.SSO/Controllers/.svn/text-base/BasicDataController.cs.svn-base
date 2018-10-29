using Offertech.Application.Busines.SystemManage;
using Offertech.Application.Cache;
using Offertech.Application.Entity.SystemManage.ViewModel;
using Offertech.Util;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Offertech.SOA.SSO.Controllers
{
    public class BasicDataController : ApiControllerBase
    {
        private DataItemCache dataItemCache = new DataItemCache();

        private AreaCache areaCache = new AreaCache();
        [HttpGet]
        public string HelloWorld()
        {
            return "this is BasicDataController";
        }

        /// <summary>
        /// 获取数据字典列表（绑定控件）
        /// </summary>
        /// <param name="EnCode">代码</param>
        /// <returns>返回列表树Json</returns>
        [HttpGet]
        public dynamic GetDataItemTreeJson(string EnCode)
        {
            var data = dataItemCache.GetDataItemList(EnCode);
            var treeList = new List<TreeEntity>();
            foreach (DataItemModel item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.ItemDetailId) == 0 ? false : true;
                tree.id = item.ItemDetailId;
                tree.text = item.ItemName;
                tree.value = item.ItemValue;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return treeList.TreeToJson();
        }

        /// <summary>
        /// 获取数据字典列表（绑定控件）
        /// </summary>
        /// <param name="EnCode">代码</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public dynamic GetDataItemListJson(string EnCode)
        {
            var data = dataItemCache.GetDataItemList(EnCode);
            return data.ToJson();
        }

        /// <summary>
        /// 区域列表（主要是绑定下拉框）
        /// </summary>
        /// <param name="parentId">节点Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public dynamic GetAreaListJson(string parentId)
        {
            var data = areaCache.GetAreaList(parentId == null ? "0" : parentId);
            return data.ToJson();
        }
    }
}