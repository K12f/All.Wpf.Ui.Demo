using System.Windows.Media;
using System.Windows.Threading;

using CommunityToolkit.Mvvm.ComponentModel;

using FlyleafLib;
using FlyleafLib.MediaPlayer;

using Microsoft.Extensions.Configuration;

namespace Hosting.Wpf.Demo
{
    public partial class MainViewModel:ObservableValidator
    {
        private readonly Dispatcher _dispatcher;
        [ObservableProperty] private string _message;
        [ObservableProperty] private string? _logLevel;

        public Player Player { get; set; }
        public Config Config { get; set; }

        public MainViewModel(IConfiguration configuration,Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _logLevel = configuration.GetValue<string>("Logging:LogLevel:Microsoft");
            _message = "hello";
            
            Config = new Config();
            Config.Video.BackgroundColor = Colors.DarkGray;

            Player = new Player(Config);

            var url = @"http://v3-web.douyinvod.com/c3266d2fd75c2555f49b0aa2190a6fa2/66c6ba87/video/tos/cn/tos-cn-ve-15/okl09VUmmQCqAOA9fBuFW9LEIVDgDjAYhgfmrg/?a=6383\\u0026ch=26\\u0026cr=3\\u0026dr=0\\u0026lr=all\\u0026cd=0%7C0%7C0%7C3\\u0026cv=1\\u0026br=685\\u0026bt=685\\u0026cs=0\\u0026ds=4\\u0026ft=pEaFx4hZffPdr5~-v1cNvAq-antLjrKzAdG.RkaIC37_ljVhWL6\\u0026mime_type=video_mp4\\u0026qs=0\\u0026rc=ZzZkNTU3Nzc7Ozo0ZTdmPEBpM2xkdng5cnJvdDMzNGkzM0BgL14uLjFiXjUxYzEtNDRhYSNpbC5kMmRjMnBgLS1kLS9zcw%3D%3D\\u0026btag=c0000e00030000\\u0026cquery=100K_100o_100w_100B_100H\\u0026dy_q=1724288859\\u0026feature_id=46a7bb47b4fd1280f3d3825bf2b29388\\u0026l=2024082209073879471FE2529DF246AD93";
            Player.Open(url);

        }

        async Task FooAsync()
        {
            _dispatcher.Invoke(() => Message = "hello world");
        }
    }
}