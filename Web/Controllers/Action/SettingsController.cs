namespace Web.Controllers.Action
{
    using System.Web.Mvc;

    public class SettingsController : BaseController
    {
        // GET: Settings
        public ActionResult DataBase()
        {
            return this.View();
        }
        public ActionResult Project()
        {
            return this.View();
        }
    }
}