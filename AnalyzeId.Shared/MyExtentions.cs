using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Shared
{
 public  static class MyExtentions
    {
        public static bool HasValue(this string txt)
        {
            return !string.IsNullOrWhiteSpace(txt);
        }
    }
}
