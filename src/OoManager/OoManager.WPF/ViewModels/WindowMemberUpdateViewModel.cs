using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using OoManager.WPF.Models;
using OoManager.WPF.Services;
using Microsoft.EntityFrameworkCore;
using OoManager.Common.Models;

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
            UpdateMember = AppData.SelectedMember!;
        }

        [RelayCommand]
        private async Task BtnOkAsync(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(UpdateMember.name))
                {
                    var _foundata = AppData.OoDbContext!.members.FindAsync(UpdateMember.id)!;
                    await _foundata; UpdateMember = _foundata.Result!;
                    AppData.OoDbContext!.Entry(UpdateMember).State = EntityState.Detached;
                    AppData.OoDbContext!.members.Entry(UpdateMember).State = EntityState.Modified;
                    AppData.OoDbContext!.SaveChanges();

                    await Utiles.RefreshOoDbAsync(AppData);

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

        }
    }
}
