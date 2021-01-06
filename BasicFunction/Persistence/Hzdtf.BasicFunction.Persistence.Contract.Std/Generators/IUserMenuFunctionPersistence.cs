﻿using Hzdtf.Persistence.Contract.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.BasicFunction.Model.Standard;

namespace Hzdtf.BasicFunction.Persistence.Contract.Standard
{
    /// <summary>
    /// 用户菜单功能持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IUserMenuFunctionPersistence : IPersistence<int, UserMenuFunctionInfo>
    {
    }
}
