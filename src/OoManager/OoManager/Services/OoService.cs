using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using MaterialDesignThemes.Wpf;
using OoManager.Models;
using Utiles;

namespace OoManager.Services
{
    public class OoService : IOoService
    {
        public AppData InitApp(AppData AppData)
        {
            //AppData.NavigationList = new()
            //{
            //    new NavigationItem
            //    {
            //        Title = "Home",
            //        SelectedIcon = PackIconKind.Home,
            //        UnselectedIcon = PackIconKind.HomeOutline,
            //        Source = "/Views/PageHome.xaml",
            //    },
            //    new NavigationItem
            //    {
            //        Title = "Members",
            //        SelectedIcon = PackIconKind.Users,
            //        UnselectedIcon = PackIconKind.UsersOutline,
            //        Source = "/Views/PageMembers.xaml",
            //    },
            //    new NavigationItem
            //    {
            //        Title = "Lectures",
            //        SelectedIcon = PackIconKind.CalendarMultipleCheck,
            //        UnselectedIcon = PackIconKind.CalendarCheck,
            //        Source = "/Views/PageLecture.xaml",
            //    },
            //};

            //AppData.SelectedIndex = 0;
            //AppData.SelectedItem = AppData.NavigationList[0];

            // Init GireBase
            AppData = GetFireBase(AppData);

            // Init Members
            AppData.Members = new();
            AppData.MemberData = new();
            AppData.OoService!.RefreshMembersAsync(AppData);
            AppData.TotalMembers = AppData.Members.Count;
            AppData.TotalMembersString = $"총 {AppData.TotalMembers}명";

            return AppData;
        }


        public AppData GetFireBase(AppData AppData)
        {
            try
            {
                string FirebaseDatabaseUrl = "https://leeyeonjundb-default-rtdb.asia-southeast1.firebasedatabase.app";
                JsonModel jsonModel = MyUtiles.GetJsonModel();

                FirebaseClient client = new(FirebaseDatabaseUrl,
                                            new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(jsonModel.OoManager.FireBaseAuth) });

                AppData.FirebaseDB = client.Child(AppData.FireBaseDbName);

                return AppData;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
        }

        public async Task<IReadOnlyCollection<FirebaseObject<Member>>> GetMembersAsync(AppData AppData)
        {
            IReadOnlyCollection<FirebaseObject<Member>> Members = await AppData.FirebaseDB
                    .Child("member")
                    .OnceAsync<Member>();

            return Members;
        }

        public async void RefreshMembersAsync(AppData AppData)
        {
            AppData.Members.Clear();

            // Get members
            Task<IReadOnlyCollection<FirebaseObject<Member>>> members1 = GetMembersAsync(AppData);
            await members1;
            IReadOnlyCollection<FirebaseObject<Member>> members = members1.Result;

            foreach (var member in members)
            {
                AppData.MemberData = new();
                AppData.MemberData.Key = member.Key;
                AppData.MemberData!.Member = member.Object;
                AppData.MemberData.SelectedGrade = member.Object.member_grade_str;
                AppData.MemberData.SelectedState = member.Object.member_status;
                AppData.Members.Add(AppData.MemberData);
            }
        }

        public async Task<AppData> InitMembers(AppData AppData)
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
                mid += 1;
                AppData.MemberData.Member = new();
                AppData.MemberData.Member.member_grade_str = _memeber.Item1;
                AppData.MemberData.Member.member_name = _memeber.Item2;
                AppData.MemberData.Member.member_xp = _memeber.Item3;
                AppData.MemberData.Member.mid = mid;

                AppData = ConvertGradeOld(AppData);

                //await AppData.FirebaseDB
                //        .Child("member")
                //        .PostAsync(AppData.MemberData.Member);

                Task<FirebaseObject<Member>> returnMember = AppData.FirebaseDB
                        .Child("member")
                        .PostAsync(AppData.MemberData.Member);
                await returnMember;
                AppData.MemberData.Key = returnMember.Result.Key;
                AppData.Members.Add(AppData.MemberData);
                AppData.MemberData = new();
            }
            return AppData;
        }

        public AppData ConvertGradeOld(AppData AppData)
        {
            if (AppData.MemberData.Member.member_grade_str == "6살")
                AppData.MemberData.Member.member_grade = 6;
            else if (AppData.MemberData.Member.member_grade_str == "7살")
                AppData.MemberData.Member.member_grade = 7;
            else if (AppData.MemberData.Member.member_grade_str == "초1")
                AppData.MemberData.Member.member_grade = 8;
            else if (AppData.MemberData.Member.member_grade_str == "초2")
                AppData.MemberData.Member.member_grade = 9;
            else if (AppData.MemberData.Member.member_grade_str == "초3")
                AppData.MemberData.Member.member_grade = 10;
            else if (AppData.MemberData.Member.member_grade_str == "초4")
                AppData.MemberData.Member.member_grade = 11;
            else if (AppData.MemberData.Member.member_grade_str == "초5")
                AppData.MemberData.Member.member_grade = 12;
            else if (AppData.MemberData.Member.member_grade_str == "초6")
                AppData.MemberData.Member.member_grade = 13;
            else if (AppData.MemberData.Member.member_grade_str == "중1")
                AppData.MemberData.Member.member_grade = 14;
            else if (AppData.MemberData.Member.member_grade_str == "중2")
                AppData.MemberData.Member.member_grade = 15;
            else if (AppData.MemberData.Member.member_grade_str == "중3")
                AppData.MemberData.Member.member_grade = 16;
            else if (AppData.MemberData.Member.member_grade_str == "고1")
                AppData.MemberData.Member.member_grade = 17;
            else if (AppData.MemberData.Member.member_grade_str == "고2")
                AppData.MemberData.Member.member_grade = 18;
            else if (AppData.MemberData.Member.member_grade_str == "고3")
                AppData.MemberData.Member.member_grade = 19;

            return AppData;
        }

        public async Task<AppData> AddMemberAsync(AppData AppData)
        {
            Task<FirebaseObject<Member>> returnMember = AppData.FirebaseDB
                       .Child("member")
                       .PostAsync(AppData.MemberData.Member);
            await returnMember;
            AppData.MemberData.Key = returnMember.Result.Key;
            AppData.Members.Add(AppData.MemberData);
            AppData.MemberData = new();

            return AppData;
        }

        public async Task UpdateMemberAsync(AppData AppData)
        {
            await AppData.FirebaseDB
                .Child("member")
                .Child(AppData.MemberData.Key)
                .PutAsync(AppData.MemberData.Member);
        }
    }
}
