using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.Services;

namespace WebApplication1.Models
{
    public class Modify
    {
        private UserService userservice;
        public Modify(UserService userservice)
        {
            this.userservice = userservice;
        }
        //添加数据，调用该函数，返回日志信息
        public ModifyInfo AddInfo(DateTime Time, string Table, int Num, long Token)//时间 操作的表 数量 Token
        {
            ModifyInfo NewModify = new ModifyInfo();
            NewModify.Time = Time;
            NewModify.Modify = Table + "表中添加了" + Num.ToString() + "条数据";
            NewModify.Username = userservice.FindUser(Token).UserName;
            return NewModify;
        }
        //删除数据，调用该函数，返回日志信息
        public ModifyInfo DeleteInfo(DateTime Time, string Table, int Num, long Token)//时间 操作的表 数量 Token
        {
            ModifyInfo NewModify = new ModifyInfo();
            NewModify.Time = Time;
            NewModify.Modify = Table + "表中删除了" + Num.ToString() + "条数据";
            NewModify.Username = userservice.FindUser(Token).UserName;
            return NewModify;
        }
        //更新日志时，调用该函数，返回日志信息
        public ModifyInfo UpdataInfo(DateTime Time, string Table, int Num, long Token)//时间 操作的表 数量 Token
        {
            ModifyInfo NewModify = new ModifyInfo();
            NewModify.Time = Time;
            NewModify.Modify = Table + "表中更新了" + Num.ToString() + "条数据";
            NewModify.Username = userservice.FindUser(Token).UserName;
            return NewModify;
        }
    }
}
