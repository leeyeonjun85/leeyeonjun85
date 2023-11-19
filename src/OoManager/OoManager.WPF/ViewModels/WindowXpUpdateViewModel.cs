using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OoManager.WPF.Models;

namespace OoManager.WPF.ViewModels
{
    public partial class WindowXpUpdateViewModel : ViewModelBase, IParameterReceiver
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = new();

        [ObservableProperty]
        private string _gradeString = "중1";
        [ObservableProperty]
        private string _name = string.Empty;
        [ObservableProperty]
        private int _xp = 10;
        [ObservableProperty]
        private string _memo = $"회원등록 : {DateTime.Now:yyyy-MM-dd}";
        [ObservableProperty]
        private string _xpMemo = string.Empty;
        #endregion


        public WindowXpUpdateViewModel()
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
                    //AppData.MemberData.Member.member_grade_str = GradeString;
                    //AppData.MemberData.Member.member_grade = AppData.OoService!.ConvertGradeOld(AppData.MemberData.Member.member_grade_str);
                    //AppData.MemberData.Member.member_name = Name;
                    //AppData.MemberData.Member.member_text = Memo;
                    //AppData.MemberData.Member.member_xp = Xp;
                    //AppData.MemberData.Member.member_xp_log = XpMemo;

                    //await AppData.OoService!.UpdateMemberAsync(AppData);

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

        void IParameterReceiver.ReceiveParameter(object parameter)
        {
            if (parameter is AppData _appData)
            {
                AppData = _appData;
                AppData.MemberData = AppData.SelectedMember!;

                //GradeString = AppData.MemberData.Member.member_grade_str;
                //Name = AppData.MemberData.Member.member_name;
                //Xp = AppData.MemberData.Member.member_xp;
                //Memo = AppData.MemberData.Member.member_text;
                //XpMemo = AppData.MemberData.Member.member_xp_log;
            }
        }
    }
}
