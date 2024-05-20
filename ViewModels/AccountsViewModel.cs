using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
        Transactions = new ObservableCollection<Transaction>();

        Messenger.Register<OnAccountAddedMessage>(this, (sender, payload) => { Accounts.Add(payload.Account); });

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
    }

    partial void OnSelectedAccountChanged(Account value)
    {
        if (Accounts.Count > 0) _ = HandleAccountChangeAsync(value);
    }

    private async Task HandleAccountChangeAsync(Account value)
    {
        try
        {
            var transactions = await _rest.GetTransactions(value.id);
            Console.WriteLine(transactions);
            Transactions.Clear();
            if (transactions != null)
                foreach (var trn in transactions)
                    Transactions.Add(trn);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}