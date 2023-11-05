using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DataBaseTools.Models;
using DataBaseTools.Services;
using Microsoft.EntityFrameworkCore;

namespace DataBaseTools.ViewModels
{
    public partial class PageOracleViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        [ObservableProperty]
        private AppData _appData = App.Data;


        public PageOracleViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private async Task AddDataAsync(object? obj)
        {

            AppData.SQLiteData = new()
            {
                Name = AppData.SQLiteAddName,
                Old = AppData.SQLiteAddOld
            };
            AppData.SQLiteContext!.sqliteDB.Add(AppData.SQLiteData);
            await AppData.SQLiteContext!.SaveChangesAsync();

            Utiles.InitSQLite(AppData);


            //await Task.Run(() =>
            //{
            //    using (SqliteConnection conn = new(AppData.SQLiteConnectionString))
            //    {
            //        conn.Open();

            //        string sql = string.Empty;
            //        sql += "INSERT INTO 'SqliteTestTable1'('Name', 'Old')";
            //        sql += $"    VALUES ('{AppData.SQLiteAddName}', {AppData.SQLiteAddOld});";

            //        using (SqliteCommand cmd = new(sql, conn))
            //        {
            //            cmd.ExecuteNonQuery();
            //        }

            //        Utiles.InitSQLite(AppData);
            //    }
            //});
        }

        [RelayCommand]
        private async Task BtnUpdateAsync(object? obj)
        {
            var _findData = await AppData.SQLiteContext!.sqliteDB.FindAsync(AppData.SQLiteData.Id);


            if (_findData is not null)
            {
                AppData.SQLiteContext.Entry(_findData).State = EntityState.Detached;
                _findData.Name = AppData.SQLiteUpdateName;
                _findData.Old = AppData.SQLiteUpdateOld;
                AppData.SQLiteContext.sqliteDB.Entry(_findData).State = EntityState.Modified;
                await AppData.SQLiteContext!.SaveChangesAsync();

                Utiles.InitSQLite(AppData);
            }
            else
            {
                MessageBox.Show("데이터베이스에 원본데이터가 없습니다.");
            }


            //await Task.Run(() =>
            //{
            //    Application.Current.Dispatcher.Invoke(() =>
            //    {
            //        var _foundata = AppData.ContextSQLite!.sqliteDB.FindAsync(AppData.SQLiteData.Id)!;
            //        ModelSQLite? foundata = _foundata.Result;
            //        AppData.SQLiteData.Name = AppData.SQLiteUpdateName;
            //        AppData.SQLiteData.Old = AppData.SQLiteUpdateOld;

            //        if (foundata is not null)
            //        {
            //            //AppData.ContextSQLite!.Entry(foundata).CurrentValues.SetValues(AppData.SQLiteData);
            //            AppData.ContextSQLite.Entry(foundata).State = EntityState.Detached;
            //            AppData.ContextSQLite!.sqliteDB.Entry(AppData.SQLiteData).State = EntityState.Modified;
            //            //AppData.ContextSQLite!.sqliteDB.Update(AppData.SQLiteData);
            //            //AppData.ContextSQLite!.sqliteDB.Entry(AppData.SQLiteData).Property(s => s.Name).IsModified = true;
            //            //AppData.ContextSQLite!.sqliteDB.Entry(AppData.SQLiteData).Property(s => s.Old).IsModified = true;

            //            AppData.ContextSQLite!.SaveChanges();

            //            Utiles.InitSQLite(AppData);
            //        }
            //    });
            //});
        }

        [RelayCommand]
        private async Task BtnDeleteAsync(object? obj)
        {
            foreach (ModelSQLite _sqlitemodel in AppData.SQLiteSelectedItems)
            {
                AppData.SQLiteContext!.sqliteDB.Remove(_sqlitemodel);
                await AppData.SQLiteContext!.SaveChangesAsync();
            }

            Utiles.InitSQLite(AppData);
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
                            foreach (ModelSQLite _sqlitemodel in selectedItems)
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

        [RelayCommand]
        private async Task BtnRefreshSQLiteClick(object? obj)
        {
            if (AppData.SQLiteContext is not null)
            {
                AppData.SQLiteContext = new(AppData.SQLiteConnectionString);
                AppData.SQLiteItemsSource = new();
                await AppData.SQLiteContext.sqliteDB.LoadAsync();
                AppData.SQLiteItemsSource = AppData.SQLiteContext.sqliteDB.Local.ToObservableCollection();
            }

            Utiles.InitSQLite(AppData);
        }

        public void Receive(ValueChangedMessage<AppData> message)
        {
            //AppData = message.Value;
        }
    }
}
