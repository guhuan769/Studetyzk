using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgr.Domain.ValueObject;

namespace UserMgr.Domain
{
    /// <summary>
    /// 需要状 NuGet\Install-Package MediatR -Version 11.1.0
    /// </summary>
    /// <param name="PhoneNumber"></param>
    /// <param name="Result"></param>
    public record class UserAccessResultEvent(PhoneNumber PhoneNumber , UserAccessResult Result):INotification;
}
