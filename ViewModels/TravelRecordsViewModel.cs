using TravelRecords.Services;

namespace TravelRecords.ViewModels;

public class TravelRecordsViewModel(Rest rest) : ViewModelBase
{
    private Rest _rest = rest;
}