#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System;
using System.Threading;
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
        private static ILogger? _logger;
        private static IViewService? _viewService;
        private static AppData _data = new();
        public static ILogger? Logger { get => _logger; set => _logger = value; }
        public static IViewService? ViewService { get => _viewService; set => _viewService = value; }
        public static AppData Data { get => _data; set => _data = value; }

        private IServiceProvider ConfigureServices()
        {

            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            // Database : EFCore SeojungriOracle
            //builder.Services.AddDbContext<ContextOracle>(p =>
            //{
            //    p.UseOracle(Data.OracleConnectionString);
            //    p.ConfigureWarnings(b => b.Ignore(RelationalEventId.CommandExecuted)); // 데이터를 저장할 때 발생하는 알람은 로그에서 무시합니다.
            //});

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