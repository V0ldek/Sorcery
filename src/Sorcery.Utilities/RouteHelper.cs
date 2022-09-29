// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

namespace Sorcery.Utilities;

public static class RouteHelper
{
    public static string BuildRoute(string root, string name)
    {
        var urlName = GetUrlName(name);
        return $"{root}/{urlName}";
    }

    public static string BuildRoute(string root, int id, string name)
    {
        var urlName = GetUrlName(name);
        return $"{root}/{id}-{urlName}";
    }

    public static string BuildRoute(string root, params string[] components) =>
        $"{root}/{string.Join("/", components.Select(GetUrlName))}";

    private static string GetUrlName(string name) =>
        string.Concat(name.Trim().ToLowerInvariant().Select(c => char.IsLetterOrDigit(c) || c == '-' ? c : '_'));
}
