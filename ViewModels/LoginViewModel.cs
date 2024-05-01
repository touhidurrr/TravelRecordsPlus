using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace TravelRecords.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    [RelayCommand]
    private void Login()
    {
        Messenger.Send(new UserLoggedInMessage());
    }
}