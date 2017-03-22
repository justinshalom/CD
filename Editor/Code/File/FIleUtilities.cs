using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Editor.Code.File
{
    public class FIleUtilities
    {
        internal static string GetFileContent(string a, string c, string v)
        {
            string root = System.AppDomain.CurrentDomain.BaseDirectory+@"\Views\";
            var path = string.Format(@"{0}{1}\{2}.cshtml", root, c, v);
            if (!string.IsNullOrEmpty(a))
            {
                path = string.Format(@"{0}{1}\{2}\{3}.cshtml", root, a, c, v);
            }
            return System.IO.File.ReadAllText(path);
        }
    }
}