using System;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Commands;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;

namespace Mohaymen.GiteaClient.Gitea.File.CreateFile.Mappers;

internal static class CreateFileCommandMapper
{
    internal static CreateFileCommand Map(this CreateFileCommandDto createFileCommandDto)
    {
        if (createFileCommandDto is null)
        {
            throw new ArgumentNullException(nameof(createFileCommandDto));
        }

        return new CreateFileCommand
        {
            RepositoryName = createFileCommandDto.RepositoryName,
            FilePath = createFileCommandDto.FilePath,
            Content = createFileCommandDto.Content,
            Author = createFileCommandDto.Author,
            BranchName = createFileCommandDto.BranchName,
            CommitMessage = createFileCommandDto.CommitMessage
        };
    }
}