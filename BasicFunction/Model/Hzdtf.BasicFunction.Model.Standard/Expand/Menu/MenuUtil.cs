using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using MessagePack;

namespace Hzdtf.BasicFunction.Model.Standard.Expand.Menu
{
    /// <summary>
    /// 菜单辅助类
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public static class MenuUtil
    {
        /// <summary>
        /// 转换为有组织的列表并排序
        /// </summary>m>
        /// <param name="menus">菜单列表</param>
        /// <param name="parentId">父ID</param>
        /// <returns>有组织的列表</returns>
        public static IList<MenuInfo> ToOrigAndSort(this IList<MenuInfo> menus, int parentId = 0)
        {
            if (menus.IsNullOrCount0())
            {
                return null;
            }

            IList<MenuInfo> list = ToOrig(menus, parentId);
            Sort(list);

            return list;
        }

        /// <summary>
        /// 转换为有组织的列表
        /// </summary>
        /// <param name="menus">菜单列表</param>
        /// <param name="parentId">父ID</param>
        /// <returns>有组织的列表</returns>
        public static IList<MenuInfo> ToOrig(this IList<MenuInfo> menus, int parentId = 0)
        {
            if (menus.IsNullOrCount0())
            {
                return null;
            }

            IList<MenuInfo> result = new List<MenuInfo>();
            foreach (MenuInfo menu in menus)
            {
                if (menu.ParentId == parentId)
                {
                    result.Add(menu);

                    menu.Children = ToOrig(menus, menu.Id);
                }
            }

            return result;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="menus">菜单列表</param>
        /// <returns>有组织的菜单列表</returns>
        public static void Sort(IList<MenuInfo> menus)
        {
            if (menus.IsNullOrCount0())
            {
                return;
            }

            menus.Sort(new MenuComparer(), x =>
            {
                Sort(x.Children);
            });
        }

        /// <summary>
        /// 转换为菜单树列表
        /// </summary>
        /// <param name="menus">菜单</param>
        /// <returns>菜单树列表</returns>
        public static IList<MenuTreeInfo> ToMenuTrees(this IList<MenuInfo> menus)
        {
            if (menus.IsNullOrCount0())
            {
                return null;
            }

            IList<MenuTreeInfo> menuTrees = new List<MenuTreeInfo>();
            foreach (var m in menus)
            {
                MenuTreeInfo mt = new MenuTreeInfo()
                {
                    Id = m.Id,
                    Code = m.Code,
                    Text = m.Name
                };
                menuTrees.Add(mt);

                // 把功能项也添加到菜单树中
                if (!m.Functions.IsNullOrCount0())
                {
                    foreach (var f in m.Functions)
                    {
                        MenuTreeInfo mf = new MenuTreeInfo()
                        {
                            Id = f.Id,
                            Code = f.Code,
                            Text = f.Name,
                            Type = MenuFunctionType.FUNCTION,
                            MenuFunctionId = f.MenuFunctionId
                        };
                        mt.Children.Add(mf);
                    }
                }

                if (m.Children.IsNullOrCount0())
                {
                    continue;
                }

                IList<MenuTreeInfo> childMenus = ToMenuTrees(m.Children);
                foreach (var c in childMenus)
                {
                    mt.Children.Add(c);
                }
            }

            return menuTrees;
        }

        /// <summary>
        /// 菜单比较
        /// </summary>
        class MenuComparer : Comparer<MenuInfo>
        {
            /// <summary>
            /// 比较
            /// </summary>
            /// <param name="x">菜单X</param>
            /// <param name="y">菜单Y</param>
            /// <returns>比较后的值</returns>
            public override int Compare(MenuInfo x, MenuInfo y)
            {
                if (x.Sort < y.Sort)
                {
                    return -1;
                }
                else if (x.Sort == y.Sort)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}
