using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using WpfTemplet1.Models;
using WpfTemplet1.Views;

namespace WpfTemplet1.ViewModels
{
    public partial class WindowMainViewModel : ViewModelBase, IRecipient<ValueChangedMessage<SubData>>
    {
        private Random _random = new();

        [ObservableProperty]
        private AppData _appData = App.Data;

        [ObservableProperty]
        private string _statusBar1 = "Ready";
        [ObservableProperty]
        private string _statusBar2 = "Meassage";
        [ObservableProperty]
        private bool _progressBarIsIndeterminate = false;
        [ObservableProperty]
        private int _statusBarProgressBar = 20;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmptyAndIsNumeric))]
        [NotifyCanExecuteChangedFor(nameof(BtnShowWindowSubClickCommand))]
        private string _tbName = $"이연준{DateTime.Now.Second % 10}";
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmptyAndIsNumeric))]
        [NotifyCanExecuteChangedFor(nameof(BtnShowWindowSubClickCommand))]
        private string _tbOld = string.Empty;

        [ObservableProperty]
        private string _tbMessage = "Receive Message";

        private bool IsNotEmptyAndIsNumeric => (TbName != string.Empty
                                             && TbOld != string.Empty
                                             && IsNumeric(TbOld));
        private bool IsNumeric(string str) => IsNumeric().IsMatch(str);

        public WindowMainViewModel()
        {
            IsActive = true;
            TbOld = $"{_random.Next(5, 80)}";
        }

        [RelayCommand(CanExecute = nameof(IsNotEmptyAndIsNumeric))]
        private void BtnShowWindowSubClick(ViewModelBase? obj)
        {
            App.ViewService?.ShowView<WindowSub, WindowSubViewModel>(
                new SubData() { Name = TbName, Old = Convert.ToInt32(TbOld) }
            );
        }

        public void Receive(ValueChangedMessage<SubData> message)
        {
            TbName = message.Value.Name;
            TbOld = $"{message.Value.Old}";
        }

        [GeneratedRegex("^[0-9]*$")]
        private static partial Regex IsNumeric();

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {

        }
    }
}
