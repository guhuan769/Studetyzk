using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserMgr.Domain;
using UserMgr.Domain.ValueObject;
using UserMgr.Infrastracture;

namespace UserMgr.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// 调用应用服务
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        private readonly UserDomainService userDomainService;

        public LoginController(UserDomainService userDomainService)
        {
            this.userDomainService = userDomainService;
        }

        /// <summary>
        /// 根据手机号和密码进行登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [UnitOfWork(typeof(UserDBContext))]//因为CheckPassword可能有修改数据得操作
        public async Task<IActionResult> LoginByPhoneAndPassword(LoginByPhoneAndPasswordResult req)
        {
            if (req.password.Length <= 3)
            {
                return BadRequest("密码长度必须大于3");
            }
            var result = await userDomainService.CheckPassword(req.phoneNumber, req.password);
            switch (result)
            {
                case UserAccessResult.OK:
                    return BadRequest("登录成功");

                case UserAccessResult.Lockout:
                    return BadRequest("用户被锁定");
                case UserAccessResult.PhoneNumberNotFound:
                case UserAccessResult.NoPassword:
                case UserAccessResult.PasswordError:
                    return BadRequest("登录失败");
                default:
                    throw new ApplicationException($"未知值{result}");
            }
        }

        //public async Task
    }
}
