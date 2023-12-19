using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ContosoPizza.Services;
using ContosoPizza.ViewModels;
using ContosoPizza.Views;
using ContosoPizza.Models;

namespace ContosoPizza
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ILogger? _logger;
        private static IViewService? _viewService;
        private static AppData _data = new();
        public static ILogger? Logger { get => _logger; set => _logger = value; }
        public static IViewService? ViewService { get => _viewService; set => _viewService = value; }
        public static AppData Data { get => _data; set => _data = value; }

        private static IServiceProvider ConfigureServices()
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            // Views
            builder.Services.AddSingleton<WindowMain>();
            builder.Services.AddTransient<WindowNewPizza>();

            // ViewModels
            builder.Services.AddSingleton<WindowMainViewModel>();
            builder.Services.AddTransient<WindowNewPizzaViewModel>();

            // Logging
            builder.Services.AddLogging(x =>
            {
                x.AddConsole();
                x.AddDebug();
            });

            // Services
            builder.Services.AddSingleton<IViewService, ViewService>();

            // Add SQLite Database
            builder.Services.AddSqlite<PizzaContext>("Data Source=ContosoPizza.db");

            IHost host = builder.Build();
            return host.Services;
        }

        public App()
        {
            Ioc.Default.ConfigureServices(ConfigureServices());

            Logger = (ILogger<App>)Ioc.Default.GetService(typeof(ILogger<App>))!;
            ViewService = (IViewService)Ioc.Default.GetService(typeof(IViewService))!;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            App.ViewService?.ShowView<WindowMain, WindowMainViewModel>();
        }
    }

}
