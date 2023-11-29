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
    public partial class WindowMemberUpdateViewModel : ViewModelBase, IParameterReceiver
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = App.Data;

        [ObservableProperty]
        private ModelMember _updateMember = new();
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
                AppData.MemberData = UpdateMember;

                if (!string.IsNullOrEmpty(AppData.MemberData.name))
                {
                    await Utiles.UpdateMemberAsync(AppData);
                    await Utiles.RefreshOoDbAsync();
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
            if (parameter is ModelMember model)
            {
                UpdateMember = model;
            }
        }
    }
}
