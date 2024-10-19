using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;

namespace ChitChat.Infrastructure.Logging
{
    internal static class SerilogRegistration
    {
        public static void AddHostSerilogConfiguration(this IHostBuilder host)
        {
            host.UseSerilog((ctx, service, config) =>
            {
                config
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(new JsonFormatter(), "logs/log-.txt", rollingInterval: RollingInterval.Day);
            });
        }
        //public static WebApplicationBuilder AddCustomSerilog(this WebApplicationBuilder builder)
        //{
        //    var configuration = builder.Configuration;
        //    var serilogOptions = configuration.GetSection(nameof(SerilogOptions)).Get<SerilogOptions>();

        //    if (!serilogOptions!.Enabled) return builder;

        //    var services = builder.Services;

        //    var options = new ConfigurationReaderOptions(typeof(ConsoleLoggerConfigurationExtensions).Assembly, typeof(SerilogExpression).Assembly)
        //    {
        //        SectionName = nameof(SerilogOptions)
        //    };

        //    services.UseSerilog(serilog =>
        //    {
        //        serilog
        //            .ReadFrom.Configuration(configuration, options)
        //            .Enrich.FromLogContext()
        //            .WriteTo.File(formatter: new CompactJsonFormatter(),
        //                            path: $@"{AppContext.BaseDirectory}/logs/Log.txt",
        //                            buffered: true,
        //                            flushToDiskInterval: TimeSpan.FromSeconds(serilogOptions.FlushFileToDiskInSeconds),
        //                            rollingInterval: RollingInterval.Day,
        //                            fileSizeLimitBytes: serilogOptions.FileSizeLimitInBytes,
        //                            rollOnFileSizeLimit: true);

        //        if (serilogOptions.LogToConsole)
        //        {
        //            serilog.WriteTo.Console(new CompactJsonFormatter());
        //        }

        //        var seqOptions = serilogOptions.SeqOptions;
        //        if (seqOptions is not null && seqOptions is { Enabled: true })
        //        {
        //            serilog
        //                .WriteTo.Seq(serverUrl: seqOptions.SeqUrl, apiKey: seqOptions.SeqApiKey);
        //        }

        //        var applicationInsightsOptions = serilogOptions.ApplicationInsightsOptions;
        //        if (applicationInsightsOptions is not null && applicationInsightsOptions is { Enabled: true })
        //        {
        //            serilog
        //                .WriteTo.ApplicationInsights(applicationInsightsOptions.ConnectionString, TelemetryConverter.Traces);
        //        }
        //    });

        //    return builder;
        //}

        //public static IApplicationBuilder UseCustomSerilog(this IApplicationBuilder application)
        //{
        //    var configuration = application.ApplicationServices.GetRequiredService<IConfiguration>();

        //    var serilogOptions = configuration.GetRequiredSection(nameof(SerilogOptions)).Get<SerilogOptions>();

        //    if (!serilogOptions!.Enabled)
        //    {
        //        return application;
        //    }

        //    application.UseSerilogRequestLogging();

        //    return application;
        //}
    }

}
