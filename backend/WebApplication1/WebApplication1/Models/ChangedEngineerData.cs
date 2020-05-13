using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ChangedEngineerData
    {
        //需要执行添加的数据信息
        public List<EngineerInfo> AddengineerInfos { get; set; }
        //需要执行删除的数据信息
        public List<EngineerInfo> DeleteengineerInfos { get; set; }
        //需要执行更新的数据信息
        public List<EngineerInfo> UpdataengineerInfos { get; set; }
    }
}
