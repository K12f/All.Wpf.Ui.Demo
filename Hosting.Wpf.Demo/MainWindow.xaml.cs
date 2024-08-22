using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using FlyleafLib;

namespace Hosting.Wpf.Demo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
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
        InitializeComponent();
    }
}