using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Data;
namespace CRM.DAL
{
    /// <summary>
    /// 部门
    /// </summary>
    public partial class SysDepartmentRepository : BaseRepository<SysDepartment>, IDisposable
    {
        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param> 
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public IQueryable<SysDepartment> DaoChuData(DB_CRMEntities db, string order, string sort, string search, params object[] listQuery)
        {
            string where = string.Empty;
            int flagWhere = 0;

            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());
            if (queryDic != null && queryDic.Count > 0)
            {
                foreach (var item in queryDic)
                {
                    if (flagWhere != 0)
                    {
                        where += " and ";
                    }
                    flagWhere++;



                    if (queryDic.ContainsKey("SysDocument") && !string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key == "SysDocument")
                    {//查询多对多关系的列名
                        where += "EXISTS(select p from it.SysDocument as p where p.Id='" + item.Value + "')";
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(Start_Time)) //开始时间
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(Start_Time)) + "] >=  CAST('" + item.Value + "' as   System.DateTime)";
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(End_Time)) //结束时间+1
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(End_Time)) + "] <  CAST('" + Convert.ToDateTime(item.Value).AddDays(1) + "' as   System.DateTime)";
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(Start_Int)) //开始数值
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(Start_Int)) + "] >= " + item.Value.GetInt();
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(End_Int)) //结束数值
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(End_Int)) + "] <= " + item.Value.GetInt();
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(DDL_Int)) //精确查询数值
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(DDL_Int)) + "] =" + item.Value;
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(DDL_String)) //精确查询字符串
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(DDL_String)) + "] = '" + item.Value + "'";
                        continue;
                    }
                    where += "it.[" + item.Key + "] like '%" + item.Value + "%'";//模糊查询
                }
            }
            return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext
                     .CreateObjectSet<SysDepartment>().Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
                     .AsQueryable();

        }
        /// <summary>
        /// 通过主键id，获取部门---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>部门</returns>
        public SysDepartment GetById(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetById(db, id);
            }
        }
        /// <summary>
        /// 通过主键id，获取部门---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>部门</returns>
        public SysDepartment GetById(DB_CRMEntities db, string id)
        {
            return db.SysDepartment.SingleOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// 获取当前层次最大的职位级别编码
        /// </summary>
        /// <param name="iLength">职级编码长度</param>
        /// <returns>最大的职位级别编码</returns>
        public long GetMaxPositionLevel(DB_CRMEntities db, string sPositionLevel)
        {
            if (string.IsNullOrEmpty(sPositionLevel) || sPositionLevel.Length <= 3)
            {
                var q = (from p in db.SysDepartment where p.PositionLevel.Length == 3 select p.PositionLevel).Max();
                long level = 0;
                long.TryParse(q, out level);
                if (level < 1)
                {
                    level = 100;
                }
                return level;
            }
            else
            {
                var q = (from p in db.SysDepartment
                         where p.PositionLevel.Length == sPositionLevel.Length
                         && p.PositionLevel.Substring(0, sPositionLevel.Length - 3) == sPositionLevel.Substring(0, sPositionLevel.Length - 3)
                         select p.PositionLevel).Max();
                long level = 0;
                long.TryParse(q, out level);
                if (level < 1)
                {
                    long.TryParse(sPositionLevel, out level);
                }
                return level;

            }

        }

        /// <summary>
        /// 获取子集最大的职位级别编码
        /// </summary>
        /// <param name="iLength">职级编码长度</param>
        /// <returns>最大的职位级别编码</returns>
        public long GetMaxChildPositionLevel(DB_CRMEntities db, string sPositionLevel)
        {
            var q = (from p in db.SysDepartment
                     where p.PositionLevel.Length == sPositionLevel.Length + 3
                     && p.PositionLevel.Substring(0, sPositionLevel.Length) == sPositionLevel
                     select p.PositionLevel).Max();
            long level = 0;
            long.TryParse(q, out level);
            if (level < 1)
            {
                
                long.TryParse(sPositionLevel + "000", out level);
            }
            return level;
        }


        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysDocument> GetRefSysDocument(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetRefSysDocument(db, id);
            }
        }
        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysDocument> GetRefSysDocument(DB_CRMEntities db, string id)
        {
            return from m in db.SysDepartment
                   from f in m.SysDocument
                   where m.Id == id
                   select f;

        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysDocument> GetRefSysDocument(DB_CRMEntities db)
        {
            return from m in db.SysDepartment
                   from f in m.SysDocument
                   select f;
        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysDocument> GetRefSysDocument()
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetRefSysDocument(db);
            }
        }


        /// <summary>
        /// 确定删除一个对象，调用Save方法
        /// </summary>
        /// <param name="id">一条数据的主键</param>
        /// <returns></returns>    
        public int Delete(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                this.Delete(db, id);
                return Save(db);
            }
        }
        /// <summary>
        /// 删除一个部门
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条部门的主键</param>
        public void Delete(DB_CRMEntities db, string id)
        {
            SysDepartment deleteItem = GetById(db, id);
            if (deleteItem != null)
            {
                db.SysDepartment.Remove(deleteItem);
            }
        }
        /// <summary>
        /// 删除对象集合
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="deleteCollection">主键的集合</param>
        public void Delete(DB_CRMEntities db, string[] deleteCollection)
        {
            //数据库设置级联关系，自动删除子表的内容   
            IQueryable<SysDepartment> collection = from f in db.SysDepartment
                                                   where deleteCollection.Contains(f.Id)
                                                   select f;
            foreach (var deleteItem in collection)
            {
                db.SysDepartment.Remove(deleteItem);
            }
        }

        public void Dispose()
        {
        }
    }
}

