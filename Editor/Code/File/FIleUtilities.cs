using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Editor.Code.File
{
    /// <summary>
    /// File Utilities
    /// </summary>
    internal static class FIleUtilities
    {
        internal enum Dot
        {
            js,
            cshtml,
            html,
            cs,
            aspx,
            css
        }

        /// <summary>
        /// Gets the root path.
        /// </summary>
        /// <value>
        /// The root path.
        /// </value>
        internal static string RootPath
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory; }
        }

        /// <summary>
        /// Gets the view path.
        /// </summary>
        /// <value>
        /// The view path.
        /// </value>
        internal static string ViewPath
        {
            get { return @"\Views\"; }
        }

        internal static string SharedPath
        {
            get { return @"\Shared\"; }
        }

        /// <summary>
        /// Gets the controller path.
        /// </summary>
        /// <value>
        /// The controller path.
        /// </value>
        internal static string ControllerPath
        {
            get { return @"\Controllers\"; }
        }

        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        /// <param name="a">area.</param>
        /// <param name="c">The controller.</param>
        /// <param name="v">The view.</param>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="dot">The dot Extension.</param>
        /// <returns>
        /// File Content
        /// </returns>
        internal static string GetFileContent(string a, string c, string v, string folderPath, Dot dot)
        {
            string root = RootPath + folderPath;
            var filepath = string.Format(@"{0}{1}\{2}.{3}", root, c, v, dot.ToString());
            if (!string.IsNullOrEmpty(a))
            {
                filepath = string.Format(@"{0}{1}\{2}\{3}.{4}", root, a, c, v, dot.ToString());
            }
            return System.IO.File.ReadAllText(filepath);
        }

        /// <summary>
        /// Sets the content of the file.
        /// </summary>
        /// <param name="a">area.</param>
        /// <param name="c">The controller.</param>
        /// <param name="v">The view.</param>
        /// <param name="content">The content.</param>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="dot">The dot Extension.</param>
        /// <returns>
        /// true
        /// </returns>
        internal static object SetFileContent(string a, string c, string v, string content, string folderPath, Dot dot)
        {
            string root = RootPath + folderPath;
            var path = string.Format(@"{0}{1}\{2}.{3}", root, c, v, dot.ToString());
            if (!string.IsNullOrEmpty(a))
            {
                path = string.Format(@"{0}{1}\{2}\{3}.{4}", root, a, c, v, dot.ToString());
            }
            System.IO.File.WriteAllText(path, content);
            return true;
        }

        internal static string GetFileContent(string layoutFile)
        {
            return System.IO.File.ReadAllText(layoutFile);
        }

        internal static object SetFileContent(string content,string path)
        {
            try
            {
                System.IO.File.WriteAllText(path, content);
                return true;
            }
            catch (Exception e)
            {
                var obj = e;
                return false;
            }
        }
    }
}