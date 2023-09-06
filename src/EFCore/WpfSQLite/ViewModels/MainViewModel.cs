using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using WpfSQLite.Services;

namespace WpfSQLite.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IViewService _viewService;

        private void ShowSubView(object? _)
        {
            _viewService.ShowSubView(new Models.SubData { StringData = "가나다", IntData = 123 });
        }

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("MainWindow Loaded");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            //MessageBox.Show("MainWindow Closing");
        }

        public MainViewModel(IViewService viewService)
        {
            _viewService = viewService;
        }

        public ICommand ShowSubViewCommand => new RelayCommand<object>(ShowSubView);
    }
}
