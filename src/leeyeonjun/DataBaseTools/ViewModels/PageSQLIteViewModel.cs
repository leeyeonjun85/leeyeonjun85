using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DataBaseTools.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseTools.ViewModels
{
    public partial class PageSQLIteViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        [ObservableProperty]
        private AppData _appData = App.Data;


        public PageSQLIteViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private void MouseLeftButtonUp(object? obj)
        {
            if (obj is not MouseButtonEventArgs args) return;
            if (args!.OriginalSource is not TextBlock textBlock) return;
            if (textBlock.DataContext is not SQLiteModel selectedItem) return;

            AppData.String1 = $"{selectedItem.Name} / {selectedItem.Old}";
            AppData.SQLiteUpdateName = selectedItem.Name!;
            AppData.SQLiteUpdateOld = selectedItem.Old!;
            AppData.SQLiteData = selectedItem;
        }

        [RelayCommand]
        private async Task AddDataAsync(object? obj)
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    AppData.SQLiteData.Name = AppData.SQLiteAddName;
                    AppData.SQLiteData.Old = AppData.SQLiteAddOld;
                    AppData.ContextSQLite!.sqliteDB.Add(AppData.SQLiteData);
                    AppData.ContextSQLite!.SaveChangesAsync();

                    AppData.SQLiteAddName = string.Empty;
                    AppData.SQLiteAddOld = 0;
                    AppData.SQLiteData = new();
                });
            });
        }

        [RelayCommand]
        private async Task BtnUpdateAsync(object? obj)
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var _foundata = AppData.ContextSQLite!.sqliteDB.FindAsync(AppData.SQLiteData.Id)!;
                    SQLiteModel? foundata = _foundata.Result;
                    AppData.SQLiteData.Name = AppData.SQLiteUpdateName;
                    AppData.SQLiteData.Old = AppData.SQLiteUpdateOld;

                    if (foundata is not null)
                    {
                        //AppData.ContextSQLite!.Entry(foundata).CurrentValues.SetValues(AppData.SQLiteData);
                        AppData.ContextSQLite.Entry(foundata).State = EntityState.Detached;
                        //AppData.ContextSQLite!.sqliteDB.Entry(AppData.SQLiteData).State = EntityState.Modified;
                        //AppData.ContextSQLite!.sqliteDB.Update(AppData.SQLiteData);
                        AppData.ContextSQLite!.sqliteDB.Entry(AppData.SQLiteData).Property(s => s.Name).IsModified = true;
                        AppData.ContextSQLite!.sqliteDB.Entry(AppData.SQLiteData).Property(s => s.Old).IsModified = true;

                        AppData.ContextSQLite!.SaveChanges();
                    }
                });
            });
        }

        [RelayCommand]
        private async Task BtnDeleteAsync(object? obj)
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    AppData.ContextSQLite!.sqliteDB.Remove(AppData.SQLiteData);
                    AppData.ContextSQLite!.SaveChanges();
                    AppData.String1 = string.Empty;
                    AppData.SQLiteUpdateName = string.Empty;
                    AppData.SQLiteUpdateOld = 0;
                });
            });
        }

        public void Receive(ValueChangedMessage<AppData> message)
        {
            //AppData = message.Value;
        }
    }
}
