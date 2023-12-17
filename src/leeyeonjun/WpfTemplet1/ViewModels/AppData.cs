using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfTemplet1.ViewModels
{
    public partial class AppData : ViewModelBase
    {
        [ObservableProperty]
        private string _windowTitle = "WpfTemplet1 Window Title";
    }
}
