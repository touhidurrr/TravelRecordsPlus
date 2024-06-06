using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelRecords.Models;
using TravelRecords.Services;

namespace TravelRecords.ViewModels;

public partial class TravelRecordsViewModel : ViewModelBase
{
    private Rest _rest;
    [ObservableProperty] private string _newTitle = "";
    [ObservableProperty] private string _newFrom = "";
    [ObservableProperty] private string _newTo = "";
    [ObservableProperty] private string _newPartnerEmail = "";

    public ObservableCollection<TravelRecord> TravelRecords { get; set; }
    public ObservableCollection<User> Partners { get; set; }
    [ObservableProperty] private TravelRecord? _selectedTravelRecord;

    public TravelRecordsViewModel(Rest rest)
    {
        _rest = rest;
        TravelRecords = new ObservableCollection<TravelRecord>();
        Partners = new ObservableCollection<User>();
        _ = _rest.DoAfterUserIsAvailable(async () => { await RefreshTravelRecords(); });
    }

    private async Task RefreshTravelRecords()
    {
        TravelRecords.Clear();
        var records = await _rest.GetTravelRecords();
        Console.WriteLine(records);
        if (records != null)
            foreach (var rec in records)
                TravelRecords.Add(rec);
    }

    private async Task RefreshPartners()
    {
        Partners.Clear();
        var users = await _rest.GetPartners(SelectedTravelRecord.id);
        Console.WriteLine(users);
        if (users != null)
            foreach (var user in users)
                Partners.Add(user);
    }


    [RelayCommand]
    private async Task AddTravelRecord()
    {
        try
        {
            var newTravelRecord =
                await _rest.AddTravelRecord(new AddTravelRecordInputs(_rest.GetUser().id, NewTitle, NewFrom, NewTo));
            Console.WriteLine(newTravelRecord);
            if (newTravelRecord != null)
            {
                TravelRecords.Add(newTravelRecord);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [RelayCommand]
    private async Task AddPartner()
    {
        if (SelectedTravelRecord.id == null) return;
        try
        {
            var user = await _rest.AddPartner(SelectedTravelRecord.id, NewPartnerEmail);
            if (user != null) Partners.Add(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    partial void OnSelectedTravelRecordChanged(TravelRecord record)
    {
        if (TravelRecords.Count < 1) return;
        RefreshPartners();
    }
}