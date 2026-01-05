using System.Windows;
using ContosoPizza.ViewModels;

namespace ContosoPizza.Services
{
    public interface IViewService
    {
        bool ActivateView<TView>() where TView : Window;
        void ShowView<TView, TViewModel>(object? parameter = null)
            where TView : Window
            where TViewModel : ViewModelBase;
    }
}