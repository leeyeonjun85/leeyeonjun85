﻿using System;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DataBaseTools.Models;
using DataBaseTools.Services;
using DataBaseTools.Views;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DataBaseTools.ViewModels
{
    public partial class PageHomeViewModel : ViewModelBase, IRecipient<ValueChangedMessage<SubData>>
    {

        [ObservableProperty]
        private AppData _appData = App.Data;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmptyAndIsNumeric))]
        [NotifyCanExecuteChangedFor(nameof(BtnShowWindowSubClickCommand))]
        private string _tbName = ConfigurationManager.AppSettings["MyName"]?.ToString() ?? "이름을 입력하세요";
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmptyAndIsNumeric))]
        [NotifyCanExecuteChangedFor(nameof(BtnShowWindowSubClickCommand))]
        private string _tbOld = ConfigurationManager.AppSettings["MyOld"]?.ToString() ?? "0";

        [ObservableProperty]
        private string _tbMessage = "Sample Message";

        private bool IsNotEmptyAndIsNumeric => (TbName != string.Empty
                                             && TbOld != string.Empty
                                             && IsNumeric(TbOld)
                                                );
        private bool IsNumeric(string str) => IsNumeric().IsMatch(str);

        public PageHomeViewModel()
        {
            IsActive = true;
        }

        [RelayCommand(CanExecute = nameof(IsNotEmptyAndIsNumeric))]
        private void BtnShowWindowSubClick(ViewModelBase? obj)
        {
            App.viewService.ShowView<WindowSub, WindowSubViewModel>(
                new SubData() { Name = TbName, Old = Convert.ToInt32(TbOld), Message = TbMessage }
            );
        }

        [RelayCommand]
        private void BtnTest1(ViewModelBase? obj)
        {
            AppData.String1 = AppData.String2;
        }

        [RelayCommand]
        private void MongoDb(ViewModelBase? obj)
        {
            App.viewService.ShowView<MongoDbView, MongoDbViewModel>();
        }

        [RelayCommand]
        private void FireBase(ViewModelBase? obj)
        {
            App.viewService.ShowView<FireBaseView, FireBaseViewModel>();
        }

        [RelayCommand]
        private void SeojungriOracle(ViewModelBase? obj)
        {
            App.viewService.ShowView<SeojungriOracleView, SeojungriOracleViewModel>(AppData);
        }

        [RelayCommand]
        private async Task BtnSQLiteAsync(Button? _btn)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (!AppData.SQLiteIsConnected)
                    {
                        using (SqliteConnection conn = new(AppData.SQLiteConnectionString))
                        {
                            conn.OpenAsync();

                            string sql = string.Empty;
                            sql += "CREATE TABLE IF NOT EXISTS 'SqliteTestTable1' (";
                            sql += "    'Id' INTEGER PRIMARY KEY AUTOINCREMENT,";
                            sql += "    'Name' VARCHAR(16),";
                            sql += "    'Old' INTEGER";
                            sql += ");";

                            using (SqliteCommand cmd = new(sql, conn))
                            {
                                cmd.ExecuteNonQuery();
                            }

                            sql = "INSERT INTO 'SqliteTestTable1'('Name', 'Old') VALUES ('이연준', 38);";
                            using (SqliteCommand cmd = new(sql, conn))
                            {
                                cmd.ExecuteNonQuery();
                            }


                            //AppData.SQLiteItemsSource = new DataTable();
                            //sql = "SELECT * FROM sqlite_master WHERE type='table';";
                            //using (SqliteCommand cmd = new(sql, conn))
                            //{
                            //    using (SqliteDataReader rdr = cmd.ExecuteReader())
                            //    {
                            //        AppData.SQLiteItemsSource.Load(rdr);
                            //    }
                            //}


                            sql = "SELECT * FROM SqliteTestTable1;";
                            using (SqliteCommand cmd = new(sql, conn))
                            {
                                using (SqliteDataReader rdr = cmd.ExecuteReader())
                                {
                                    while (rdr.Read())
                                    {
                                        AppData.SQLiteItemsSource.Add(new SQLiteModel() { Id = Convert.ToInt32(rdr["Id"]), Name = rdr["Name"].ToString(), Old = Convert.ToInt32(rdr["Old"]) });
                                    }
                                }
                            }

                            AppData.SQLiteIsEnabled = true;
                            AppData.SQLiteIsState = conn.State;
                            AppData.SQLiteIsConnected = true;
                            Utiles.InitSQLite(AppData);
                            AppData.StatusBar1 = "Status : SQLite Connected"; ;
                            AppData.StatusBar2 = "SQLite 데이터베이스에 연결되었습니다.";
                        }
                    }
                    else
                    {
                        AppData.SQLiteDataReader?.Close();
                        AppData.SQLiteDataReader?.Dispose();
                        AppData.SQLiteCommand?.Dispose();
                        AppData.SQLiteConnection?.Close();
                        AppData.SQLiteConnection?.Dispose();
                        AppData.SQLiteContext?.Dispose();

                        AppData.SQLiteIsEnabled = false;
                        AppData.SQLiteIsState = ConnectionState.Closed;
                        AppData.SQLiteIsConnected = false;
                        AppData.StatusBar1 = "Status : Ready"; ;
                        AppData.StatusBar2 = "SQLite 데이터베이스 연결이 해제되었습니다.";
                        AppData.SQLiteItemsSource = new();

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            AppData.BtnSQLiteBackground = new SolidColorBrush(AppData.PrimaryColor);
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex}");
                    throw;
                }
            });
        }

        [RelayCommand]
        private void Sftp(ViewModelBase? obj)
        {
            App.viewService.ShowView<SftpView, SftpViewModel>();
        }


        public void Receive(ValueChangedMessage<SubData> message)
        {
            TbName = message.Value.Name;
            TbOld = $"{message.Value.Old}";
            TbMessage = message.Value.Message;
        }

        [GeneratedRegex("^[0-9]*$")]
        private static partial Regex IsNumeric();
    }
}
