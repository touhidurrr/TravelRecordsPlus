using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelRecords.Models;
using TravelRecords.Services;

namespace TravelRecords.ViewModels;

public record OnAccountAddedMessage(Account Account);

public partial class DashboardViewModel : ViewModelBase
{
    private Rest _rest;

    public ObservableCollection<Account> Accounts { get; }

    public DashboardViewModel(Rest rest = null!)
    {
        _rest = rest;

        Accounts = new ObservableCollection<Account>();

        _ = _rest.DoAfterUserIsAvailable(async () =>
        {
            try
            {
                var accounts = await _rest.GetAccounts();
                Console.WriteLine(accounts);
                if (accounts != null)
                    foreach (var account in accounts)
                        Accounts.Add(account);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        });
    }

    [ObservableProperty] private string _accountName = "";
    [ObservableProperty] private float _accountBalance = 0;

    [RelayCommand]
    private async Task AddAccount()
    {
        var account = await _rest.AddAccount(new AddAccountInputs(AccountName, AccountBalance, _rest.GetUser().id));
        if (account is null) return;
        Accounts.Add(account);
        Messenger.Send(this, new OnAccountAddedMessage(account));
    }
}