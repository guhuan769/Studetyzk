using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgr.Domain.ValueObject;
using Zack.Commons;

namespace UserMgr.Domain.Entities
{
    public record User : IAggregateRoot
    {
        public Guid Id { get; init; }
        public PhoneNumber PhoneNumber { get; private set; }
        /// <summary>
        /// 密码散列值
        /// </summary>
        private string? passwordHash;
        public UserAccessFail UserAccessFail { get; private set; }

        private User()
        {

        }

        public User(PhoneNumber phoneNumber)//必须得传手机号
        {
            this.PhoneNumber = phoneNumber;
            this.Id = Guid.NewGuid();
            //把自己传进去
            this.UserAccessFail = new UserAccessFail(this);
        }
        /// <summary>
        /// 判断密码是否为空
        /// </summary>
        /// <returns></returns>
        public bool HasPassword()
        {
            return !string.IsNullOrEmpty(this.passwordHash);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public void ChangePassword(string password)
        {
            if (password.Length <= 3)
            {
                throw new ArgumentOutOfRangeException("密码长度必须大于3");
            }
            this.passwordHash = HashHelper.ComputeMd5Hash(password);    
        }

        /// <summary>
        /// 检查代码是否修改正确
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        { 
            return this.passwordHash == HashHelper.ComputeMd5Hash(password);
        }

        /// <summary>
        /// 修改手机号
        /// </summary>
        /// <param name="phoneNumber"></param>
        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }
    }
}
