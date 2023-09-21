using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Firebase.Database;
using Firebase.Database.Query;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.ApplicationServices;
using OoManager.Common;
using OoManager.Models;
using OoManager.Services;
using OoManager.Views;
using Utiles;

namespace OoManager.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppModel _appModel = new();

        [ObservableProperty]
        private PageHomeModel _pageHome = new();

        [ObservableProperty]
        private ObservableCollection<NavigationItem> _navigationList = new();
        #endregion


        public MainViewModel(
            OoDbContext ooDbContext,
            IOoService ooService)
        {
            AppModel.OoDbContext = ooDbContext;
            AppModel.OoService = ooService;

            NavigationList = new()
            {
                new NavigationItem
                {
                    Title = "Home",
                    SelectedIcon = PackIconKind.Home,
                    UnselectedIcon = PackIconKind.HomeOutline,
                    Source = "/Views/PageHome.xaml",
                },
                new NavigationItem
                {
                    Title = "Members",
                    SelectedIcon = PackIconKind.Users,
                    UnselectedIcon = PackIconKind.UsersOutline,
                    Source = "/Views/PageMembers.xaml",
                },
            };

            AppModel.SelectedIndex = 1;
            AppModel.SelectedItem = NavigationList[1];
        }


        [RelayCommand]
        private void SelectionChanged(object obj)
        {
            if (obj is NavigationItem navigationItem)
            {
                switch (navigationItem.Title)
                {
                    case "Home":
                        {
                            //AppModel.SelectedIndex = 0;
                            //AppModel.PageHomeVisibility = Visibility.Visible;
                            //AppModel.PageMembersVisibility = Visibility.Hidden;

                            AppModel.SelectedIndex = 1;
                            AppModel.SelectedItem = NavigationList[1];
                            AppModel.PageHomeVisibility = Visibility.Hidden;
                            AppModel.PageMembersVisibility = Visibility.Visible;

                            break;
                        }
                    case "Members":
                        {
                            AppModel.SelectedIndex = 0;
                            AppModel.SelectedItem = NavigationList[0];
                            AppModel.PageHomeVisibility = Visibility.Visible;
                            AppModel.PageMembersVisibility = Visibility.Hidden;

                            //AppModel.SelectedIndex = 1;
                            //AppModel.PageHomeVisibility = Visibility.Hidden;
                            //AppModel.PageMembersVisibility = Visibility.Visible;

                            break;
                        }
                    default: throw new Exception();
                }

                AppModel.TestInt += 1;

                object[] message = new object[] { AppModel, PageHome };
                WeakReferenceMessenger.Default.Send(new ValueChangedMessage<object[]>(message));
            }
            else throw new Exception();
        }

        private void chathubReceiveMessageHandler(string user, string message)
        {
            var encodedMsg = $"{user}: {message}";
            AppModel.ChatText += Environment.NewLine + encodedMsg;
        }

        private void ooHubReceiveMessageHandler(OoMessageType ooMessageType, string[]? args)
        {
            MessageBox.Show($"{ooMessageType}");
        }


        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            //AppModel = AppModel.OoService?.InitAppModel(AppModel);

            //AppModel.SelectedIndex = 0;
            //AppModel.SelectedItem = AppModel.NavigationList[0];

            App.LOGGER!.LogInformation("프로그램이 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.LOGGER!.LogInformation("프로그램이 종료되었습니다.");
        }
    }
}
