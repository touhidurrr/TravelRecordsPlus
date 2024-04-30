using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelRecords.Models;

namespace TravelRecords.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase _currentPage = new TestViewModel();

    [ObservableProperty] private ListItemTemplate _selectedPageItem;

    private readonly List<ListItemTemplate> _listItemTemplates =
    [
        new ListItemTemplate("Test", "Test", new TestViewModel()),
        new ListItemTemplate("Test 2", "Work", new TestViewModel()),
    ];

    public ObservableCollection<ListItemTemplate> Items { get; }

    public MainWindowViewModel()
    {
        Items = new ObservableCollection<ListItemTemplate>(_listItemTemplates);
        SelectedPageItem = Items[0];
        CurrentPage = SelectedPageItem.ViewModel;
    }

    partial void OnSelectedPageItemChanged(ListItemTemplate value)
    {
        SelectedPageItem = value;
        CurrentPage = SelectedPageItem.ViewModel;
    }

    [ObservableProperty] private bool _isLoggedIn = false;

    [RelayCommand]
    private void ToggleLogin() => IsLoggedIn = !IsLoggedIn;
}