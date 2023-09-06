using System.Windows;
using Wpf_DataBase.Models;
using Wpf_DataBase.ViewModels;

namespace Wpf_DataBase.Services
{
    public interface IViewService
    {
        void ShowFireBaseView();
        void ShowMainView();
        void ShowMongoDbView();
        void ShowSubView(SubData subData);
        void ShowView<TView, TViewModel>(object? parameter = null)
            where TView : Window
            where TViewModel : ViewModelBase;
    }
}