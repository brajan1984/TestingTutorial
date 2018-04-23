using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorkshop.Models
{
    public class Hour24Model
    {
        public TimeNoModel hour;
        public TimeNoModel minutes;

        public TimeNoModel seconds;

        public override string ToString()
        {
            return new TimeSpan(hour.fullNo, minutes.fullNo, seconds == null ? 0 : seconds.fullNo).ToString();
        }
    }
}
