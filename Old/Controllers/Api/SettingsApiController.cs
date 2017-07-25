// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsApiController.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the SettingsApiController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Editor.Controllers
{
    using System.Web.Http;

    using Editor.Models;
    using Editor.Models.DataAccess;

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
        public IHttpActionResult SaveDBConnection(DatabaseConnectionModel model)
        { 
            if (ModelState.IsValid)
            {
                SettingsDA.SaveDbConnection(model);
            }
            return Ok(false, "", "");

        }
    }
}
