using System;
using Polly;
using Polly.Contrib.Simmy;
using Polly.Contrib.Simmy.Latency;
using Polly.Contrib.Simmy.Outcomes;

static class PolicyBuilder
{
    public static Policy CreateLatency() => MonkeyPolicy.InjectLatency(with =>
        with.Latency(TimeSpan.FromSeconds(Settings.Latency.Seconds))
        .InjectionRate(Settings.Latency.InjectionRate)
        .Enabled(Settings.Latency.Enabled));

    public static Policy CreateFault() => MonkeyPolicy.InjectException(with =>
        with.Fault(new Exception("Something gone wrong."))
            .InjectionRate(Settings.Fault.InjectionRate)
            .Enabled(Settings.Fault.Enabled)
        );
}