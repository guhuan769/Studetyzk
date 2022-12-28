using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UserMgr.Domain;
using UserMgr.Infrastracture;

namespace UserMgr.WebAPI
{
    public class UserAccessResultEventHandler : INotificationHandler<UserAccessResultEvent>
    {
        private readonly IUserRepository userRepository;
        private readonly UserDBContext userDBContext;
        public UserAccessResultEventHandler(IUserRepository userRepository, UserDBContext userDBContext = null)
        {
            this.userRepository = userRepository;
            this.userDBContext = userDBContext;
        }

        //private readonly IServiceScopeFactory serviceScopeFactory;

        //public UserAccessResultEventHandler(IServiceScopeFactory serviceScopeFactory)
        //{
        //    this.serviceScopeFactory = serviceScopeFactory;
        //}

        /// <summary>
        /// 处理该事件 监听该事件
        /// </summary>
        /// <param name="notification"></param>
        public async Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
        {
            //using var scope = serviceScopeFactory.CreateScope();
            //IUserRepository userRepository =
            //    scope.ServiceProvider.GetRequiredService<IUserRepository>();
            //UserDBContext userDBContext =
            //    scope.ServiceProvider.GetRequiredService<UserDBContext>();
            //async 此处手动调用SaveChange
            await userRepository.AddNewLoginHistoryAsync(notification.PhoneNumber, $"登录结果是{notification.Result}");
            await userDBContext.SaveChangesAsync();
        }
    }
}
