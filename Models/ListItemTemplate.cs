using TravelRecords.ViewModels;

namespace TravelRecords.Models;

public class ListItemTemplate
{
    public string Label { get; }

    public string IconKind { get; }

    public ViewModelBase ViewModel { get; }

    public ListItemTemplate(string label, string iconKey, ViewModelBase vm)
    {
        Label = label;
        IconKind = iconKey;
        ViewModel = vm;
    }
}