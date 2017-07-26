namespace Web.Controllers.Api
{
    using System.Net;
    using System.Web.Mvc;

    using Web.Code.Api.Objects;
    using Web.Controllers.Action;

    /// <summary>
    /// Base Api Controller
    /// </summary>
    public class BaseApiController : BaseController
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
        protected JsonResult Ok(bool IsTrue, string message, object data)
        {
            var rt = new ApiJsonDto
            {
                IsTrue = IsTrue,
                Message = message,
                Data = data
            };
            return Json(rt);
        }

    }
    
}