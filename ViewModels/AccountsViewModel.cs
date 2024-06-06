using System;
using System.Collections.ObjectModel;
using System.Linq;
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
    [ObservableProperty] private string? _addTransactionRef = "";
    [ObservableProperty] private float _addTransactionAmount = 0;

    public ObservableCollection<Account> Accounts { get; }
    public ObservableCollection<Transaction> Transactions { get; set; }

    public AccountsViewModel(Rest rest = null!)
    {
        _rest = rest;

        Accounts = new ObservableCollection<Account>();
        Transactions = new ObservableCollection<Transaction>();

        Messenger.Register<OnAccountAddedMessage>(this, (_, _) => { _ = RefreshAccounts(); });

        _ = _rest.DoAfterUserIsAvailable(async () =>
        {
            try
            {
                await RefreshAccounts();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        });
    }

    private async Task RefreshAccounts()
    {
        Accounts.Clear();
        var accounts = await _rest.GetAccounts();
        Console.WriteLine(accounts);
        if (accounts != null)
            foreach (var account in accounts)
                Accounts.Add(account);
        SelectedAccount = Accounts[0];
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

    [RelayCommand]
    private async Task AddTransaction()
    {
        try
        {
            var transactions = await _rest.AddTransaction(
                new AddTransactionInputs(
                    AddTransactionRef.Length == 1 ? null : AddTransactionRef,
                    AddTransactionAmount,
                    SelectedAccount.id
                )
            );
            if (transactions is null) return;
            Transactions.Clear();
            foreach (var trn in transactions)
                Transactions.Add(trn);
            SelectedAccount = SelectedAccount with { balance = SelectedAccount.balance + AddTransactionAmount };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [RelayCommand]
    private async Task RemoveAccount(int accountId)
    {
        try
        {
            var res = await _rest.RemoveAccount(accountId);
            if (res != null && Convert.ToBoolean(res.success))
                Accounts.Remove(Accounts.First(x => x.id == accountId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
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