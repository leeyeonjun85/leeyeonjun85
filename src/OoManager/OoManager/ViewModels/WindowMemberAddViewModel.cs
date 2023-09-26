using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        private string _memo = $"회원등록 : {DateTime.Now:yyyy-MM-dd}";
        #endregion


        public WindowMemberAddViewModel()
        {
            IsActive = true;
        }


        [RelayCommand]
        private void BtnOk(object obj)
        {
            Console.WriteLine(AppData.ToString());

            Window?.Close();
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
