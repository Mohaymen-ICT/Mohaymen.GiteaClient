namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums;

public enum PullRequestState
{
    Closed,
    Open,
    All
}

public static class PullRequestStateExtensions
{
    public static string ToLowerString(this PullRequestState pullRequestState)
    {
        return pullRequestState.ToString().ToLowerInvariant();
    }
}