using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GitClient.APICall.Business.HttpClientFactory;
using Mohaymen.GitClient.APICall.Business.HttpClientFactory.Abstractions;
using Mohaymen.GitClient.APICall.Business.HttpRequestBuilder;
using Mohaymen.GitClient.APICall.Business.HttpRequestBuilder.Abstractions;
using Mohaymen.GitClient.APICall.Business.Serialization;
using Mohaymen.GitClient.APICall.Business.Serialization.Abstractions;
using Mohaymen.GitClient.APICall.Business.Wrappers;
using Mohaymen.GitClient.APICall.Business.Wrappers.Abstractions;
using Mohaymen.GitClient.APICall.Facades;
using Mohaymen.GitClient.APICall.Facades.Abstractions;
using Mohaymen.GitClient.Common.DependencyInjection.Abstractions;

namespace Mohaymen.GitClient.APICall;

internal class ApiCallDependencyInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IJsonSerializer, JsonSerializer>();
        serviceCollection.AddSingleton<IHttpRequestMessageFactory, HttpRequestMessageFactory>();
        serviceCollection.AddSingleton<IHttpClientFactory, HttpClientFactory>();
        serviceCollection.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
        serviceCollection.AddSingleton<IApiCallFacade, ApiCallFacade>();
    }
}