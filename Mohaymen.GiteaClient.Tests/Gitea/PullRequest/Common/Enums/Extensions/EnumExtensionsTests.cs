using FluentAssertions;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums.Extensions;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.PullRequest.Common.Enums.Extensions;

public class EnumExtensionsTests
{
    [Theory]
    [InlineData(MergeType.ManuallyMerged, "manually-merged")]
    [InlineData(MergeType.Squash, "squash")]
    [InlineData(PullRequestState.Open, "open")]
    [InlineData(SortCriteria.Recentupdate, "recentupdate")]
    public void Normalize_ShouldConvertEnumValueToProperString_WhenEver(Enum enumValue, string expected)
    {
        //Arrange
        
        // Act
        var actual = enumValue.Normalize();

        // Assert
        actual.Should().Be(expected);
    }
}