using System;
using System.Collections.Generic;
using CRM.DAL;
namespace CRM.IBLL
{
    public interface ISysFieldHander
    {
        string GetMyTextsById(string id);
        List<SysField> GetSysField(string table, string colum);
        List<SysField> GetSysField(string table, string colum, string parentMyTexts);
        List<SysField> GetSysFieldByParent(string id, string parentid, string value);
        List<SysField> GetSysFieldByParentId(string id);
        List<SysField> GetSysFieldByParentIdByNew(string id, string dataValue,int type);
    }
}

