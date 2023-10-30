using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using OoManager.Models;
using OoManager.Services;
using Microsoft.EntityFrameworkCore;

namespace OoManager.ViewModels
{
    public partial class WindowMemberUpdateViewModel : ViewModelBase, IParameterReceiver
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = App.Data;

        [ObservableProperty]
        private string _gradeString = "중1";
        [ObservableProperty]
        private string _name = string.Empty;
        [ObservableProperty]
        private int _money = 150000;
        [ObservableProperty]
        private string _status = "재원";
        [ObservableProperty]
        private string _phonenumber = "010-0000-0000";
        [ObservableProperty]
        private string _classPlan = "월화수목금";
        [ObservableProperty]
        private int _xp = 10;
        [ObservableProperty]
        private string _memo = $"회원등록 : {DateTime.Now:yyyy-MM-dd}";
        [ObservableProperty]
        private string _xpMemo = string.Empty;
        #endregion

        public WindowMemberUpdateViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private async Task BtnOkAsync(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Name))
                {
                    var _foundata = AppData.OoDbContext!.members.FindAsync(AppData.SelectedMember.id)!;
                    await _foundata; AppData.MemberData = _foundata.Result!;
                    AppData.OoDbContext!.Entry(AppData.MemberData).State = EntityState.Detached;

                    AppData.MemberData.classPlan = ClassPlan;
                    AppData.MemberData.grade = GradeString;
                    AppData.MemberData.old = Utiles.ConvertGradeOld(GradeString);
                    AppData.MemberData.money = Convert.ToInt32(Money);
                    AppData.MemberData.phoneNumber = Phonenumber;
                    AppData.MemberData.name = Name;
                    AppData.MemberData.memberState = Status;
                    AppData.MemberData.memberMemo = Memo;
                    AppData.MemberData.xp = Xp;
                    AppData.MemberData.xpLog = XpMemo;

                    AppData.OoDbContext!.members.Entry(AppData.MemberData).State = EntityState.Modified;

                    AppData.OoDbContext!.SaveChanges();

                    await Utiles.RefreshOoDbAsync(AppData);
                    WeakReferenceMessenger.Default.Send(new ValueChangedMessage<AppData>(AppData));

                    Window?.Close();
                }
                else MessageBox.Show("이름은 꼭 있어야해!!");
            }
            catch (Exception)
            {

                throw;
            }

        }

        [RelayCommand]
        private void BtnCancel(object obj)
        {
            Window?.Close();
        }

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
        }

        async void IParameterReceiver.ReceiveParameter(object parameter)
        {
            if (parameter is AppData _appData)
            {
                AppData = _appData;
                AppData.MemberData = new();

                ClassPlan = AppData.SelectedMember!.classPlan;
                GradeString = AppData.SelectedMember.grade;
                Money = AppData.SelectedMember.money;
                Phonenumber = AppData.SelectedMember.phoneNumber;
                Name = AppData.SelectedMember.name;
                Status = AppData.SelectedMember.memberState;
                Memo = AppData.SelectedMember.memberMemo;
                Xp = AppData.SelectedMember.xp;
                XpMemo = AppData.SelectedMember.xpLog;
            }
        }
    }
}
