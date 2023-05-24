using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace PowerBIStreamManager;
public class DataStreamer : IStreamerService
{
    private readonly PerformanceCounter cpu;
    private readonly PerformanceCounter ram;
    private readonly PerformanceCounter com;
    private readonly PerformanceCounter disk;
    private CancellationTokenSource cancellationTokenSource;

    public DataStreamer()
    {
        cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        ram = new PerformanceCounter("Memory", "Available MBytes");
        com = new PerformanceCounter("Memory", "Committed Bytes");
        disk = new PerformanceCounter("System", "File Read Bytes/sec");
        cancellationTokenSource = new();
    }

    public async Task Start(string requestUri, Action<string>? onGetContent = null)
    {
        var cancellationToken = cancellationTokenSource.Token;
        using HttpClient httpClient = new();

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var content = GetContentArray();
                onGetContent?.Invoke(content);
                var result = await httpClient.PostAsync(requestUri, new StringContent(content, Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false);
                result.EnsureSuccessStatusCode();
                await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            }
        }
        catch (TaskCanceledException)
        {
            return;
        }
    }

    public string GetContent()
    {
        return JsonSerializer.Serialize(GetData(), ComputerDataContext.Default.ComputerData);
    }

    private string GetContentArray()
    {
        return JsonSerializer.Serialize(new[] { GetData() }, ComputerDataContext.Default.ComputerDataArray);
    }
    
    private ComputerData GetData()
    {
        return ComputerData.Create(Environment.MachineName, cpu, ram, com, disk);
    }

    public Task Stop()
    {
        cancellationTokenSource.Cancel(true);
        return Task.CompletedTask;
    }

    public void Reset()
    {
        cancellationTokenSource = new();
    }
}
