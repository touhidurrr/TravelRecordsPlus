using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using TravelRecords.Models;
using TravelRecords.Services;

namespace TravelRecords.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private Rest _rest;

    [ObservableProperty] private PageItemTemplate _selectedPage;
    public ObservableCollection<PageItemTemplate> Pages { get; }

    public MainViewModel(Rest rest = null!)
    {
        _rest = rest;

        Pages = new ObservableCollection<PageItemTemplate>(new[]
        {
            new PageItemTemplate("Dashboard", "Dashboard", new DashboardViewModel(_rest)),
            new PageItemTemplate("Accounts", "AccountBalance", new AccountsViewModel(_rest)),
            new PageItemTemplate("Profile", "Profile", new ProfileViewModel(_rest)),
        });

        SelectedPage = Pages[0];
    }

    partial void OnSelectedPageChanged(PageItemTemplate value)
    {
        SelectedPage = value;
    }
}