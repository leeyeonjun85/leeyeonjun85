using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;
using Firebase.Database.Query;
using MaterialDesignThemes.Wpf;
using Microsoft.AspNetCore.SignalR.Client;
using OoManager.Common;
using OoManager.Models;
using Utiles;

namespace OoManager.Services
{
    public class OoService : IOoService
    {
        public AppData InitApp(AppData AppData)
        {
            AppData.NavigationList = new()
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
            };

            AppData.SelectedIndex = 0;
            AppData.SelectedItem = AppData.NavigationList[0];

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

                AppData.FirebaseDB = client.Child("o2study_test");

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

        public async Task<IReadOnlyCollection<FirebaseObject<OoMembers>>> GetMembersAsync(AppData AppData)
        {
            IReadOnlyCollection<FirebaseObject<OoMembers>> Members = await AppData.FirebaseDB
                    .Child("member")
                    .OnceAsync<OoMembers>();

            return Members;
        }

        public async void RefreshMembersAsync(AppData AppData)
        {
            // Get members
            Task<IReadOnlyCollection<FirebaseObject<OoMembers>>> members1 = GetMembersAsync(AppData);
            await members1;
            IReadOnlyCollection<FirebaseObject<OoMembers>> members = members1.Result;

            foreach (var member in members)
            {
                OoMembers addMemeber = member.Object;
                addMemeber.Key = member.Key;
                AppData.Members.Add(addMemeber);
            }
        }

        //private async void InitFireBase(AppData AppData)
        //{
        //    int mid = 0;

        //    List<Tuple<string, string, int>> memeberList = new();

        //    memeberList.Add(new Tuple<string, string, int>("고1", "이연준", 10));
        //    memeberList.Add(new Tuple<string, string, int>("고2", "이채은", 10));
        //    memeberList.Add(new Tuple<string, string, int>("초1", "도연우", 735));
        //    memeberList.Add(new Tuple<string, string, int>("초1", "박재현", 950));
        //    memeberList.Add(new Tuple<string, string, int>("초1", "도연우", 735));
        //    memeberList.Add(new Tuple<string, string, int>("초1", "임수현", 185));
        //    memeberList.Add(new Tuple<string, string, int>("초1", "서동인", 45));
        //    memeberList.Add(new Tuple<string, string, int>("초2", "김윤", 80));
        //    memeberList.Add(new Tuple<string, string, int>("초2", "박서연", 885));
        //    memeberList.Add(new Tuple<string, string, int>("초2", "도화랑", 375));
        //    memeberList.Add(new Tuple<string, string, int>("초3", "유희건", 1585));
        //    memeberList.Add(new Tuple<string, string, int>("초3", "이윤지", 990));
        //    memeberList.Add(new Tuple<string, string, int>("초4", "임서진", 775));
        //    memeberList.Add(new Tuple<string, string, int>("초4", "김민", 435));
        //    memeberList.Add(new Tuple<string, string, int>("초4", "박시연", 925));
        //    memeberList.Add(new Tuple<string, string, int>("초5", "이세연", 460));
        //    memeberList.Add(new Tuple<string, string, int>("초5", "유지흔", 1210));
        //    memeberList.Add(new Tuple<string, string, int>("초5", "정윤후", 185));
        //    memeberList.Add(new Tuple<string, string, int>("초5", "서민지", 905));
        //    memeberList.Add(new Tuple<string, string, int>("초5", "서아인", 420));
        //    memeberList.Add(new Tuple<string, string, int>("중1", "김태연", 40));
        //    memeberList.Add(new Tuple<string, string, int>("중1", "윤미래", 10));
        //    memeberList.Add(new Tuple<string, string, int>("중1", "도하율", 10));
        //    memeberList.Add(new Tuple<string, string, int>("중2", "오채은", 10));
        //    memeberList.Add(new Tuple<string, string, int>("중2", "서아민", 10));
        //    memeberList.Add(new Tuple<string, string, int>("중3", "김태은", 10));
        //    memeberList.Add(new Tuple<string, string, int>("중3", "지예도", 10));
        //    memeberList.Add(new Tuple<string, string, int>("중3", "구영우", 55));
        //    memeberList.Add(new Tuple<string, string, int>("중3", "이신아", 10));
        //    memeberList.Add(new Tuple<string, string, int>("중3", "이우주", 10));
        //    memeberList.Add(new Tuple<string, string, int>("고3", "오예은", 10));

