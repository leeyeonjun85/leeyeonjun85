using System.Collections.ObjectModel;
using System;
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
using DataBaseTools.Services;

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
        private async Task AddDataAsync(object? obj)
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    AppData.SQLiteData = new()
                    {
                        Name = AppData.SQLiteAddName,
                        Old = AppData.SQLiteAddOld
                    };
                    AppData.SQLiteContext!.sqliteDB.Add(AppData.SQLiteData);
                    AppData.SQLiteContext!.SaveChangesAsync();

                    Utiles.InitSQLite(AppData);
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
                    var _foundata = AppData.SQLiteContext!.sqliteDB.FindAsync(AppData.SQLiteData.Id)!;
                    SQLiteModel? foundata = _foundata.Result;
                    AppData.SQLiteData.Name = AppData.SQLiteUpdateName;
                    AppData.SQLiteData.Old = AppData.SQLiteUpdateOld;

                    if (foundata is not null)
                    {
                        //AppData.SQLiteContext!.Entry(foundata).CurrentValues.SetValues(AppData.SQLiteData);
                        AppData.SQLiteContext.Entry(foundata).State = EntityState.Detached;
                        //AppData.SQLiteContext!.sqliteDB.Entry(AppData.SQLiteData).State = EntityState.Modified;
                        //AppData.SQLiteContext!.sqliteDB.Update(AppData.SQLiteData);
                        AppData.SQLiteContext!.sqliteDB.Entry(AppData.SQLiteData).Property(s => s.Name).IsModified = true;
                        AppData.SQLiteContext!.sqliteDB.Entry(AppData.SQLiteData).Property(s => s.Old).IsModified = true;

                        AppData.SQLiteContext!.SaveChanges();

                        Utiles.InitSQLite(AppData);
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
                    foreach (SQLiteModel _sqlitemodel in AppData.SQLiteSelectedItems)
                    {
                        AppData.SQLiteContext!.sqliteDB.Remove(_sqlitemodel);
                        AppData.SQLiteContext!.SaveChanges();
                    }
                    Utiles.InitSQLite(AppData);
                });
            });
        }

        [RelayCommand]
        private async Task SelectionChangedAsync(object? obj)
        {
            await Task.Run(() => 
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        if (obj is DataGrid dataGrid)
                        {
                            System.Collections.IList selectedItems = dataGrid.SelectedItems;
                            AppData.String1 = string.Empty;
                            AppData.SQLiteSelectedItems = new();
                            foreach (SQLiteModel _sqlitemodel in selectedItems)
                            {
                                AppData.String1 += $"{_sqlitemodel.Id} / {_sqlitemodel.Name} / {_sqlitemodel.Old}{Environment.NewLine}";
                                AppData.SQLiteUpdateName = _sqlitemodel.Name!;
                                AppData.SQLiteUpdateOld = _sqlitemodel.Old!;
                                AppData.SQLiteData = _sqlitemodel;
                                AppData.SQLiteSelectedItems.Add(_sqlitemodel);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Utiles.ExceptionTask(ex);
                    }
                    finally
                    {

                    }
                });
            });
        }

        public void Receive(ValueChangedMessage<AppData> message)
        {
            //AppData = message.Value;
        }
    }
}
