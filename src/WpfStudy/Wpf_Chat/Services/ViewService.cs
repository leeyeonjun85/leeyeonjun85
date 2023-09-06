using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Wpf_Chat.Models;
using Wpf_Chat.ViewModels;
using Wpf_Chat.Views;

namespace Wpf_Chat.Services
{
    public class ViewService : IViewService
    {

        public void ShowView<TView, TViewModel>(object? parameter = null)
         where TView : Window
         where TViewModel : ViewModelBase
        {
            ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(TViewModel))!;
            Window view = (Window)Ioc.Default.GetService(typeof(TView))!;

            viewModel.SetWindow(view);

            if (parameter is not null && viewModel is IParameterReceiver parameterReceiver)
            {
                parameterReceiver.ReceiveParameter(parameter);
            }

            view.DataContext = viewModel;
            view.Show();
        }

        public void ShowMainView()
        {
            ShowView<MainView, MainViewModel>();
        }

        private bool ActivateView<TView>() where TView : Window
        {
            IEnumerable<Window> windows = Application.Current.Windows.OfType<TView>();
            if (windows.Any())
            {
                windows.ElementAt(0).Activate();
                return true;
            }
            return false;
        }

        public void ShowSubView(SubData subData)
        {
            if (!ActivateView<SubView>())
            {
                ShowView<SubView, SubViewModel>(subData);
            }
        }
    }
}
