﻿using Microsoft.Extensions.Logging;
using PowerBIStreamManager.UI.ViewModels;

namespace PowerBIStreamManager.UI;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services
            .AddScoped<Views.StreamConfigurationView>()
            .AddScoped<StreamConfigurationViewModel>()
            .AddScoped<IStreamerService, DataStreamer>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
