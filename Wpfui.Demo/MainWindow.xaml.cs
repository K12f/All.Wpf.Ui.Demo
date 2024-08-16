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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Controls;

namespace Wpfui.Demo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }
    

    private async void OnShowDialogClick(object sender, RoutedEventArgs e)
    {
        // Dispatch to the UI queue
        await Application.Current.Dispatcher.InvokeAsync(ShowSampleDialogAsync);
    }
    
    private async Task ShowSampleDialogAsync()
    {
        // Defining dialog object
        ContentDialog myDialog =
            new()
            {
                Title = "My sample dialog",
                Content = "Content of the dialog",
                CloseButtonText = "Close button",
                PrimaryButtonText = "Primary button",
                SecondaryButtonText = "Secondary button"
            };
        
        var snackbar = new Snackbar(new SnackbarPresenter() )
        {
            IsShown =true,
            Title = "fdas",
            Content = "fdasfdsa",
            IsCloseButtonEnabled =true
        };
        
        

        // Setting the dialog container
        myDialog.DialogHost = ContentPresenterForDialogs;

        // Showing the dialog
        await myDialog.ShowAsync(CancellationToken.None);
    }
}