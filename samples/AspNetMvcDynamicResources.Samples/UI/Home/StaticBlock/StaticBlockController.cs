using System.Web.Mvc;

namespace AspNetMvcDynamicResources.Samples.UI.Home.Block
{
    public class StaticBlockController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index(int index)
        {
            return PartialView("~/UI/Home/StaticBlock/Index.cshtml", index);
        }
    }
}