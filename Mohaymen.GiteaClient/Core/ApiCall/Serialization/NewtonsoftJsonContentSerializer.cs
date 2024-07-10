using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;

namespace Mohaymen.GiteaClient.Core.ApiCall.Serialization;

/// <summary>
/// A <see langword="class"/> implementing <see cref="IHttpContentSerializer"/> using the Newtonsoft.Json APIs
/// </summary>
/// <remarks>
/// Creates a new <see cref="NewtonsoftJsonContentSerializer"/> instance with the specified parameters
// this file is copied from the refit.newtonsoftjson nuget jsonseralizer in github 
// see : https://github.com/reactiveui/refit/blob/main/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs
/// </remarks>
/// <param name="jsonSerializerSettings">The serialization settings to use for the current instance</param>
internal sealed class NewtonsoftJsonContentSerializer(
    JsonSerializerSettings? jsonSerializerSettings
) : IHttpContentSerializer
{
    /// <summary>
    /// The <see cref="Lazy{T}"/> instance providing the JSON serialization settings to use
    /// </summary>
    readonly Lazy<JsonSerializerSettings> jsonSerializerSettings =
        new(
            () =>
                jsonSerializerSettings
                ?? JsonConvert.DefaultSettings?.Invoke()
                ?? new JsonSerializerSettings()
        );

    /// <summary>
    /// Creates a new <see cref="NewtonsoftJsonContentSerializer"/> instance
    /// </summary>
    public NewtonsoftJsonContentSerializer()
        : this(null)
    {
    }

    /// <inheritdoc/>
    public HttpContent ToHttpContent<T>(T item)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(item, jsonSerializerSettings.Value),
            Encoding.UTF8,
            "application/json"
        );

        return content;
    }

    /// <inheritdoc/>
    public async Task<T?> FromHttpContentAsync<T>(
        HttpContent content,
        CancellationToken cancellationToken = default
    )
    {
        if (content == null)
        {
            return default;
        }

        var serializer = JsonSerializer.Create(jsonSerializerSettings.Value);

        using var stream = await content
            .ReadAsStreamAsync(cancellationToken)
            .ConfigureAwait(false);
        using var reader = new StreamReader(stream);
        using var jsonTextReader = new JsonTextReader(reader);

        return serializer.Deserialize<T>(jsonTextReader);
    }

    /// <summary>
    /// Calculates what the field name should be for the given property. This may be affected by custom attributes the serializer understands
    /// </summary>
    /// <param name="propertyInfo">A PropertyInfo object.</param>
    /// <returns>
    /// The calculated field name.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">propertyInfo</exception>
    public string? GetFieldNameForProperty(PropertyInfo propertyInfo)
    {
        return propertyInfo switch
        {
            null => throw new ArgumentNullException(nameof(propertyInfo)),
            _
                => propertyInfo
                    .GetCustomAttributes<JsonPropertyAttribute>(true)
                    .Select(a => a.PropertyName)
                    .FirstOrDefault()
        };
    }
}