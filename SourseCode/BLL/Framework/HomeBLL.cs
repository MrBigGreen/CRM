using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.DAL;
using CRM.IBLL;
namespace CRM.BLL
{
    public class HomeBLL : IHomeBLL, IDisposable
    {
        /// <summary>
        /// 根据PersonId 获取已经启用的菜单
        /// </summary>
        /// <param name="personId">人员的Id</param>
        /// <returns>菜单拼接的字符串</returns>
        public string GetMenuByAccount(ref Common.Account person)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                string personId = person.Id;
                var roleIds =
                           (
                           from r in db.SysRole
                           from p in r.SysPerson
                           where p.Id == personId
                           select r.Id
                           ).ToList();
                person.RoleIds = roleIds;

                List<SysMenu> menuNeed =
                            (
                            from m2 in db.SysMenu
                            from f in m2.SysMenuSysRoleSysOperation

                            where roleIds.Any(a => a == f.SysRoleId) && f.SysOperationId == null
                            select m2
                            ).Distinct().OrderBy(o => o.Remark).ToList();//此方法由临海人(qq:1012999282)提供
                //person.MenuIds = menuNeed.Where(w => w.IsLeaf == null).Select(s => s.Url).ToList();

                StringBuilder strmenu2 = new StringBuilder();//拼接菜单的字符串
                int lever = 0;//上一个菜单的层级
                int current = 0;//当前菜单的层级
                //在1.2版本中修改
                if (menuNeed.Count > 1)
                {
                    for (int i = 0; i < menuNeed.Count; i++)
                    {
                        current = menuNeed[i].Remark.Length / 4;//按照4位数字的编码格式

                        if (current == 1)//加载根目录的菜单
                        {
                            //解决ie6下没有滚动条的问题
                            strmenu2.Replace('^', ' ')
                                .Append(string.Format(" <div data-options=@iconCls:'{0}',selected:{1}@ title=@{2}@> <div class=@easyui-panel@ fit=@true@ border=@false@><ul class=@easyui-tree@ >^</ul></div></div>", menuNeed[i].Iconic, menuNeed[i].State == "折叠" ? "false" : "true", menuNeed[i].Name));
                        }
                        else if (current < lever)//进入上一个菜单层级
                        {
                            string replace = string.Empty;
                            for (int c = 0; c < lever - current; c++)//减少了几个层级
                            {
                                replace += ("</ul></li>");
                            }
                            if ((i == menuNeed.Count - 1) || (menuNeed[i].Remark.Length >= menuNeed[i + 1].Remark.Length))
                                strmenu2.Replace("^" + replace, replace + GetNode(menuNeed[i], true));
                            else
                                strmenu2.Replace("^" + replace, replace + GetNode(menuNeed[i], false));
                        }
                        else//进入本级菜单或者下一个菜单
                        {

                            if ((i == menuNeed.Count - 1) || (menuNeed[i].Remark.Length >= menuNeed[i + 1].Remark.Length))//最后一个，或者下一个长度不小于这个的长度
                                strmenu2.Replace("^", GetNode(menuNeed[i], true));
                            else
                                strmenu2.Replace("^", GetNode(menuNeed[i], false));

                        }
                        lever = current;
                    }
                }
                return strmenu2.ToString().Replace('@', '"').Replace('^', ' ');
            }
        }
        /// <summary>
        /// 获取节点的字符串
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetNode(SysMenu item, bool isLeaf = false)
        {
            List<string> dataoptions = new List<string>();
            if (!string.IsNullOrWhiteSpace(item.Iconic))
            {
                dataoptions.Add(string.Format("iconCls:'{0}'", item.Iconic));
            }

            if (isLeaf)
            {
                return string.Format("<li data-options=@{0}@><a href=@#@ icon=@{1}@ rel=@{2}@ id=@{3}@>{4}</a></li>^", string.Join(",", dataoptions), item.Iconic, item.Url, item.Id, item.Name);

            }
            else
            {
                ////此处可以在数据字典中配置，将State字段，配置为下拉框，下拉框其中有一个值是"收缩"
                if (!string.IsNullOrWhiteSpace(item.State) && item.State == "折叠")//收缩展开 && item.State == "收缩"
                {//菜单默认显示关闭
                    dataoptions.Add(string.Format("state:'closed'"));
                }
                return string.Format("<li data-options=@{0}@><span>{1}</span><ul>^</ul></li>", string.Join(",", dataoptions), item.Name);
            }
        }

        public void Dispose()
        {

        }
    }
}

