using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityCheck.Domain.Chart
{
    public static class ChartColors
    {
        public static List<string> Colors = new List<string>()
        {
            ColorTranslator.ToHtml(Color.Coral),
            ColorTranslator.ToHtml(Color.Chocolate),
            ColorTranslator.ToHtml(Color.DarkGreen),
            ColorTranslator.ToHtml(Color.DodgerBlue),
            ColorTranslator.ToHtml(Color.Orange),
            ColorTranslator.ToHtml(Color.Purple),
            ColorTranslator.ToHtml(Color.Red),
            ColorTranslator.ToHtml(Color.White),
            ColorTranslator.ToHtml(Color.Yellow),
            ColorTranslator.ToHtml(Color.Khaki),
            ColorTranslator.ToHtml(Color.Magenta),
            ColorTranslator.ToHtml(Color.Cyan),
            ColorTranslator.ToHtml(Color.Tomato)
        };
    }
}
