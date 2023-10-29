using System;
using System.Configuration;
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
using DataBaseTools.Views;
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
                        AppData.ContextSQLite = new SQLiteContext(AppData.SQLiteConnectionString);
                        AppData.ConnectionSQLite = AppData.ContextSQLite.Database.GetDbConnection();
                        AppData.CommandSQLite = AppData.ConnectionSQLite.CreateCommand();
                        AppData.ContextSQLite.Database.EnsureCreatedAsync();
                        AppData.ConnectionSQLite.Open();
                        AppData.CommandSQLite.CommandText = "PRAGMA journal_mode=Off;";
                        AppData.CommandSQLite.ExecuteNonQuery();

                        AppData.SQLiteIsConnected = true;
                        AppData.StatusBar1 = "Status : Connected"; ;
                        AppData.StatusBar2 = "SQLite 데이터베이스에 연결되었습니다.";

                        AppData.ContextSQLite.sqliteDB.LoadAsync();
                        AppData.SQLiteItemsSource = AppData.ContextSQLite.sqliteDB.Local.ToObservableCollection();

                        Dispatcher dispatchObject = Application.Current.Dispatcher;
                        if (dispatchObject == null || dispatchObject.CheckAccess())
                        {
                            AppData.SQLiteBackground = new SolidColorBrush(AppData.SecondaryColor);
                        }
                        else dispatchObject.Invoke(() =>
                        {
                            AppData.SQLiteBackground = new SolidColorBrush(AppData.SecondaryColor);
                        });
                    }
                    else
                    {
                        AppData.CommandSQLite!.Dispose();
                        AppData.ConnectionSQLite!.Close();
                        AppData.ConnectionSQLite.Dispose();
                        AppData.ContextSQLite!.Dispose();

                        AppData.SQLiteIsConnected = false;
                        AppData.StatusBar1 = "Status : Ready"; ;
                        AppData.StatusBar2 = "SQLite 데이터베이스 연결이 해제되었습니다.";
                        AppData.SQLiteItemsSource = new();

                        Dispatcher dispatchObject = Application.Current.Dispatcher;
                        if (dispatchObject == null || dispatchObject.CheckAccess())
                        {
                            AppData.SQLiteBackground = new SolidColorBrush(AppData.PrimaryColor);
                        }
                        else dispatchObject.Invoke(() =>
                        {
                            AppData.SQLiteBackground = new SolidColorBrush(AppData.PrimaryColor);
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
