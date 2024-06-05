using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelRecords.Services;

namespace TravelRecords.ViewModels;

public partial class ProfileViewModel : ViewModelBase
{
    private Rest _rest;
    private User _user;

    [ObservableProperty] private string _name = "";
    [ObservableProperty] private string _email = "";
    [ObservableProperty] private string _username = "";
    [ObservableProperty] private string _password = "";
    [ObservableProperty] private string _newPassword = "";
    [ObservableProperty] private string _confirmNewPassword = "";

    public ProfileViewModel(Rest rest)
    {
        _rest = rest;
        _ = rest.DoAfterUserIsAvailable(() =>
        {
            _user = _rest.GetUser();
            Name = _user.name;
            Email = _user.email;
            Username = _user.username;
        });
    }

    [RelayCommand]
    private async Task Save()
    {
        try
        {
            var saveInputs = new UpdateUserInputs(Name, Email, Password, NewPassword);
            Console.WriteLine(saveInputs);
            await _rest.UpdateUser(_user.id, saveInputs);
        }
        catch
        {
            // ignored
        }
    }
}