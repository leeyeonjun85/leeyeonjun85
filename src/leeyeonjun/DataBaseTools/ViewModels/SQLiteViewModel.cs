using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using DataBaseTools.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataBaseTools.ViewModels
{
    public partial class SQLiteViewModel : ViewModelBase, IParameterReceiver
    {
        [ObservableProperty]
        private AppData _appData = new();

        [ObservableProperty]
        private SQLiteContext _context;

        [ObservableProperty]
        private string _addNameText = "";
        [ObservableProperty]
        private int _addYearsText = 0;
        [ObservableProperty]
        private ObservableCollection<SQLiteModel> _yeonjunTestItemsSource = new();
        [ObservableProperty]
        private SQLiteModel _selectedData = new();
        [ObservableProperty]
        private string _selectedDataString = "아이디 / 이름 / 나이";

        public SQLiteViewModel()
        {

        }



        [RelayCommand]
        private void BtnUpdate(object? obj)
        {
            //SQLiteModel foundata = Context.sqliteDB.Find(SelectedData.Id)!;

            Context.Entry(SelectedData).State = EntityState.Modified;
            Context.SaveChanges();
        }

        [RelayCommand]
        private void BtnDelete(object? obj)
        {
            Context.sqliteDB.Remove(SelectedData);
            Context.SaveChanges();
        }

        [RelayCommand]
        private void MouseLeftButtonUp(object? obj)
        {
            if (obj is not MouseButtonEventArgs args) return;
            if (args!.OriginalSource is not TextBlock textBlock) return;
            if (textBlock.DataContext is not SQLiteModel yeonjunTest) return;
            SelectedData = yeonjunTest;
            SelectedDataString = $"{yeonjunTest.Name} / {yeonjunTest.Old}";
        }

        [RelayCommand]
        private async Task ConnectAsync()
        {
            Context.Database.EnsureCreated();

            AppData.StatusBar1 = "Status : Connected"; ;
            AppData.StatusBar2 = "SQLite 데이터베이스에 연결되었습니다.";

            await Context.sqliteDB.LoadAsync();
            YeonjunTestItemsSource = Context.sqliteDB.Local.ToObservableCollection();

        }

        [RelayCommand]
        private void AddData(object? obj)
        {
            Context.sqliteDB.Add(new SQLiteModel() { Name = AddNameText, Old = AddYearsText });
            Context.SaveChanges();
        }

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            App.logger.LogInformation("SQLite가 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.logger.LogInformation("SQLite가 종료되었습니다.");
        }

        public async void ReceiveParameter(object parameter)
        {
            if (parameter is AppData _appData)
            {
                AppData = _appData;
            }

            App.SQLiteConnectionString = AppData.SQLiteConnectionString;
            Context = (SQLiteContext)Ioc.Default.GetService(typeof(SQLiteContext))!;

            await ConnectAsync();
        }
    }
}
