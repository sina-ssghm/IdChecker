using System;
using System.Collections.Generic;
using System.IO;
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
        
        public static string UrlToDirectoryPath(this string txt)
        {
            return txt!=null? txt.Remove(0, txt.IndexOf("Files") - 1):null;
        }

        public static string PathToUrl(this string txt)
        {
            return txt != null ? Directory.GetCurrentDirectory()+@"\wwwroot"+txt : null;
        }



    }
}
