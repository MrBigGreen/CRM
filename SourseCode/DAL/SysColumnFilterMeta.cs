using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{


    [MetadataType(typeof(SysColumnFilterMetadata))]//使用SysColumnFilterMetadata对SysColumnFilter进行数据验证
    public partial class SysColumnFilter
    {

        #region 自定义属性，即由数据实体扩展的实体

        #endregion

    }
    public class SysColumnFilterMetadata
    {


    }
    /// <summary>
    /// 自定义列
    /// </summary>
    public class CustomColumn
    {
        public string SysMenuID { get; set; }
        public string SysPersonID { get; set; }
        public string ColumnName { get; set; }
        public string ColumnText { get; set; }
        public int Sort { get; set; }
        public int ColumnWidth { get; set; }
        public bool IsShow { get; set; }
        public bool IsCondition { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<int> VersionNo { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedTime { get; set; }


        public int CustomShow { get; set; }

    }




}
