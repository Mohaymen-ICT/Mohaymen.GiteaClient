namespace Mohaymen.GiteaClient.APICall.Domain;

public sealed class GiteaResponseDto<TResponseBody>
{
    public bool IsSuccessfull { get; init; }
    public string? ErrorMessage { get; init; }
    public TResponseBody? ResponseBody { get; init; }
}