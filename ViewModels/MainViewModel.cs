using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using TravelRecords.Models;
using TravelRecords.Services;

namespace TravelRecords.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private Rest _rest;

    [ObservableProperty] private ListItemTemplate _selectedPageItem;


    public ObservableCollection<ListItemTemplate> Items { get; }

    public MainViewModel(Rest rest = null!)
    {
        _rest = rest;

        Items = new ObservableCollection<ListItemTemplate>(new[]
        {
            new ListItemTemplate("Test", "Test", new TestViewModel(_rest)),
            new ListItemTemplate("Test 2", "Work", new TestViewModel(_rest)),
            new ListItemTemplate("Profile", "Profile", new ProfileViewModel(_rest))
        });

        SelectedPageItem = Items[0];
    }

    partial void OnSelectedPageItemChanged(ListItemTemplate value)
    {
        SelectedPageItem = value;
    }
}