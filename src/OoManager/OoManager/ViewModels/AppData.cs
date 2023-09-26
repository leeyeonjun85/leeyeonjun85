using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.SignalR.Client;
using OoManager.Common;
using OoManager.Services;
using OoManager.ViewModels;

namespace OoManager.Models
{
    public partial class AppData : ViewModelBase
    {
        // WindowMain
        [ObservableProperty]
        private string _fireBaseDbName = "o2study_test";
        [ObservableProperty]
        private string _currentPage = "MainView";
        [ObservableProperty]
        private string _windowTitle = $"오투공부방 Manger - {ConfigurationManager.AppSettings["Version"]}";

        [ObservableProperty]
        private ObservableCollection<NavigationItem> _navigationList = new();
        [ObservableProperty]
        private int _selectedIndex;
        [ObservableProperty]
        private NavigationItem? _selectedItem;
        [ObservableProperty]
        private Visibility _pageHomeVisibility = Visibility.Visible;
        [ObservableProperty]
        private Visibility _pageMembersVisibility = Visibility.Hidden;

        [ObservableProperty]
        private OoDbContext? _ooDbContext;
        [ObservableProperty]
        private IOoService? _ooService;
        [ObservableProperty]
        private HubConnection? _chatHubConnection;
        [ObservableProperty]
        private HubConnection? _ooHubConnection;
        //[ObservableProperty]
        //private Regex? _regexIsNumeric = new Regex("[0-9]+"); //regex that matches Numeric

        [ObservableProperty]
        private ChildQuery? _firebaseDB;

        [ObservableProperty]
        private MemberData _memberData = new();
        [ObservableProperty]
        private ObservableCollection<MemberData> _members = new();
        

        [ObservableProperty]
        private bool _canConnectDb;

        [ObservableProperty]
        private MemberData? _selectedMember;
    }
}
