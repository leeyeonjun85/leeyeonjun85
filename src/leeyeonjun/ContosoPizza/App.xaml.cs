﻿using System.Windows;
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
        private IViewService? _viewService;
        public IViewService? ViewService { get => _viewService; set => _viewService = value; }

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
            builder.Services.AddSingleton<IPizzaService, PizzaService>();

            // Add SQLite Database
            builder.Services.AddSqlite<PizzaContext>("Data Source=ContosoPizza.db");

            IHost host = builder.Build();
            return host.Services;
        }

        public App()
        {
            Ioc.Default.ConfigureServices(ConfigureServices());
            ViewService = (IViewService)Ioc.Default.GetService(typeof(IViewService))!;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ViewService?.ShowView<WindowMain, WindowMainViewModel>();
        }
    }

}
