using System;
using Avalonia;
using Avalonia.Controls;

namespace TravelRecords.Views;

public partial class MainWindow : Window
{

    // private void UpdateLoginMenuSize(Size size)
    // {
    //     if (LoginMenu.IsVisible)
    //     {
    //         LoginMenu.Width = size.Width;
    //         LoginMenu.Height = size.Height;
    //     }
    // }
    //
    // private void ToggleLoginMenuVisibility()
    // {
    //     LoginMenu.IsVisible = !LoginMenu.IsVisible;
    // }

    public MainWindow()
    {
        InitializeComponent();
        // this.GetObservable(ClientSizeProperty).Subscribe(UpdateLoginMenuSize);
        // LoginMenu.GetControl<Button>("LoginButton").Click += (sender, e) => ToggleLoginMenuVisibility();
        // LoginMenu.GetControl<Button>("RegisterButton").Click += (sender, e) => ToggleLoginMenuVisibility();
    }
}