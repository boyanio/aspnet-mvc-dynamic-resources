using System;

namespace AspNetMvcDynamicResources
{
    internal static class PathUtil
    {
        internal static string Format(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            if (path.StartsWith("~"))
            {
                path = path.Substring(1);
            }

            if (!path.StartsWith("/"))
            {
                path = $"/{path}";
            }

            return path;
        }
    }
}