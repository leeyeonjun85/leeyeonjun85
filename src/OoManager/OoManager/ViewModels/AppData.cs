using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Database.Query;
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
        private OoDbContext? _ooDbContext;

        [ObservableProperty]
        private ObservableCollection<NavigationItem> _navigationList = new();
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
        private ObservableCollection<ModelMembers> _members = new();
        [ObservableProperty]
        private Dictionary<int, int> _membersDict = new();
        [ObservableProperty]
        private ModelMembers _memberData = new();
        [ObservableProperty]
        private ModelMembers? _selectedMember;
        [ObservableProperty]
        private string _pageMembersStatus = "전체";

        public List<string> GradeStrings { get; set; } = new()
            { "6살", "7살", "초1", "초2", "초3", "초4", "초5", "초6", "중1", "중2", "중3", "고1", "고2", "고3" };
        public List<string> StateStrings4 { get; set; } = new()
            { "전체", "재원", "휴원", "보류" };
        public List<string> StateStrings3 { get; set; } = new()
            { "재원", "휴원", "보류" };

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
