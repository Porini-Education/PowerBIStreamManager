using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;

namespace PowerBIStreamManager.WPF.ViewModels;
public partial class StreamConfigurationViewModel : ObservableObject
{
    public StreamConfigurationViewModel()
    {
        Streamer = new DataStreamer();
    }

    public IStreamerService Streamer { get; }

    [RelayCommand]
    private async Task StartStreaming()
    {
        if (IsStreaming)
        {
            return;
        }

        try
        {
            IsStreaming = true;
            await Streamer.Start(Uri!, OnGetContent).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                _ = MessageBox.Show(App.Current.MainWindow, ex.Message, "Check Uri.");
            });
        }
        finally 
        { 
            Streamer.Reset();
            IsStreaming = false;
        }
    }

    private void OnGetContent(string content)
    {
        var data = JsonSerializer.Deserialize(content, ComputerDataContext.Default.ComputerDataArray)![0];
        App.Current.Dispatcher.Invoke(() =>
        {
            if (SentContent.Count >= 100)
            {
                SentContent.RemoveAt(SentContent.Count - 1);
            }
            SentContent.Insert(0,data);
        });
    }

    [RelayCommand]
    private Task StopStreaming() => Streamer.Stop();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanStartStreaming))]
    private string? _uri;

    [ObservableProperty]
    private bool _isStreaming;

    public bool CanStartStreaming => !string.IsNullOrEmpty(Uri) && !IsStreaming;

    public string ExampleContent => Streamer.GetContent();

    public ObservableCollection<ComputerData> SentContent { get; } = new();

    [RelayCommand]
    private void ResetSentContent() => SentContent.Clear();
}
