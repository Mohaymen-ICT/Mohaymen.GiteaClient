namespace Mohaymen.GitClient.APICall.Business.Serialization.Abstractions;

internal interface IJsonSerializer
{
    string SerializeObject(object inputObject);
    T DeserializeJson<T>(string serializedJson);
}