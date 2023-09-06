using System;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wpf_SignalR.Services;
using Wpf_SignalR.ViewModels;
using Wpf_SignalR.Views;

namespace Wpf_SignalR
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ILogger? LOGGER;

        private IServiceProvider ConfigureServices()
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            // Views
            builder.Services.AddSingleton<MainView>();
            builder.Services.AddTransient<SubView>();

            // ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<SubViewModel>();

            // Logging
            builder.Services.AddLogging(x =>
            {
                x.AddConsole();
                x.AddDebug();
            });

            // Services
            builder.Services.AddSingleton<IViewService, ViewService>();
            builder.Services.AddSingleton<ISignalRControl, SignalRControl>();

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
