using BlazorServerSignalR.Areas.Identity;
using BlazorServerSignalR.Data;
using BlazorServerSignalR.Hubs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using OoManager.Common;

namespace BlazorServerSignalR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration Directory
            IConfigurationSection cofigDir = builder.Configuration.GetSection("Directory");
            string dirRoot = Path.Combine(Path.GetPathRoot(Directory.GetCurrentDirectory())!, cofigDir["Root"] ?? "OoManager");
            string dirDataBase = Path.Combine(dirRoot, cofigDir["DataBase"] ?? "DataBase");

            // Validate Directory
            if (!Directory.Exists(dirRoot))
                Directory.CreateDirectory(dirRoot);
            if (!Directory.Exists(dirDataBase))
                Directory.CreateDirectory(dirDataBase);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<BlazorServerSignalRDbContext>(options =>
                //options.UseSqlServer(connectionString));
                options.UseSqlite($"Data Source={dirDataBase}{Path.DirectorySeparatorChar}BlazorServerSignalR.db"));
            builder.Services.AddDbContext<OoDbContext>(options =>
                options.UseSqlite($"Data Source={dirDataBase}{Path.DirectorySeparatorChar}OoDb.db"));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<BlazorServerSignalRDbContext>();

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            builder.Services.AddSingleton<WeatherForecastService>();

            //응답 압축 미들웨어 서비스 추가:
            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                      new[] { "application/octet-stream" });
            });

            WebApplication app = builder.Build();

            // 처리 파이프라인 구성의 맨 위에 있는 응답 압축 미들웨어를 사용합니다.
            app.UseResponseCompression();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //if (builder.Environment.IsStaging())
            //{
            //    builder.WebHost.UseStaticWebAssets();
            //}


            // 웹소켓 추가
            //var webSocketOptions = new Microsoft.AspNetCore.Builder.WebSocketOptions
            //{
            //    KeepAliveInterval = TimeSpan.FromMinutes(5) // 프록시가 연결을 유지할 수 있도록 클라이언트로 "핑(ping)" 프레임을 전송하는 빈도
            //};
            //app.UseWebSockets(webSocketOptions);




            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();
            app.MapControllers();

            app.MapBlazorHub();

            //허브를 Blazor로 매핑하기 위하여 엔드포인트를 추가
            app.MapHub<ChatHub>("/chathub");
            app.MapHub<OoHub>("/ooHub");

            app.MapFallbackToPage("/_Host");

            //app.Run();

            var url = "https://172.30.1.45:7076";
            url = "https://*:7076";
            app.Run(url);
        }
    }
}