using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Firebase.Database.Query;
using Firebase.Database;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using OoManager.Common;
using OoManager.Models;
using Utiles;
using System.Net;
using System;

namespace OoManager.ViewModels
{
    public partial class PageMembersViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppModel>>
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppModel _appModel = new();
        #endregion

        int mid = 10;


        public PageMembersViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private async void UpdateMember(object obj)
        {
            var aaa = AppModel.SelectedMember;

            await AppModel.FirebaseClient
                        .Child("o2study")
                        .Child("member")
                        .Child(AppModel.SelectedMember.Key)
                        .PutAsync(AppModel.SelectedMember);
        }

        [RelayCommand]
        private async void Test11(object obj)
        {
            List<Tuple<string, string, int>> memeberList = new();

            memeberList.Add(new Tuple<string, string, int>("초1", "박재현", 950));
            memeberList.Add(new Tuple<string, string, int>("초1", "도연우", 735));
            memeberList.Add(new Tuple<string, string, int>("초1", "임수현", 185));
            memeberList.Add(new Tuple<string, string, int>("초1", "서동인", 45));
            memeberList.Add(new Tuple<string, string, int>("초2", "김윤", 80));
            memeberList.Add(new Tuple<string, string, int>("초2", "박서연", 885));
            memeberList.Add(new Tuple<string, string, int>("초2", "도화랑", 375));
            memeberList.Add(new Tuple<string, string, int>("초3", "유희건", 1585));
            memeberList.Add(new Tuple<string, string, int>("초3", "이윤지", 990));
            memeberList.Add(new Tuple<string, string, int>("초4", "임서진", 775));
            memeberList.Add(new Tuple<string, string, int>("초4", "김민", 435));
            memeberList.Add(new Tuple<string, string, int>("초4", "박시연", 925));
            memeberList.Add(new Tuple<string, string, int>("초5", "이세연", 460));
            memeberList.Add(new Tuple<string, string, int>("초5", "유지흔", 1210));
            memeberList.Add(new Tuple<string, string, int>("초5", "정윤후", 185));
            memeberList.Add(new Tuple<string, string, int>("초5", "서민지", 905));
            memeberList.Add(new Tuple<string, string, int>("초5", "서아인", 420));
            memeberList.Add(new Tuple<string, string, int>("중1", "김태연", 40));
            memeberList.Add(new Tuple<string, string, int>("중1", "윤미래", 10));
            memeberList.Add(new Tuple<string, string, int>("중1", "도하율", 10));
            memeberList.Add(new Tuple<string, string, int>("중2", "오채은", 10));
            memeberList.Add(new Tuple<string, string, int>("중2", "서아민", 10));
            memeberList.Add(new Tuple<string, string, int>("중3", "김태은", 10));
            memeberList.Add(new Tuple<string, string, int>("중3", "지예도", 10));
            memeberList.Add(new Tuple<string, string, int>("중3", "구영우", 55));
            memeberList.Add(new Tuple<string, string, int>("중3", "이신아", 10));
            memeberList.Add(new Tuple<string, string, int>("중3", "이우주", 10));
            memeberList.Add(new Tuple<string, string, int>("고3", "오예은", 10));
            
            foreach(var item in memeberList)
            {
                AppModel.Member_grade_str = item.Item1;
                AppModel.Member_name = item.Item2;
                AppModel.Member_xp = item.Item3;
                mid += 1;

                if (AppModel.Member_grade_str == "초1")
                    AppModel.Member_grade = 8;
                else if (AppModel.Member_grade_str == "초2")
                    AppModel.Member_grade = 9;
                else if (AppModel.Member_grade_str == "초3")
                    AppModel.Member_grade = 10;
                else if (AppModel.Member_grade_str == "초4")
                    AppModel.Member_grade = 11;
                else if (AppModel.Member_grade_str == "초5")
                    AppModel.Member_grade = 12;
                else if (AppModel.Member_grade_str == "초6")
                    AppModel.Member_grade = 13;
                else if (AppModel.Member_grade_str == "중1")
                    AppModel.Member_grade = 14;
                else if (AppModel.Member_grade_str == "중2")
                    AppModel.Member_grade = 15;
                else if (AppModel.Member_grade_str == "중3")
                    AppModel.Member_grade = 16;
                else if (AppModel.Member_grade_str == "고1")
                    AppModel.Member_grade = 17;
                else if (AppModel.Member_grade_str == "고2")
                    AppModel.Member_grade = 18;
                else if (AppModel.Member_grade_str == "고3")
                    AppModel.Member_grade = 19;

                OoMembers newMember = new()
                {
                    member_grade = AppModel.Member_grade,
                    member_grade_str = AppModel.Member_grade_str,
                    member_name = AppModel.Member_name,
                    member_xp = AppModel.Member_xp,
                    mid = mid,
                };

                await AppModel.FirebaseClient
                        .Child("o2study")
                        .Child("member")
                        .PostAsync(newMember);

                AppModel.Member_grade_str = "";
                AppModel.Member_name = "";
                AppModel.Member_xp = 10;
            }
        }

