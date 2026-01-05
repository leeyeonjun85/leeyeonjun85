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
            if (AppData.OracleContext is null) return;

            AppData.OracleData = new()
            {
                Id = AppData.OracleItemsSource[^1].Id + 1,
                Name = AppData.AddName,
                Old = AppData.AddOld
            };
            AppData.OracleContext.LeeyeonjunTestTable1.Add(AppData.OracleData);
            await AppData.OracleContext.SaveChangesAsync();

            Utiles.InitOracle();


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
            if (AppData.OracleContext is null) return;

            var findData = await AppData.OracleContext.LeeyeonjunTestTable1.FindAsync(AppData.OracleData.Id);


            if (findData is not null)
            {
                AppData.OracleContext.Entry(findData).State = EntityState.Detached;
                findData.Name = AppData.UpdateName;
                findData.Old = AppData.UpdateOld;
                AppData.OracleContext.LeeyeonjunTestTable1.Entry(findData).State = EntityState.Modified;
                await AppData.OracleContext.SaveChangesAsync();

                Utiles.InitOracle();
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
            if (AppData.OracleContext is null) return;

            foreach (ModelOracle oracleemodel in AppData.OracleSelectedItems)
            {
                AppData.OracleContext.LeeyeonjunTestTable1.Remove(oracleemodel);
                await AppData.OracleContext.SaveChangesAsync();
            }

            Utiles.InitOracle();
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
                            AppData.OracleSelectedItems = new();
                            foreach (ModelOracle model in selectedItems)
                            {
                                AppData.String1 += $"{model.Id} / {model.Name} / {model.Old}{Environment.NewLine}";
                                AppData.UpdateName = model.Name!;
                                AppData.UpdateOld = model.Old!;
                                AppData.OracleData = model;
                                AppData.OracleSelectedItems.Add(model);
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
        private async Task BtnRefreshClickAsync(object? obj)
        {
            if (AppData.OracleContext is not null)
            {
                AppData.OracleItemsSource = new();
                await AppData.OracleContext.LeeyeonjunTestTable1.LoadAsync();
                AppData.OracleItemsSource = AppData.OracleContext.LeeyeonjunTestTable1.Local.ToObservableCollection();
            }
        }

        public void Receive(ValueChangedMessage<AppData> message)
        {
            //AppData = message.Value;
        }
    }
}
