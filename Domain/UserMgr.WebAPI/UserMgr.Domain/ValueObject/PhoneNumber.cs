using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgr.Domain.ValueObject
{
    public record PhoneNumber(int RegionNumber,string PhoneNum);
}
