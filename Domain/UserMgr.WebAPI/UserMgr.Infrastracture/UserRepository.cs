using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using UserMgr.Domain;
using UserMgr.Domain.Entities;
using UserMgr.Domain.ValueObject;
using Zack.Infrastructure.EFCore;

namespace UserMgr.Infrastracture
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDBContext dBContext;
        private readonly IDistributedCache distributedCache;//分布式缓存 天然有一个超时机制
        private readonly IMediator mediator;

        public UserRepository(UserDBContext dBContext, IDistributedCache distributedCache, IMediator mediator = null)
        {
            this.dBContext = dBContext;
            this.distributedCache = distributedCache;
            this.mediator = mediator;
        }

        public async Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string message)
        {
            User? user = await FindOneAsync(phoneNumber);
            Guid? userId = null;
            if (user != null)
            {
                userId = user.Id;

            }
            dBContext.UserLoginHistorys.Add(new UserLoginHistory(userId, phoneNumber, message, DateTime.Now));
        }

        public Task<User?> FindOneAsync(PhoneNumber phoneNumber)
        {
            //NuGet\Install-Package Zack.Infrastructure -Version 1.1.3
            //return dBContext.Users.SingleOrDefault(x => x.PhoneNumber.PhoneNum == phoneNumber.PhoneNum && x.PhoneNumber.RegionNumber == phoneNumber.RegionNumber);
            User? user = dBContext.Users.SingleOrDefault(ExpressionHelper.MakeEqual((User u) => u.PhoneNumber, phoneNumber));
            return Task.FromResult(user);
        }

        public Task<User?> FindOneAsync(Guid userId)
        {
            User? user = dBContext.Users.SingleOrDefault(x => x.Id == userId);
            return Task.FromResult(user);
        }

        public async Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phoneNumber)
        {
            string key = $"PhoneNumberCode_{phoneNumber.RegionNumber}_{phoneNumber.PhoneNum}";
            string? code = await distributedCache.GetStringAsync(key);
            distributedCache.Remove(key);
            return code;
        }

        public Task PublishEventAsync(UserAccessResultEvent _event)
        {
            //发布服务此处需要使用Mediator
            return mediator.Publish(_event);
        }

        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber, string code)
        {
            string key = $"PhoneNumberCode_{phoneNumber.RegionNumber}_{phoneNumber.PhoneNum}";
            //int code = Random.Shared.Next(1000,9999);
            //再用异步得时候如果代码是最后一句话可以省略 async
            return distributedCache.SetStringAsync(key, code,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
        }
    }
}
