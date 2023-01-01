using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly IOptionsSnapshot<JWTSettings> optionsSnapshot;
        public LoginController(UserDomainService userDomainService, IOptionsSnapshot<JWTSettings> optionsSnapshot)
        {
            this.userDomainService = userDomainService;
            this.optionsSnapshot = optionsSnapshot;
        }

        /// <summary>
        /// 根据手机号和密码进行登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [UnitOfWork(typeof(UserDBContext))]//因为CheckPassword可能有修改数据得操作
        public async Task<IActionResult> LoginByPhoneAndPassword(LoginByPhoneAndPasswordResult req)
        {
            #region jwttest
            string jwt = string.Empty;
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "1"));
            claims.Add(new Claim(ClaimTypes.Name, "gh"));
            //新增角色
            claims.Add(new Claim(ClaimTypes.Role,"admin"));

            string key = optionsSnapshot.Value.SecKey;
            DateTime expire = DateTime.Now.AddSeconds(optionsSnapshot.Value.ExpireSeconds);
            byte[] secBytes = Encoding.UTF8.GetBytes(key);
            var secKey = new SymmetricSecurityKey(secBytes);
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(claims: claims,
                expires: expire, signingCredentials: credentials);
            jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            #endregion

            if (req.password.Length <= 3)
            {
                return BadRequest("密码长度必须大于3");
            }
            var result = await userDomainService.CheckPassword(req.phoneNumber, req.password);
            switch (result)
            {
                case UserAccessResult.OK:
                    return Ok($"登录成功 {jwt}");

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
