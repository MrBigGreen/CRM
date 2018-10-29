using CRM.BLL.Framework;
using CRM.DAL;
using CRM.IBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.App.Commons
{
    public class DefaultListProvider
    {


        public static List<SysCodeTable> GetSysCodeTables(string CodeCategory)
        {
            List<SysCodeTable> list = new List<SysCodeTable>();
            object obj = HttpRuntime.Cache.Get(CodeCategory);
            if (obj == null)
            {
                ISysCodeTableHander baseDDL = new SysCodeTableHander();
                list = baseDDL.GetSysCodeTables(CodeCategory);
                HttpRuntime.Cache.Insert(CodeCategory, list);
            }
            else
            {
                list = (List<SysCodeTable>)obj;
            }

            return list;
        }
        /// <summary>
        /// 根据类别和值查询最匹配的数据
        /// </summary>
        /// <param name="CodeCategory"></param>
        /// <param name="CodeValue"></param>
        /// <returns></returns>
        public static SysCodeTable GetDefaultEntity(string CodeCategory, string CodeValue)
        {
            List<SysCodeTable> list = new List<SysCodeTable>();
            object obj = HttpRuntime.Cache.Get(CodeCategory);
            if (obj == null)
            {
                ISysCodeTableHander baseDDL = new SysCodeTableHander();
                list = baseDDL.GetSysCodeTables(CodeCategory);
                HttpRuntime.Cache.Insert(CodeCategory, list);
            }
            else
            {
                list = (List<SysCodeTable>)obj;
            }
            var q = list.Where(s => s.CodeValue.Contains(CodeValue)).LastOrDefault();
            return q;
        }
        /// <summary>
        /// 根据类别和值查询最匹配的数据
        /// </summary>
        /// <param name="CodeCategory"></param>
        /// <param name="CodeValue"></param>
        /// <returns></returns>
        public static SysCodeTable GetLastOrDefault(string CodeCategory, string CodeValue)
        {
            List<SysCodeTable> list = new List<SysCodeTable>();
            object obj = HttpRuntime.Cache.Get(CodeCategory);
            if (obj == null)
            {
                ISysCodeTableHander baseDDL = new SysCodeTableHander();
                list = baseDDL.GetSysCodeTables(CodeCategory);
                HttpRuntime.Cache.Insert(CodeCategory, list);
            }
            else
            {
                list = (List<SysCodeTable>)obj;
            }
            var q = list.Where(s => s.CodeValue.Contains(CodeValue)).LastOrDefault();
            return q;
        }
        /// <summary>
        /// 根据类别和值查询最匹配的数据
        /// </summary>
        /// <param name="CodeCategory"></param>
        /// <param name="CodeValue"></param>
        /// <returns></returns>
        public static SysCodeTable GetFirstOrDefault(string CodeCategory, string CodeValue)
        {
            List<SysCodeTable> list = new List<SysCodeTable>();
            object obj = HttpRuntime.Cache.Get(CodeCategory);
            if (obj == null)
            {
                ISysCodeTableHander baseDDL = new SysCodeTableHander();
                list = baseDDL.GetSysCodeTables(CodeCategory);
                HttpRuntime.Cache.Insert(CodeCategory, list);
            }
            else
            {
                list = (List<SysCodeTable>)obj;
            }
            var q = list.Where(s => s.CodeValue.Contains(CodeValue)).FirstOrDefault();
            return q;
        }
    }
}