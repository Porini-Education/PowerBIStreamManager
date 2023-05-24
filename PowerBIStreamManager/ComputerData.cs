using System.Diagnostics;
using System.Text.Json.Serialization;

namespace PowerBIStreamManager;
internal sealed class ComputerData
{
    [JsonPropertyName("computerName")]
    public string ComputerName { get; }
    [JsonPropertyName("cpuPercentage")]
    public float CpuPercentage { get; }
    [JsonPropertyName("memoryAvailable")]
    public float MemoryAvailable { get; }
    [JsonPropertyName("memoryCommitted")]
    public float MemoryCommitted { get; }
    [JsonPropertyName("systemReadBytes")]
    public float SystemReadBytes { get; }
    [JsonPropertyName("timestamp")]
    public DateTimeOffset Timestamp { get; }

    public ComputerData(
      string pcName,
      PerformanceCounter cpu,
      PerformanceCounter ram,
      PerformanceCounter com,
      PerformanceCounter disk)
    {
        ComputerName = pcName;
        CpuPercentage = cpu.NextValue();
        MemoryAvailable = ram.NextValue();
        MemoryCommitted = com.NextValue();
        SystemReadBytes = disk.NextValue();
        Timestamp = DateTimeOffset.Now;
    }
}
