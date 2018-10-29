using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    /// <summary>
    /// easyui中的datagrid
    /// </summary>
    public class datagrid
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int total;
        /// <summary>
        /// 行数据集
        /// </summary>
        public dynamic rows;

        /// <summary>
        /// 页脚
        /// </summary>
        public dynamic footer;
    }
    /// <summary>
    /// easyui中的tree
    /// </summary>
    public class treegrid
    {
        /// <summary>
        /// 行数据集
        /// </summary>
        public dynamic rows;
    }
    /// <summary>
    /// 树形列表控件对应的对象
    /// </summary>
    public class SystemTree
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string id;
        /// <summary>
        /// 显示内容
        /// </summary>
        public string text;
        /// <summary>
        /// 图标
        /// </summary>
        public string iconCls;
        /// <summary>
        /// 是否被选中，checked为C#关键字，所以前面加@
        /// </summary>
        public bool @checked = false;
        /// <summary>
        /// 当前是展开还是收缩的状态
        /// </summary>
        public string state;
        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<SystemTree> children = new List<SystemTree>();
    }
    /// <summary>
    /// 列表中的按钮导航
    /// </summary>
    public class toolbar
    {
        /// <summary>
        /// 显示的文本
        /// </summary>
        public string text;
        /// <summary>
        /// 图标
        /// </summary>
        public string iconCls;
        /// <summary>
        /// 处理方法
        /// </summary>
        public string handler;
    }

    /// <summary>
    /// easyui 列
    /// </summary>
    public class columns
    {
        /// <summary>
        /// 列的字段名。
        /// </summary>
        public string field;

        /// <summary>
        /// 列的标题文字
        /// </summary>
        public string title;


        /// <summary>
        /// 列的宽度
        /// </summary>
        public int width;


        /// <summary>
        /// True 就隐藏此列
        /// </summary>
        public bool hidden;
    }


}

