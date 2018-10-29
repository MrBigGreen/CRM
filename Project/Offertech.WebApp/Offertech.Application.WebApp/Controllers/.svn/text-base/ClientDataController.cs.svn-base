using Offertech.Application.Cache;
using Offertech.Application.Entity.SystemManage.ViewModel;
using Offertech.Util;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Offertech.Application.Entity.BaseManage;
using Offertech.Application.Busines.AuthorizeManage;
using Offertech.Application.Code;
using Offertech.Application.Entity.AuthorizeManage;
using Offertech.Application.Entity.SystemManage;
using Offertech.Application.Busines.SystemManage;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using System.Text;

namespace Offertech.Application.WebApp.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2015.09.01 13:32
    /// 描 述：客户端数据
    /// </summary>
    public class ClientDataController : MvcControllerBase
    {
        private DataItemCache dataItemCache = new DataItemCache();
        private OrganizeCache organizeCache = new OrganizeCache();
        private DepartmentCache departmentCache = new DepartmentCache();
        private PostCache postCache = new PostCache();
        private RoleCache roleCache = new RoleCache();
        private UserGroupCache userGroupCache = new UserGroupCache();
        private UserCache userCache = new UserCache();
        private AuthorizeBLL authorizeBLL = new AuthorizeBLL();
        private AreaCache areaCache = new AreaCache();
        private AreaBLL areaBLL = new AreaBLL();
        private CompanyCache companyCache = new CompanyCache();

        #region 获取数据
        /// <summary>
        /// 批量加载数据给客户端（把常用数据全部加载到浏览器中 这样能够减少数据库交互）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult GetClientDataJson()
        {
            var jsonData = new
            {
                organize = this.GetOrganizeData(),              //公司
                department = this.GetDepartmentData(),          //部门
                post = this.GetPostData(),                      //岗位
                role = this.GetRoleData(),                      //角色
                userGroup = this.GetUserGroupData(),            //用户组
                user = this.GetUserData(),                      //用户
                dataItem = this.GetDataItem(),                  //字典
                authorizeMenu = this.GetModuleData(),           //导航菜单
                authorizeButton = this.GetModuleButtonData(),   //功能按钮
                authorizeColumn = this.GetModuleColumnData(),   //功能视图
                area = this.GetAreaData(),                      //行政区域
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 获取数据字典列表（绑定控件）
        /// </summary>
        /// <param name="EnCode">代码</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetDataItemListJson(string EnCode)
        {
            var data = dataItemCache.GetDataItemList(EnCode);
            return ToJsonResult(data);
        }
        #endregion

        #region 获取城市区域数据
        /// <summary>
        /// 获取区域树Json
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetAreaTreeJson()
        {
            var data = areaCache.GetList().ToList();
            var treeList = new List<TreeEntity>();
            foreach (AreaEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.AreaId) == 0 ? false : true;
                tree.id = item.AreaId;
                tree.text = item.AreaName;
                tree.value = item.AreaId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                tree.Index = item.Layer;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }



        /// <summary>
        /// 区域列表
        /// </summary>
        /// <param name="value">当前主键</param>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetAreaListJson(string value, string keyword)
        {
            string parentId = value == null ? "0" : value;
            var data = areaBLL.GetList(parentId, keyword).ToList();
            return Content(data.ToJson());
        }
        /// <summary>
        /// 区域列表（主要是绑定下拉框）
        /// </summary>
        /// <param name="parentId">节点Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetAreaListJson(string parentId)
        {
            var data = areaCache.GetAreaList(parentId == null ? "0" : parentId);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取城市
        /// </summary>
        /// <param name="parentId">节点Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetCityListJson(string parentId)
        {
            var data = areaCache.GetCityList(parentId);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 区域实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetAreaFormJson(string keyValue)
        {
            var data = areaBLL.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 合同管理 - 签约公司
        /// <summary>
        /// 公司数据
        /// </summary>
        /// <param name="enCode">分类</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCompanyListJson(string enCode, string keyword)
        {
            var data = companyCache.GetList().ToList();

            var expression = LinqExtensions.True<CompanyEntity>();
            if (!string.IsNullOrWhiteSpace(enCode))
            {
                data = data.Where(t => t.EnCode.ToLower() == enCode.ToLower()).ToList();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(t => t.FullName.Contains(keyword)).ToList();
            }

            return ToJsonResult(data);
        }

        #endregion

        #region 处理基础数据
        /// <summary>
        /// 获取公司数据
        /// </summary>
        /// <returns></returns>
        private object GetOrganizeData()
        {
            var data = organizeCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (OrganizeEntity item in data)
            {
                var fieldItem = new
                {
                    EnCode = item.EnCode,
                    FullName = item.FullName
                };
                dictionary.Add(item.OrganizeId, fieldItem);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取部门数据
        /// </summary>
        /// <returns></returns>
        private object GetDepartmentData()
        {
            var data = departmentCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (DepartmentEntity item in data)
            {
                var fieldItem = new
                {
                    EnCode = item.EnCode,
                    FullName = item.FullName,
                    OrganizeId = item.OrganizeId
                };
                dictionary.Add(item.DepartmentId, fieldItem);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取岗位数据
        /// </summary>
        /// <returns></returns>
        private object GetUserGroupData()
        {
            var data = userGroupCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    EnCode = item.EnCode,
                    FullName = item.FullName
                };
                dictionary.Add(item.RoleId, fieldItem);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取岗位数据
        /// </summary>
        /// <returns></returns>
        private object GetPostData()
        {
            var data = postCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    EnCode = item.EnCode,
                    FullName = item.FullName
                };
                dictionary.Add(item.RoleId, fieldItem);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取角色数据
        /// </summary>
        /// <returns></returns>
        private object GetRoleData()
        {
            var data = roleCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    EnCode = item.EnCode,
                    FullName = item.FullName
                };
                dictionary.Add(item.RoleId, fieldItem);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <returns></returns>
        private object GetUserData()
        {
            var data = userCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (UserEntity item in data)
            {
                var fieldItem = new
                {
                    EnCode = item.EnCode,
                    Account = item.Account,
                    RealName = item.RealName,
                    OrganizeId = item.OrganizeId,
                    DepartmentId = item.DepartmentId
                };
                dictionary.Add(item.UserId, fieldItem);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <returns></returns>
        private object GetDataItem()
        {
            var dataList = dataItemCache.GetDataItemList();
            var dataSort = dataList.Distinct(new Comparint<DataItemModel>("EnCode"));
            Dictionary<string, object> dictionarySort = new Dictionary<string, object>();

            try
            {

                foreach (DataItemModel itemSort in dataSort)
                {
                    var dataItemList = dataList.Where(t => t.EnCode.Equals(itemSort.EnCode));
                    Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                    foreach (DataItemModel itemList in dataItemList)
                    {
                        //dictionaryItemList.Add(itemList.ItemValue, itemList.ItemName);
                        dictionaryItemList.Add(itemList.ItemName, itemList.ItemValue);
                    }
                    foreach (DataItemModel itemList in dataItemList)
                    {
                        dictionaryItemList.Add(itemList.ItemDetailId, itemList.ItemName);
                    }
                    dictionarySort.Add(itemSort.EnCode, dictionaryItemList);
                }
            }
            catch (System.Exception ex)
            {

            }
            return dictionarySort;
        }

        /// <summary>
        /// 行政区域数据
        /// </summary>
        /// <returns></returns>
        private object GetAreaData()
        {
            var data = areaCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (AreaEntity item in data)
            {
                var fieldItem = new
                {
                    AreaCode = item.AreaCode,
                    AreaName = item.AreaName,
                    ParentId = item.ParentId,
                };
                dictionary.Add(item.AreaId, fieldItem);
            }
            return dictionary;
        }
        #endregion

        #region 处理授权数据
        /// <summary>
        /// 获取功能数据
        /// </summary>
        /// <returns></returns>
        private object GetModuleData()
        {
            return authorizeBLL.GetModuleList(SystemInfo.CurrentUserId);
        }
        /// <summary>
        /// 获取功能按钮数据
        /// </summary>
        /// <returns></returns>
        private object GetModuleButtonData()
        {
            var data = authorizeBLL.GetModuleButtonList(SystemInfo.CurrentUserId);
            var dataModule = data.Distinct(new Comparint<ModuleButtonEntity>("ModuleId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ModuleButtonEntity item in dataModule)
            {
                var buttonList = data.Where(t => t.ModuleId.Equals(item.ModuleId));
                dictionary.Add(item.ModuleId, buttonList);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取功能视图数据
        /// </summary>
        /// <returns></returns>
        private object GetModuleColumnData()
        {
            var data = authorizeBLL.GetModuleColumnList(SystemInfo.CurrentUserId);
            var dataModule = data.Distinct(new Comparint<ModuleColumnEntity>("ModuleId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ModuleColumnEntity item in dataModule)
            {
                var columnList = data.Where(t => t.ModuleId.Equals(item.ModuleId));
                dictionary.Add(item.ModuleId, columnList);
            }
            return dictionary;
        }
        #endregion



    }
}
