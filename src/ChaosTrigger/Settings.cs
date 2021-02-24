using System;

static class Settings
{
    public static class Latency
    {
        public static uint Seconds = Convert.ToUInt32(Environment.GetEnvironmentVariable("ChaosLatencySeconds"));

        public static double InjectionRate = Convert.ToDouble(Environment.GetEnvironmentVariable("ChaosLatencyInjectionRate"));
        
        public static bool Enabled = Convert.ToBoolean(Environment.GetEnvironmentVariable("ChaosLatencyEnabled"));
    }

    public static class Fault
    {
        public static double InjectionRate = Convert.ToDouble(Environment.GetEnvironmentVariable("ChaosFaultInjectionRate"));

        public static bool Enabled = Convert.ToBoolean(Environment.GetEnvironmentVariable("ChaosFaultEnabled"));
    }
}