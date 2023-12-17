using System.Configuration;
using System.Data;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using ContosoPizza.Models;
using ContosoPizza.ViewModels;
using ContosoPizza.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContosoPizza
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider ConfigureServices()
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            // Add SQLite Database
            builder.Services.AddSqlite<PizzaContext>("Data Source=ContosoPizza.db");

            // Views
            builder.Services.AddSingleton<WindowMain>();

            // ViewModels
            builder.Services.AddSingleton<WindowMainViewModel>();

            IHost host = builder.Build();
            return host.Services;
        }

        public App()
        {
            Ioc.Default.ConfigureServices(ConfigureServices());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(WindowMainViewModel))!;
            Window view = (Window)Ioc.Default.GetService(typeof(WindowMain))!;
            view.DataContext = viewModel;
            view.Show();


            //if (Ioc.Default.GetService(typeof(WindowMainViewModel)) is ViewModelBase viewModel)
            //{
            //    if (Ioc.Default.GetService(typeof(WindowMain)) is Window view)
            //    {
            //        view.DataContext = viewModel;
            //        view.Show();
            //    }
            //}
        }
    }

}
