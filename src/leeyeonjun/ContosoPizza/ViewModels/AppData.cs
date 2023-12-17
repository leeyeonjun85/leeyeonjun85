using CommunityToolkit.Mvvm.ComponentModel;

namespace ContosoPizza.ViewModels
{
    public partial class AppData : ViewModelBase
    {
        [ObservableProperty]
        private string _windowTitle = "ContosoPizza Window Title";
    }
}
