using System.Diagnostics;
using MediatR;
using Mohaymen.GiteaClient.Commons.Observability.Abstraction;
using Mohaymen.GiteaClient.Gitea.File.Common.Facade;
using Mohaymen.GiteaClient.Gitea.File.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Queries;
using Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Dto;
using Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Queries;
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
		var traceInstrumentation = Substitute.For<ITraceInstrumentation>();
		_sut = new FileFacade(_mediator, traceInstrumentation);
		traceInstrumentation.ActivitySource.Returns(new ActivitySource("test"));
	}

	[Fact]
	public async Task GetFileAsync_ShouldCallSend_WhenEver()
	{
		// Arrange
		const string repositoryName = "repo";
		const string filePath = "file_path";
		const string referenceName = "ref";
		var commandDto = new GetFileMetadataQueryDto
		{
			RepositoryName = repositoryName,
			FilePath = filePath,
			ReferenceName = referenceName,
		};

		// Act
		await _sut.GetFileAsync(commandDto, default);

		// Assert
		await _mediator.Received(1).Send(Arg.Is<GetFileMetadataQuery>(x => x.RepositoryName == repositoryName
																		  && x.FilePath == filePath
																		  && x.ReferenceName == referenceName));
	}

	[Fact]
	public async Task GetFilesMetadataAsync_ShouldCallSend_WhenEver()
	{
		// Arrange
		const string repositoryName = "fakeRepositoryName";
		const string branchName = "fakeBranchName";
		var queryDto = new GetFilesMetadataQueryDto
		{
			BranchName = branchName,
			RepositoryName = repositoryName
		};

		// Act
		await _sut.GetFilesMetadataAsync(queryDto, default);

		// Assert
		await _mediator.Received(1).Send(Arg.Is<GetFilesMetadataQuery>(x => x.RepositoryName == repositoryName &&
																			x.BranchName == branchName), default);
	}
}
