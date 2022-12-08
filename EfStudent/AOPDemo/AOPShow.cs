using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPDemo
{
    public class AOPShow
    {
        DbHelper dbHelper = new DbHelper();
        public  void Show()
        {
            Console.WriteLine("用户注册,提交信息");
            UserInfo userInfo = new UserInfo()
            {
                Account = "Administrator",
                Name = "白虎",
                Password= "Password",
            };
             dbHelper.Save(userInfo);
        }
    }
}
