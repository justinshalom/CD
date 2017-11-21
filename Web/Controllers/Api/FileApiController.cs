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
        public JsonResult SaveProfile(string json,string filename)
        {
            var folderPath = Server.MapPath("~/GeneratedContent/ProfileJson/");
            var filePath = folderPath + filename + ".json";
            System.IO.File.WriteAllText(filePath, json);
            return this.OutPut(false);
        }
        public JsonResult GetProfile(string filename)
        {
            var folderPath = Server.MapPath("~/GeneratedContent/ProfileJson/");
            var filePath = folderPath + filename + ".json";
            if (System.IO.File.Exists(filePath))
            {
                var json = System.IO.File.ReadAllText(filePath);
                return this.OutPut(json);
            }
            return this.OutPut(false);
        }
        public JsonResult CreateDirectory(string folderPath)
        {
            if (!folderPath.Contains(":\\"))
            {
                folderPath = Server.MapPath(folderPath);
            }
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            return this.OutPut(true);
        }

        public JsonResult GetJson(string filePath)
        {
            if (System.IO.File.Exists(filePath)&& filePath!="")
            {
                string programText = System.IO.File.ReadAllText(filePath);
                return this.OutPut(programText);
            }
            else
            {
                return this.OutPut(false);
            }
            
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
                return this.OutPut(directoryModelList,true);
            }
            catch (System.Exception ex)
            {
                return this.OutPut(ex.Message, true);
            }
        }
    }
}