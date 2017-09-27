// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DIrectoryModel.cs" company="CodeEditor">
//   Code Editor
// </copyright>
// <summary>
//   Defines the DIrectory Model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Web.Models
{
    using System;

    public class DIrectoryModel
    {
        public string DirectoryName { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public string Extension { get; set; }
    }
}