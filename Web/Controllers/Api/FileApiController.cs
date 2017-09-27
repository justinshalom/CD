// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileApiController.cs" company="CD">
//  CD
// </copyright>
// <summary>
//   The file API controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using Web.Code.Config;
using Web.Models;

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

        public JsonResult ListDirectory(string directoryName)
        {
            try
            {
                var directoryModelList = new List<DIrectoryModel>();


                foreach (string d in Directory.GetDirectories(directoryName))
                {
                    var pieces = d.Split('\\');
                    var directoryModel = new DIrectoryModel
                    {
                        Extension = "",
                        FileName = "",
                        DirectoryName = pieces[pieces.Length - 1],
                        FullPath = d
                    };
                    directoryModelList.Add(directoryModel);
                }

                foreach (string f in Directory.GetFiles(directoryName))
                {
                    var pieces = f.Split('\\');
                    var file = pieces[pieces.Length - 1];
                    var extpieces = file.Split('.');
                    var extension = "";
                    if (extpieces[extpieces.Length - 1] != null)
                    {
                        extension = extpieces[extpieces.Length - 1].ToLower();
                    }

                    var directoryModel = new DIrectoryModel
                    {
                        Extension = extension,
                        DirectoryName = "",
                        FileName = file,
                        FullPath = f
                    };
                    directoryModelList.Add(directoryModel);
                }
                return this.OutPut(directoryModelList);
            }
            catch (System.Exception ex)
            {
                return this.OutPut(ex.Message);
            }
        }
    }
}