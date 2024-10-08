﻿using System;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Commands;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Context;

namespace Mohaymen.GiteaClient.Gitea.File.CreateFile.Mappers;

internal static class CreateFileRequestMapper
{
    internal static CreateFileRequest Map(this CreateFileCommand createFileCommand, string encodedContent)
    {
        if (createFileCommand is null)
        {
            throw new ArgumentNullException(nameof(createFileCommand));
        }

        return new CreateFileRequest
        {
            Content = encodedContent,
            Author = createFileCommand.Author,
            BranchName = createFileCommand.BranchName,
            CommitMessage = createFileCommand.CommitMessage,
        };
    }
}