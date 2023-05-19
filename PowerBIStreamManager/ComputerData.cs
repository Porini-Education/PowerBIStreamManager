using System.Diagnostics;

namespace PowerBIStreamManager;
internal sealed class ComputerData
{
    public string ComputerName { get; }
    public float CpuPercentage { get; }
    public float MemoryAvailable { get; }
    public float MemoryCommitted { get; }
    public float SystemReadBytes { get; }
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
