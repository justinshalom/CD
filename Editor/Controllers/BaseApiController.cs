using Editor.Code.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Editor.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IHttpActionResult Ok(bool error, string message, object data)
        {
            var rt = new ApiJsonDto();
            rt.IsError = error;
            rt.Message = message;
            rt.Data=data;
            return Ok(rt);
        }

    }
    
}