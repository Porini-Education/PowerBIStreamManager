namespace PowerBIStreamManager;
public interface IStreamerService
{
    void Reset();
    Task Start(string requestUri);
    Task Stop();
}
