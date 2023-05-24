namespace PowerBIStreamManager;
public interface IStreamerService
{
    string GetContent();
    void Reset();
    Task Start(string requestUri, Action<string>? onGetContent = null);
    Task Stop();
}
