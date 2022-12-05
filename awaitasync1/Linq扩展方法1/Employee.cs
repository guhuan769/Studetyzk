using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq扩展方法1
{
    internal class Employee
    {
        public long Id { get; set; }//ID
        public string Name { get; set; } //姓名
        public int  Age { get; set; }//年龄

        public bool Gender { get; set; }       //性别

        public int Salary { get; set; }//月薪

        public override string ToString()
        {
            return $"Id={Id},Name={Name},Age={Age},Gender={Gender},Salary{Salary}";
        }
    }
}
