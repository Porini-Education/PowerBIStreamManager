namespace PowerBIStreamManager.UI;

public partial class App
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

#if WINDOWS
    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        window.Width = 700;
        window.Height = 250;

        return window;
    }
#endif
}
