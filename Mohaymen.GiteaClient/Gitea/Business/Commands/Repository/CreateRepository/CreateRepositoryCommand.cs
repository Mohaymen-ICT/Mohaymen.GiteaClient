using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.APICall.Domain;
using Mohaymen.GiteaClient.APICall.Facades.Abstractions;
using Mohaymen.GiteaClient.Gitea.Domain.Dtos.Repository.CreateRepository;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Business.Commands.Repository.CreateRepository;

internal sealed class CreateRepositoryCommand : IRequest<GiteaResponseDto<CreateRepositoryResponseDto>>
{
    [JsonProperty("default_branch")] public string DefaultBranch { get; init; }

    [JsonProperty("name")] public string Name { get; init; }

    [JsonProperty("private")] public bool IsPrivateBranch { get; init; }
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