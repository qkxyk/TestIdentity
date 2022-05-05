using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExample
{
    internal class Contents
    {
        public static readonly IEnumerable<string> WebSites = new string[]
        {
            "https://www.zhihu.com",
            "https://www.baidu.com",
            "https://www.weibo.com",
            "https://www.stackoverflow.com",
            "https://www.docs.microsoft.com",
            "https://www.hexucloud.cn",
            "https://www.bilibili.com"

        };
    }
}
