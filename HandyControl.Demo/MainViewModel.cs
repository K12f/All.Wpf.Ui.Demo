using System.ComponentModel.DataAnnotations;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;


using HandyControl.Controls;

namespace HandyControl.Demo
{
    [ObservableRecipient]
    public partial class MainViewModel : ObservableValidator
    {
        [ObservableProperty] [Required] private string _name;

        public MainViewModel()
        {
            Messenger = WeakReferenceMessenger.Default;
        }

        [RelayCommand]
        private void Submit()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                MessageBox.Show("error");
            }
            else
            {
                MessageBox.Show("right");
            }
        }
    }
}