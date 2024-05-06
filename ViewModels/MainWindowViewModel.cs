using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using TravelRecords.Services;

namespace TravelRecords.ViewModels;

public record UserLoggedInMessage();

public record UserLoggedOutMessage();

public partial class MainWindowViewModel : ViewModelBase
{
    
    private Rest _rest = new Rest();
    
    [ObservableProperty] private ViewModelBase _activeContent;

    public MainWindowViewModel()
    {
        var mainViewModel = new MainViewModel(_rest);
        var loginViewModel = new LoginViewModel(_rest);
        _activeContent = loginViewModel;

        Messenger.Register<UserLoggedInMessage>(this, (_, _) => ActiveContent = mainViewModel);
        Messenger.Register<UserLoggedOutMessage>(this, (_, _) => ActiveContent = loginViewModel);
    }
}