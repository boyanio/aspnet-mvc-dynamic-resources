using AspNetMvcDynamicResources;
using System.Linq;
using System.Web.Optimization;

namespace System.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class DynamicResourceAttachmentAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);

            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));

            // A view can trigger child actions to render child views. We want to
            // execute this only for the initial one.
            if (filterContext.ParentActionViewContext == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    AttachDynamicScripts(filterContext.HttpContext);
                    AttachDynamicStyleSheets(filterContext.HttpContext);
                }
            }
        }

        private void AttachDynamicScripts(HttpContextBase httpContext)
        {
            var paths = DynamicScriptExtensions.GetRegisteredPaths(httpContext).ToArray();
            if (paths.Any())
            {
                const string headerName = "X-Scripts";

                var bundle = DynamicBundleCreator.GerOrCreateScriptBundleFor(paths);
                string urls = Scripts.RenderFormat("{0}", bundle.Path)
                    .ToHtmlString()
                    .Trim()
                    .Replace(Environment.NewLine, ",");
                httpContext.Response.Headers.Add(headerName, urls);
            }
        }

        private void AttachDynamicStyleSheets(HttpContextBase httpContext)
        {
            var paths = DynamicStyleSheetExtensions.GetRegisteredPaths(httpContext).ToArray();
            if (paths.Any())
            {
                const string headerName = "X-StyleSheets";

                var bundle = DynamicBundleCreator.GerOrCreateStyleSheetBundleFor(paths);
                string urls = Styles.RenderFormat("{0}", bundle.Path)
                    .ToHtmlString()
                    .Trim()
                    .Replace(Environment.NewLine, ",");
                httpContext.Response.Headers.Add(headerName, urls);
            }
        }
    }
}