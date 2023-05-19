namespace PowerBIStreamManager.WPF.Views;
/// <summary>
/// Interaction logic for StreamConfigurationView.xaml
/// </summary>
public partial class StreamConfigurationView
{
    public StreamConfigurationView()
    {
        DataContext = new ViewModels.StreamConfigurationViewModel();
        InitializeComponent();
    }

}
