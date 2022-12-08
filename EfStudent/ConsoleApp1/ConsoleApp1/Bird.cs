using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCORE
{
    public class Bird
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //[Key]
        public string Number { get; set; }
    }
}
