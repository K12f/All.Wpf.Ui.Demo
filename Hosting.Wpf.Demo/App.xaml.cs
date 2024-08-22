using System.Windows;

using CommunityToolkit.Mvvm.Messaging;

using FlyleafLib;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Hosting.Wpf.Demo
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        private static void Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();
            host.Start();

            App app = new();
            app.InitializeComponent();
            app.MainWindow = host.Services.GetRequiredService<MainWindow>();
            app.MainWindow.Visibility = Visibility.Visible;

            app.Run();
            
            Engine.Start(new EngineConfig()
            {
                FFmpegPath = ":FFmpeg",
                FFmpegDevices =
                    false, // Prevents loading avdevice/avfilter dll files. Enable it only if you plan to use dshow/gdigrab etc.

#if RELEASE
                            FFmpegLogLevel = FFmpegLogLevel.Quiet,
                            LogLevel = LogLevel.Quiet,

#else
                FFmpegLogLevel = FFmpegLogLevel.Warning,
                LogLevel = (FlyleafLib.LogLevel)LogLevel.Debug,
                LogOutput = ":debug",
                //LogOutput         = ":console",
                //LogOutput         = @"C:\Flyleaf\Logs\flyleaf.log",                
#endif

                //PluginsPath       = @"C:\Flyleaf\Plugins",

                UIRefresh =
                    false, // Required for Activity, BufferedDuration, Stats in combination with Config.Player.Stats = true
                UIRefreshInterval = 250, // How often (in ms) to notify the UI
                UICurTimePerSecond =
                    true, // Whether to notify UI for CurTime only when it's second changed or by UIRefreshInterval
            });
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.json");
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    Log.Logger = new LoggerConfiguration()
                        .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger();
                    logging.AddSerilog(Log.Logger);
                })
                .ConfigureServices(container =>
                {
                    container.AddHostedService<CheckUpdateService>();


                    container.AddSingleton<MainViewModel>();
                    container.AddSingleton<MainWindow>(sp => new MainWindow()
                    {
                        DataContext = sp.GetRequiredService<MainViewModel>()
                    });

                    container.AddSingleton<WeakReferenceMessenger>();
                    container.AddSingleton<IMessenger>(sp => sp.GetRequiredService<WeakReferenceMessenger>());

                    container.AddSingleton(_ => Current.Dispatcher);
                });
        }
    }
}