using System;
using System.Web.Optimization;

namespace AspNetMvcDynamicResources.Samples
{
    internal static class BundleConfig
    {
        internal static void RegisterBundles(BundleCollection bundles)
        {
            if (bundles == null)
                throw new ArgumentNullException(nameof(bundles));

            bundles.Add(new ScriptBundle("~/bundles/common/scripts").IncludeDirectory("~/assets/js", "*.js", true));
            bundles.Add(new StyleBundle("~/bundles/common/stylesheets").IncludeDirectory("~/assets/css", "*.css", true));
        }
    }
}