#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using DataBaseTools.Models;
using DataBaseTools.Services;
using DataBaseTools.ViewModels;
using DataBaseTools.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataBaseTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ILogger logger;
        public static IViewService viewService;
        public static string SQLiteConnectionString = string.Empty;
        public static AppData Data = new();

        private IServiceProvider ConfigureServices()
        {

            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            // Database : EFCore SeojungriOracle
            builder.Services.AddDbContext<TestOracleContext>(p =>
            {
                p.UseOracle(JsonData.GetEdcoreWorksJsonData("SeojungriOracle"));
                p.ConfigureWarnings(b => b.Ignore(RelationalEventId.CommandExecuted)); // 데이터를 저장할 때 발생하는 알람은 로그에서 무시합니다.
            });

            // Views
            builder.Services.AddSingleton<WindowMain>();
            builder.Services.AddTransient<WindowSub>();
            builder.Services.AddTransient<MongoDbView>();
            builder.Services.AddTransient<FireBaseView>();
            builder.Services.AddTransient<SeojungriOracleView>();
            builder.Services.AddTransient<SQLiteView>();
            builder.Services.AddTransient<SftpView>();

            // ViewModels
            builder.Services.AddSingleton<WindowMainViewModel>();
            builder.Services.AddTransient<WindowSubViewModel>();
            builder.Services.AddTransient<MongoDbViewModel>();
            builder.Services.AddTransient<FireBaseViewModel>();
            builder.Services.AddTransient<SeojungriOracleViewModel>();
            builder.Services.AddTransient<SQLiteViewModel>();
            builder.Services.AddTransient<SftpViewModel>();

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

            logger = (ILogger<App>)Ioc.Default.GetService(typeof(ILogger<App>))!;
            viewService = (IViewService)Ioc.Default.GetService(typeof(IViewService))!;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            App.viewService.ShowView<WindowMain, WindowMainViewModel>();
        }
    }
}