        [RelayCommand]
        private async void DeleteMember(object obj)
        {
            var aaa = AppModel.SelectedMember;

            await AppModel.FirebaseClient
                    .Child("o2study")
                    .Child("member")
                    .Child(AppModel.SelectedMember.Key)
                    .DeleteAsync();
        }

        [RelayCommand]
        private async void AddMember(object obj)
        {
            if (string.IsNullOrEmpty(AppModel.Member_name))
                return;
            else
            {
                mid += 1;

                if (AppModel.Member_grade_str == "초1")
                    AppModel.Member_grade = 8;
                else if (AppModel.Member_grade_str == "초2")
                    AppModel.Member_grade = 9;
                else if (AppModel.Member_grade_str == "초3")
                    AppModel.Member_grade = 10;
                else if (AppModel.Member_grade_str == "초4")
                    AppModel.Member_grade = 11;
                else if (AppModel.Member_grade_str == "초5")
                    AppModel.Member_grade = 12;
                else if (AppModel.Member_grade_str == "초6")
                    AppModel.Member_grade = 13;
                else if (AppModel.Member_grade_str == "중1")
                    AppModel.Member_grade = 14;
                else if (AppModel.Member_grade_str == "중2")
                    AppModel.Member_grade = 15;
                else if (AppModel.Member_grade_str == "중3")
                    AppModel.Member_grade = 16;
                else if (AppModel.Member_grade_str == "고1")
                    AppModel.Member_grade = 17;
                else if (AppModel.Member_grade_str == "고2")
                    AppModel.Member_grade = 18;
                else if (AppModel.Member_grade_str == "고3")
                    AppModel.Member_grade = 19;

                OoMembers newMember = new()
                {
                    member_grade = AppModel.Member_grade,
                    member_grade_str = AppModel.Member_grade_str,
                    member_name = AppModel.Member_name,
                    member_xp = AppModel.Member_xp,
                    mid = mid,
                };

                await AppModel.FirebaseClient
                        .Child("o2study")
                        .Child("member")
                        .PostAsync(newMember);

                AppModel.Member_grade_str = "";
                AppModel.Member_name = "";
                AppModel.Member_xp = 10;
            }
        }



        
        






        private async void GetFireBase(AppModel AppModel)
        {
            Task<IReadOnlyCollection<FirebaseObject<OoMembers>>> members1 = GetMembersAsync(AppModel);
            await members1;
            IReadOnlyCollection<FirebaseObject<OoMembers>> members = members1.Result;

            foreach (var member in members)
            {
                OoMembers addMemeber = member.Object;
                addMemeber.Key = member.Key;
                AppModel.Members.Add(addMemeber);
            } 
        }

        public async Task<IReadOnlyCollection<FirebaseObject<OoMembers>>> GetMembersAsync(AppModel AppModel)
        {
            string FirebaseDatabaseUrl = "https://leeyeonjundb-default-rtdb.asia-southeast1.firebasedatabase.app";
            JsonModel jsonModel = MyUtiles.GetJsonModel();
            AppModel.FirebaseClient = new FirebaseClient(FirebaseDatabaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(jsonModel.OoManager.FireBaseAuth) });

            IReadOnlyCollection<FirebaseObject<OoMembers>> Members = await AppModel.FirebaseClient
                    .Child("o2study")
                    .Child("member")
                    .OnceAsync<OoMembers>();

            return Members;
        }


        public void Receive(ValueChangedMessage<AppModel> message)
        {
            AppModel = message.Value;
            AppModel.Members = new();
            GetFireBase(AppModel);
        }
    }
}
