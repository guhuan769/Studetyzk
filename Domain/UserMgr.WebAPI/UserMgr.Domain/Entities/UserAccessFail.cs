using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgr.Domain.Entities
{
    public record UserAccessFail
    {
        public Guid Id { get; init; }
        public User User { get; init; }
        public Guid UserId { get; init; }
        /// <summary>
        /// 是否已锁定
        /// </summary>

        [Description("锁定状态")]
        private bool isLockOut;

        /// <summary>
        /// 锁定日期
        /// </summary>
        [Description("锁定时间")]
        public DateTime? LockEnd { get; private set; }

        /// <summary>
        /// 锁定次数
        /// </summary>
        [Description("锁定次数")]
        public int AccessFailCount { get; private set; }

        /// <summary>
        /// 这是给EF core 加载时用的空的构造方法
        /// </summary>
        [Description("无参构造函数")]
        
        private UserAccessFail() { }

        /// <summary>
        /// 这是给程序员使用的 传进一个User
        /// </summary>
        /// <param name="user"></param>
        [Description("有参构造函数")]
        public UserAccessFail(User user)
        {
            this.Id = Guid.NewGuid();
            this.User = user;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            this.AccessFailCount = 0;
            this.LockEnd = null;
            this.isLockOut = false;
        }

        /// <summary>
        /// 登录错误
        /// </summary>
        public void Fail()
        {
            //登录错误计数
            this.AccessFailCount++;
            //如果登录次数大于等于3就锁定用户
            if (this.AccessFailCount >= 3)
            {
                //锁定5分钟
                this.LockEnd = DateTime.Now.AddMinutes(5);
                this.isLockOut = true;
            }
        }

        /// <summary>
        /// 判断用户是否被锁定
        /// </summary>
        /// <returns></returns>
        public bool IsLockOut()
        {
            if (this.isLockOut)
            {
                //如果当前日期大于锁定日期 就证明锁定已过期
                if (DateTime.Now > this.LockEnd)//已经超过了锁定时间
                {
                    Reset();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
