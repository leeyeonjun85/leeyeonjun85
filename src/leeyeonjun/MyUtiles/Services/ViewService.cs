#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using MyUtiles.ViewModels;
using Microsoft.Extensions.Logging;

namespace MyUtiles.Services
{
    public class ViewService : IViewService
    {
        public void ShowView<TView, TViewModel>(object? parameter = null)
            where TView : Window
            where TViewModel : ViewModelBase
        {
            try
            {
                ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(TViewModel))!;
                Window view = (Window)Ioc.Default.GetService(typeof(TView))!;

                if (ActivateView<TView>())
                {
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
                App.logger.LogError($"Error in Show Window{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex}");
                throw;
            }
        }

        public bool ActivateView<TView>() where TView : Window
        {
            IEnumerable<Window> windows = Application.Current.Windows.OfType<TView>();
            if (windows.Any())
            {
                windows.ElementAt(0).Activate();
                return true;
            }
            return false;
        }
    }
}
