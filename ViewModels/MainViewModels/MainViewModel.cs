using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Models.ViewControlModels;
using System;
using System.Windows;
using System.Windows.Threading;
using ViewModels.GlobalVariable;
using ViewModels.ResponseService;
using ViewModels.SignalR;
using Views.AgiViews;
using Views.IntelViews;
using Views.StrViews;
using Views.VitViews;


namespace ViewModels.MainViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private UIElement? _currentView;

    [ObservableProperty]
    private SideBarModel _sideBarModel = new SideBarModel();

    private UIElement? _currentView2;

    private readonly ServerConnection _serverConnection;

    private readonly AgiView _agiView;
    private readonly StrView _strView;
    private readonly VitView _vitView;
    private readonly IntelView _intelView;


    public MainViewModel(AgiView agiView, StrView strView, VitView vitView, IntelView intelView, ServerConnection serverConnection)
    {
        _agiView = agiView;
        _strView = strView;
        _vitView = vitView;
        _intelView = intelView;

        SideBarModel.PropertyChanged += SideBarModel_PropertyChanged;
        SideBarModel.SideBarItemSelected = "Agi";
        _serverConnection = serverConnection;
    }

    private void SideBarModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(SideBarModel.SideBarItemSelected):
                if (SideBarModel.SideBarItemSelected == "Agi")
                {
                    CurrentView = _agiView;
                }

                if (SideBarModel.SideBarItemSelected == "Str")
                {
                    CurrentView = _strView;
                }

                if (SideBarModel.SideBarItemSelected == "Ene")
                {
                    CurrentView = _intelView;
                }

                if (SideBarModel.SideBarItemSelected == "Vit")
                {
                    CurrentView = _vitView;
                }

                break;

            default:
                break;
        }
    }

    [RelayCommand]
    public async Task SetCurrentView()
    {

        //throw new InvalidOperationException("Đây là một lỗi gây ra khi bấm nút.");
        //SideBarModel.SideBarItemList.Add("Linh");

        //Application.Current.Dispatcher.Invoke(() =>
        //{
        //    SideBarModel.SideBarItemList.Add("Linh");
        //});
        var request = new ObjectResponse();

        var user = new
        {
            Username = "testuser",
            Password = "testpassword"
        };

        var result = await request.Login(user);
        if (result.IsSuccess)
        {
            AccessToken.Instance.Token = result.Value.Token;
        }

    }


    [RelayCommand]
    public async Task Connect()
    {
        _serverConnection.InvokeAsync(async hub =>
        {
            try
            {
                await hub.InvokeAsync("UpdateMap", "request");
            }
            catch
            {

            }
        });
    }

    public void GetRandomSideBarItem()
    {
        Random random = new Random();
        int index = random.Next(0, 4);
        SideBarModel.SideBarItemSelected = SideBarModel.SideBarItemList[index];
    }

}
