using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PerformanceMonitor.API.Domain.Repository;
using System.Diagnostics;

namespace PerformanceMonitor.API
{
    public class Startup
    {
        private int _processId;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _processId = Process.GetCurrentProcess().Id;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IUserRepository, FakeUserRepository>();
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

            InitializePerformanceMonitor();
        }

        private void InitializePerformanceMonitor()
        {
            InitializeMonitor();
            InitializeCollector();
        }

        private void InitializeMonitor()
        {
            Process.Start(@"dotnet-counters.exe", @$"monitor --refresh-interval 1 -p {_processId}");
        }

        private void InitializeCollector()
        {
            Process.Start(@"dotnet-counters.exe", @$"collect --process-id {_processId} --refresh-interval 1 --format json");
        }
    }
}
