using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WpfTemplet1.Services;
using WpfTemplet1.ViewModels;
using WpfTemplet1.Views;

namespace WpfTemplet1
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
            builder.Services.AddTransient<WindowSub>();

            // ViewModels
            builder.Services.AddSingleton<WindowMainViewModel>();
            builder.Services.AddTransient<WindowSubViewModel>();

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
