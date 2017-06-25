using System.Web.Mvc;

namespace AspNetMvcDynamicResources.Samples.UI.Home.DynamicBlock
{
    public class DynamicBlockController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            return PartialView("~/UI/Home/DynamicBlock/Index.cshtml");
        }

        [HttpGet]
        public ActionResult Updated()
        {
            return PartialView("~/UI/Home/DynamicBlock/Updated.cshtml");
        }
    }
}