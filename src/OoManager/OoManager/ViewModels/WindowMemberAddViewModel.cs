using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OoManager.Models;
using OoManager.Services;

namespace OoManager.ViewModels
{
    public partial class WindowMemberAddViewModel : ViewModelBase, IParameterReceiver
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


        public WindowMemberAddViewModel()
        {
            IsActive = true;
        }





        [RelayCommand]
        private async Task BtnOkAsync(object obj)
        {
            if (!string.IsNullOrEmpty(this.Name))
            {

                AppData.MemberData.classPlan = ClassPlan;
                AppData.MemberData.grade = GradeString;
                AppData.MemberData.old = Utiles.ConvertGradeOld(GradeString);
                AppData.MemberData.money = Convert.ToInt32(Money);
                AppData.MemberData.phoneNumber = Phonenumber;
                AppData.MemberData.name = Name;
                AppData.MemberData.memberState = Status;
                AppData.MemberData.memberMemo = Memo;
                AppData.MemberData.xp = Xp;
                AppData.MemberData.xpLog = $"회원등록 {Xp}xp";

                await AppData.OoDbContext!.members.AddAsync(AppData.MemberData);
                AppData.OoDbContext!.SaveChanges();

                Window?.Close();
            }
            else MessageBox.Show("회원등록하려면 이름은 꼭 써야해!!");
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

        void IParameterReceiver.ReceiveParameter(object parameter)
        {
            if (parameter is AppData _appData)
            {
                AppData = _appData;
                AppData.MemberData = new();
            }
        }
    }
}
