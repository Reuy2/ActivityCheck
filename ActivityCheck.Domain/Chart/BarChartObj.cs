using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityCheck.Domain.Chart
{
    public class BarChartObj
    {
        public string label { get;set; }

        public List<int> data { get; set; }

        public string backgroundColor { get; set; }
    }
}
