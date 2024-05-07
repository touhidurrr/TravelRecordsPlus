using TravelRecords.ViewModels;

namespace TravelRecords.Models;

public class PageItemTemplate
{
    public string Label { get; }

    public string IconKind { get; }

    public ViewModelBase ViewModel { get; }

    public PageItemTemplate(string label, string iconKey, ViewModelBase vm)
    {
        Label = label;
        IconKind = iconKey;
        ViewModel = vm;
    }
}