using EFCore_MySQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EFCore_MySQL
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());

            // Service Add
            var services = new ServiceCollection()
                                .AddSingleton<MainForm>()
                                .AddLogging(builder =>
                                {
                                    builder.AddDebug();
                                    builder.AddFile($"logs{Path.DirectorySeparatorChar}LogText.txt");
                                })
                                .AddDbContext<ModelContext>()
                                .BuildServiceProvider()
                                .GetRequiredService<MainForm>();
            Application.Run(services);
        }
    }
}