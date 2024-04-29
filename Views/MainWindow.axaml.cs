using System;
using Avalonia;
using Avalonia.Controls;

namespace TravelRecords.Views;

public partial class MainWindow : Window
{
    private Login _mLogin;

    private void SetLoginSize(Size size)
    {
        if (_mLogin.IsVisible)
        {
            _mLogin.Width = size.Width;
            _mLogin.Height = size.Height;
        }
    }
    
    private void ToggleLoginVisibility()
    {
        _mLogin.IsVisible = !_mLogin.IsVisible;
    }

    public MainWindow()
    {
        InitializeComponent();
        _mLogin = this.GetControl<Login>("Login");
        this.GetObservable(ClientSizeProperty).Subscribe(SetLoginSize);
        _mLogin.GetControl<Button>("LoginButton").Click += (sender, e) => ToggleLoginVisibility();
        _mLogin.GetControl<Button>("RegisterButton").Click += (sender, e) => ToggleLoginVisibility();
    }
}