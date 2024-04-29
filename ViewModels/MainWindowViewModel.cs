using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TravelRecords.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

    [ObservableProperty] private string _test = "Test";

    [ObservableProperty] private bool _isEnabled = true;

    [RelayCommand]
    public void ToggleEnabled()
    {
        IsEnabled = !IsEnabled;
    }
}