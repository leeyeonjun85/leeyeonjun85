using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;
using Firebase.Database.Query;
using OoManager.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace OoManager.ViewModels
{
    public partial class WindowMemberAddViewModel : ViewModelBase, IParameterReceiver
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = new();

        [ObservableProperty]
        private string _gradeString = "중1";
        [ObservableProperty]
        private string _name = string.Empty;
        [ObservableProperty]
        private string _money = "150000";
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
                AppData.MemberData.Member.member_class = ClassPlan;
                AppData.MemberData.Member.member_grade_str = GradeString;
                AppData.MemberData.Member.member_grade = AppData.OoService!.ConvertGradeOld(AppData.MemberData.Member.member_grade_str);
                AppData.MemberData.Member.member_money = Money;
                AppData.MemberData.Member.member_motherphone = Phonenumber;
                AppData.MemberData.Member.member_name = Name;
                AppData.MemberData.Member.member_status = Status;
                AppData.MemberData.Member.member_text = Memo;
                AppData.MemberData.Member.member_xp = Xp;
                AppData.MemberData.Member.member_xp_log = $"회원등록 {Xp}xp";
                AppData.MemberData.Member.mid = AppData.Members[^1].Member.mid + 1;

                Task<AppData> _appData = AppData.OoService!.AddMemberAsync(AppData);
                await _appData;
                AppData = _appData.Result;

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
