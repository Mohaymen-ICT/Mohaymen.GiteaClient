namespace Mohaymen.GitClient.APICall.Domain;

internal class HttpResponseDto<TResponseBody>
{
    public bool IsSuccessfull { get; set; }
    public string? ErrorMessage { get; set; }
    public TResponseBody? ResponseBody { get; set; }
}