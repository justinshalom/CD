using System.Web.Mvc;
using Editor.Code.Api;

namespace Editor.Controllers
{
    public class BaseController:Controller
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
        protected JsonResult Json(bool error, string message, object data)
        {
            var rt = new ApiJsonDto
            {
                IsTrue = error,
                Message = message,
                Data = data
            };
            return Json(rt);
        }
    }
}
