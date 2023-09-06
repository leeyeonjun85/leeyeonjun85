using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;

namespace MainForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        //{
        //    // To customize application configuration such as set high DPI settings or default font,
        //    // see https://aka.ms/applicationconfiguration.
        //    ApplicationConfiguration.Initialize();
        //    Application.Run(new MainForm());
        //}

        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection()
                .AddSingleton<MainForm>()
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=products.db"))
                .AddTransient<IProductService, ProductService>()
                .BuildServiceProvider()
                .GetRequiredService<MainForm>();

            //services.AddSingleton<MainForm>();
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=products.db"));
            ////services.AddDbContext<ApplicationDbContext>(options => options.UseOracle("User Id=testuser1;Password=0330;Data Source=localhost:1521/XEPDB1;"));
            ////services.AddSingleton<IProductService, ProductService>();
            //services.AddScoped<IProductService, ProductService>();
            //services.BuildServiceProvider();
            //services.GetRequiredService<MainForm>();

            // Run the main form with DI
            Application.Run(services);
        }


    }
}