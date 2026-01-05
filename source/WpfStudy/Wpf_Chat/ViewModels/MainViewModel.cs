using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Wpf_Chat.Models;
using Wpf_Chat.Services;

namespace Wpf_Chat.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        #region Private Field
        private readonly IViewService _viewService;
        private readonly ISignalRControl _signalRControl;
        private readonly IConfiguration _config;
        private HubConnection? _hubConnection;
        #endregion


        #region Bingding Members
        public string TbServerIpText
        {
            get { return _tbServerIpText; }
            set { SetProperty(ref _tbServerIpText, value); }
        }
        private string _tbServerIpText = string.Empty;

        public string TbVpnText
        {
            get { return _tbVpnText; }
            set { SetProperty(ref _tbVpnText, value); }
        }
        private string _tbVpnText = string.Empty;

        public string TbUserNameText
        {
            get { return _tbUserNameText; }
            set { SetProperty(ref _tbUserNameText, value); }
        }
        private string _tbUserNameText = string.Empty;

        public string TbPasswordText
        {
            get { return _tbPasswordText; }
            set { SetProperty(ref _tbPasswordText, value); }
        }
        private string _tbPasswordText = string.Empty;

        public bool TbServerIpIsEnabled
        {
            get { return _tbServerIpIsEnabled; }
            set { SetProperty(ref _tbServerIpIsEnabled, value); }
        }
        private bool _tbServerIpIsEnabled = true;

        public bool TbVpnIsEnabled
        {
            get { return _tbVpnIsEnabled; }
            set { SetProperty(ref _tbVpnIsEnabled, value); }
        }
        private bool _tbVpnIsEnabled = true;

        public bool TbUserNameIsEnabled
        {
            get { return _tbUserNameIsEnabled; }
            set { SetProperty(ref _tbUserNameIsEnabled, value); }
        }
        private bool _tbUserNameIsEnabled = true;

        public bool TbPasswordIsEnabled
        {
            get { return _tbPasswordIsEnabled; }
            set { SetProperty(ref _tbPasswordIsEnabled, value); }
        }
        private bool _tbPasswordIsEnabled = true;

        public string TbNickNameText
        {
            get { return _tbNickNameText; }
            set { SetProperty(ref _tbNickNameText, value); }
        }
        private string _tbNickNameText = string.Empty;

        public string TbStatusBar1Text
        {
            get { return _tbStatusBar1Text; }
            set { SetProperty(ref _tbStatusBar1Text, value); }
        }
        private string _tbStatusBar1Text = "Status: Ready";

        public string TbStatusBar2Text
        {
            get { return _tbStatusBar2Text; }
            set { SetProperty(ref _tbStatusBar2Text, value); }
        }
        private string _tbStatusBar2Text = "Please Connect Server first!";

        public bool BtnConnectIsEnabled
        {
            get { return _btnConnectIsEnabled; }
            set { SetProperty(ref _btnConnectIsEnabled, value); }
        }
        private bool _btnConnectIsEnabled = true;

        public bool BtnChatOpenIsEnabled
        {
            get { return _btnChatOpenIsEnabled; }
            set { SetProperty(ref _btnChatOpenIsEnabled, value); }
        }
        private bool _btnChatOpenIsEnabled = false;
        #endregion

        #region Constructor
        public MainViewModel(
            ISignalRControl signalRControl,
            IViewService viewService,
            IConfiguration config)
        {
            // 서비스 준비
            _viewService = viewService;
            _signalRControl = signalRControl;

            // appsettings.josn
            _config = config;
            IConfigurationSection MyProfile = _config.GetSection("MyProfile");
            IConfigurationSection _configSignalR = _config.GetSection("AppConfiguration");
            

            //TbServerIpText = SignalRConfig["Server"]!;
            //TbVpnText = SignalRConfig["VPN"]!;
            //TbUserNameText = SignalRConfig["UserName"]!;
            //TbPasswordText = SignalRConfig["Password"]!;
            TbNickNameText = MyProfile["Name"]!;
            MyProfile["Name"] = "도토리";
            TbNickNameText = MyProfile["Name"]!;

            //ConfigurationManager.AppSettings[paramName];

            TbServerIpText = Properties.Settings.Default.SignalRConfig_Server;
            TbVpnText = Properties.Settings.Default.SignalRConfig_VPN;
            TbUserNameText = Properties.Settings.Default.SignalRConfig_UserName;
            TbPasswordText = Properties.Settings.Default.SignalRConfig_Password;
        }
        #endregion

        //public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

        public void ConnectServer(object? obj)
        {
            _hubConnection = _signalRControl.Connect();
            TbStatusBar1Text = _hubConnection.State.ToString();
            TbStatusBar2Text = "원하는 서비스를 실행해 주세요.";
            BtnConnectIsEnabled = false;
            BtnChatOpenIsEnabled = true;
            TbServerIpIsEnabled = false;
            TbVpnIsEnabled = false;
            TbUserNameIsEnabled = false;
            TbPasswordIsEnabled = false;

            Properties.Settings.Default.SignalRConfig_Server = TbServerIpText;
            Properties.Settings.Default.SignalRConfig_VPN = TbVpnText;
            Properties.Settings.Default.SignalRConfig_UserName = TbUserNameText;
            Properties.Settings.Default.SignalRConfig_Password = TbPasswordText;
            Properties.Settings.Default.Save();
        }

        private void ShowSubView(object? obj)
        {
            try
            {
                var subData = new SubData()
                {
                    NickName = TbNickNameText,
                    HConnection = _hubConnection,
                };
                _viewService.ShowSubView(subData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.Source}");
                throw;
            }
        }


        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
            }
        }

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("MainWindow Loaded");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            var result = MessageBox.Show("종료 하시겠습니까?", "프로그램 종료", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        public ICommand BtnChatOpenCommand => new RelayCommand<object>(ShowSubView);
        public ICommand BtnConnectCommand => new RelayCommand<object>(ConnectServer);
    }
}
