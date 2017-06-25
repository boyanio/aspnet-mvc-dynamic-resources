using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace AspNetMvcDynamicResources
{
    internal static class DynamicBundleCreator
    {
        internal static Bundle GerOrCreateScriptBundleFor(IEnumerable<string> paths)
        {
            return GerOrCreateBundleFor(paths, virtualPath => new ScriptBundle(virtualPath));
        }

        internal static Bundle GerOrCreateStyleSheetBundleFor(IEnumerable<string> paths)
        {
            return GerOrCreateBundleFor(paths, virtualPath => new StyleBundle(virtualPath));
        }

        private static Bundle GerOrCreateBundleFor(IEnumerable<string> paths, Func<string, Bundle> createBundle)
        {
            if (paths == null)
                throw new ArgumentNullException(nameof(paths));

            paths = paths.ToArray();
            if (!paths.Any())
                throw new ArgumentOutOfRangeException(nameof(paths), "No paths");

            if (createBundle == null)
                throw new ArgumentNullException(nameof(createBundle));

            string bundleName = ComputeBundleName(paths);
            string bundleVirtualPath = $"~/bundles/dynamic/{bundleName}";
            var bundle = BundleTable.Bundles.GetBundleFor(bundleVirtualPath);
            if (bundle == null)
            {
                bundle = createBundle(bundleVirtualPath);
                ConfigureBundle(bundle, paths);
            }
            return bundle;
        }

        private static void ConfigureBundle(Bundle bundle, IEnumerable<string> paths)
        {
            bundle.Include(paths.Select(x => x.StartsWith("~") ? x : $"~{(x.StartsWith("/") ? x : $"/{x}")}").ToArray());

            BundleTable.Bundles.Add(bundle);
        }

        private static string ComputeBundleName(IEnumerable<string> paths)
        {
            return ComputeStringHash(
                string.Join(",", paths.OrderBy(x => x).Select(x => x.ToLower())));
        }

        private static string ComputeStringHash(string input)
        {
            string result;
            using (var sha = new SHA256Managed())
            {
                byte[] inputHashBytes = sha.ComputeHash(Encoding.Unicode.GetBytes(input));
                result = HttpServerUtility.UrlTokenEncode(inputHashBytes);
            }
            return result;
        }
    }
}