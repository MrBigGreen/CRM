using System.Linq;
 
using System.Data;
using System;
namespace CRM.DAL
{
    public abstract class BaseViewRepository<T> where T : class
    {
        public string Start_Time { get { return "Start_Time"; } }
        public string End_Time { get { return "End_Time"; } }
        public string Start_Int { get { return "Start_Int"; } }
        public string End_Int { get { return "End_Int"; } }
        public string End_String { get { return "End_String"; } }
        public string DDL_String { get { return "DDL_String"; } }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns>集合</returns>
        public virtual IQueryable<T> GetAll()
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetAll(db);
            }
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns>集合</returns>
        public virtual IQueryable<T> GetAll(DB_CRMEntities db)
        {
            return db.Set<T>().AsQueryable();
        }

    }
}

