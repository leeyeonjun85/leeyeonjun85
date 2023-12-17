using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using ContosoPizza.Models;
using ContosoPizza.Views;

namespace ContosoPizza.ViewModels
{
    public partial class WindowMainViewModel : ViewModelBase
    {



        [ObservableProperty]
        private string _tbMessage = "Receive Message";



        public WindowMainViewModel()
        {

        }



        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {

        }
    }
}
