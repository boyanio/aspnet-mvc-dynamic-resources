using System.Web.Mvc;

namespace AspNetMvcDynamicResources.Samples
{
    internal static class FilterConfig
    {
        internal static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new DynamicResourceAttachmentAttribute());
        }
    }
}