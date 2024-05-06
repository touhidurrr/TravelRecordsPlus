﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelRecords.Models;
using TravelRecords.Services;

namespace TravelRecords.ViewModels;

// Template for the ViewModel of the pages that would appear on main window
// After making a new file, you can copy the contents of this file and paste it in the new file
// Remember to remove this comments
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
        var account = await _rest.AddAccount(new AddAccountInputs(AccountName, AccountBalance));
        if (account is null) return;
        Accounts.Add(account);
    }
}