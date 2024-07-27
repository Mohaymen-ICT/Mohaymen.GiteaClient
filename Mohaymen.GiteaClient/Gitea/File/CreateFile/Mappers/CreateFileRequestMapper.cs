using System;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Commands;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Context;

namespace Mohaymen.GiteaClient.Gitea.File.CreateFile.Mappers;

internal static class CreateFileRequestMapper
{
    internal static CreateFileRequest ToCreateFileRequest(this CreateFileCommand createFileCommand)
    {
        ArgumentNullException.ThrowIfNull(createFileCommand);

        return new CreateFileRequest
        {
            Content = createFileCommand.Content,
            Author = createFileCommand.Author,
            BranchName = createFileCommand.BranchName,
            CommitMessage = createFileCommand.CommitMessage,
        };
    }
}