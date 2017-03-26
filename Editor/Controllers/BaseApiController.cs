﻿using Editor.Code.Api;
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
        /// <param name="error">if set to <c>true</c> [error].</param>
        /// <param name="message">The message.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        /// Json Data
        /// </returns>
        protected IHttpActionResult Ok(bool error, string message, object data)
        {
            var rt = new ApiJsonDto
            {
                IsError = error,
                Message = message,
                Data = data
            };
            return Ok(rt);
        }

    }
    
}