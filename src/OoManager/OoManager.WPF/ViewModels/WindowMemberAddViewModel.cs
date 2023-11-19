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
    public partial class WindowMemberAddViewModel : ViewModelBase, IParameterReceiver
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = App.Data;

        [ObservableProperty]
        private ModelMember _addMember = new();
        #endregion


        public WindowMemberAddViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private async Task BtnOkAsync(object obj)
        {
            if (!string.IsNullOrEmpty(AddMember.name))
            {
                await AppData.OoDbContext!.members.AddAsync(AddMember);
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
