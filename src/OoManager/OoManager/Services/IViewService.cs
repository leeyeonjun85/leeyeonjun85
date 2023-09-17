using System.Windows;
using OoManager.Models;
using OoManager.ViewModels;

namespace OoManager.Services
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
