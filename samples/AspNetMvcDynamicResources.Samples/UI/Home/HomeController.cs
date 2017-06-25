using System.Web.Mvc;

namespace AspNetMvcDynamicResources.Samples.UI.Home
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/UI/Home/Index.cshtml");
        }
    }
}