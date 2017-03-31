using System;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using AngleSharp.Parser.Html;
using CsQuery;
using Editor.Code;
using Editor.Code.File;
using Editor.Code.Html;

namespace Editor.Controllers
{
    /// <summary>
    /// Json Api Requests
    /// </summary>
    /// <seealso cref="Editor.Controllers.BaseApiController" />
    public class JsonApiController : BaseApiController
    {
        private string regreplace(string pattern, string substitution, string content)
        {
            Regex regex = new Regex(pattern);
            string result = regex.Replace(content, substitution);
            return result;
        }
        private string ApplyGeneratedId(string content,string controller, string view,string appendfrom)
        {
            var seperator = "-$-";
            var seperatorreg = @"-\$-";
            content = regreplace(@"(\()("")(.*)("")(.*)("")(.*)("")(.*)("")(.*)("")(\))", @"$1"+ seperator + "$3" + seperator + "$5" + seperator + "$7" + seperator + "$9" + seperator + "$11" + seperator + "$13", content);
            content = regreplace(@"(\()("")(.*)("")(.*)("")(.*)("")(\))", @"$1'" + seperator + "$3'" + seperator + "$5'" + seperator + "$7'" + seperator + "$9", content);
            content = regreplace(@"(\()("")(.*)("")(\))", @"$1" + seperator + "$3" + seperator + "$5", content);
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
                    el.RemoveAttribute("genid");
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
            html = regreplace(seperatorreg, '"'.ToString(),html);
            return html;
        }
        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="view">The view.</param>
        /// <param name="area">The area.</param>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public IHttpActionResult PrepareHtmlContent(string controller, string view, string area = "")
        {
            var content = FIleUtilities.GetFileContent(area, controller, view, FIleUtilities.ViewPath, FIleUtilities.Dot.cshtml);
            var html = ApplyGeneratedId(content,controller, view,"body");
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
                return Ok(true, string.Empty, string.Empty);
            }
            if (html != content)
            {
                var success = FIleUtilities.SetFileContent(area, controller, view, html, FIleUtilities.ViewPath, FIleUtilities.Dot.cshtml);
                return Ok(true, string.Empty, string.Empty);
            }
            return Ok(false, String.Empty, string.Empty);
        }

        /// <summary>
        /// Adds the class.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="view">The view.</param>
        /// <param name="area">The area.</param>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public IHttpActionResult addClass(string className, string id, string controller, string view, string area = "")
        {
            var content = FIleUtilities.GetFileContent(area, controller, view, FIleUtilities.ViewPath, FIleUtilities.Dot.cshtml);
            try
            {
                var parser = new HtmlParser();
                var document = parser.Parse(content);
                document.QuerySelector("[genid]").SetAttribute("new", "new");

                var html = document.DocumentElement.OuterHtml;
            }
            catch (Exception e)
            {
                var eobj = e;
            }

            var t = new cQuery("#" + id, content);
            t.addClass(className).removeClass(className).attr("name", "value").removeAttr("name").css("name", "value");

            return Ok(false, String.Empty, string.Empty);
        }
    }
}