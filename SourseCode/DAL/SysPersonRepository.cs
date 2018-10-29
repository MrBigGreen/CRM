using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Data;
using CRM.DAL.CommonEntity;
namespace CRM.DAL
{
    /// <summary>
    /// 人员
    /// </summary>
    public partial class SysPersonRepository : BaseRepository<SysPerson>, IDisposable
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
        public IQueryable<SysPerson> DaoChuData(DB_CRMEntities db, string order, string sort, string search, string sSysDepartmentId, params object[] listQuery)
        {
            string where = string.Empty;
            int flagWhere = 0;

            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());
            #region 条件
            if (queryDic != null && queryDic.Count > 0)
            {
                foreach (var item in queryDic)
                {
                    if (flagWhere != 0)
                    {
                        where += " and ";
                    }
                    flagWhere++;


                    if (queryDic.ContainsKey("SysDepartmentId") && !string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Value == "noway" && item.Key == "SysDepartmentId")
                    {//查询一对多关系的列名
                        where += "it.SysDepartmentId is null";
                        continue;
                    }

                    if (queryDic.ContainsKey("SysRole") && !string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key == "SysRole")
                    {//查询多对多关系的列名
                        where += "EXISTS(select p from it.SysRole as p where p.Id='" + item.Value + "')";
                        continue;
                    }
                    if (queryDic.ContainsKey("SysDocument") && !string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key == "SysDocument")
                    {//查询多对多关系的列名
                        where += "EXISTS(select p from it.SysDocument as p where p.Id='" + item.Value + "')";
                        continue;
                    }
                    if (queryDic.ContainsKey("PositionStatus") && !string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key == "PositionStatus")
                    {//查询多对多关系的列名
                        if (item.Value.ToUpper() == "TRUE")
                        {
                            where += "it.[PositionStatus] = true ";
                        }
                        else if (item.Value.ToUpper() == "FALSE")
                        {
                            where += "it.[PositionStatus] = false ";
                        }
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
            #endregion
            List<string> list = new List<string>();
            if (!string.IsNullOrWhiteSpace(sSysDepartmentId))
            {
                SysDepartment depar = db.SysDepartment.FirstOrDefault(s => s.Id == sSysDepartmentId);
                if (depar != null)
                {
                    list = (from m in db.SysDepartment
                            where m.PositionLevel.Substring(0, depar.PositionLevel.Length) == depar.PositionLevel
                            select m.Id).ToList();
                }
                //where += "EXISTS (select * from (select * from SysDepartment  a where  exists(select  b.PositionLevel from SysDepartment b where b.Id='" + sSysDepartmentId + "' and b.PositionLevel=Left(a.PositionLevel,len(b.PositionLevel))) ) tab where id=it.SysDepartmentId)";
                //where += "it.[SysDepartmentId] in (select id from (select * from SysDepartment  a where  exists(select  b.PositionLevel from SysDepartment b where b.Id='1307311605187265267d33f281235' and b.PositionLevel=Left(a.PositionLevel,len(b.PositionLevel))) ) tab )";                 
            }
            if (list.Count > 0)
            {
                return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext
                         .CreateObjectSet<SysPerson>().Where(string.IsNullOrEmpty(where) ? "true" : where)
                         .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
                         .Where(s => list.Contains(s.SysDepartmentId)).AsQueryable();
            }
            else
            {
                return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext
                     .CreateObjectSet<SysPerson>().Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
                     .AsQueryable();
            }


        }
        /// <summary>
        /// 通过主键id，获取人员---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>人员</returns>
        public SysPerson GetById(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetById(db, id);
            }
        }
        /// <summary>
        /// 通过主键id，获取人员---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>人员</returns>
        public SysPerson GetById(DB_CRMEntities db, string id)
        {
            return db.SysPerson.SingleOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysRole> GetRefSysRole(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetRefSysRole(db, id);
            }
        }
        /// <summary>
        /// 获取在该表一条数据中，出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysRole> GetRefSysRole(DB_CRMEntities db, string id)
        {
            return from m in db.SysPerson
                   from f in m.SysRole
                   where m.Id == id
                   select f;

        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysRole> GetRefSysRole(DB_CRMEntities db)
        {
            return from m in db.SysPerson
                   from f in m.SysRole
                   select f;
        }
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>外键实体集合</returns>
        public IQueryable<SysRole> GetRefSysRole()
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetRefSysRole(db);
            }
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
            return from m in db.SysPerson
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
            return from m in db.SysPerson
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
        /// 删除一个人员
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条人员的主键</param>
        public void Delete(DB_CRMEntities db, string id)
        {
            SysPerson deleteItem = GetById(db, id);
            if (deleteItem != null)
            {
                db.SysPerson.Remove(deleteItem);
            }
        }
        /// <summary>
        /// 删除对象集合
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="deleteCollection">主键的集合</param>
        public List<string> Delete(DB_CRMEntities db, string[] deleteCollection)
        {
            List<string> HDpic = new List<string>();//获取删除用户头像图片路径
            //数据库设置级联关系，自动删除子表的内容   
            IQueryable<SysPerson> collection = from f in db.SysPerson
                                               where deleteCollection.Contains(f.Id)
                                               select f;
            foreach (var deleteItem in collection)
            {
                HDpic.Add(deleteItem.HDpic);
                db.SysPerson.Remove(deleteItem);
            }
            return HDpic;
        }

        /// <summary>
        /// 根据SysDepartmentId，获取所有人员数据
        /// </summary>
        /// <param name="id">外键的主键</param>
        /// <returns></returns>
        public IQueryable<SysPerson> GetByRefSysDepartmentId(DB_CRMEntities db, string id)
        {
            return from c in db.SysPerson
                   where c.SysDepartmentId == id
                   select c;

        }
        #region 获取账号能见度 create by chand 2015-08-19
        /// <summary>
        /// 获取账号能见度
        /// create by chand 2015-08-19
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PersonEntity> GetPersonVisibility(DB_CRMEntities db, string id)
        {
            var data = db.Database.SqlQuery<PersonEntity>(string.Format("select * from [dbo].[fn_SysPersonVisibility]('{0}',1)", id)).ToList();
            return data;
        }

        #endregion



        #region 获取薪资主体的权限
        /// <summary>
        /// 获取薪资主体的权限
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SysPersonID"></param>
        /// <returns></returns>
        public List<string> GetSalarySubjectSysPerson(DB_CRMEntities db, string SysPersonID)
        {
            return (from s in db.TP_SalarySubjectSysPerson where s.SysPersonID == SysPersonID select s.SalarySubjectID).ToList<string>();
        }
        #endregion


        public void Dispose()
        {
        }
    }
}

