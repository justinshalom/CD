﻿using System;
using System.Web.Http;
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
            var signaturetext = "GeneratedFromCodeEditorDon'tRemoveIt";
            var editorSignature = new string[1] {@"<!--" + signaturetext + "!-->"};
            var match = content.Split(editorSignature, StringSplitOptions.None);
            var layout = content.Split(new string[2]{"Layout=","Layout ="},StringSplitOptions.None);
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
            var layoutmatch = layoutcontent.Split(editorSignature, StringSplitOptions.None);
            if (layoutmatch.Length < 3)
            {
                layoutcontent = string.Join("", layoutmatch);
                layoutcontent = editorSignature[0] + System.Environment.NewLine + layoutcontent + System.Environment.NewLine +
                          editorSignature[0];
                FIleUtilities.SetFileContent(layoutcontent, layoutFile);
                return Ok(true, string.Empty, string.Empty);
            }
            if (match.Length < 3)
            {
                content = string.Join("", match);
                content = editorSignature[0] + System.Environment.NewLine + content + System.Environment.NewLine +
                          editorSignature[0];
                var success = FIleUtilities.SetFileContent(area, controller, view, content, FIleUtilities.ViewPath, FIleUtilities.Dot.cshtml);
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
            var t = new cQuery("#" + id,content);
            t.addClass(className).removeClass(className).attr("name","value").removeAttr("name").css("name","value");
            
            return Ok(false, String.Empty, string.Empty);
        }
    }
   
}