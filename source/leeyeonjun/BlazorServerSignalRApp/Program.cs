using System.Net;
using System.Net.Sockets;
using BlazorServerSignalRApp.Data;
using BlazorServerSignalRApp.Server.Hubs;
using Microsoft.AspNetCore.ResponseCompression;

namespace BlazorServerSignalRApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();

            if (args.Length >= 3 && args[2] is string hub)
            {
                app.MapHub<ChatHub>($"/{hub}");
            }
            else
                app.MapHub<ChatHub>("/chathub");

            //app.MapHub<ChatHub>("/chathub");
            app.MapFallbackToPage("/_Host");

            string url = string.Empty;
            if (args.Length >= 1 && args[0] is string ipv4 && args.Length >= 2 && args[1] is string port)
            {
                url = $"https://{ipv4}:{port}";
            }
            else
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress iPAddress in host.AddressList)
                {
                    if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        url = $"https://{iPAddress}:6714";
                    }
                }
            }

            app.Run(url);
            //app.Run();
        }
    }
}