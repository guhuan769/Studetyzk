using UserMgr.Domain.ValueObject;

namespace UserMgr.WebAPI.Controllers
{
    public record LoginByPhoneAndPasswordResult(PhoneNumber phoneNumber,string password);
}
