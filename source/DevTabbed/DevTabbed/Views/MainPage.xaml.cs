using DevTabbed.ViewModels;
using System.Runtime.CompilerServices;

namespace DevTabbed.Views
{
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}