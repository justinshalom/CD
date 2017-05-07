using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Editor.Code.Api
{
    public class ApiJsonDto
    {
        public bool IsTrue { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}