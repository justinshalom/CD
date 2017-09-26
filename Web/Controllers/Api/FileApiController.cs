// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileApiController.cs" company="CD">
//  CD
// </copyright>
// <summary>
//   The file API controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis.CSharp;

namespace Web.Controllers.Api
{
    using System.Web.Mvc;
   
    public class FileApiController : BaseApiController
    {
        public JsonResult SaveCode(string filePath, string fileContent)
        {
            System.IO.File.WriteAllText(filePath, fileContent);
            return this.OutPut(false);
        }

        public JsonResult GetJson(string filePath)
        {
            string programText = System.IO.File.ReadAllText(filePath);
            var programTree = CSharpSyntaxTree.ParseText(programText);
            var syntaxRoot = programTree.GetRoot();
            return this.OutPut(syntaxRoot);
        }
    }
}