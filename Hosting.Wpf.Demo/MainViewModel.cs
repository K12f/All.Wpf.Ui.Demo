using System.Windows.Threading;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.Configuration;

namespace Hosting.Wpf.Demo
{
    public partial class MainViewModel:ObservableValidator
    {
        private readonly Dispatcher _dispatcher;
        [ObservableProperty] private string _message;
        [ObservableProperty] private string? _logLevel;

        public MainViewModel(IConfiguration configuration,Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _logLevel = configuration.GetValue<string>("Logging:LogLevel:Microsoft");
            _message = "hello";
        }

        async Task FooAsync()
        {
            _dispatcher.Invoke(() => Message = "hello world");
        }
    }
}