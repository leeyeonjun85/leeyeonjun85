using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DataBaseTools.Models;
using DataBaseTools.Services;
using DataBaseTools.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;

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

            AppData.OracleContext = (ContextOracle)Ioc.Default.GetService(typeof(ContextOracle))!;
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
        private async Task BtnOracleConnectClickAsync(object? obj)
        {
            if (AppData.SelectedPage is null) return;


            if (!AppData.IsOracleConnected)
            {
                AppData.OracleContext = new(AppData.OracleConnectionString);

                bool resultDataBaseConnect = false;
                await Task.Run(() =>
                {
                    //"true" if the database is created, "false" if it already existed
                    Task<bool> _resultDataBaseConnect = AppData.OracleContext.Database.EnsureCreatedAsync();
                    //await _resultDataBaseConnect;
                    bool resultDataBaseConnect = _resultDataBaseConnect.Result;
                });



                if (resultDataBaseConnect)
                {
                    AppData.StatusBar1 = "Status : Oracle Connected";
                    AppData.StatusBar2 = $"'LeeyeonjunTestTable1' 테이블을 생성하였습니다.";
                }
                else
                {
                    AppData.StatusBar1 = "Status : Oracle Connected";
                    AppData.StatusBar2 = $"'LeeyeonjunTestTable1' 데이터를 불러왔습니다.";
                }


                AppData.OracleContext.LeeyeonjunTestTable1.Load();
                AppData.OracleItemsSource = AppData.OracleContext.LeeyeonjunTestTable1.Local.ToObservableCollection();

                if (AppData.OracleItemsSource.Count > 0)
                {
                    AppData.IsOracleConnected = true;
                    AppData.StatusBar1 = "Status : Oracle Connected";
                    AppData.StatusBar2 = $"{AppData.OracleConnectionString}";
                    AppData.NavigationList[Pages.Oracle].IsEnabled = true;
                    Utiles.RefreshPageNavigationItems(AppData.SelectedPage);
                    AppData.BtnOracleConnect.Content = "Connected";
                    AppData.BtnOracleConnect.Background = new SolidColorBrush(AppData.ColorSecondary);
                    AppData.BtnOracleConnect.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            else
            {
                await Utiles.DisposeOracleAsync();

                AppData.IsOracleConnected = false;
                AppData.StatusBar1 = "Status : Ready"; ;
                AppData.StatusBar2 = "오라클 데이터베이스 연결이 해제되었습니다.";
                AppData.NavigationList[Pages.Oracle].IsEnabled = false;
                Utiles.RefreshPageNavigationItems(AppData.SelectedPage);
                AppData.BtnOracleConnect.Content = "Connect";
                AppData.BtnOracleConnect.Background = new SolidColorBrush(AppData.ColorPrimary);
                AppData.BtnOracleConnect.Foreground = new SolidColorBrush(Colors.White);
                AppData.OracleItemsSource = new();
            }





            //if (AppData.OracleContext.Database.CanConnect())
            //{
            //    AppData.OracleConnection = (OracleConnection)AppData.OracleContext.Database.GetDbConnection();

            //    await AppData.OracleConnection.OpenAsync();
            //    AppData.OracleCommand.Connection = AppData.OracleConnection;

            //    // Get Table List
            //    Utiles.GetAllTables(AppData);
            //    //List<OracleTable>? table_TESTUSER = AppData.OracleTableList
            //    //                                        .Where(x => x.OWNER == "TESTUSER") as List<OracleTable>;

            //    var tableNames = AppData.OracleTableList
            //                                .Select(x => x.TABLE_NAME);

            //    if (!tableNames.Contains("LeeyeonjunTestTable1"))
            //    {
            //        try
            //        {
            //            RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)AppData.OracleContext.Database.GetService<IDatabaseCreator>();
            //            databaseCreator.CreateTables();

            //            //AppData.OracleCommand.CommandText = "CREATE TABLE LeeyeonjunTestTable1 ("
            //            //                              + "Id NUMBER(8),"
            //            //                              + "Name VARCHAR2(16),"
            //            //                              + "Old NUMBER(7,2),"
            //            //                              + "CONSTRAINT LeeyeonjunTestTable1_pk PRIMARY KEY (Id)"
            //            //                              + ")";
            //            //AppData.OracleCommand.ExecuteNonQuery();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show($"{ex}");
            //        }
            //    }

            //    await AppData.OracleConnection.CloseAsync();
            //}
        }

        [RelayCommand]
        private async Task BtnSQLiteAsync(object? obj)
        {
            if (AppData.SelectedPage is null) return;

            try
            {
                // Cennect if Not Connected
                if (!AppData.SQLiteIsConnected)
                {
                    AppData.SQLiteContext = new(AppData.SQLiteConnectionString);

                    await Task.Run(() =>
                    {
                        AppData.SQLiteContext.Database.EnsureCreated();
                        AppData.SQLiteConnection = AppData.SQLiteContext.Database.GetDbConnection();
                        AppData.SQLiteConnection.Open();
                        AppData.SQLiteCommand = AppData.SQLiteConnection.CreateCommand();
                        AppData.SQLiteCommand.CommandText = "PRAGMA journal_mode=Off;";
                        AppData.SQLiteCommand.ExecuteNonQuery();
                    });

                    await AppData.SQLiteContext.sqliteDB.LoadAsync();
                    AppData.SQLiteItemsSource = AppData.SQLiteContext.sqliteDB.Local.ToObservableCollection();

                    #region Connect by Connection(ConnectionString)
                    //using (SqliteConnection conn = new(AppData.SQLiteConnectionString))
                    //{
                    //    await conn.OpenAsync();

                    //    string sql = string.Empty;
                    //    sql += "CREATE TABLE IF NOT EXISTS 'SqliteTestTable1' (";
                    //    sql += "    'Id' INTEGER PRIMARY KEY AUTOINCREMENT,";
                    //    sql += "    'Name' VARCHAR(16),";
                    //    sql += "    'Old' INTEGER";
                    //    sql += ");";

                    //    using (SqliteCommand cmd = new(sql, conn))
                    //    {
                    //        cmd.ExecuteNonQuery();
                    //    }

                    //    //sql = "INSERT INTO 'SqliteTestTable1'('Name', 'Old') VALUES ('이연준', 38);";
                    //    //using (SqliteCommand cmd = new(sql, conn))
                    //    //{
                    //    //    cmd.ExecuteNonQuery();
                    //    //}


                    //    //AppData.SQLiteItemsSource = new DataTable();
                    //    //sql = "SELECT * FROM sqlite_master WHERE type='table';";
                    //    //using (SqliteCommand cmd = new(sql, conn))
                    //    //{
                    //    //    using (SqliteDataReader rdr = cmd.ExecuteReader())
                    //    //    {
                    //    //        AppData.SQLiteItemsSource.Load(rdr);
                    //    //    }
                    //    //}


                    //    sql = "SELECT * FROM SqliteTestTable1;";
                    //    using (SqliteCommand cmd = new(sql, conn))
                    //    {
                    //        using (SqliteDataReader rdr = cmd.ExecuteReader())
                    //        {
                    //            while (rdr.Read())
                    //            {
                    //                AppData.SQLiteItemsSource.Add(new ModelSQLite() { Id = Convert.ToInt32(rdr["Id"]), Name = rdr["Name"].ToString(), Old = Convert.ToInt32(rdr["Old"]) });
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion

                    AppData.SQLiteIsConnected = true;
                    AppData.StatusBar1 = "Status : SQLite Connected";
                    AppData.StatusBar2 = $"{AppData.SQLiteConnectionString}";
                    AppData.NavigationList[Pages.SQLite].IsEnabled = true;
                    Utiles.RefreshPageNavigationItems(AppData.SelectedPage);
                    AppData.BtnSQLite.Content = "Connected";
                    AppData.BtnSQLite.Background = new SolidColorBrush(AppData.ColorSecondary);
                    AppData.BtnSQLite.Foreground = new SolidColorBrush(Colors.Black);
                    Utiles.InitSQLite();
                }
                // Discennect if Connected
                else
                {
                    await Utiles.DisposeSQLiteAsync();

                    AppData.SQLiteIsConnected = false;
                    AppData.StatusBar1 = "Status : Ready"; ;
                    AppData.StatusBar2 = "SQLite 데이터베이스 연결이 해제되었습니다.";
                    AppData.NavigationList[Pages.SQLite].IsEnabled = false;
                    Utiles.RefreshPageNavigationItems(AppData.SelectedPage);
                    AppData.BtnSQLite.Content = "Connect";
                    AppData.BtnSQLite.Background = new SolidColorBrush(AppData.ColorPrimary);
                    AppData.BtnSQLite.Foreground = new SolidColorBrush(Colors.White);
                    AppData.SQLiteItemsSource = new();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex}");
                throw;
            }
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
