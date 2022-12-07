﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public DateTime Birthder { get; set; }

        public string BirthPlace { get; set; }
        public double? Salary { get; set; }
    }
}
