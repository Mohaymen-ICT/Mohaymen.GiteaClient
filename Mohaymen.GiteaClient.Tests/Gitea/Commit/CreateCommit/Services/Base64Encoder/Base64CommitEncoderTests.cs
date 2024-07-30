using FluentAssertions;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Context;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder.Abstractions;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Commit.CreateCommit.Services.Base64Encoder;

public class Base64CommitEncoderTests
{
    private readonly IBase64CommitEncoder _sut;

    public Base64CommitEncoderTests()
    {
        _sut = new Base64CommitEncoder();
    }

    [Fact]
    public void EncodeFileContentsToBase64_ShouldSetBase64EncodedStringToFileContents_WhenEver()
    {
        // Arrange
        const string path1 = "fakePath1";
        const string path2 = "fakePath2";
        const string content1 = "fakeContent1";
        const string content2 = "fakeContent2";
        var fileCommitCommandModels = new List<FileCommitCommandModel>
        {
            new()
            {
                Path = path1,
                Content = content1,
                CommitActionCommand = CommitActionCommand.Create
            },
            new()
            {
                Path = path2,
                Content = content2,
                CommitActionCommand = CommitActionCommand.Create
            }
        };
        
        var expected = new List<FileCommitCommandModel>
        {
            new()
            {
                Path = path1,
                Content = "ZmFrZUNvbnRlbnQx",
                CommitActionCommand = CommitActionCommand.Create
            },
            new()
            {
                Path = path2,
                Content = "ZmFrZUNvbnRlbnQy",
                CommitActionCommand = CommitActionCommand.Create
            }
        };
        
        
        // Act
        var actual = _sut.EncodeFileContentsToBase64(fileCommitCommandModels);
        
        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
    
}