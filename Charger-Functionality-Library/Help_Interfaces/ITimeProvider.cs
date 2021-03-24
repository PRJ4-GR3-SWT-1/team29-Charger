using System;
using System.Collections.Generic;
using System.Text;

namespace Charger_Functionality_Library.Help_Interfaces
{
    public interface ITimeProvider
    {
        public DateTime CurrentTime();
    }

    public class TimeProvider : ITimeProvider
    {
        public DateTime CurrentTime()
        {
            DateTime now = DateTime.Now;
            return now;
        }
    }
}
