﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GH.MauiLesson
{

    public class FlyoutPageFlyoutMenuItem
    {
        public FlyoutPageFlyoutMenuItem()
        {
            TargetType = typeof(FlyoutPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}