using CRM.BLL.Framework;
using CRM.DAL;
using CRM.IBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-07-07
    /// 描述说明：实现CodeTable
    /// </summary>
    public class SysCodeTableModels
    {

        public static SelectList GetAllProvince()
        {
            ISysCodeTableHander baseDDL = new SysCodeTableHander();
            return new SelectList(baseDDL.GetSysCodeTableByParentId("ProvinceCategory"), "CodeID", "CodeValue");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SelectList GetSysCodeTableByParentId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                List<SelectList> sl = new List<SelectList>();
                
                return new SelectList(sl);
            }
            ISysCodeTableHander baseDDL = new SysCodeTableHander();
            return new SelectList(baseDDL.GetSysCodeTableByParentId(id), "CodeID", "CodeValue");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SelectList GetSysCodeTableById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                List<SelectList> sl = new List<SelectList>();
                return new SelectList(sl);
            }
            ISysCodeTableHander baseDDL = new SysCodeTableHander();
            return new SelectList(baseDDL.GetSysCodeTableById(id), "CodeID", "CodeValue");

        }
        /// <summary>
        /// 根据主键id，获取数据字典的展示字段
        /// </summary>
        /// <param name="id">父亲节点的主键</param>
        /// <returns></returns>
        public static string GetMyTextsById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return string.Empty;
            }
            ISysCodeTableHander baseDDL = new SysCodeTableHander();
            return baseDDL.GetCodeValueById(id); 
        }


        


    }
}