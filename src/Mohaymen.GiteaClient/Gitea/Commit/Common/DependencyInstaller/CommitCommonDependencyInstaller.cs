using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Core.DependencyInjection.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.Common.Facades;
using Mohaymen.GiteaClient.Gitea.Commit.Common.Facades.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Commit.Common.DependencyInstaller;

internal class CommitCommonDependencyInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ICommitFacade, CommitFacade>();
        serviceCollection.AddSingleton<IBase64CommitEncoder, Base64CommitEncoder>();
    }
}