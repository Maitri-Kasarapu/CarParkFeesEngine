using CarParkFeesEngine.API.Repositories.Implementations;
using CarParkFeesEngine.API.Services.Implementations;
using CarParkFeesEngine.API.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarParkFeesEngine.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<ICarParkFeesCalculatorService, CarParkFeesCalculatorService>();
            services.AddTransient<IPackageRateCalculatorService, PackageRateCalculatorService>();
            services.AddTransient<IHourlyRateCalculatorService, HourlyRateCalculatorService>();
            services.AddTransient<IPackageRatesRepository, PackageRatesRepository>();
            services.AddTransient<IHourlyRatesRepository, HourlyRatesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
