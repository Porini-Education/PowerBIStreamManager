namespace PowerBIStreamManager.UI.Views;

public partial class StreamConfigurationView
{
	public StreamConfigurationView(ViewModels.StreamConfigurationViewModel viewModel)
	{
        BindingContext = viewModel;
		InitializeComponent();
	}
}