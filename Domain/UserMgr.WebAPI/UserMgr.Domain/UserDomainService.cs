using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgr.Domain.Entities;
using UserMgr.Domain.ValueObject;

namespace UserMgr.Domain
{
    /// <summary>
    /// 实现领域服务
    /// </summary>
    public class UserDomainService
    {
        /// <summary>
        /// 注入一个UserRepository
        /// </summary>
        private IUserRepository userRepository;

        /// <summary>
        /// 注入一个短信发送服务
        /// </summary>
        private ISmsCodeSender smsCodeSender;

        public UserDomainService(ISmsCodeSender smsCodeSender, IUserRepository userRepository)
        {
            this.smsCodeSender = smsCodeSender;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// 此处体现就是我的聚合根管理得所有得我其他里所有得对象
        /// </summary>
        /// <param name="user"></param>
        public void ResetAccessFail(User user)
        {
            user.UserAccessFail.Reset();
        }

        /// <summary>
        /// 是否已锁定 true 锁定 false 未锁定
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsLockOut(User user)
        {
            return user.UserAccessFail.IsLockOut();
        }

        /// <summary>
        /// 登陆错误得次数记录
        /// </summary>
        /// <param name="user"></param>
        public void AccessFail(User user)
        {
            user.UserAccessFail.Fail();
        }

        /// <summary>
        /// 判断密码是否正确
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        public async Task<UserAccessResult> CheckPassword(PhoneNumber phoneNumber, string password)
        {
            UserAccessResult result;
            //根据手机号查询该用户
            var user = await userRepository.FindOneAsync(phoneNumber);
            if (user == null)//如果等于null就不存在
            {
                result = UserAccessResult.PhoneNumberNotFound;
            }
            else if (IsLockOut(user))//如果被锁定就告诉用户被锁定了
            {
                result = UserAccessResult.Lockout;
            }
            else if (user.HasPassword())//如果没密码就告诉用户没密码
            {
                result = UserAccessResult.NoPassword;
            }
            else if (user.CheckPassword(password))//检查密码成功
            {
                result = UserAccessResult.OK;
            }
            else
            {
                result = UserAccessResult.PasswordError;
            }
            if (user != null)
            {
                if (result == UserAccessResult.OK)//如果发现结果时OK
                {
                    ResetAccessFail(user);//重置
                }
                else
                {
                    this.AccessFail(user);//处理登录失败
                }
            }

            await userRepository.PublishEventAsync(new UserAccessResultEvent(phoneNumber, result));

            return result;

        }

        /// <summary>
        /// 检查验证码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>

        public async Task<CheckCodeResult> CheckCodeAsync(PhoneNumber phoneNumber, string code)
        {
            User? user = await userRepository.FindOneAsync(phoneNumber);
            if (user == null)
            {
                return CheckCodeResult.PhoneNumberNotFound;
            }
            else if (IsLockOut(user)) //
            {
                return CheckCodeResult.Lockout;
            }

            //服务器端找到这个验证码
            string? codeInServer = await userRepository.FindPhoneNumberCodeAsync(phoneNumber);
            if (codeInServer == null)
            {
                return CheckCodeResult.CodeError;
            }
            if (codeInServer == code)
            {
                return CheckCodeResult.OK;
            }
            else
            {
                AccessFail(user);
                return CheckCodeResult.CodeError;
            }
        }
    }
}
