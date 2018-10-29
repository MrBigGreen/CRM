using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.BLL
{
    /// <summary>
    /// 多对多数据关系中使用
    /// </summary>
    public class DataOfDiffrent
    {
        /// <summary>
        /// 多对多数据关系中，主键为string类型
        /// </summary>
        /// <param name="newId">新的主键</param>
        /// <param name="oldId">已有的主键</param>
        /// <param name="addId">新增加的主键</param>
        /// <param name="deleteId">删除的主键</param>
        public static void GetDiffrent(List<string> newId, List<string> oldId, ref List<string> addId, ref List<string> deleteId)
        {
            addId = (from n in newId
                     where oldId.All(a => (a != n))
                     where !string.IsNullOrWhiteSpace(n)
                     select n).ToList();

            deleteId = (from o in oldId
                        where newId.All(a => (a != o))
                        where !string.IsNullOrWhiteSpace(o)
                        select o).ToList();
        }
        /// <summary>
        /// 多对多数据关系中，主键为int类型
        /// </summary>
        /// <param name="newId">新的主键</param>
        /// <param name="oldId">已有的主键</param>
        /// <param name="addId">新增加的主键</param>
        /// <param name="deleteId">删除的主键</param>
        public static void GetDiffrentInt(List<string> newId, List<string> oldId, ref List<int> addId, ref List<int> deleteId)
        {
            addId = (from n in newId
                     where oldId.All(a => (a != n))
                     where !string.IsNullOrWhiteSpace(n)
                     select Convert.ToInt32(n)).ToList();

            deleteId = (from o in oldId
                        where newId.All(a => (a != o))
                        where !string.IsNullOrWhiteSpace(o)
                        select Convert.ToInt32(o)).ToList();
        }
    }
}

