using System.Linq;

using System.Data;
using System;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;


namespace CRM.DAL
{
    public abstract class BaseRepository<T> where T : class
    {
        /// <summary>
        /// 开始时间的标识
        /// </summary>
        public string Start_Time { get { return "Start_Time"; } }
        /// <summary>
        /// 结束时间的标识
        /// </summary>
        public string End_Time { get { return "End_Time"; } }
        /// <summary>
        /// 开始数值的标识
        /// </summary>
        public string Start_Int { get { return "Start_Int"; } }
        /// <summary>
        /// 结束数值的标识
        /// </summary>
        public string End_Int { get { return "End_Int"; } }

        public string End_String { get { return "End_String"; } }  /// <summary>
        /// 精确字符串
        /// </summary>
        public string DDL_String { get { return "DDL_String"; } }
        /// <summary>
        /// 精确数字
        /// </summary>
        public string DDL_Int { get { return "DDL_Int"; } }
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

        /// <summary>
        /// 数据集排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">数据集</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">升序asc（默认）还是降序desc</param>
        /// <returns></returns>
        public virtual IQueryable<T> DataSorting<T>(IQueryable<T> source, string order, string sort)
        {
            string sortingDir = string.Empty;
            if (string.IsNullOrEmpty(order) || order.ToUpper().Trim() == "ASC")
                sortingDir = "OrderBy";
            else if (order.ToUpper().Trim() == "DESC")
                sortingDir = "OrderByDescending";
            ParameterExpression param = Expression.Parameter(typeof(T), sort);
            PropertyInfo pi = typeof(T).GetProperty(sort);
            Type[] types = new Type[2];
            types[0] = typeof(T);
            types[1] = pi.PropertyType;
            Expression expr = Expression.Call(typeof(Queryable), sortingDir, types, source.Expression, Expression.Lambda(Expression.Property(param, sort), param));
            IQueryable<T> query = source.AsQueryable().Provider.CreateQuery<T>(expr);
            return query;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="entity">将要创建的对象</param>
        public virtual void Create(DB_CRMEntities db, T entity)
        {
            if (entity != null)
            {
                db.Set<T>().Add(entity);
            }
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity">一个对象</param>
        /// <returns></returns>
        public virtual int Create(T entity)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                Create(db, entity);
                return this.Save(db);
            }
        }
        /// <summary>
        /// 创建对象集合
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="entitys">对象集合</param>
        public virtual void Create(DB_CRMEntities db, IQueryable<T> entitys)
        {
            foreach (var entity in entitys)
            {
                this.Create(db, entity);
            }
        }
        /// <summary>
        /// 编辑一个对象
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="entity">将要编辑的一个对象</param>
        /// <param name="isAttach">是否附加到数据库上下文</param>
        public virtual T Edit(DB_CRMEntities db, T entity, bool isAttach = true)
        {
            if (isAttach)
                db.Set<T>().Attach(entity);

            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return entity;
        }
        /// <summary>
        /// 编辑对象集合
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="entitys">对象集合</param>
        public virtual void Edit(DB_CRMEntities db, IQueryable<T> entitys)
        {
            foreach (T entity in entitys)
            {
                this.Edit(db, entity);
            }
        }
        /// <summary>
        /// 提交保存，持久化到数据库
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <returns>受影响行数</returns>
        public int Save(DB_CRMEntities db)
        {
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                return db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                string error = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    error += eve;
                }
            }
            return 0;
            //return db.SaveChanges();
        }

    }
}

