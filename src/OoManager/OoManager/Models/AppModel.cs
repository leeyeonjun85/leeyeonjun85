using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Database;
using Microsoft.AspNetCore.SignalR.Client;
using OoManager.Common;
using OoManager.Services;
using OoManager.ViewModels;

namespace OoManager.Models
{
    public partial class AppModel : ViewModelBase
    {
        [ObservableProperty]
        private string _testString = "test 스트링";
        [ObservableProperty]
        private int _testInt = 0;


        [ObservableProperty]
        private string _windowTitle = $"오투공부방 Manger - {ConfigurationManager.AppSettings["Version"]}";

        [ObservableProperty]
        private OoDbContext? _ooDbContext;
        [ObservableProperty]
        private IViewService? _viewService;
        [ObservableProperty]
        private IOoService? _ooService;
        [ObservableProperty]
        private HubConnection? _chatHubConnection;
        [ObservableProperty]
        private HubConnection? _ooHubConnection;
        [ObservableProperty]
        private Regex? _regexIsNumeric = new Regex("[0-9]+"); //regex that matches Numeric


        [ObservableProperty]
        private List<NavigationItem>? _navigationList;
        [ObservableProperty]
        private int _selectedIndex;
        [ObservableProperty]
        private NavigationItem? _selectedItem;

        [ObservableProperty]
        private Visibility _pageHomeVisibility = Visibility.Visible;
        [ObservableProperty]
        private Visibility _pageMembersVisibility = Visibility.Hidden;

        
        [ObservableProperty]
        private ObservableCollection<OoMembers> _members = new();
        [ObservableProperty]
        private bool _canConnectDb;

        [ObservableProperty]
        private OoMembers _selectedMember;

        [ObservableProperty]
        private string? _key;
        [ObservableProperty]
        private string? _member_class;
        [ObservableProperty]
        private int _member_grade;
        [ObservableProperty]
        private string? _member_grade_str;
        [ObservableProperty]
        private string? _member_money;
        [ObservableProperty]
        private string? _member_motherphone;
        [ObservableProperty]
        private string? _member_name;
        [ObservableProperty]
        private string? _member_status;
        [ObservableProperty]
        private string? _member_text;
        [ObservableProperty]
        private int _member_xp;
        [ObservableProperty]
        private string? _member_xp_log;
        [ObservableProperty]
        private int _mid;
        [ObservableProperty]
        private string? _wid;



        [ObservableProperty]
        private string _chatSendText = "";
        [ObservableProperty]
        private string _chatText = "=== 채팅을 시작합니다 ===";

        [ObservableProperty]
        private FirebaseClient? _firebaseClient;
    }
}
