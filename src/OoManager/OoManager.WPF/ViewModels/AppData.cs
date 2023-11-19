using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Database.Query;
using MaterialDesignThemes.Wpf;
using Microsoft.AspNetCore.SignalR.Client;
using OoManager.Common.Models;
using OoManager.WPF.Models;

namespace OoManager.WPF.ViewModels
{
    public partial class AppData : ViewModelBase
    {
        // WindowMain
        public string dirRoot { get; set; } = string.Empty;
        public string dirDataBase { get; set; } = string.Empty;
        public string WindowTitle { get; set; } = string.Empty;
        [ObservableProperty]
        private ObservableCollection<NavigationItem> _navigationList = new() {
            new(
                name: "Home",
                title: "오투공부방 관리 시스템",
                selectedIcon: PackIconKind.Home,
                unselectedIcon: PackIconKind.HomeOutline,
                source: "/Views/PageHome.xaml",
                isEnabled: true
            ),
            new(
                name: "Members",
                title: "회원 관리",
                selectedIcon: PackIconKind.Users,
                unselectedIcon: PackIconKind.UsersOutline,
                source: "/Views/PageMembers.xaml",
                isEnabled: true
            ),
            new(
                name: "Lectures",
                title: "수업 관리",
                selectedIcon: PackIconKind.CalendarMultipleCheck,
                unselectedIcon: PackIconKind.CalendarCheck,
                source: "/Views/PageLecture.xaml",
                isEnabled: true
            )
        };

        [ObservableProperty]
        private NavigationItem? _selectedPage;
        [ObservableProperty]
        private int _selectedPageIndex;

        [ObservableProperty]
        private Visibility _pageHomeVisibility = Visibility.Visible;
        [ObservableProperty]
        private Visibility _pageMembersVisibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _pageLectureVisibility = Visibility.Hidden;

        // SQLite
        [ObservableProperty]
        private bool _ooDbIsConnected = false;
        public OoDbContext? OoDbContext { get; set; }
        public DbConnection? OoDbConnection { get; set; }
        public DbCommand? OoDbCommand { get; set; }
        public DbDataReader? OoDbDataReader { get; set; }

        // SignalR
        [ObservableProperty]
        private bool _isSignalRConnected = false;
        [ObservableProperty]
        private bool _noSignalRConnected = true;
        public HubConnection? HubConn { get; set; }
        public string SignalRIPv4 { get; set; } = string.Empty;
        public int SignalRPort { get; set; }
        public string SignalRHub { get; set; } = string.Empty;
        public int SignalRMode { get; set; } = 0;
        [ObservableProperty]
        private string _signalRAddress = string.Empty;
        public Process? SignalRServerProcess { get; set; }

        // Page Home
        [ObservableProperty]
        private string _serverPath = string.Empty;

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
        private ObservableCollection<ModelMember> _memberList = new();
        [ObservableProperty]
        private Dictionary<int, int> _membersDict = new();
        [ObservableProperty]
        private ModelMember _memberData = new();
        [ObservableProperty]
        private ModelMember? _selectedMember;
        [ObservableProperty]
        private string _pageMembersStatus = "전체";

        public List<string> GradeStrings { get; set; } = new()
            { "6살", "7살", "초1", "초2", "초3", "초4", "초5", "초6", "중1", "중2", "중3", "고1", "고2", "고3" };
        public List<string> StateStrings4 { get; set; } = new()
            { "전체", "재원", "휴원", "보류" };
        public List<string> StateStrings3 { get; set; } = new()
            { "재원", "휴원", "보류" };

        // Page Lessons
        [ObservableProperty]
        private ObservableCollection<ModelLessons> _lessonList = new();
        [ObservableProperty]
        private ObservableCollection<LessonData> _lessonDataList = new();

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


    }
}
