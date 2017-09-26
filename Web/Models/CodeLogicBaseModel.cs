// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FieldName.cs" company="CodeEditor">
//   Code Editor
// </copyright>
// <summary>
//   Defines the FieldNameAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Web.Models
{
    using System;

    public class CodeLogicBaseModel
    {
        public string ClassName { get; set; }
        public string Language { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string NameSpaceName { get; set; }
        public List<string> Imports { get; set; }

    }
}