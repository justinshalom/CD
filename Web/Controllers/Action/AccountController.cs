namespace Web.Controllers.Action
{
    using System.Web.Mvc;

    public class AccountController : BaseController
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return this.View();
        }
    }
}