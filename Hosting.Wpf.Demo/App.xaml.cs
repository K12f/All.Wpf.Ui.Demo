using System.Windows;

using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

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