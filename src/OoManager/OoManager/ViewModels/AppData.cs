﻿using System.Collections.Generic;
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
using OoManager.Services;
using OoManager.ViewModels;

namespace OoManager.Models
{
    public partial class AppData : ViewModelBase
    {
        // WindowMain
        [ObservableProperty]
        private IAppUtiles? _ooService;
        [ObservableProperty]
        private bool _canConnectDb;
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
        private int _membersRest;
        [ObservableProperty]
        private int _membersPutOff;
        [ObservableProperty]
        private int _membersTotalMoney;


        // Page Members
        [ObservableProperty]
        private ObservableCollection<MemberData> _members = new();
        [ObservableProperty]
        private Dictionary<int, int> _membersDict = new();
        [ObservableProperty]
        private MemberData _memberData = new();
        [ObservableProperty]
        private MemberData? _selectedMember;
        [ObservableProperty]
        private string _pageMembersStatus = "재원";


        // Page Lectures
        [ObservableProperty]
        private ObservableCollection<LectureData> _lecturesTotal = new();
        [ObservableProperty]
        private Dictionary<string, int> _lecturesTotalDict = new();
        [ObservableProperty]
        private LectureData _lectureData = new();

        [ObservableProperty]
        private List<string> _lectureHeaderList = new();

        [ObservableProperty]
        private string _lectureHeader1 = string.Empty;
        [ObservableProperty]
        private string _lectureHeader2 = string.Empty;
        [ObservableProperty]
        private string _lectureHeader3 = string.Empty;
        [ObservableProperty]
        private string _lectureHeader4 = string.Empty;
        [ObservableProperty]
        private string _lectureHeader5 = string.Empty;
        [ObservableProperty]
        private string _lectureHeader6 = string.Empty;
        [ObservableProperty]
        private string _lectureHeader7 = string.Empty;

        [ObservableProperty]
        private Visibility _lectureHeader1Visibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _lectureHeader2Visibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _lectureHeader3Visibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _lectureHeader4Visibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _lectureHeader5Visibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _lectureHeader6Visibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _lectureHeader7Visibility = Visibility.Hidden;
    }
}