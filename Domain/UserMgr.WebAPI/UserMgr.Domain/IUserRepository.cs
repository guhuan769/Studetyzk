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
    /// 仓储接口 主要用于放在领域层中
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// 根据手机号找用户 可能会返回NULL
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Task<User?> FindOneAsync(PhoneNumber phoneNumber);
        /// <summary>
        /// 根据用户ID找用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<User?> FindOneAsync(Guid userId);
        /// <summary>
        /// 添加登录日志
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="message"></param>
        /// <returns></returns>

        public Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber , string message);

        /// <summary>
        ///保存手机号对应得code
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber ,string code);

        /// <summary>
        /// 找到手机得码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>

        public Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phoneNumber );

        public Task PublishEventAsync(UserAccessResultEvent _event);

        public Task AddUser(User user);
    }
}
