using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
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
        private ObservableCollection<NavigationItem> _navigationList = new();

        [ObservableProperty]
        private NavigationItem? _selectedPage;
        [ObservableProperty]
        private int _selectedIndex;


        // SQLite
        [ObservableProperty]
        private bool _isOoDbConnected = false;
        [ObservableProperty]
        private string _ooDbConnectionString = string.Empty;
        public OoDbContext? OoDbContext { get; set; }
        public DbConnection? OoDbConnection { get; set; }
        public DbCommand? OoDbCommand { get; set; }
        public DbDataReader? OoDbDataReader { get; set; }


        // SignalR
        [ObservableProperty]
        private bool _isSignalRConnected = false;
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
        private int _membersTotal;
        [ObservableProperty]
        private int _membersNormal;
        [ObservableProperty]
        private int _membersRest;
        [ObservableProperty]
        private int _membersAttention;
        [ObservableProperty]
        private int _membersCancel;
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
            { "재원", "휴원", "관심", "퇴회" };

        // Page Lessons
        [ObservableProperty]
        private ObservableCollection<LessonData> _lessonDataList = new();
        [ObservableProperty]
        private ObservableCollection<ModelLessons> _lessonList = new();
        public LessonData? LessonData { get; set; }
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
