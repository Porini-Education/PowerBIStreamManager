using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PowerBIStreamManager.UI.ViewModels;
public partial class StreamConfigurationViewModel : ObservableObject
{
    public StreamConfigurationViewModel(IStreamerService streamer)
    {
        Streamer = streamer;
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
            await Application.Current!.MainPage!.DisplayAlert("Check Uri.", ex.Message, "Ok");
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
    [NotifyPropertyChangedFor(nameof(CanStartStreaming))]
    private bool _isStreaming;

    public bool CanStartStreaming => !string.IsNullOrEmpty(Uri) && !IsStreaming;
}
