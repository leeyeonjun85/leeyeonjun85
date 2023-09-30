using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Database;
using Firebase.Database.Query;
using MaterialDesignThemes.Wpf;
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
        private ChildQuery? _firebaseDB;
        [ObservableProperty]
        private string _fireBaseDbName = "o2study_test";
        [ObservableProperty]
        private string _windowTitle = $"오투공부방 Manger - {ConfigurationManager.AppSettings["Version"]}";

        [ObservableProperty]
        private ObservableCollection<NavigationItem> _navigationList = new()
            {
                new NavigationItem
                {
                    Title = "Home",
                    SelectedIcon = PackIconKind.Home,
                    UnselectedIcon = PackIconKind.HomeOutline,
                    Source = "/Views/PageHome.xaml",
                },
                new NavigationItem
                {
                    Title = "Members",
                    SelectedIcon = PackIconKind.Users,
                    UnselectedIcon = PackIconKind.UsersOutline,
                    Source = "/Views/PageMembers.xaml",
                },
                new NavigationItem
                {
                    Title = "Lectures",
                    SelectedIcon = PackIconKind.CalendarMultipleCheck,
                    UnselectedIcon = PackIconKind.CalendarCheck,
                    Source = "/Views/PageLecture.xaml",
                },
            };
        [ObservableProperty]
        private NavigationItem? _selectedPage;
        [ObservableProperty]
        private int _selectedPageIndex;
        [ObservableProperty]
        private string _selectedPageTitle = "MainView";

        [ObservableProperty]
        private Visibility _pageHomeVisibility = Visibility.Visible;
        [ObservableProperty]
        private Visibility _pageMembersVisibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _pageLectureVisibility = Visibility.Hidden;


        // Page Home
        [ObservableProperty]
        private string _fireBaseState = string.Empty;
        [ObservableProperty]
        private int _membersTotal;
        [ObservableProperty]
        private string _membersTotalString = string.Empty;
        [ObservableProperty]
        private int _membersNormal;
        [ObservableProperty]
        private string _membersNormalString = string.Empty;
        [ObservableProperty]
        private int _membersRest;
        [ObservableProperty]
        private string _membersRestString = string.Empty;
        [ObservableProperty]
        private int _membersPutOff;
        [ObservableProperty]
        private string _membersPutOffString = string.Empty;



        [ObservableProperty]
        private OoDbContext? _ooDbContext;
        [ObservableProperty]
        private IOoService? _ooService;


        //[ObservableProperty]
        //private Regex? _regexIsNumeric = new Regex("[0-9]+"); //regex that matches Numeric

        

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
