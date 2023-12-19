using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Logging;
using ContosoPizza.ViewModels;

namespace ContosoPizza.Services
{
    public class ViewService : IViewService
    {
        public void ShowView<TView, TViewModel>(object? parameter = null)
            where TView : Window
            where TViewModel : ViewModelBase
        {
            try
            {
                if (ActivateView<TView>())
                {
                    ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(TViewModel))!;
                    Window view = (Window)Ioc.Default.GetService(typeof(TView))!;
                    viewModel.SetWindow(view);

                    if (parameter != null && viewModel is IParameterReceiver parameterReceiver)
                    {
                        parameterReceiver.ReceiveParameter(parameter);
                    }

                    view.DataContext = viewModel;
                    view.Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public bool ActivateView<TView>() where TView : Window
        {
            IEnumerable<Window> windows = Application.Current.Windows.OfType<TView>();
            if (windows.Any())
            {
                windows.ElementAt(0).Activate();
                return false;
            }
            return true;
        }
    }
}
