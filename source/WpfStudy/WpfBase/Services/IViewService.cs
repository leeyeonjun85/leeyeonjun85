using System.Windows;
using WpfBase.Models;
using WpfBase.ViewModels;

namespace WpfBase.Services
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
