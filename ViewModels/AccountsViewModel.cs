using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelRecords.Models;
using TravelRecords.Services;

namespace TravelRecords.ViewModels;

public partial class AccountsViewModel : ViewModelBase
{
    private Rest _rest;

    [ObservableProperty] private Account _selectedAccount;

    public ObservableCollection<Account> Accounts { get; }
    public ObservableCollection<Transaction> Transactions { get; set; }

    public AccountsViewModel(Rest rest = null!)
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
                SelectedAccount = Accounts[0];
                try
                {
                    var transactions = await _rest.GetTransactions(SelectedAccount.id);
                    Console.WriteLine(transactions);
                    Transactions = new ObservableCollection<Transaction>(transactions ?? throw new InvalidOperationException());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
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
        var account = await _rest.AddAccount(new AddAccountInputs(AccountName, AccountBalance));
        if (account is null) return;
        Accounts.Add(account);
    }
}