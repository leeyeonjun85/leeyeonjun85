using System;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using DataBaseTools.Services;
using DataBaseTools.ViewModels;
using DataBaseTools.Views;
using Edcore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Utiles;

namespace DataBaseTools
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

            JsonModel jsonModel = MyUtiles.GetJsonModel();

            // Database : EFCore SeojungriOracle
            builder.Services.AddDbContext<TestOracleContext>(p =>
            {
                p.UseOracle(jsonModel.ConnectionStrings.SeojungriOracle);
                p.ConfigureWarnings(b => b.Ignore(RelationalEventId.CommandExecuted)); // 데이터를 저장할 때 발생하는 알람은 로그에서 무시합니다.
            });

            // Database : EFCore SQLite
            builder.Services.AddDbContext<TestSQLiteContext>(p =>
            {
                p.UseSqlite(jsonModel.ConnectionStrings.SQLite);
                p.ConfigureWarnings(b => b.Ignore(RelationalEventId.CommandExecuted)); // 데이터를 저장할 때 발생하는 알람은 로그에서 무시합니다.
            });

            // Views
            builder.Services.AddSingleton<MainView>();
            builder.Services.AddTransient<SubView>();
            builder.Services.AddTransient<MongoDbView>();
            builder.Services.AddTransient<FireBaseView>();
            builder.Services.AddTransient<SeojungriOracleView>();
            builder.Services.AddTransient<SQLiteView>();
            builder.Services.AddTransient<SftpView>();

            // ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<SubViewModel>();
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