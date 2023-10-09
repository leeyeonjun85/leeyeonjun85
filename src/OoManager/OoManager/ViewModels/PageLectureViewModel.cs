using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json.Linq;
using OoManager.Models;
using OoManager.Views;

namespace OoManager.ViewModels
{
    public partial class PageLectureViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = new();
        #endregion

        public PageLectureViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private void MemberUpdate(object obj)
        {
            if (AppData.SelectedMember is not null)
            {
                ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(WindowMemberUpdateViewModel))!;
                Window view = (Window)Ioc.Default.GetService(typeof(WindowMemberUpdate))!;
                viewModel.SetWindow(view);
                if (viewModel is IParameterReceiver parameterReceiver)
                {
                    parameterReceiver.ReceiveParameter(AppData);
                }
                view.DataContext = viewModel;
                view.Show();
            };
        }

        [RelayCommand]
        private void LectureUpdate(object obj)
        {
            if (AppData.SelectedMember is not null)
            {
                ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(WindowLecturesUpdateViewModel))!;
                Window view = (Window)Ioc.Default.GetService(typeof(WindowLecturesUpdate))!;
                viewModel.SetWindow(view);
                if (viewModel is IParameterReceiver parameterReceiver)
                {
                    parameterReceiver.ReceiveParameter(AppData);
                }
                view.DataContext = viewModel;
                view.Show();
            };
        }

        [RelayCommand]
        private void XpUpdate(object obj)
        {
            if (AppData.SelectedMember is not null)
            {
                ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(WindowXpUpdateViewModel))!;
                Window view = (Window)Ioc.Default.GetService(typeof(WindowXpUpdate))!;
                viewModel.SetWindow(view);
                if (viewModel is IParameterReceiver parameterReceiver)
                {
                    parameterReceiver.ReceiveParameter(AppData);
                }
                view.DataContext = viewModel;
                view.Show();
            };
        }

        [RelayCommand]
        private async Task BonusAsync(object obj)
        {
            if (AppData.SelectedMember is not null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(
                    $"정말로 '{AppData.SelectedMember.Member.member_name}' 에게 보너스 5xp?{Environment.NewLine}{AppData.SelectedMember.Member.member_xp} + 5 = {AppData.SelectedMember.Member.member_xp+5}",
                    $"{AppData.SelectedMember.Member.member_name}' 보너스",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    AppData.MemberData = AppData.SelectedMember;
                    AppData.MemberData.Member.member_xp_log += $"{Environment.NewLine}{DateTime.Now:yyyy-MM-dd HH:mm:ss} / 보너스 +5 / {AppData.MemberData.Member.member_xp}+5={AppData.MemberData.Member.member_xp+5}";
                    AppData.MemberData.Member.member_xp += 5;

                    await AppData.OoService!.UpdateMemberAsync(AppData);
                }
            };
        }


        [RelayCommand]
        private async Task BtnTest1(object obj)
        {



            //IReadOnlyCollection<FirebaseObject<object>> _lecturesDates = await AppData.FirebaseDB
            //        .Child("lecture")
            //        .OnceAsync<object>();

            //foreach (FirebaseObject<object> _lecturesDate in _lecturesDates)
            //{
            //    if (_lecturesDate.Object is IEnumerable<JObject> _lecturesDateJToken)
            //    {
            //        foreach (JToken _lectureJToken in _lecturesDateJToken)
            //        {
            //            Lecture _lecture = new()
            //            {
            //                mid = 3,
            //                o2_class_date = DateTime.Now.ToString("yyyy-MM-dd"),
            //                o2_class_homework = "테스트 숙제",
            //                o2_class_lecture = "테스트 수업",
            //                o2_class_memo = "테스트 메모",
            //                o2_class_time_in = DateTime.Now.AddMinutes(-30).ToString("HH:mm"),
            //                o2_class_time_out = DateTime.Now.ToString("HH:mm"),
            //            };

            //            AppData.LectureData = new()
            //            {
            //                DateString = ((Lecture)_lecture).o2_class_date,
            //                Key = _lecturesDate.Key,
            //                Lecture = _lecture,
            //            };
            //            AppData.LecturesTotal.Add(AppData.LectureData);
            //        }
            //    }
            //}

            //AppData.Lectures.Add



            //for (int i = 1; i <= 10; i++)
            //{
            //    Lecture _lecture = new()
            //    {
            //        mid = 3,
            //        o2_class_date = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd"),
            //        o2_class_homework = "테스트 숙제",
            //        o2_class_lecture = "테스트 수업",
            //        o2_class_memo = "테스트 메모",
            //        o2_class_time_in = DateTime.Now.AddMinutes(-30).ToString("HH:mm"),
            //        o2_class_time_out = DateTime.Now.ToString("HH:mm"),
            //    };

            //    AppData.LectureData = new()
            //    {
            //        DateString = _lecture.o2_class_date,
            //        Key = string.Empty,
            //        Lecture = _lecture,
            //    };

            //    Task<FirebaseObject<Lecture>> resultPost = AppData.FirebaseDB
            //            .Child("lecture")
            //            .Child(AppData.LectureData.DateString)
            //            .PostAsync(AppData.LectureData.Lecture);
            //    await resultPost;
            //    AppData.LectureData.Key = resultPost.Result.Key;
            //}




        }


        public void Receive(ValueChangedMessage<AppData> message)
        {
            AppData = message.Value;
        }
    }
}
