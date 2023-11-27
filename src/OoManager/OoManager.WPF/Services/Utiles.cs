using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Firebase.Database;
using Firebase.Database.Query;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OoManager.Common.Models;
using OoManager.WPF.Models;
using OoManager.WPF.ViewModels;
using System.IO;
using Newtonsoft.Json;

namespace OoManager.WPF.Services
{
    public class Utiles
    {
        public async static Task GetSQLiteAsync()
        {
            await Task.Run(() =>
            {
                if (App.Data.OoDbContext is null) return;

                App.Data.OoDbContext.Database.EnsureCreated();
                App.Data.OoDbConnection = App.Data.OoDbContext.Database.GetDbConnection();
                App.Data.OoDbConnection.Open();
                App.Data.OoDbCommand = App.Data.OoDbConnection.CreateCommand();
                App.Data.OoDbCommand.CommandText = "PRAGMA journal_mode=Off;";
                App.Data.OoDbCommand.ExecuteNonQuery();
                App.Data.OoDbConnectionString = App.Data.OoDbConnection.ConnectionString;
                App.Data.IsOoDbConnected = true;
            });

            await RefreshOoDbAsync();
        }

        public async static Task GetSignalRAddressAsync()
        {
            await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(App.Data.SignalRAddress))
                {
                    IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (IPAddress iPAddress in host.AddressList)
                    {
                        if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
                        {
                            App.Data.SignalRIPv4 = iPAddress.ToString();
                        }
                    }
                    App.Data.SignalRPort = 6714;
                    App.Data.SignalRHub = "chathub";
                    App.Data.SignalRAddress = $"https://{App.Data.SignalRIPv4}:{App.Data.SignalRPort}/{App.Data.SignalRHub}";
                }
            });
        }

        public async static Task GetSignalRAsync()
        {
            await Task.Run(async () =>
            {
                if (string.IsNullOrEmpty(App.Data.SignalRAddress))
                    await GetSignalRAddressAsync();

                App.Data.SignalRIPv4 = App.Data.SignalRAddress[App.Data.SignalRAddress.IndexOf("//")..App.Data.SignalRAddress.LastIndexOf(":")][2..];
                App.Data.SignalRPort = Convert.ToInt32(App.Data.SignalRAddress[App.Data.SignalRAddress.LastIndexOf(":")..App.Data.SignalRAddress.LastIndexOf("/")][1..]);
                App.Data.SignalRHub = App.Data.SignalRAddress[App.Data.SignalRAddress.LastIndexOf($":{App.Data.SignalRPort}/")..][$":{App.Data.SignalRPort}/".Length..];
                App.Data.SignalRAddress = $"https://{App.Data.SignalRIPv4}:{App.Data.SignalRPort}/{App.Data.SignalRHub}";

                ProcessStartInfo psi = new()
                {
                    FileName = "OoManager.Server.exe",
                    Arguments = $"\"{App.Data.SignalRIPv4}\" \"{App.Data.SignalRPort}\" \"{App.Data.SignalRHub}\"",
                    CreateNoWindow = true
                };
                App.Data.SignalRServerProcess = Process.Start(psi);
                App.Data.IsSignalRConnected = true;

                //App.Data.SignalRServerProcess = Process.Start("BlazorServerSignalRApp.exe", new string[3] { AppData.SignalRIPv4, AppData.SignalRPort.ToString(), AppData.SignalRHub });
            });
        }

        public static async Task OpenPageHomeAsync(AppData AppData)
        {
            // Init PageHome
            AppData.SelectedIndex = PagesIndex.Home;
            AppData.SelectedPage = AppData.NavigationList[PagesIndex.Home];
        }

        public static async Task OpenPageMembersAsync(AppData AppData)
        {
            // Init PageMembers
            AppData.SelectedIndex = PagesIndex.Members;
            AppData.SelectedPage = AppData.NavigationList[PagesIndex.Members];
        }

        public static async Task OpenPageLectureAsync(AppData AppData)
        {
            // Init PageLecture
            AppData.SelectedIndex = PagesIndex.Lectures;
            AppData.SelectedPage = AppData.NavigationList[PagesIndex.Lectures];
        }

        public static async Task RefreshNaviItemsAsync()
        {
            ObservableCollection<NavigationItem> tempList = new();
            
            if (App.Data.NavigationList.Count != 0)
            {
                foreach (NavigationItem item in App.Data.NavigationList)
                    tempList.Add(item);

                App.Data.NavigationList.Clear();

                foreach (NavigationItem item in tempList)
                    App.Data.NavigationList.Add(item);

                await OpenPageHomeAsync(App.Data);
            }
            else
            {
                App.Data.NavigationList.Clear();

                App.Data.NavigationList!.Add(
                new(
                    name: "Home",
                    title: "오투공부방 관리 시스템",
                    selectedIcon: PackIconKind.Home,
                    unselectedIcon: PackIconKind.HomeOutline,
                    source: "/Views/PageHome.xaml",
                    isEnabled: true
                ));
                App.Data.NavigationList.Add(
                new(
                    name: "Members",
                    title: "회원 관리",
                    selectedIcon: PackIconKind.Users,
                    unselectedIcon: PackIconKind.UsersOutline,
                    source: "/Views/PageMembers.xaml",
                    isEnabled: false
                ));
                App.Data.NavigationList.Add(
                new(
                    name: "Lectures",
                    title: "수업 관리",
                    selectedIcon: PackIconKind.CalendarMultipleCheck,
                    unselectedIcon: PackIconKind.CalendarCheck,
                    source: "/Views/PageLecture.xaml",
                    isEnabled: false
                ));

                await OpenPageHomeAsync(App.Data);
            }
        }

        public static void TurnNaviButton(Grid gridMain, string btnName, bool turnTo)
        {
            foreach (var child1 in gridMain.Children)
            {
                if (child1 is Grid gridLeftMenu)
                {
                    if (gridLeftMenu.Name is "gridLeftMenu")
                    {
                        foreach (var child2 in gridLeftMenu.Children)
                        {
                            if (child2 is ListBox listBox)
                            {
                                foreach (NavigationItem item in listBox.Items)
                                {
                                    if (item.Name == btnName)
                                    {
                                        item.IsEnabled = turnTo;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        

        

        public static async Task RefreshOoDbAsync()
        {
            if (App.Data.OoDbContext is null) return;

            // Init Lessons
            await App.Data.OoDbContext.lessons.LoadAsync();
            App.Data.LessonList = App.Data.OoDbContext.lessons.Local.ToObservableCollection();

            // Init Members
            await App.Data.OoDbContext.members.LoadAsync();
            App.Data.MemberList = App.Data.OoDbContext.members.Local.ToObservableCollection();

            //List<ModelMember> memberList = App.Data.OoDbContext.members
            //        .FromSql($"SELECT * FROM members")
            //        .OrderBy(b => b.id)
            //        .ToList();
            //App.Data.MemberList = new();
            //foreach (var member in memberList)
            //    App.Data.MemberList.Add(member);

            // Init Lessons
            //string sqlString = $"SELECT DISTINCT dateTimeStart ";
            //sqlString += $"FROM lessons ";
            //sqlString += $"WHERE dateTimeStart > {DateTime.Now.AddDays(-15)} ";
            //FormattableString sql = $"{sqlString}";

            //var recentLessonsDates = App.Data.OoDbContext.lessons
            //                                    .FromSql(sql)
            //                                    .ToList();


            List<string> recentLessonsDates = App.Data.OoDbContext.lessons
                                                .Include(e => e.ModelMember)
                                                .Where<ModelLessons>(e => e.dateTimeStart >= DateTime.Now.AddDays(-15))
                                                .Select(e => e.dateTimeStart.ToString("yyyy-MM-dd"))
                                                .ToList().Distinct().ToList();

            if (recentLessonsDates.Count > 0)
            {
                App.Data.LectureHeader1 = recentLessonsDates[^1];
                if (recentLessonsDates.Count > 1)
                {
                    App.Data.LectureHeader2 = recentLessonsDates[^2];
                    if (recentLessonsDates.Count > 2)
                    {
                        App.Data.LectureHeader3 = recentLessonsDates[^3];
                        if (recentLessonsDates.Count > 3)
                        {
                            App.Data.LectureHeader4 = recentLessonsDates[^4];
                            if (recentLessonsDates.Count > 4)
                            {
                                App.Data.LectureHeader5 = recentLessonsDates[^5];
                                if (recentLessonsDates.Count > 5)
                                {
                                    App.Data.LectureHeader6 = recentLessonsDates[^6];
                                    if (recentLessonsDates.Count > 6)
                                    {
                                        App.Data.LectureHeader7 = recentLessonsDates[^7];
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Init LessonDataList
            App.Data.LessonDataList = new();
            foreach (var member in App.Data.MemberList)
            {
                LessonData addLessonData = new(member) { ToolTips = new($"{member.name} XP 수정하기", $"{member.name} 보너스 5xp", $"{member.name} 회원정보 수정", $"{member.name} 수업정보 수정") };

                if (member.ModelLessons is not null)
                {
                    foreach (var lesson in member.ModelLessons)
                    {
                        if (lesson.dateTimeStart.ToString("yyyy-MM-dd") == App.Data.LectureHeader1)
                            addLessonData.Lesson1 = lesson;
                        else if (lesson.dateTimeStart.ToString("yyyy-MM-dd") == App.Data.LectureHeader2)
                            addLessonData.Lesson2 = lesson;
                        else if (lesson.dateTimeStart.ToString("yyyy-MM-dd") == App.Data.LectureHeader3)
                            addLessonData.Lesson3 = lesson;
                        else if (lesson.dateTimeStart.ToString("yyyy-MM-dd") == App.Data.LectureHeader4)
                            addLessonData.Lesson4 = lesson;
                        else if (lesson.dateTimeStart.ToString("yyyy-MM-dd") == App.Data.LectureHeader5)
                            addLessonData.Lesson5 = lesson;
                        else if (lesson.dateTimeStart.ToString("yyyy-MM-dd") == App.Data.LectureHeader6)
                            addLessonData.Lesson6 = lesson;
                        else if (lesson.dateTimeStart.ToString("yyyy-MM-dd") == App.Data.LectureHeader7)
                            addLessonData.Lesson7 = lesson;
                    }
                }

                App.Data.LessonDataList.Add(addLessonData);
            }
        }

        public static async Task<AppData> RefreshDataAsync(AppData AppData)
        {
            //// Init Members
            //Task<AppData> _appData1 = GetMembersAsync(AppData);
            //await _appData1; AppData = _appData1.Result;

            //// Init Lectures
            //Task<AppData> _appData2 = GetLecturesAsync(AppData);
            //await _appData2; AppData = _appData2.Result;

            //// Init Members Information
            //AppData.MembersTotal = AppData.Members.Count;
            //AppData.MembersNormal = AppData.MembersRest = AppData.MembersPutOff = AppData.MembersTotalMoney = 0;
            //foreach (MemberData member in AppData.Members)
            //{
            //    // Lectures Init
            //    foreach (LectureData memberLecture in member.Lectures)
            //    {
            //        if (!AppData.LectureHeaderList.Contains(memberLecture.o2_class_date))
            //            AppData.LectureHeaderList.Add(memberLecture.o2_class_date);
            //    }

            //    if (AppData.LectureHeaderList.Count > 7)
            //    {
            //        int _removeCount = AppData.LectureHeaderList.Count - 7;
            //        AppData.LectureHeaderList.RemoveRange(0, _removeCount);
            //    }

            //    switch (AppData.LectureHeaderList.Count)
            //    {
            //        case 0:
            //            {
            //                break;
            //            }
            //        case 1:
            //            {

            //                break;
            //            }
            //        case 2:
            //            {
            //                break;
            //            }
            //        case 3:
            //            {
            //                break;
            //            }
            //        case 4:
            //            {
            //                break;
            //            }
            //        case 5:
            //            {
            //                break;
            //            }
            //        case 6:
            //            {
            //                break;
            //            }
            //        case 7:
            //            {
            //                break;
            //            }

            //        default: throw new Exception();
            //    }


            //    switch (member.Member.member_status)
            //    {
            //        case "재원":
            //            {
            //                AppData.MembersNormal += 1;
            //                AppData.MembersTotalMoney += Convert.ToInt32(member.Member.member_money);
            //                break;
            //            }
            //        case "휴원":
            //            {
            //                AppData.MembersRest += 1;
            //                break;
            //            }
            //        case "보류":
            //            {
            //                AppData.MembersPutOff += 1;
            //                break;
            //            }

            //        default: throw new Exception();
            //    }
            //}

            //// Init Program Information
            //if (AppData.MembersTotal > 0)
            //    AppData.FireBaseState = "연결 성공";
            //else AppData.FireBaseState = "연결 실패";


            return AppData;
        }

        public static async Task<AppData> GetMembersAsync(AppData AppData)
        {
            AppData.MemberList = new();
            AppData.MembersDict = new();
            int _idx = 0;

            // Get members
            //IReadOnlyCollection<FirebaseObject<Member>> members = await AppData.FirebaseDB
            //        .Child("member")
            //        .OnceAsync<Member>();

            //foreach (var member in members)
            //{
                //AppData.MemberData = new();
                //AppData.MemberData.Key = member.Key;
                //AppData.MemberData.Member = member.Object;
                //AppData.MemberData.SelectedGrade = member.Object.member_grade_str;
                //AppData.MemberData.SelectedState = member.Object.member_status;
                //AppData.MemberData.XpUpdateToolTip = $"{AppData.MemberData.Member.member_name} XP 수정하기";
                //AppData.MemberData.BonusToolTip = $"{AppData.MemberData.Member.member_name}에게 +5xp";
                //AppData.MemberData.MemberUpdateToolTip = $"{AppData.MemberData.Member.member_name} 회원정보 수정하기";
                //AppData.MemberData.LectureUpdateToolTip = $"{AppData.MemberData.Member.member_name} 수업 수정하기";
                //AppData.Members.Add(AppData.MemberData);
                //AppData.MembersDict[member.Object.mid] = _idx;
                //_idx++;
            //}

            return AppData;
        }

        public static async Task<AppData> GetLecturesAsync(AppData AppData)
        {
            // Init Lectures
            //IReadOnlyCollection<FirebaseObject<object>> _lecturesDates = await AppData.FirebaseDB
            //        .Child("lecture")
            //        .OnceAsync<object>();

            //foreach (FirebaseObject<object> _lecturesDate in _lecturesDates)
            //{
            //    if (_lecturesDate.Object is IEnumerable<JToken> _lecturesDateJToken)
            //    {
            //        foreach (JToken _lectureJToken in _lecturesDateJToken)
            //        {
            //            Lecture _lecture = new()
            //            {
            //                mid = _lectureJToken.First!.Value<int>("mid"),
            //                o2_class_date = _lectureJToken.First.Value<string>("o2_class_date")!,
            //                o2_class_homework = _lectureJToken.First.Value<string>("o2_class_homework")!,
            //                o2_class_lecture = _lectureJToken.First.Value<string>("o2_class_lecture")!,
            //                o2_class_memo = _lectureJToken.First.Value<string>("o2_class_memo")!,
            //                o2_class_time_in = _lectureJToken.First.Value<string>("o2_class_time_in")!,
            //                o2_class_time_out = _lectureJToken.First.Value<string>("o2_class_time_out")!,
            //            };

            //            LectureData _lectureData = new()
            //            {
            //                Key = _lectureJToken.Path,
            //                Lecture = _lecture,
            //                mid = _lectureJToken.First!.Value<int>("mid"),
            //                o2_class_date = _lectureJToken.First.Value<string>("o2_class_date")!,
            //                o2_class_homework = _lectureJToken.First.Value<string>("o2_class_homework")!,
            //                o2_class_lecture = _lectureJToken.First.Value<string>("o2_class_lecture")!,
            //                o2_class_memo = _lectureJToken.First.Value<string>("o2_class_memo")!,
            //                o2_class_time_in = _lectureJToken.First.Value<string>("o2_class_time_in")!,
            //                o2_class_time_out = _lectureJToken.First.Value<string>("o2_class_time_out")!,
            //            };
            //            AppData.LecturesTotal.Add(_lectureData);
            //            AppData.LecturesTotalDict[_lectureData.Key] = _idx;
            //            //AppData.Members[AppData.MembersDict[_lectureJToken.First!.Value<int>("mid")]].Lectures.Add(_lectureData);
            //            _idx++;
            //        }
            //    }
            //}

            return AppData;
        }





        public static async Task<AppData> InitMembers(AppData AppData)
        {
            int mid = 0;

            List<Tuple<string, string, int>> memeberList = new()
            {
                new Tuple<string, string, int>("고1", "이연준", 10),
                new Tuple<string, string, int>("고2", "이채은", 100)

                //new Tuple<string, string, int>("초1", "도연우", 735),
                //new Tuple<string, string, int>("초1", "박재현", 950),
                //new Tuple<string, string, int>("초1", "도연우", 735),
                //new Tuple<string, string, int>("초1", "임수현", 185),
                //new Tuple<string, string, int>("초1", "서동인", 45),
                //new Tuple<string, string, int>("초2", "김윤", 80),
                //new Tuple<string, string, int>("초2", "박서연", 885),
                //new Tuple<string, string, int>("초2", "도화랑", 375),
                //new Tuple<string, string, int>("초3", "유희건", 1585),
                //new Tuple<string, string, int>("초3", "이윤지", 990),
                //new Tuple<string, string, int>("초4", "임서진", 775),
                //new Tuple<string, string, int>("초4", "김민", 435),
                //new Tuple<string, string, int>("초4", "박시연", 925),
                //new Tuple<string, string, int>("초5", "이세연", 460),
                //new Tuple<string, string, int>("초5", "유지흔", 1210),
                //new Tuple<string, string, int>("초5", "정윤후", 185),
                //new Tuple<string, string, int>("초5", "서민지", 905),
                //new Tuple<string, string, int>("초5", "서아인", 420),
                //new Tuple<string, string, int>("중1", "김태연", 40),
                //new Tuple<string, string, int>("중1", "윤미래", 10),
                //new Tuple<string, string, int>("중1", "도하율", 10),
                //new Tuple<string, string, int>("중2", "오채은", 10),
                //new Tuple<string, string, int>("중2", "서아민", 10),
                //new Tuple<string, string, int>("중3", "김태은", 10),
                //new Tuple<string, string, int>("중3", "지예도", 10),
                //new Tuple<string, string, int>("중3", "구영우", 55),
                //new Tuple<string, string, int>("중3", "이신아", 10),
                //new Tuple<string, string, int>("중3", "이우주", 10),
                //new Tuple<string, string, int>("고3", "오예은", 10)
        };

            foreach (var _memeber in memeberList)
            {
                //mid += 1;
                //AppData.MemberData.Member = new();
                //AppData.MemberData.Member.member_grade_str = _memeber.Item1;
                //AppData.MemberData.Member.member_grade = ConvertGradeOld(AppData.MemberData.Member.member_grade_str);
                //AppData.MemberData.Member.member_name = _memeber.Item2;
                //AppData.MemberData.Member.member_xp = _memeber.Item3;
                //AppData.MemberData.Member.mid = mid;

                ////await AppData.FirebaseDB
                ////        .Child("member")
                ////        .PostAsync(AppData.MemberData.Member);

                //Task<FirebaseObject<Member>> returnMember = AppData.FirebaseDB
                //        .Child("member")
                //        .PostAsync(AppData.MemberData.Member);
                //await returnMember;
                //AppData.MemberData.Key = returnMember.Result.Key;
                ////AppData.Members.Add(AppData.MemberData);
                //AppData.MemberData = new();
            }
            return AppData;
        }

        //public AppData ConvertGradeOld(AppData AppData)
        //{
        //    if (AppData.MemberData.Member.member_grade_str == "6살")
        //        AppData.MemberData.Member.member_grade = 6;
        //    else if (AppData.MemberData.Member.member_grade_str == "7살")
        //        AppData.MemberData.Member.member_grade = 7;
        //    else if (AppData.MemberData.Member.member_grade_str == "초1")
        //        AppData.MemberData.Member.member_grade = 8;
        //    else if (AppData.MemberData.Member.member_grade_str == "초2")
        //        AppData.MemberData.Member.member_grade = 9;
        //    else if (AppData.MemberData.Member.member_grade_str == "초3")
        //        AppData.MemberData.Member.member_grade = 10;
        //    else if (AppData.MemberData.Member.member_grade_str == "초4")
        //        AppData.MemberData.Member.member_grade = 11;
        //    else if (AppData.MemberData.Member.member_grade_str == "초5")
        //        AppData.MemberData.Member.member_grade = 12;
        //    else if (AppData.MemberData.Member.member_grade_str == "초6")
        //        AppData.MemberData.Member.member_grade = 13;
        //    else if (AppData.MemberData.Member.member_grade_str == "중1")
        //        AppData.MemberData.Member.member_grade = 14;
        //    else if (AppData.MemberData.Member.member_grade_str == "중2")
        //        AppData.MemberData.Member.member_grade = 15;
        //    else if (AppData.MemberData.Member.member_grade_str == "중3")
        //        AppData.MemberData.Member.member_grade = 16;
        //    else if (AppData.MemberData.Member.member_grade_str == "고1")
        //        AppData.MemberData.Member.member_grade = 17;
        //    else if (AppData.MemberData.Member.member_grade_str == "고2")
        //        AppData.MemberData.Member.member_grade = 18;
        //    else if (AppData.MemberData.Member.member_grade_str == "고3")
        //        AppData.MemberData.Member.member_grade = 19;

        //    return AppData;
        //}

        public async static Task AddMemberAsync(ModelMember member)
        {
            await App.Data.OoDbContext!.members.AddAsync(member);
            App.Data.OoDbContext!.SaveChanges();
        }

        public async static Task DeleteMemberAsync(ModelMember member)
        {
            App.Data.OoDbContext!.members.Remove(member);
            App.Data.OoDbContext!.SaveChanges();
        }


        public static int ConvertGradeOld(string GradeString)
        {
            int GradeInt;
            switch (GradeString)
            {
                case "6살": { GradeInt = 6; break; }
                case "7살": { GradeInt = 7; break; }
                case "초1": { GradeInt = 8; break; }
                case "초2": { GradeInt = 9; break; }
                case "초3": { GradeInt = 10; break; }
                case "초4": { GradeInt = 11; break; }
                case "초5": { GradeInt = 12; break; }
                case "초6": { GradeInt = 13; break; }
                case "중1": { GradeInt = 14; break; }
                case "중2": { GradeInt = 15; break; }
                case "중3": { GradeInt = 16; break; }
                case "고1": { GradeInt = 17; break; }
                case "고2": { GradeInt = 18; break; }
                case "고3": { GradeInt = 19; break; }
                default: throw new Exception("입력된 학년 문자열에 문제가 있습니다.");
            }

            return GradeInt;
        }

        public static async Task UpdateMemberAsync(AppData AppData)
        {
            //var _foundata = AppData.OoDbContext!.members.FindAsync(AppData.MemberData.id)!;
            //await _foundata; AppData.MemberData = _foundata.Result!;
            //AppData.OoDbContext!.Entry(AppData.MemberData).State = EntityState.Detached;
            //AppData.OoDbContext!.members.Entry(AppData.MemberData).State = EntityState.Modified;
            //AppData.OoDbContext!.SaveChanges();

            var _foundata = AppData.OoDbContext!.members.FindAsync(AppData.MemberData.id)!;
            await _foundata;
            ModelMember memberData = _foundata.Result!;
            AppData.OoDbContext!.Entry(memberData).State = EntityState.Detached;
            memberData = AppData.MemberData;
            memberData.old = ConvertGradeOld(AppData.MemberData.grade);
            AppData.OoDbContext!.members.Entry(memberData).State = EntityState.Modified;
            AppData.OoDbContext!.SaveChanges();

            //string sqlString = $"UPDATE [members] SET ";
            //sqlString += $"[id] = {AppData.MemberData.id} ";
            //sqlString += $"[grade] = {AppData.MemberData.grade} ";
            //sqlString += $"[name] = {AppData.MemberData.name} ";
            //sqlString += $"[old] = {AppData.MemberData.old} ";
            //sqlString += $"[classPlan] = {AppData.MemberData.classPlan} ";
            //sqlString += $"[memberState] = {AppData.MemberData.memberState} ";
            //sqlString += $"[phoneNumber] = {AppData.MemberData.phoneNumber} ";
            //sqlString += $"[memberMemo] = {AppData.MemberData.memberMemo} ";
            //sqlString += $"[xp] = {AppData.MemberData.xp} ";
            //sqlString += $"[xpLog] = {AppData.MemberData.xpLog} ";
            //sqlString += $"[ModelLessons] = {AppData.MemberData.ModelLessons} ";
            //sqlString += $"WHERE [id] = {AppData.MemberData.id} ";
            //FormattableString formattableString = $"{sqlString}";
            //AppData.OoDbContext!.Database.ExecuteSql(formattableString);

            await RefreshOoDbAsync();

            //AppData.SQLiteContext.Entry(_findData).State = EntityState.Detached;
            //_findData.Name = AppData.UpdateName;
            //_findData.Old = AppData.UpdateOld;
            //AppData.SQLiteContext.sqliteDB.Entry(_findData).State = EntityState.Modified;
            //await AppData.SQLiteContext!.SaveChangesAsync();
        }

        public async static Task AddLessonAsync(ModelLessons lesson)
        {
            await App.Data.OoDbContext!.lessons.AddAsync(lesson);
            App.Data.OoDbContext!.SaveChanges();
        }

        public static void DisposeSignalR()
        {
            App.Data.IsSignalRConnected = false;
            App.Data.SignalRAddress = string.Empty;
            App.Data.SignalRServerProcess?.Kill();
            App.Data.HubConn?.StopAsync();
            App.Data.HubConn = null;
        }

        public static void DisposeSQLite()
        {
            App.Data.IsOoDbConnected = false;
            App.Data.OoDbConnectionString = string.Empty;
            App.Data.OoDbDataReader?.Close();
            App.Data.OoDbDataReader?.Dispose();
            App.Data.OoDbDataReader = null;
            App.Data.OoDbCommand?.Dispose();
            App.Data.OoDbCommand = null;
            App.Data.OoDbConnection?.Close();
            App.Data.OoDbConnection?.Dispose();
            App.Data.OoDbConnection = null;
        }

        public static void DisposeAll()
        {
            DisposeSQLite();
            DisposeSignalR();
        }

        public static async Task InitSQLiteDataBase()
        {
            using (StreamReader file = File.OpenText("C:\\Users\\leeye\\Downloads\\db.json"))
            {
                try
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject json = (JObject)JToken.ReadFrom(reader);

                        var a1 = JObject.Parse(json.ToString());
                        JObject? members = json["member"] as JObject;
                        JObject? lessons = json["lecture"] as JObject;

                        foreach (KeyValuePair<string, JToken> memberJToken in members)
                        {
                            var memberJObject = memberJToken.Value as JObject;

                            ModelMember member = new();
                            member.id = Convert.ToInt32(memberJObject["mid"]);
                            member.grade = memberJObject["member_grade_str"].ToString();
                            member.name = memberJObject["member_name"].ToString();
                            member.old = ConvertGradeOld(memberJObject["member_grade_str"].ToString());
                            member.classPlan = memberJObject["member_class"].ToString();
                            if (memberJObject["member_money"].ToString() is "")
                            {
                                member.money = 0;
                            }
                            else
                            {
                                member.money = Convert.ToInt32(memberJObject["member_money"]);
                            }
                            member.memberState = memberJObject["member_status"].ToString();
                            member.phoneNumber = memberJObject["member_motherphone"].ToString();
                            member.memberMemo = memberJObject["member_text"].ToString();
                            member.xp = Convert.ToInt32(memberJObject["member_xp"]);
                            member.xpLog = memberJObject["member_xp_log"].ToString();

                            await AddMemberAsync(member);
                        }

                        foreach (KeyValuePair<string, JToken> lessonKeyValue in lessons)
                        {
                            foreach (KeyValuePair<string, JToken> jToken in (JObject)lessonKeyValue.Value)
                            {
                                var lessonJObject = jToken.Value as JObject;

                                ModelLessons lesson = new();

                                lesson.id = $"{lessonJObject["mid"]}-{lessonKeyValue.Key}";
                                if (lessonJObject["o2_class_time_in"].ToString() is "")
                                {
                                    lesson.dateTimeStart = new();
                                }
                                else
                                {
                                    lesson.dateTimeStart = DateTime.Parse($"{lessonKeyValue.Key} {lessonJObject["o2_class_time_in"]}");
                                }
                                if (lessonJObject["o2_class_time_out"].ToString() is "")
                                {
                                    lesson.dateTimeEnd = new();
                                }
                                else
                                {
                                    lesson.dateTimeEnd = DateTime.Parse($"{lessonKeyValue.Key} {lessonJObject["o2_class_time_out"]}");
                                }
                                lesson.lessonTopic = lessonJObject["o2_class_lecture"].ToString();
                                lesson.assignment = lessonJObject["o2_class_homework"].ToString();
                                lesson.lessonMemo = lessonJObject["o2_class_memo"].ToString();
                                lesson.memberId = Convert.ToInt32(lessonJObject["mid"]);

                                await AddLessonAsync(lesson);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                
            }
        }
    }
}
