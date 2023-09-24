﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Firebase.Database;
using Firebase.Database.Query;
using OoManager.Models;
using OoManager.Views;
using Utiles;

namespace OoManager.ViewModels
{
    public partial class PageMembersViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = new();
        #endregion

        public PageMembersViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private async Task RefreshAsync(object obj)
        {
            AppData.OoService.RefreshMembersAsync(AppData);

            //IReadOnlyCollection<FirebaseObject<object>> Lectures1 = await AppData.FirebaseDB
            //        .Child("lecture")
            //        .OnceAsync<object>();

            //IReadOnlyCollection<FirebaseObject<string>> Lectures2 = await AppData.FirebaseDB
            //        .Child("lecture")
            //        .OnceAsync<string>();

            //IReadOnlyCollection<FirebaseObject<OoLectures>> Lectures3 = await AppData.FirebaseDB
            //        .Child("lecture")
            //        .OnceAsync<OoLectures>();

            //OoLectures lecture1 = new()
            //{
            //    mid = 1,
            //    o2_class_date = DateTime.Now.ToString("yyyy-MM-dd"),
            //    o2_class_homework = "테스트 숙제",
            //    o2_class_lecture = "테스트 수업",
            //    o2_class_memo = "테스트 메모",
            //    o2_class_time_in = DateTime.Now.AddMinutes(-30).ToString("hh:mm"),
            //    o2_class_time_out = DateTime.Now.ToString("hh:mm"),
            //};

            //await AppData.FirebaseDB
            //        .Child("lecture")
            //        .Child(DateTime.Now.ToString("yyyy-MM-dd"))
            //        .PostAsync(lecture1);
        }



        //[RelayCommand]
        //private async void Refresh(object obj)
        //{
        //    AppData.Members = new();
        //    GetFireBase(AppData);
        //}


        [RelayCommand]
        private async void UpdateMember(object obj)
        {
            await AppData.FirebaseDB
                        .Child("member")
                        .Child(AppData.SelectedMember.Key)
                        .PutAsync(AppData.SelectedMember);
        }

        [RelayCommand]
        private async Task InitMembersAsync(object obj)
        {
            Task<AppData> _appData = AppData.OoService!.InitMembers(AppData);
            await _appData;
            AppData = _appData.Result;
        }

        [RelayCommand]
        private async void DeleteMember(object obj)
        {
            var aaa = AppData.SelectedMember;

            await AppData.FirebaseDB
                    .Child("member")
                    .Child(AppData.SelectedMember.Key)
                    .DeleteAsync();
        }

        [RelayCommand]
        private void AddMember(object obj)
        {
            //if (string.IsNullOrEmpty(AppData.Member_name))
            //    return;

            ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(WindowMemberAddViewModel))!;
            Window view = (Window)Ioc.Default.GetService(typeof(WindowMemberAdd))!;
            viewModel.SetWindow(view);
            view.DataContext = viewModel;
            view.Show();
        }


        public void Receive(ValueChangedMessage<AppData> message)
        {
            AppData = message.Value;
            AppData.Members = new();

            AppData = AppData.OoService!.GetFireBase(AppData);
            AppData.OoService!.RefreshMembersAsync(AppData);
        }
    }
}
