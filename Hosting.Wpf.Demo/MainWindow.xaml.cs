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
        // Initializes Engine (Specifies FFmpeg libraries path which is required)
        Engine.Start(new EngineConfig()
        {
#if DEBUG
            LogOutput       = ":debug",
            LogLevel        = LogLevel.Debug,
            FFmpegLogLevel  = FFmpegLogLevel.Warning,
#endif
                
            PluginsPath     = ":Plugins",
            FFmpegPath      = ":FFmpeg",

            // Use UIRefresh to update Stats/BufferDuration (and CurTime more frequently than a second)
            UIRefresh       = true,
            UIRefreshInterval= 100,
            UICurTimePerSecond = false // If set to true it updates when the actual timestamps second change rather than a fixed interval
        });

        InitializeComponent();
    }
}