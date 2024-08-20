using System.IO;
using System.Windows;

using CommunityToolkit.Mvvm.Messaging;

using HandyControl.Hosting.Template.Demo.Models;
using HandyControl.Hosting.Template.Demo.Services;
using HandyControl.Hosting.Template.Demo.ViewModels;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

namespace HandyControl.Hosting.Template.Demo
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static new App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        [STAThread]
        private static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            await host.StartAsync().ConfigureAwait(true);

            App app = new();
            app.InitializeComponent();
            app.MainWindow = host.Services.GetRequiredService<MainWindow>();
            app.MainWindow.Visibility = Visibility.Visible;
            app.Run();

            await host.StopAsync().ConfigureAwait(true);
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddUserSecrets(typeof(App).Assembly);
                })
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
                .ConfigureServices((context,container) =>
                {
                    // configuration

                    var appConfig = context.Configuration.GetSection(nameof(AppConfig));
                    container.Configure<AppConfig>(appConfig);
                    
                    container.AddHostedService<CheckUpdateService>();

                    container.AddSingleton<MainViewModel>();
                    container.AddSingleton<MainWindow>(sp => new MainWindow
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