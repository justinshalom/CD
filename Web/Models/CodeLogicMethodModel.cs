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

  
    public class CodeLogicMethodModel : CodeLogicClassModel
    {
        public string MethodVisibility { get; set; }
        public string MethodName { get; set; }
        public string InnerCode { get; set; }
        public string ReturnType { get; set; }
        public List<string> Comments { get; set; }
        public List<Dictionary<string,string>> Parameters { get; set; }
        public List<string> Functionalities { get; set; }

    }
}