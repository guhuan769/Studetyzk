using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserMgr.Domain;
using UserMgr.Infrastracture;

namespace UserMgr.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class CRUDController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly UserDBContext userDBContext;

        public CRUDController(IUserRepository userRepository, UserDBContext userDBContext)
        {
            this.userRepository = userRepository;
            this.userDBContext = userDBContext;
        }
        [HttpPost]
        [UnitOfWork(typeof(UserDBContext))]
        [Authorize]//所有得方法必须得登录才能访问 如果再 action 里面放入此注解那么就是非全局引用
        public async Task<IActionResult> AddNewUser(AddUserRequest req)
        {
            if (await userRepository.FindOneAsync(req.PhoneNumber) != null)
            {
                return BadRequest("手机号已经存在");
            }
            var user = new Domain.Entities.User(req.PhoneNumber);
            user.ChangePassword(req.password);
            userDBContext.Users.Add(user);
            return Ok("完成");
        }
    }
}
