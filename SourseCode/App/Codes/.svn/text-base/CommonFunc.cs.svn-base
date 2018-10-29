using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using  CRM.DAL;
using CRM.BLL;
using Common;
using CRM.IBLL;
using System.Collections;


namespace CRM.App.Codes
{




    /// <summary>
    /// 0904 用户菜单权限缓存操作类
    /// </summary>
    public class MenuCaching
    {
        private static readonly string MENU_CACHING_KEY = "menu_data_";//保存用户菜单的key的一部分
        private static readonly string ACCOUNT_ROLEIDS_CACHING_KEY = "account_roleids_";//保存用户菜单的key的一部分
        //private static readonly int 

        /// <summary>
        /// 获得某用户的菜单列表
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static string GetMenu(ref Account account)
        {
            string menuCacheKey = MENU_CACHING_KEY + account.Id, roleCacheKey = ACCOUNT_ROLEIDS_CACHING_KEY + account.Id;
            string userMenu = string.Empty,dataSource = string.Empty;
            object menuFromCache = HttpRuntime.Cache.Get(menuCacheKey);
            object roldIdsFromCache = HttpRuntime.Cache.Get(roleCacheKey);
            if (menuFromCache == null)
            {
                IHomeBLL home = new HomeBLL();
                //account.roldids在GetMenuByAccount中赋值
                userMenu = home.GetMenuByAccount(ref account);
                dataSource = "menu data from db";
                HttpRuntime.Cache.Insert(menuCacheKey, userMenu);
                HttpRuntime.Cache.Insert(roleCacheKey, account.RoleIds );
            }
            else
            {
                
                account.RoleIds = (roldIdsFromCache != null) ? (List<string>)roldIdsFromCache : null;
                userMenu = menuFromCache.ToString();
                dataSource = "menu data from cache";
            }

            return userMenu + "<input type='hidden'  id='dataSource' value='" + dataSource + "' />";
        }

        /// <summary>
        /// 清除所有用户菜单缓存
        /// </summary>
        public static void ClearCache()
        {
            IDictionaryEnumerator menuCacheEnum = HttpRuntime.Cache.GetEnumerator();
            IList<string> matchedKeyList = new List<string>();
            while (menuCacheEnum.MoveNext())
            {
                if (menuCacheEnum.Key.ToString().Contains(MENU_CACHING_KEY) || menuCacheEnum.Key.ToString().Contains(ACCOUNT_ROLEIDS_CACHING_KEY))
                    matchedKeyList.Add(menuCacheEnum.Key.ToString());
            }
            foreach (string key1 in matchedKeyList)
            {
                HttpRuntime.Cache.Remove(key1);
            }
        }

        /// <summary>
        /// 清除单个用户的菜单缓存
        /// </summary>
        /// <param name="userCode"></param>
        public static void ClearCache(string userCode)
        {
            HttpRuntime.Cache.Remove(MENU_CACHING_KEY+ userCode);
            HttpRuntime.Cache.Remove(ACCOUNT_ROLEIDS_CACHING_KEY  + userCode);

        }

        public static void ClearCache(string[] userCodeList)
        {
            foreach (string userCode in userCodeList)
            {
                HttpRuntime.Cache.Remove(MENU_CACHING_KEY + userCode);
                HttpRuntime.Cache.Remove(ACCOUNT_ROLEIDS_CACHING_KEY + userCode);
            }

        }
    }


 

}