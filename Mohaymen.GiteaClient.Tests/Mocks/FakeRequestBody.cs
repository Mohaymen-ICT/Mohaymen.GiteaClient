using MediatR;
using Mohaymen.GiteaClient.APICall.Domain;

namespace Mohaymen.GitClient.Tests.Mocks;

public class FakeRequestBody : IRequest<GiteaResponseDto<FakeResponseBody>>
{
    public string Name { get; set; }
    public int Age { get; set; }
}