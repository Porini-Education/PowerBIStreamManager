using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            await Streamer.Start(Uri!).ConfigureAwait(false);
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

    [RelayCommand]
    private Task StopStreaming() => Streamer.Stop();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanStartStreaming))]
    private string? _uri;

    [ObservableProperty]
    private bool _isStreaming;

    public bool CanStartStreaming => !string.IsNullOrEmpty(Uri) && !IsStreaming;
}
