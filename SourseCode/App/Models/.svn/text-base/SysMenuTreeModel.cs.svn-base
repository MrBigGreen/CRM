using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using CRM.DAL;
using CRM.BLL;
using System.Text;
using Models;
namespace CRM.App.Models
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class SysMenuTreeNodeCollection
    {
        IEnumerable<SysMenu> listTree;
        public bool Bind(IEnumerable<SysMenu> entitys, string myParentId, ref List<SystemTree> myChildren)
        {
            if (null != myParentId)
                listTree = from o in entitys
                           where o.ParentId == myParentId
                           select o;//叶子节点            
            else
                listTree = from o in entitys
                           where o.ParentId == null
                           select o;//根目录

            if (listTree != null && listTree.Any())
            {//填充数据
                foreach (var item in listTree)
                {
                    SystemTree myTree = new SystemTree() { id = item.Id.GetString(), text = item.Name.GetString() };
                    //if (string.IsNullOrWhiteSpace(item.Status))
                    //    myTree.@checked = false;
                    //else
                    //    myTree.@checked = true;
                        //if (!string.IsNullOrWhiteSpace(item.Iconic))
                    //    myTree.iconCls = item.Iconic;//开启图标
                    myChildren.Add(myTree);
                    if (Bind(entitys, item.Id, ref myTree.children))//递归调用
                    {
                        if (null != item.ParentId)
                        {//根目录
                           // myTree.iconCls = "icon-ok";//如果包含此字符串，则点击查看全部
                            myTree.state = "open";//默认是打开还是关闭
                        }
                        else
                        {
                            myTree.state = "closed";
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }               
        }
    
    
    }
}


