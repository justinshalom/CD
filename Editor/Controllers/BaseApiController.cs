using Editor.Code.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Editor.Controllers
{
    /// <summary>
    /// Base Api Controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// Return the Output of Json
        /// </summary>
        /// <param name="IsTrue">
        /// The Is True.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// Json Data
        /// </returns>
        protected IHttpActionResult Ok(bool IsTrue, string message, object data)
        {
            var rt = new ApiJsonDto
            {
                IsTrue = IsTrue,
                Message = message,
                Data = data
            };
            return Ok(rt);
        }

    }
    
}