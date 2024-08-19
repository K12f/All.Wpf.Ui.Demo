using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.Configuration;

namespace Hosting.Wpf.Demo
{
    public partial class MainViewModel:ObservableValidator
    {
        [ObservableProperty] private string _message;
        [ObservableProperty] private string? _logLevel;

        public MainViewModel(IConfiguration configuration)
        {
            _logLevel = configuration.GetValue<string>("Logging:LogLevel:Microsoft");
            _message = "hello";
        }
    }
}