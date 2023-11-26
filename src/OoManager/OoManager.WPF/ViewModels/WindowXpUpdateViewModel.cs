using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OoManager.Common.Models;
using OoManager.WPF.Models;
using OoManager.WPF.Services;

namespace OoManager.WPF.ViewModels
{
    public partial class WindowXpUpdateViewModel : ViewModelBase, IParameterReceiver
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = App.Data;

        int oldXp = 0;
        #endregion


        public WindowXpUpdateViewModel()
        {
            IsActive = true;
            oldXp = AppData.MemberData.xp;
        }

        [RelayCommand]
        private async Task BtnOkAsync(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(AppData.MemberData.name))
                {
                    if (oldXp != AppData.MemberData.xp)
                    {
                        AppData.MemberData.xpLog += $"{Environment.NewLine}{DateTime.Now:yyyy-MM-dd HH:mm:ss} {oldXp} → {AppData.MemberData.xp}";
                    }

                    await Utiles.UpdateMemberAsync(AppData);
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
