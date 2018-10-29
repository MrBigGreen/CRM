using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Data;
namespace CRM.DAL
{
    /// <summary>
    /// 文档管理
    /// </summary>
    public partial class SysDocumentRepository : BaseRepository<SysDocument>, IDisposable
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
        public IQueryable<SysDocument> DaoChuData(DB_CRMEntities db, string order, string sort, string search, params object[] listQuery)
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
                  
                    
                    
                    if (queryDic.ContainsKey("SysPerson")&& !string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key == "SysPerson")
                    {//查询多对多关系的列名
                        where += "EXISTS(select p from it.SysPerson as p where p.Id='" + item.Value + "')";
                        continue;
                    }
                    if (queryDic.ContainsKey("SysDepartment")&& !string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key == "SysDepartment")
                    {//查询多对多关系的列名
                        where += "EXISTS(select p from it.SysDepartment as p where p.Id='" + item.Value + "')";
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
                     .CreateObjectSet<SysDocument>().Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
                     .AsQueryable(); 

        }
        /// <summary>
        /// 通过主键id，获取文档管理---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>文档管理</returns>
        public SysDocument GetById(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetById(db, id);
            }                   
        }
        /// <summary>
        /// 通过主键id，获取文档管理---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>文档管理</returns>
        public SysDocument GetById(DB_CRMEntities db, string id)
        { 
                 return db.SysDocument.SingleOrDefault(s => s.Id == id); 
        }
        
        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysPerson> GetRefSysPerson(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetRefSysPerson(db, id);
            }
        }
        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysPerson> GetRefSysPerson(DB_CRMEntities db, string id)
        {
                return from m in db.SysDocument
                       from f in m.SysPerson
                       where m.Id == id
                       select f;

        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysPerson> GetRefSysPerson(DB_CRMEntities db)
        {
            return from m in db.SysDocument
                   from f in m.SysPerson
                   select f;
        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysPerson> GetRefSysPerson()
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetRefSysPerson(db);
            }
        }

        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysDepartment> GetRefSysDepartment(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetRefSysDepartment(db, id);
            }
        }
        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysDepartment> GetRefSysDepartment(DB_CRMEntities db, string id)
        {
                return from m in db.SysDocument
                       from f in m.SysDepartment
                       where m.Id == id
                       select f;

        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysDepartment> GetRefSysDepartment(DB_CRMEntities db)
        {
            return from m in db.SysDocument
                   from f in m.SysDepartment
                   select f;
        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysDepartment> GetRefSysDepartment()
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetRefSysDepartment(db);
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
        /// 删除一个文档管理
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条文档管理的主键</param>
        public void Delete(DB_CRMEntities db, string id)
        {
            SysDocument deleteItem = GetById(db, id);
            if (deleteItem != null)
            { 
                db.SysDocument.Remove(deleteItem);
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
            IQueryable<SysDocument> collection = from f in db.SysDocument
                    where deleteCollection.Contains(f.Id)
                    select f;
            foreach (var deleteItem in collection)
            {
                db.SysDocument.Remove(deleteItem);
            }
        }

        public void Dispose()
        {
        }
    }
}

