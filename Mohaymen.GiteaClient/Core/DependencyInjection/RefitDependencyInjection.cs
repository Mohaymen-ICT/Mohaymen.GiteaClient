﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Abstractions;
using Mohaymen.GiteaClient.Core.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Core.ApiCall.HttpHeader;
using Mohaymen.GiteaClient.Core.Configs;
using Refit;

namespace Mohaymen.GiteaClient.Core.DependencyInjection;

internal static class RefitDependencyInjection
{
    public static IServiceCollection AddRefitClientTypes(this IServiceCollection serviceCollection)
    {
        var refitClientTypes = GetRefitClientInterfaceTypes();
        var httpClientConfigureAction = new Action<IServiceProvider, HttpClient>((sp, httpClient) =>
        {
            var giteaConfigOptions = sp.GetService<IOptions<GiteaApiConfiguration>>();
            httpClient.BaseAddress = new Uri(giteaConfigOptions!.Value.BaseUrl);
            var apiKey = HttpHeaderFactory.GetAuthorizationHeader(giteaConfigOptions.Value.PersonalAccessToken);
            httpClient.DefaultRequestHeaders.Add("Authorization", apiKey);
        });
        foreach (var refitClientType in refitClientTypes)
        {
            serviceCollection.AddRefitClient(refitClientType, new RefitSettings
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer()
                })
                .ConfigureHttpClient(httpClientConfigureAction);
        }

        return serviceCollection;
    }
    
    private static IEnumerable<Type> GetRefitClientInterfaceTypes()
    {
        return typeof(IAssemblyMarkerInterface)
            .Assembly
            .DefinedTypes
            .Where(type => type.IsInterface && typeof(IRefitClientInterface).IsAssignableFrom(type))
            .Select(x => x.AsType());
    }
}