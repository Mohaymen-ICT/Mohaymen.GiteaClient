using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.APICall.Business.HttpClientFactory;
using Mohaymen.GiteaClient.APICall.Business.HttpClientFactory.Abstractions;
using Mohaymen.GiteaClient.APICall.Business.HttpRequestBuilder;
using Mohaymen.GiteaClient.APICall.Business.HttpRequestBuilder.Abstractions;
using Mohaymen.GiteaClient.APICall.Business.Serialization;
using Mohaymen.GiteaClient.APICall.Business.Serialization.Abstractions;
using Mohaymen.GiteaClient.APICall.Business.Wrappers;
using Mohaymen.GiteaClient.APICall.Business.Wrappers.Abstractions;
using Mohaymen.GiteaClient.APICall.Facades;
using Mohaymen.GiteaClient.APICall.Facades.Abstractions;
using Mohaymen.GiteaClient.Common.DependencyInjection.Abstractions;

namespace Mohaymen.GiteaClient.APICall;

internal sealed class ApiCallDependencyInstaller : IDependencyInstaller
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