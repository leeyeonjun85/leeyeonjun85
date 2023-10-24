using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataBaseTools.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;

namespace DataBaseTools.ViewModels
{
    public partial class SeojungriOracleViewModel : ViewModelBase, IParameterReceiver
    {
        private readonly TestOracleContext _context;

        [ObservableProperty]
        private AppData _appData = new();


        private OracleConnection? conn;
        private OracleCommand? cmd;
        private OracleDependency? dep;
        private OracleDataAdapter? adapter;
        private OracleDataReader? rdr;

        [ObservableProperty]
        private string _addNameText = "";
        [ObservableProperty]
        private int _addYearsText = 0;
        [ObservableProperty]
        private ObservableCollection<TestOracleModel> _yeonjunTestItemsSource = new();
        [ObservableProperty]
        private TestOracleModel _selectedData = new();
        [ObservableProperty]
        private string _selectedDataString = "아이디 / 이름 / 나이";


        [ObservableProperty]
        private ObservableCollection<OracleTable> _oracleTableList = new();


        public SeojungriOracleViewModel(
            TestOracleContext context
            )
        {
            _context = context;

        }



        [RelayCommand]
        private void BtnUpdate(object? obj)
        {
            //TestOracleModel foundata = _context.yeonjunTest.Find(SelectedData.Id)!;

            _context.Entry(SelectedData).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [RelayCommand]
        private void BtnDelete(object? obj)
        {
            _context.yeonjunTest.Remove(SelectedData);
            _context.SaveChanges();
        }

        [RelayCommand]
        private void MouseLeftButtonUp(object? obj)
        {
            if (obj is not MouseButtonEventArgs args) return;
            if (args!.OriginalSource is not TextBlock textBlock) return;
            if (textBlock.DataContext is not TestOracleModel yeonjunTest) return;
            SelectedData = yeonjunTest;
            SelectedDataString = $"{yeonjunTest.Name} / {yeonjunTest.Years}";
        }

        [RelayCommand]
        private async Task BtnConnectAsync(object? obj)
        {
            conn = new OracleConnection(AppData.OracleConnectionString);
            await conn.OpenAsync();
            cmd = new()
            {
                Connection = conn,
            };

            cmd.CommandText = "SELECT * FROM USER_TAB_COLUMNS";

            //cmd.CommandText = "SELECT * FROM all_tables";

            rdr = cmd.ExecuteReader();

            object[] row;
            while (rdr.Read())
            {
                ArrayList rowList = new ();
                row = new object[rdr.FieldCount];
                rdr.GetValues(row);

                if ((string)row[0] == "TESTUSER")
                {
                    OracleTableList.Add(new OracleTable()
                    { 
                        OWERNER = row[0].ToString()!,
                        TABLE_NAME = row[1].ToString()!,
                        StatRowCount = Convert.ToInt32(row[19])
                    });
                    rowList.Add(row);
                }
            }

            

            var a1 = "";
            var a2 = "";

            //_context.Database.EnsureCreated();
            //if (_context.Database.CanConnect())
            //{
            //    if (!_context.Database.GetService<IRelationalDatabaseCreator>().Exists())
            //    {
            //        RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)_context.Database.GetService<IDatabaseCreator>();
            //        databaseCreator.CreateTables();
            //        StatusBar1 = "Status : Connected"; ;
            //        StatusBar2 = "서정리 오라클 데이터베이스를 생성하였습니다.";
            //    }

            //    StatusBar1 = "Status : Connected"; ;
            //    StatusBar2 = "서정리 오라클 데이터베이스에 연결되었습니다.";

            //    _context.yeonjunTest.Load();
            //    YeonjunTestItemsSource = _context.yeonjunTest.Local.ToObservableCollection();
            //}
        }

        [RelayCommand]
        private void AddData(object? obj)
        {
            _context.yeonjunTest.Add(new TestOracleModel() { Name = AddNameText, Years = AddYearsText });
            _context.SaveChanges();
        }



        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            conn?.Close();

            App.logger.LogInformation("서정리 오라클이 종료되었습니다.");
        }


        public async void ReceiveParameter(object parameter)
        {
            await Task.Run(() =>
            {
                if (parameter is AppData _appData)
                {
                    AppData = _appData;
                }
            });
        }
    }
}
