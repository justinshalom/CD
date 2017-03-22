using Editor.Code.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using Editor.Code.File;

namespace Editor.Controllers
{
    public class JsonApiController : BaseApiController
    {
        /// <summary>
        /// Get File Content
        /// </summary>
        /// <param name="c"></param>
        /// <param name="v"></param>
        /// <param name="a"></param>
        /// <returns></returns>
       
        public IHttpActionResult GetFileContent(string c, string v, string a = "")
        {
            var content = FIleUtilities.GetFileContent(a, c, v);
            var editorSignature = "<--GeneratedFromCodeEditorDon'tRemoveIt!-->/n";
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(editorSignature);
            if (match.Length < 2)
            {
                content = content.Replace(editorSignature, "");
            }
            content = editorSignature + content + editorSignature;

            return Ok(false, String.Empty, content);
        }
    }
}