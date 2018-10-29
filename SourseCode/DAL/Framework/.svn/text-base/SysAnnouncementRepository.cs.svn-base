using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Data;
using System.Data.SqlClient;
namespace CRM.DAL
{
    /// <summary>
    /// 公告管理
    /// </summary>
    public partial class SysAnnouncementRepository
    {
        public SysAnnouncement GetTop(int num)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return db.SysAnnouncement.Where(w => w.State == "开启").OrderBy(o => o.CreateTime).Take(num).SingleOrDefault();
            }
        }
          

    }
}

