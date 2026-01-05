using System.ComponentModel;
using System.Windows;
using Wpf_Chat.Models;
using Wpf_Chat.ViewModels;

namespace Wpf_Chat.Services
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
