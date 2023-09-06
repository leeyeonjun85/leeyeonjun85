using System.Windows;
using Wpf_SignalR.Models;
using Wpf_SignalR.ViewModels;

namespace Wpf_SignalR.Services
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
