using Mohaymen.GiteaClient.Commons.Observability;
using OpenTelemetry.Trace;

namespace Mohaymen.GiteaClient.Commons.Extensions;

public static class ObservabilityDependencyInjectionExtensions
{
    public static TracerProviderBuilder AddGiteaInstrumentation(this TracerProviderBuilder builder)
    {
        builder.AddSource(TraceInstrumentation.Name);
        return builder;
    }
}