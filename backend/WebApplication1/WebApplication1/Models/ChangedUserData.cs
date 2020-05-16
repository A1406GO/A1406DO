﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ChangedUserData
    {
        //需要执行添加的数据信息
        public List<UserInfo> Add { get; set; }
        //需要执行删除的数据信息
        public List<UserInfo> Delete { get; set; }
        //需要执行更新的数据信息
        public List<UserInfo> Update { get; set; }
    }
}
