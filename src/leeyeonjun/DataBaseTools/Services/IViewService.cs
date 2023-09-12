using System.Windows;
using DataBaseTools.Models;
using DataBaseTools.ViewModels;

namespace DataBaseTools.Services
{
    public interface IViewService
    {
        void ShowFireBaseView();
        void ShowMainView();
        void ShowMongoDbView();
        void ShowSeojungriOracleView();
        void ShowSQLiteView();
        void ShowSubView(SubData subData);
        void ShowView<TView, TViewModel>(object? parameter = null)
            where TView : Window
            where TViewModel : ViewModelBase;
    }
}