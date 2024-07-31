using System;
using System.Text.RegularExpressions;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums.Extensions;

public static class EnumExtensions
{
    public static string Normalize<T>(this T enumValue) where T : Enum
    {
        var enumString = enumValue.ToString();
        var pattern = "([a-z])([A-Z])";
        var replacement = "$1-$2";
        var result = Regex.Replace(enumString, pattern, replacement).ToLowerInvariant();
        return result;
    }
}