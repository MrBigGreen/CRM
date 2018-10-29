using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    public class SysCodeTableRepository : IDisposable
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CodeCategory"></param>
        /// <returns></returns>
        public List<SysCodeTable> GetSysCodeTables(DB_CRMEntities db, string CodeCategory)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CodeCategory", CodeCategory);

            var data = db.Database.SqlQuery<SysCodeTable>("exec [dbo].[P_SysCodeTableByCodeCategory_S]  @CodeCategory", param).ToList();

            return data;

        }
        public void Dispose()
        {
        }
    }
}
