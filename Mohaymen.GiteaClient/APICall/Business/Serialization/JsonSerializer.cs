using Mohaymen.GitClient.APICall.Business.Serialization.Abstractions;
using Newtonsoft.Json;

namespace Mohaymen.GitClient.APICall.Business.Serialization;

internal class JsonSerializer : IJsonSerializer
{
    public string SerializeObject(object inputObject)
    {
        return JsonConvert.SerializeObject(inputObject);
    }

    public T DeserializeJson<T>(string serializedJson)
    {
        return JsonConvert.DeserializeObject<T>(serializedJson);
    }
}