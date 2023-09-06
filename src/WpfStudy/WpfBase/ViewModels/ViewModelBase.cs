using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfBase.ViewModels
{
    public abstract class ViewModelBase : ObservableRecipient
    {
        protected Window? Window;

        protected virtual void OnWindowClosing(object? sender, System.ComponentModel.CancelEventArgs e) { }

        protected virtual void OnWindowLoaded(object sender, RoutedEventArgs e) { }

        private void AddLifecycleHandler()
        {
            Window!.Loaded += OnWindowLoaded;
            Window!.Closing += OnWindowClosing;
        }

        internal void SetWindow(Window window)
        {
            Window = window;
            AddLifecycleHandler();
        }
    }
}
