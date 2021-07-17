using Hangfire.MemoryStorage;
using Hangfire.Sqlite.Jobs;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfire.Sqlite
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

            var ambiente = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if(ambiente.ToLower() == "development")
            {
                services.AddHangfire(op =>
                {
                    //Configurar o hangfire para trabalhar em memoria
                        op.UseMemoryStorage();                    
                });                
            }
            else
            {
                services.AddHangfire(op => {
                    op.UseSQLiteStorage();
                });
            }

            services.AddHangfireServer();            




            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hangfire.Sqlite", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hangfire.Sqlite v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
            app.UseHangfireDashboard();


            var cronMinuto = Cron.Minutely();
            var conMinutoInterval = Cron.MinuteInterval(1);

            BackgroundJob.Enqueue(() => new JobConcorrente().Executar());



            //RecurringJob.AddOrUpdate("JobTeste1",() => Console.WriteLine("RecuringJob"), Cron.Minutely);
        }
    }
}
