using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AngleSharp.Parser.Html;
using Editor.Code.File;

namespace Editor.Controllers
{
    /// <summary>
    /// Json Controller
    /// </summary>
    /// <seealso cref="Editor.Controllers.BaseController" />
    public class JsonController : BaseController
    {
        private string regreplace(string pattern, string substitution, string content)
        {
            Regex regex = new Regex(pattern);
            string result = regex.Replace(content, substitution);
            return result;
        }
        private string ApplyGeneratedId(string content, string controller, string view, string appendfrom = "")
        {
            var seperator = "-$-";
            var seperatorreg = @"-\$-";
            content = regreplace(@"(\()("")(.*)("")(.*)("")(.*)("")(.*)("")(.*)("")(\))", @"$1" + seperator + "$3" + seperator + "$5" + seperator + "$7" + seperator + "$9" + seperator + "$11" + seperator + "$13", content);
            content = regreplace(@"(\()("")(.*)("")(.*)("")(.*)("")(\))", @"$1'" + seperator + "$3'" + seperator + "$5'" + seperator + "$7'" + seperator + "$9", content);
            content = regreplace(@"(\()("")(.*)("")(\))", @"$1" + seperator + "$3" + seperator + "$5", content);
            Regex regex = new Regex(@"(\()(.*)("")(.*)(\))");
            while (regex.Match(content).Length > 0)
            {
                content = regreplace(@"(\()(.*)("")(.*)(\))", @"$1$2" + seperator + "$4$5", content);
            }
            var genidname = "data-genid";
            var html = "";
            try
            {
                var parser = new HtmlParser();
                var document = parser.Parse(content);
                var uniquekey = 0;
                var elements = document.QuerySelectorAll("*");

                foreach (var el in elements)
                {
                    el.RemoveAttribute(genidname);
                    el.SetAttribute(genidname, controller + "_" + view + "_" + uniquekey);
                    uniquekey++;
                }
                if (appendfrom != "")
                {
                    html = document.DocumentElement.QuerySelector(appendfrom).InnerHtml;
                }
                else
                {
                    html = document.DocumentElement.OuterHtml;
                }
            }
            catch (Exception e)
            {
                var eobj = e;
            }
            html = regreplace(seperatorreg, '"'.ToString(), html);
            return html;
        }

        /// <summary>
        /// Adds the class.
        /// </summary>
        /// <param name="genid"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="controllername">The controller.</param>
        /// <param name="view">The view.</param>
        /// <param name="area">The area.</param>
        /// <returns></returns>

        public JsonResult Attribute(string genid, string key = "", string value = "", string controllername = "", string view = "layout", string area = "")
        {
            var content = "";
            var filepath = "";
            if (area == "" && controllername == "shared")
            {
                filepath = FIleUtilities.RootPath + FIleUtilities.ViewPath + FIleUtilities.SharedPath +
                           "_" + view + FIleUtilities.Dot.cshtml;
                content = FIleUtilities.GetFileContent(filepath);

            }
            else
            {
                content = FIleUtilities.GetFileContent(area, controllername, view, FIleUtilities.ViewPath,
                    FIleUtilities.Dot.cshtml);
            }
            try
            {
                var parser = new HtmlParser();
                var document = parser.Parse(content);
                document.QuerySelector("[data-genid=" + genid + "]").SetAttribute(key, value);
                var html = "";
                html = area == "shared" ? document.DocumentElement.OuterHtml : document.DocumentElement.QuerySelector("body").InnerHtml;
                if (filepath.Length == 0)
                {
                    FIleUtilities.SetFileContent(area, controllername, view, html, FIleUtilities.ViewPath,
                        FIleUtilities.Dot.cshtml);
                }
                else
                {
                    FIleUtilities.SetFileContent(html, filepath);
                }
            }
            catch (Exception e)
            {
                var eobj = e;
            }
            return Json(false, String.Empty, string.Empty);
        }

        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        /// <param name="controllername">The controller.</param>
        /// <param name="view">The view.</param>
        /// <param name="area">The area.</param>
        /// <returns></returns>

        public JsonResult PrepareHtmlContent(string controllername, string view, string area = "")
        {
            var content = FIleUtilities.GetFileContent(area, controllername, view, FIleUtilities.ViewPath, FIleUtilities.Dot.cshtml);
            var html = ApplyGeneratedId(content, controllername, view, "body");
            var layout = content.Split(new string[2] { "Layout=", "Layout =" }, StringSplitOptions.None);
            var layoutFile = "";
            if (layout.Length < 2)
            {
                layoutFile = FIleUtilities.RootPath + FIleUtilities.ViewPath + FIleUtilities.SharedPath +
                    "_Layout.cshtml";
            }
            else
            {
                layoutFile = FIleUtilities.RootPath + string.Join("", layout[1].Split(new string[2] { System.Environment.NewLine, ";" }, StringSplitOptions.RemoveEmptyEntries)[0].Split(new string[1] { '"' + "" }, StringSplitOptions.RemoveEmptyEntries)).Replace("~", "").Replace(" ", "");
            }

            var layoutcontent = FIleUtilities.GetFileContent(layoutFile);
            var layouthtml = ApplyGeneratedId(layoutcontent, "Shared", "Layout");

            if (layouthtml != layoutcontent)
            {

                ////layoutcontent = editorSignature[0] + System.Environment.NewLine + layoutcontent + System.Environment.NewLine +
                ////editorSignature[0];
                FIleUtilities.SetFileContent(layouthtml, layoutFile);
                return Json(true, string.Empty, string.Empty);
            }
            if (html != content)
            {
                var success = FIleUtilities.SetFileContent(area, controllername, view, html, FIleUtilities.ViewPath, FIleUtilities.Dot.cshtml);
                return Json(true, string.Empty, string.Empty);
            }
            return Json(false, String.Empty, string.Empty);
        }
	}
}