using AspNetMvcDynamicResources;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace System.Web.Mvc
{
    public static class DynamicScriptExtensions
    {
        private static readonly string HttpRequestItemKey = typeof(DynamicScriptExtensions).Name;

        public static void RegisterDynamicScript(this HtmlHelper helper, string path)
        {
            if (helper == null)
                throw new ArgumentNullException(nameof(helper));

            RegisterPath(helper.ViewContext.HttpContext, PathUtil.Format(path));
        }

        public static void RegisterDynamicScripts(this HtmlHelper helper, params string[] paths)
        {
            if (paths == null)
                throw new ArgumentNullException(nameof(paths));

            foreach (string path in paths)
            {
                RegisterDynamicScript(helper, path);
            }
        }

        public static IHtmlString RenderDynamicScripts(this HtmlHelper helper)
        {
            if (helper == null)
                throw new ArgumentNullException(nameof(helper));

            var registeredPaths = GetRegisteredPaths(helper.ViewContext.HttpContext);
            if (!registeredPaths.Any())
                return MvcHtmlString.Empty;

            var bundle = DynamicBundleCreator.GerOrCreateScriptBundleFor(registeredPaths);
            return Scripts.Render(bundle.Path);
        }

        private static void RegisterPath(HttpContextBase httpContext, string path)
        {
            if (!httpContext.Items.Contains(HttpRequestItemKey))
            {
                httpContext.Items[HttpRequestItemKey] = new HashSet<string>();
            }
            ((HashSet<string>)httpContext.Items[HttpRequestItemKey]).Add(path);
        }

        internal static string[] GetRegisteredPaths(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            return (httpContext.Items[HttpRequestItemKey] as IEnumerable<string> ?? Enumerable.Empty<string>()).ToArray();
        }
    }
}