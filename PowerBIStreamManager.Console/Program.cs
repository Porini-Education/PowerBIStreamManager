// See https://aka.ms/new-console-template for more information

using PowerBIStreamManager;

DataStreamer streamer = new();
string? uri = default;
bool stream = true;
Console.CancelKeyPress += Console_CancelKeyPress;
while (stream)
{
    try
    {
        Console.WriteLine("Please provide an API Url:");
        if (!string.IsNullOrWhiteSpace(uri))
        {
            Console.WriteLine("(Last: {0}):", uri);
        }
        uri = Console.ReadLine();

        await streamer.Start(uri!);
    }
    catch (HttpRequestException ex)
    {
        Console.Error.WriteLine("{0}: {1}", "Check Url", ex.Message);
        uri = null;
    }
    finally
    {
        streamer.Reset();

        Console.WriteLine("Press Y to start again");
        var key = Console.ReadKey();
        stream = key.Key == ConsoleKey.Y;
        Console.Clear();
    }
}

void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
{
    streamer.Stop();
}