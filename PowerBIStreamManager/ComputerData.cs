using System.Diagnostics;
using System.Text.Json.Serialization;

namespace PowerBIStreamManager;

public sealed record ComputerData(string ComputerName, float CpuPercentage, float MemoryAvailable, float MemoryCommitted, float SystemReadBytes)
{
    public DateTimeOffset Timestamp { get; set; }

    internal static ComputerData Create(
        string computerName,
        PerformanceCounter cpuPercentage,
        PerformanceCounter memoryAvailable,
        PerformanceCounter memoryCommitted,
        PerformanceCounter systemReadBytes)
    {
        return new ComputerData(computerName, cpuPercentage.NextValue(), memoryAvailable.NextValue(), memoryCommitted.NextValue(), systemReadBytes.NextValue()) { Timestamp = DateTimeOffset.Now };
    }
}

[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(ComputerData[]))]
public partial class ComputerDataContext : JsonSerializerContext
{
}
