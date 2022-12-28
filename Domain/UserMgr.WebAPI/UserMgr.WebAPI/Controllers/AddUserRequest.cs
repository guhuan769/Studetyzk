using UserMgr.Domain.ValueObject;

namespace UserMgr.WebAPI.Controllers
{
    public record AddUserRequest(PhoneNumber PhoneNumber ,string password);
}
