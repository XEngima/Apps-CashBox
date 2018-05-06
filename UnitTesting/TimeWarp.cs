using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.Classes
{
    public class TimeWarp : IDisposable
    {
        public int Days
        {
            get;
            private set;
        }

        public TimeWarp(int days)
        {
            Days = days;
            CurrentApplication.SkipDays(days);
        }

        public void Dispose()
        {
            CurrentApplication.SkipDays(-Days);
        }
    }
}
