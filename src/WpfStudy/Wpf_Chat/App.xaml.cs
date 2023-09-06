using System;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wpf_Chat.Services;
using Wpf_Chat.ViewModels;
using Wpf_Chat.Views;

namespace Wpf_Chat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ILogger? LOGGER;

        private static IServiceProvider ConfigureServices()
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            // Configuration
            var config = builder.Configuration.GetSection("AppConfiguration");

            var serverIP = config["ServerIP"];

            // Views
            builder.Services.AddSingleton<MainView>();
            builder.Services.AddTransient<SubView>();

            // Services
            builder.Services.AddSingleton<IViewService, ViewService>();
            builder.Services.AddSingleton<ISignalRControl, SignalRControl>();

            // ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<SubViewModel>();

            IHost host = builder.Build();
            return host.Services;
        }

        public App()
        {
            IServiceProvider serviceProvider = ConfigureServices();
            Ioc.Default.ConfigureServices(serviceProvider);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LOGGER = (ILogger<App>)Ioc.Default.GetService(typeof(ILogger<App>))!;
            var viewService = (IViewService)Ioc.Default.GetService(typeof(IViewService))!;
            viewService.ShowMainView();
        }
    }
}