        //    foreach (var item in memeberList)
        //    {
        //        mid += 1;
        //        AppData.Member_grade_str = item.Item1;
        //        AppData.Member_name = item.Item2;
        //        AppData.Member_xp = item.Item3;


        //        if (AppData.Member_grade_str == "초1")
        //            AppData.Member_grade = 8;
        //        else if (AppData.Member_grade_str == "초2")
        //            AppData.Member_grade = 9;
        //        else if (AppData.Member_grade_str == "초3")
        //            AppData.Member_grade = 10;
        //        else if (AppData.Member_grade_str == "초4")
        //            AppData.Member_grade = 11;
        //        else if (AppData.Member_grade_str == "초5")
        //            AppData.Member_grade = 12;
        //        else if (AppData.Member_grade_str == "초6")
        //            AppData.Member_grade = 13;
        //        else if (AppData.Member_grade_str == "중1")
        //            AppData.Member_grade = 14;
        //        else if (AppData.Member_grade_str == "중2")
        //            AppData.Member_grade = 15;
        //        else if (AppData.Member_grade_str == "중3")
        //            AppData.Member_grade = 16;
        //        else if (AppData.Member_grade_str == "고1")
        //            AppData.Member_grade = 17;
        //        else if (AppData.Member_grade_str == "고2")
        //            AppData.Member_grade = 18;
        //        else if (AppData.Member_grade_str == "고3")
        //            AppData.Member_grade = 19;

        //        OoMembers newMember = new()
        //        {
        //            mid = mid,
        //            member_grade = AppData.Member_grade,
        //            member_grade_str = AppData.Member_grade_str,
        //            member_name = AppData.Member_name,
        //            member_xp = AppData.Member_xp,
        //        };

        //        await AppData.FirebaseDB
        //                .Child("member")
        //                .PostAsync(newMember);

        //        AppData.Member_grade_str = "";
        //        AppData.Member_name = "";
        //        AppData.Member_xp = 10;
        //    }
        //}


        public async Task<AppData> InitMembers(AppData AppData)
        {
            int mid = 0;

            List<Tuple<string, string, int>> memeberList = new()
            {
                new Tuple<string, string, int>("고1", "이연준", 10),
                new Tuple<string, string, int>("고2", "이채은", 10)
            };

            foreach (var item in memeberList)
            {
                mid += 1;
                AppData.Member_grade_str = item.Item1;
                AppData.Member_name = item.Item2;
                AppData.Member_xp = item.Item3;

                AppData = ConvertGradeOlde(AppData);



                OoMembers newMember = new()
                {
                    mid = mid,
                    member_grade = AppData.Member_grade,
                    member_grade_str = AppData.Member_grade_str!,
                    member_name = AppData.Member_name!,
                    member_xp = AppData.Member_xp,
                };

                await AppData.FirebaseDB
                        .Child("member")
                        .PostAsync(newMember);

                AppData.Member_grade_str = "";
                AppData.Member_name = "";
                AppData.Member_xp = 10;
            }
            return AppData;
        }

        public AppData ConvertGradeOlde(AppData AppData)
        {
            if (AppData.Member_grade_str == "6살")
                AppData.Member_grade = 6;
            else if (AppData.Member_grade_str == "7살")
                AppData.Member_grade = 7;
            else if (AppData.Member_grade_str == "초1")
                AppData.Member_grade = 8;
            else if (AppData.Member_grade_str == "초2")
                AppData.Member_grade = 9;
            else if (AppData.Member_grade_str == "초3")
                AppData.Member_grade = 10;
            else if (AppData.Member_grade_str == "초4")
                AppData.Member_grade = 11;
            else if (AppData.Member_grade_str == "초5")
                AppData.Member_grade = 12;
            else if (AppData.Member_grade_str == "초6")
                AppData.Member_grade = 13;
            else if (AppData.Member_grade_str == "중1")
                AppData.Member_grade = 14;
            else if (AppData.Member_grade_str == "중2")
                AppData.Member_grade = 15;
            else if (AppData.Member_grade_str == "중3")
                AppData.Member_grade = 16;
            else if (AppData.Member_grade_str == "고1")
                AppData.Member_grade = 17;
            else if (AppData.Member_grade_str == "고2")
                AppData.Member_grade = 18;
            else if (AppData.Member_grade_str == "고3")
                AppData.Member_grade = 19;

            return AppData;
        }
    }
}
