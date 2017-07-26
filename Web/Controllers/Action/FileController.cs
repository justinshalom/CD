namespace Web.Controllers.Action
{
    using System.Web.Mvc;

    /// <summary>
    /// File Controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class FileController : BaseController
    {

        /// <summary>
        /// Files the manager.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult FileManager()
        {
            return this.View();
        }
        public ContentResult GetJson(string filename)
        {
            var file=this.Server.MapPath("~/Content/json/" + filename);
            var content=System.IO.File.ReadAllText(filename);
            return this.Content(content, "application/json");
        }
	}
}