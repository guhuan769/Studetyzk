using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgr.Domain
{
    public enum UserAccessResult
    {
        OK, //成功
        PhoneNumberNotFound,//手机号不存在
        Lockout,//锁定
        NoPassword,//密码不存在
        PasswordError//密码错误
    }
}
