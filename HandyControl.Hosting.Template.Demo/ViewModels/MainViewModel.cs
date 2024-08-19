using System.Windows.Threading;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using HandyControl.Hosting.Template.Demo.Models;
using HandyControl.Hosting.Template.Demo.Services;

using Microsoft.Extensions.Configuration;

namespace HandyControl.Hosting.Template.Demo.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly Dispatcher _dispatcher;
        private readonly User _user;

        [ObservableProperty] private ViewModelBase? _currentViewModel;

        [ObservableProperty] private string? _logLevel;
        [ObservableProperty] private string _message;

        public MainViewModel(IConfiguration configuration, Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _logLevel = configuration.GetValue<string>("Logging:LogLevel:Microsoft");
            _message = "_user.Name";


        }

        private async Task FooAsync()
        {
            _dispatcher.Invoke(() => Message = "hello world");
        }
    }
}