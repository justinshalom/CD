// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsApiController.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the SettingsApiController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Controllers.Api
{
    using System.Web.Mvc;

    using Web.Models;
    using Web.Models.DataAccess;

    /// <summary>
    /// The settings api controller.
    /// </summary>
    public class SettingsApiController : BaseApiController
    {
        /// <summary>
        /// The save db connection.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        public JsonResult SaveDBConnection(DatabaseConnectionModel model)
        { 
            if (ModelState.IsValid)
            {
                SettingsDA.SaveDbConnection(model);
            }
            return this.OutPut(string.Empty);

        }
    }
}
