using Avalonia;
using Avalonia.Controls;

namespace TravelRecords.Views;

public partial class MainWindow : Window
{
    private void ToggleLoginMenuVisibility()
    {
        LoginMenu.IsVisible = !LoginMenu.IsVisible;
    }

    public MainWindow()
    {
        InitializeComponent();
        LoginMenu.GetControl<Button>("LoginButton").Click += (sender, e) => ToggleLoginMenuVisibility();
        LoginMenu.GetControl<Button>("RegisterButton").Click += (sender, e) => ToggleLoginMenuVisibility();
    }
}