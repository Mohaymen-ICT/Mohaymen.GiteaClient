namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums.Extensions;

public static class SortCriteriaExtensions
{
    public static string Normalize(this SortCriteria sortCriteria)
    {
        return sortCriteria.ToString().ToLowerInvariant();
    }
}