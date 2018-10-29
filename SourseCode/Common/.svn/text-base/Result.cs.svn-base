using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 系统公共使用部分
    /// </summary>
    public static class Result
    { 
        /// <summary>
        /// 获取字符串类型的主键
        /// </summary>
        /// <returns></returns>
        public static string GetNewId()
        {
            
            string id = DateTime.Now.ToString("yyMMddHHmmssfffffff");
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            id += guid.Substring(0, 10);
            return id;
        } 
    }
}
