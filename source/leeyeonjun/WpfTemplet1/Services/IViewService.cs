using System.Windows;
using WpfTemplet1.ViewModels;

namespace WpfTemplet1.Services
{
    public interface IViewService
    {
        bool ActivateView<TView>() where TView : Window;
        void ShowView<TView, TViewModel>(object? parameter = null)
            where TView : Window
            where TViewModel : ViewModelBase;
    }
}