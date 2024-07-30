namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums.Extensions;

public static class PullRequestStateExtensions
{
    public static string Normalize(this PullRequestState pullRequestState)
    {
        return pullRequestState.ToString().ToLowerInvariant();
    }
}