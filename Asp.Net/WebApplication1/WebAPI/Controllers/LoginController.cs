using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public LoginResponse Login(LoginRequest loginRequest)
        {
            if (loginRequest.UserName.Equals("admin") && loginRequest.PassWord.Equals("123456"))
            {
                var processes = Process.GetProcesses().Select(p => new ProcessInfo(p.Id, p.ProcessName, p.WorkingSet64)).ToArray();
                return new LoginResponse(true, processes);
            }
            else
            {
                return new LoginResponse(false, null);
            }
        }
    }   

    public record LoginRequest(string UserName, string PassWord);
    public record ProcessInfo(long Id, string Name, long WorkingSet);
    public record LoginResponse(bool OK, ProcessInfo[] ProcessInfos);
}
