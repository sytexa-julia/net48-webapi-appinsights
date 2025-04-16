# .NET Framework 4.8, ASP.NET Web API, Application Insights

A working minimal example. Just fill in your AppInsights connection string in `ApplicationInsights.config`.

NOTE: Sampling has been disabled; all telemetry will be transmitted. Uncomment these lines in `ApplicationInsights.config` to change this:

```xml
<!--<Add Type="Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel.AdaptiveSamplingTelemetryProcessor, Microsoft.AI.ServerTelemetryChannel">
  <MaxTelemetryItemsPerSecond>5</MaxTelemetryItemsPerSecond>
  <ExcludedTypes>Event</ExcludedTypes>
</Add>-->
<!--<Add Type="Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel.AdaptiveSamplingTelemetryProcessor, Microsoft.AI.ServerTelemetryChannel">
  <MaxTelemetryItemsPerSecond>5</MaxTelemetryItemsPerSecond>
  <IncludedTypes>Event</IncludedTypes>
</Add>-->
```
