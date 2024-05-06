using CommunityToolkit.Mvvm.ComponentModel;
using TravelRecords.Services;

namespace TravelRecords.ViewModels;

public partial class ProfileViewModel : ViewModelBase
{
    private Rest _rest;

    [ObservableProperty] private string _name = "";
    [ObservableProperty] private string _email = "";
    [ObservableProperty] private string _username = "";

    public ProfileViewModel(Rest rest)
    {
        _rest = rest;
        _ = rest.DoAfterUserIsAvailable(() =>
        {
            User user = _rest.GetUser();
            Name = user.name;
            Email = user.email;
            Username = user.username;
        });
    }
}