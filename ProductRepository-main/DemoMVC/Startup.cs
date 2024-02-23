using DemoMVC.Models;
using Microsoft.AspNetCore.Hosting;
namespace DemoMVC
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IProductRepository>();

        }
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

        }
    }
}
