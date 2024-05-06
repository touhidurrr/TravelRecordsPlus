using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TravelRecords.Services;

namespace TravelRecords.ViewModels;

public partial class LoginViewModel(Rest rest) : ViewModelBase
{
    [ObservableProperty] private string _name = "";
    [ObservableProperty] private string _email = "";
    [ObservableProperty] private string _username = "";
    [ObservableProperty] private string _password = "";
    [ObservableProperty] private string _confirmPassword = "";

    [ObservableProperty] private string _errorText = "";
    [ObservableProperty] private bool _errorPopupVisible = false;

    private void ShowErrorPopup(string errorText)
    {
        ErrorText = errorText;
        ErrorPopupVisible = true;
    }

    [RelayCommand]
    private void HideErrorPopup() => ErrorPopupVisible = false;

    [RelayCommand]
    private async Task Login()
    {
        LoginResponse? loginRes = null;
        try
        {
            loginRes = await rest.Login(new LoginInputs(Username, Password));
            Console.WriteLine(loginRes);
            rest.SetToken(loginRes!.token);
            rest.SetUser(loginRes.user);
            Messenger.Send<UserLoggedInMessage>();
        }
        catch (Exception e)
        {
            ShowErrorPopup("Unable to login");
        }
    }

    [RelayCommand]
    private async Task Register()
    {
        if (Password != ConfirmPassword)
        {
            ShowErrorPopup("Passwords do not match");
            return;
        }

        if (Password.Length < 8)
        {
            ShowErrorPopup("Password must be at least 8 characters long");
            return;
        }

        LoginResponse? loginRes = null;
        try
        {
            loginRes = await rest.Login(new LoginInputs(Username, Password));
            Console.WriteLine(loginRes);
            rest.SetToken(loginRes!.token);
            rest.SetUser(loginRes.user);
            Messenger.Send<UserLoggedInMessage>();
        }
        catch (Exception e)
        {
            ShowErrorPopup("Unable to register");
        }
    }
}