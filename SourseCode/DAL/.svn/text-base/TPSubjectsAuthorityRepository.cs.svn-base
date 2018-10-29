using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    public partial class TPSubjectsAuthorityRepository : BaseRepository<TP_SubjectsAuthority>, IDisposable
    {

        #region 通过主键获取一个对象
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TP_SubjectsAuthority GetById(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetById(db, id);
            }
        }
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TP_SubjectsAuthority GetById(DB_CRMEntities db, string id)
        {
            return db.TP_SubjectsAuthority.SingleOrDefault(s => s.SubjectsAuthorityID == id);
        }
        #endregion

        #region 通过主键删除对象
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
                return 1;
            }
        }
        /// <summary>
        /// 删除一个
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条角色的主键</param>
        public void Delete(DB_CRMEntities db, string id)
        {
            IQueryable<TP_SubjectsAuthority> collection = from f in db.TP_SubjectsAuthority
                                                          where f.SubjectsAuthorityID.Contains(id)
                                                      select f;
            foreach (var deleteItem in collection)
            {
                db.TP_SubjectsAuthority.Remove(deleteItem);
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
            IQueryable<TP_SubjectsAuthority> collection = from f in db.TP_SubjectsAuthority
                                                          where deleteCollection.Contains(f.SubjectsAuthorityID)
                                                      select f;
            foreach (var deleteItem in collection)
            {
                db.TP_SubjectsAuthority.Remove(deleteItem);
            }
        }
        #endregion


        public void Dispose()
        {

        }
    }
}
