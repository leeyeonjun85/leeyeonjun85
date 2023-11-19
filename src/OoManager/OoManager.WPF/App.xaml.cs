#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OoManager.Common.Models;
using OoManager.WPF.ViewModels;
using OoManager.WPF.Views;

namespace OoManager.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ILogger? LOGGER { get; set; }
        public static AppData Data { get; set; } = new();
        public App()
        {
            IServiceProvider serviceProvider = ConfigureServices();
            Ioc.Default.ConfigureServices(serviceProvider);

            LOGGER = (ILogger<App>)Ioc.Default.GetService(typeof(ILogger<App>))!;
            LOGGER!.LogInformation("프로그램이 시작되었습니다.");
        }

        private static IServiceProvider ConfigureServices()
        {

            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            // Configuration Directory
            IConfigurationSection cofigDir = builder.Configuration.GetSection("Directory");
            Data.dirRoot = Path.Combine(Path.GetPathRoot(Directory.GetCurrentDirectory())!, cofigDir["Root"] ?? "OoManager");
            Data.dirDataBase = Path.Combine(Data.dirRoot, cofigDir["DataBase"] ?? "DataBase");

            // Validate Directory
            if (!Directory.Exists(Data.dirRoot))
                Directory.CreateDirectory(Data.dirRoot);
            if (!Directory.Exists(Data.dirDataBase))
                Directory.CreateDirectory(Data.dirDataBase);

            builder.Services.AddDbContext<OoDbContext>(p =>
            {
                p.UseSqlite($"Data Source={Data.dirDataBase}{Path.DirectorySeparatorChar}OoDb.db");
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
