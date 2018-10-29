using CRM.DAL;
using CRM.IBLL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.Framework
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-07-07
    /// 描述说明：实现CodeTable-数据处理
    /// </summary>
    public class SysCodeTableHander : IDisposable, ISysCodeTableHander
    {
        protected DB_CRMEntities db = new DB_CRMEntities();
        SysCodeTableRepository repository = new SysCodeTableRepository();

        /// <summary>
        /// 获取下拉框的数据
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="colum">列明</param>
        /// <returns></returns>
        public List<SysCodeTable> GetSysCodeTableById(string id)
        {

            return (from m in db.SysCodeTable
                    where m.CodeID == id
                    orderby m.CodeSeq
                    select m).ToList();

        }
        /// <summary>
        /// 根据父亲的Id，获取数据字典
        /// </summary>
        /// <param name="id">父亲节点的主键</param>
        /// <returns></returns>
        public List<SysCodeTable> GetSysCodeTableByParentId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return (from m in db.SysCodeTable
                    where m.CodeCategory == id
                    orderby m.CodeSeq
                    select m).ToList();

        }

        public string GetCodeValueById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            var entity = (from m in db.SysCodeTable
                     where m.CodeID == id
                     select m).SingleOrDefault();

            if (entity == null)
            {
                return "";
            }
            else
            {
                return entity.CodeValue;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CodeCategory"></param>
        /// <returns></returns>
        public List<SysCodeTable> GetSysCodeTables(string CodeCategory)
        {
            return repository.GetSysCodeTables(db, CodeCategory);
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
