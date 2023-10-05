using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OoManager.Models;

namespace OoManager.ViewModels
{
    public partial class WindowLecturesUpdateViewModel : ViewModelBase, IParameterReceiver
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = new();
        [ObservableProperty]
        private MemberData _selectedMember = new();
        [ObservableProperty]
        private string _windowTitle = string.Empty;

        #endregion


        public WindowLecturesUpdateViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private async Task BtnOkAsync(object obj)
        {
            await Task.Delay(1000);
            
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

                if (AppData.SelectedMember is not null)
                {
                    SelectedMember = AppData.SelectedMember;
                    WindowTitle = $"{SelectedMember.Member.member_name} 수업 관리";
                }

                
            }
        }
    }
}
