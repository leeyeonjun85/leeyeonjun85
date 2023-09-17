using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.DependencyInjection;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OoManager.Common;
using OoManager.Services;
using OoManager.ViewModels;
using OoManager.Views;

namespace OoManager
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

            // Configuration
            string pathRoot = Path.Combine(Path.GetPathRoot(Directory.GetCurrentDirectory())!, ConfigurationManager.AppSettings["Root"] ?? "Root");
            string pathAppName = Path.Combine(pathRoot, ConfigurationManager.AppSettings["AppName"] ?? "AppName");
            string pathLogs = Path.Combine(pathAppName, ConfigurationManager.AppSettings["Logs"] ?? "Logs");
            string pathDataBase = Path.Combine(pathRoot, ConfigurationManager.AppSettings["DataBase"] ?? "DataBase");

            // Validate Directory
            if (!Directory.Exists(pathRoot))
                Directory.CreateDirectory(pathRoot);
            if (!Directory.Exists(pathAppName))
                Directory.CreateDirectory(pathAppName);
            if (!Directory.Exists(pathLogs))
                Directory.CreateDirectory(pathLogs);
            if (!Directory.Exists(pathDataBase))
                Directory.CreateDirectory(pathDataBase);

            // Oo Database
            builder.Services.AddDbContext<OoDbContext>(options =>
                options.UseSqlite($"Data Source={pathDataBase}{Path.DirectorySeparatorChar}OoDb.db"));

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
            builder.Services.AddSingleton<IOoService, OoService>();

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

            LOGGER = (ILogger<App>?)Ioc.Default.GetService(typeof(ILogger<App>));

            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.DeepPurple];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignLightTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);

            var viewService = (IViewService)Ioc.Default.GetService(typeof(IViewService))!;
            viewService.ShowMainView();
        }
    }
}
