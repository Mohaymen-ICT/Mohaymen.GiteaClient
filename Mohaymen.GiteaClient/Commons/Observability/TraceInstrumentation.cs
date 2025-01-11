using System.Diagnostics;
using Mohaymen.GiteaClient.Commons.Observability.Abstraction;

namespace Mohaymen.GiteaClient.Commons.Observability;

internal sealed class TraceInstrumentation : ITraceInstrumentation
{
	public const string Name = "GiteaClient";
	private const string Version = "1.0.0";
	public ActivitySource ActivitySource { get; } = new ActivitySource(Name, Version);
}
