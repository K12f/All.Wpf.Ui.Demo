using System.Windows;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                .ConfigureServices(container =>
                {
                    container.AddSingleton<MainViewModel>();
                    container.AddSingleton<MainWindow>(sp=>new MainWindow()
                    {
                        DataContext = sp.GetRequiredService<MainViewModel>()
                    });
                });
        }
    }
}