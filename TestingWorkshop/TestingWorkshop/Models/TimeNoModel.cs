using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorkshop.Models
{
    public class TimeNoModel
    {
        public int first;
        public int second;

        public int fullNo
        {
            get
            {
                return int.Parse(string.Format("{0}{1}", first, second));
            }
        }
    }
}
