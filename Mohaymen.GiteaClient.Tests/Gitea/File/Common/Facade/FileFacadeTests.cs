using MediatR;
using Mohaymen.GiteaClient.Gitea.File.Common.Facade;
using Mohaymen.GiteaClient.Gitea.File.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Commands;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using NSubstitute;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.File.Common.Facade;

public class FileFacadeTests
{
    private readonly IMediator _mediator;
    private readonly IFileFacade _sut;

    public FileFacadeTests()
    {
        _mediator = Substitute.For<IMediator>();
        _sut = new FileFacade(_mediator);
    }

    [Fact]
    public async Task GetFileAsync_ShouldCallSend_WhenEver()
    {
        // Arrange
        const string repositoryName = "repo";
        const string filePath = "file_path";
        const string referenceName = "ref";
        var commandDto = new GetFileCommandDto
        {
            RepositoryName = repositoryName,
            FilePath = filePath,
            ReferenceName = referenceName,
        };

        // Act
        await _sut.GetFileAsync(commandDto, default);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<GetFileCommand>(x => x.RepositoryName == repositoryName
                                                                          && x.FilePath == filePath
                                                                          && x.ReferenceName == referenceName));
    }
}