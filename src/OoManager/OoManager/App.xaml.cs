using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OoManager.Models;
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
        public static AppData Data = new();
        public App()
        {
            IServiceProvider serviceProvider = ConfigureServices();
            Ioc.Default.ConfigureServices(serviceProvider);

            LOGGER = (ILogger<App>)Ioc.Default.GetService(typeof(ILogger<App>))!;
            LOGGER!.LogInformation("프로그램이 시작되었습니다.");
        }

        private IServiceProvider ConfigureServices()
        {

            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            // Configuration Directory
            IConfigurationSection cofigDir = builder.Configuration.GetSection("Directory");
            string dirRoot = Path.Combine(Path.GetPathRoot(Directory.GetCurrentDirectory())!, cofigDir["Root"] ?? "OoManager");
            string dirDataBase = Path.Combine(dirRoot, cofigDir["DataBase"] ?? "DataBase");

            // Validate Directory
            if (!Directory.Exists(dirRoot))
                Directory.CreateDirectory(dirRoot);
            if (!Directory.Exists(dirDataBase))
                Directory.CreateDirectory(dirDataBase);

            builder.Services.AddDbContext<OoDbContext>(p =>
            {
                p.UseSqlite($"Data Source={dirDataBase}{Path.DirectorySeparatorChar}OoDb.db");
            });

            // Views
            builder.Services.AddSingleton<WindowMain>();
            builder.Services.AddTransient<SubView>();
            builder.Services.AddTransient<WindowMemberAdd>();
            builder.Services.AddTransient<WindowMemberUpdate>();
            builder.Services.AddTransient<WindowXpUpdate>();
            builder.Services.AddTransient<WindowLecturesUpdate>();

            // ViewModels
            builder.Services.AddSingleton<WindowMainViewModel>();
            builder.Services.AddTransient<SubViewModel>();
            builder.Services.AddTransient<WindowMemberAddViewModel>();
            builder.Services.AddTransient<WindowMemberUpdateViewModel>();
            builder.Services.AddTransient<WindowXpUpdateViewModel>();
            builder.Services.AddTransient<WindowLecturesUpdateViewModel>();

            // Logging
            builder.Services.AddLogging(x =>
            {
                x.AddConsole();
                x.AddDebug();
            });


            IHost host = builder.Build();
            return host.Services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            #region Show Main Window
            try
            {
                ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(WindowMainViewModel))!;
                Window view = (Window)Ioc.Default.GetService(typeof(WindowMain))!;
                viewModel.SetWindow(view);
                view.DataContext = viewModel;
                view.Show();
            }
            catch (Exception ex)
            {
                LOGGER!.LogError($"Error in Show Main Window{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex}");
                throw;
            }
            #endregion
        }
    }
}
