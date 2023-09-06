using System;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wpf_DataBase.Services;
using Wpf_DataBase.ViewModels;
using Wpf_DataBase.Views;

namespace Wpf_DataBase
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
            builder.Services.AddTransient<MongoDbView>();
            builder.Services.AddTransient<FireBaseView>();

            // ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<SubViewModel>();
            builder.Services.AddTransient<MongoDbViewModel>();
            builder.Services.AddTransient<FireBaseViewModel>();

            // Logging
            builder.Services.AddLogging(x =>
            {
                x.AddConsole();
                x.AddDebug();
            });

            // Services
            builder.Services.AddSingleton<IViewService, ViewService>();

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
