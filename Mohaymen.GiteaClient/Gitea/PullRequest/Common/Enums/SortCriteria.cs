namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums;

public enum SortCriteria
{
    Oldest,
    RecentUpdate,
    LeastUpdate,
    MostComment,
    LeastComment,
    Priority
}

public static class SortCriteriaExtensions
{
    public static string ToLowerString(this SortCriteria sortCriteria)
    {
        return sortCriteria.ToString().ToLowerInvariant();
    }
}