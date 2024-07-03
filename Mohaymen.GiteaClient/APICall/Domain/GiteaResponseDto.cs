namespace Mohaymen.GitClient.APICall.Domain;

public sealed class GiteaResponseDto<TResponseBody>
{
    public bool IsSuccessfull { get; set; }
    public string? ErrorMessage { get; set; }
    public TResponseBody? ResponseBody { get; set; }
}