using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace TravelRecords.ViewModels;

public record UserLoggedInMessage();

public record UserLoggedOutMessage();

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase _activeContent;

    public MainWindowViewModel()
    {
        var mainViewModel = new MainViewModel();
        var loginViewModel = new LoginViewModel();
        _activeContent = loginViewModel;

        Messenger.Register<UserLoggedInMessage>(this, (_, _) => ActiveContent = mainViewModel);
        Messenger.Register<UserLoggedOutMessage>(this, (_, _) => ActiveContent = loginViewModel);
    }
}