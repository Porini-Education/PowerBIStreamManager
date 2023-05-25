using System.Windows;
using System.Windows.Controls;

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

    private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        if (e.PropertyName == nameof(ComputerData.Timestamp))
        {
            if (e.Column is DataGridTextColumn col)
            {
                col.Binding.StringFormat = "yyyy-MM-dd HH:mm:ss";
            }
        }
    }
}
