using System;
using System.Collections.Generic;
using System.Text;

namespace Charger_Functionality_Library.Help_Interfaces
{
    public interface ITimeProvider
    {
        public DateTime CurrentTime()
        {
            DateTime test = new DateTime(2000, 01, 01);
            return test;
        }
    }
}
