namespace Mohaymen.GiteaClient.IntegrationTests.Common.Models.Requests.TokenRequest;

internal sealed class TokenAccessLevel
{
    public required TokenAccessArea TokenAccessArea { get; init; }
    public required TokenAccessType TokenAccessType { get; init; }

    public override string ToString()
    {
        return $"{TokenAccessType.ToString().ToLowerInvariant()}:{TokenAccessArea.ToString().ToLowerInvariant()}";
    }
}