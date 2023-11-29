using System.Windows;
using OoManager.WPF.ViewModels;

namespace DataBaseTools.Services
{
    public interface IViewService
    {
        bool ActivateView<TView>() where TView : Window;
        void ShowView<TView, TViewModel>(object? parameter = null)
            where TView : Window
            where TViewModel : ViewModelBase;
    }
}