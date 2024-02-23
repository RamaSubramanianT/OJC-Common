using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DemoMVC;
using DemoMVC.Models;
internal partial class Program
{
    
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        /*var builder1 = WebApplication.CreateBuilder(args);
        builder1.Services.AddLog4net();
        var log4net = builder.Build();
        log4net.Run();*/
        
        // Moves to Login Page when the Web page is inactive for more than 2 minutes
        builder.Services.AddAuthentication(
            CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.LoginPath = "/Home/Login";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(2);
            });

        builder.Services.AddAuthorization();

        //To display the message "The field is required." instead of a builtin required statement
        builder.Services.AddRazorPages().AddMvcOptions(options =>
        {
            options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor( _ => "The field is required.");
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        //Add support to logging with SERILOG
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
            logging.RequestHeaders.Add("REsponseHeader");
        });

        //Dependency Injection
        builder.Services.AddSingleton<IProductRepository, UpdateProduct>();


        builder.Services.AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN");

        builder.Services.AddWebOptimizer(pipeline =>
        {
            pipeline.MinifyJsFiles("js/**.js");
            pipeline.AddJavaScriptBundle("/js/bundle.js", "js/**/*.js");
        });

        var app = builder.Build();

        app.UseMiddleware<Middleware>();
        
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseWebOptimizer();

        //Add support to logging request with SERILOG
        app.UseSerilogRequestLogging();
        app.UseHttpLogging();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Login}/{id?}");

        app.MapRazorPages();

        /*app.Run(async (context) =>
        {
            await context.Response.WriteAsync("Hello World!");
        });*/
        app.Run();
    }
}