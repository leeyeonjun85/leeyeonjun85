using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WpfSQLite.Models;
using WpfSQLite.Services;
using WpfSQLite.ViewModels;
using WpfSQLite.Views;

namespace WpfSQLite
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _services = default!;

        private IServiceProvider ConfigurationService()
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            // Views
            builder.Services.AddSingleton<MainView>();
            builder.Services.AddTransient<SubView>();

            // Services
            builder.Services.AddSingleton<IViewService, ViewService>();

            // ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<SubViewModel>();

            // DbContext
            builder.Services.AddDbContext<ModelContext>(options
                => options.UseSqlite("Data Source=WpfSQLite.db"));

            IHost host = builder.Build();
            return host.Services;
        }

        public App()
        {
            _services = ConfigurationService();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var viewService = (IViewService)_services.GetService(typeof(IViewService))!;
            viewService.ShowMainView();
        }
    }
}
