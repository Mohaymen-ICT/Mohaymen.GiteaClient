using System.Diagnostics;

namespace Mohaymen.GiteaClient.Commons.Observability.Abstraction;

internal interface ITraceInstrumentation
{
	ActivitySource ActivitySource { get; }
}
