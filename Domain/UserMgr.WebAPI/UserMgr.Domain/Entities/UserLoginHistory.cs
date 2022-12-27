using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgr.Domain.ValueObject;

namespace UserMgr.Domain.Entities
{

    /*
    小结 EF Core 怎么不用外键。
        EF Core 对于实体之间得关系得外键是强制生成得。 （互联网公司不建外键是为了方便分库分表）
            1） 写SQL脚本删除外键
            2） DDD思维 。 聚合内得外键保留。 聚合之间自然没有外键
     */
    public class UserLoginHistory : IAggregateRoot
    {
        //public UserLoginHistory(long id) { this.Id = id; }
        public long Id { get; init; }
        /// <summary>
        /// 指一个指向User实体得外键，但是在物理上，我们并没有创建他们得外键关系
        /// </summary>
        public Guid? UserId { get; init; }//用户ID
        public PhoneNumber PhoneNumber { get; init; }//手机号
        public DateTime CreateDateTime { get; init; }//创建时间
        public string Message { get;init; }//消息

        private UserLoginHistory() { }
        public UserLoginHistory(Guid? UserId, PhoneNumber phoneNumber, string message, DateTime createDateTime)
        {
            this.UserId = UserId;
            PhoneNumber = phoneNumber;
            Message = message;
            CreateDateTime = createDateTime;
        }


    }
}
