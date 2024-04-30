using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelRecords.Models;

namespace TravelRecords.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

    [ObservableProperty] private string _test = "Test";

    [ObservableProperty] private bool _isEnabled = true;

    [RelayCommand]
    public void ToggleEnabled()
    {
        IsEnabled = !IsEnabled;
    }
    
    [ObservableProperty]
    private ViewModelBase _currentPage = new TestViewModel();
    
    
    [ObservableProperty]
    private ListItemTemplate _selectedPageItem;

    private readonly List<ListItemTemplate> _listItemTemplates =
    [
        new ListItemTemplate("Test", "Test", new TestViewModel()),
        new ListItemTemplate("New Test", "User", new Test2ViewModel()),
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
}