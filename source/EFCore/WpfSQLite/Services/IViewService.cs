using System.Windows;
using WpfSQLite.Models;
using WpfSQLite.ViewModels;

namespace WpfSQLite.Services
{
    public interface IViewService
    {
        void ShowView<TView, TViewModel>(object? parameter = null)
          where TView : Window
          where TViewModel : ViewModelBase;

        void ShowMainView();

        void ShowSubView(SubData subData);
    }
}
