using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GitClient.APICall.Domain;
using Mohaymen.GitClient.APICall.Facades.Abstractions;
using Newtonsoft.Json;

namespace Mohaymen.GitClient.Gitea.Business.Commands.Repository.CreateRepository;

public sealed class CreateRepositoryCommand : IRequest<GiteaResponseDto<CreateRepositoryResponseDto>>
{
    [JsonProperty("default_branch")] public string DefaultBranch { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("private")] public bool IsPrivateBranch { get; set; }
}

internal sealed class CreateRepositoryCommandHandler : IRequestHandler<CreateRepositoryCommand, GiteaResponseDto<CreateRepositoryResponseDto>>
{
    private readonly IApiCallFacade _apiCallFacade;

    public CreateRepositoryCommandHandler(IApiCallFacade apiCallFacade)
    {
        _apiCallFacade = apiCallFacade ?? throw new ArgumentNullException(nameof(apiCallFacade));
    }

    public async Task<GiteaResponseDto<CreateRepositoryResponseDto>> Handle(CreateRepositoryCommand request,
        CancellationToken cancellationToken)
    {
        var httpRestApiDto = new HttpRestApiDto<CreateRepositoryCommand>
        {
            HttpMethod = HttpMethod.Post,
            BodyDto = request
        };
        return await _apiCallFacade.SendAsync<CreateRepositoryCommand, CreateRepositoryResponseDto>(httpRestApiDto, cancellationToken).ConfigureAwait(false);
    }
}