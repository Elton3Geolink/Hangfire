using Hangfire;
using Hangfire.MemoryStorage;
using HangFire.InMemory.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HangFire.InMemory
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

            //Registrar o servico do hangfire
            services.AddHangfire(op => {

                //Configurar o hangfire para trabalhar em memoria
                op.UseMemoryStorage();
            });

            //Subir o mid
            services.AddHangfireServer();
         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Subir a dashboard do Hangfire
            app.UseHangfireDashboard();

            #region Jobs Fire and forget


            //string jobId = BackgroundJob.Enqueue(() => Console.WriteLine("************             Job tipo fire and forget        ****************"));
            //Console.WriteLine($"JobId: {jobId}");
            //BackgroundJob.Enqueue(() => new JobTeste1().Executar());

            #endregion Jobs Fire and forget


            #region delayded Jobs

            //BackgroundJob.Schedule(() => new JobTeste1().Executar(), TimeSpan.FromSeconds(5));            

            #endregion delayded Jobs






            #region Continuation JOBS

            //JobTeste1 job = new JobTeste1();

            //string jobId = BackgroundJob.Enqueue(() => job.Executar());

            //BackgroundJob.ContinueJobWith(jobId, () => job.Continue(jobId));

            #endregion Continuation JOBS








            #region RECURING JOBS

            //var cronMinuto = Cron.Minutely();
            //var conMinutoInterval = Cron.MinuteInterval(1);


            //RecurringJob.AddOrUpdate("JobConcorrente", () => new JobConcorrente().Executar(), Cron.Hourly());                        


            //Alternativa para serializacao de Jobs (Continuation) de forma recorrente sem utilizar a versao PRO do Hangfire

            RecurringJob.AddOrUpdate("JobRecorrente", () => new JobFactoryContinuation().Executar(), Cron.Minutely);


            #endregion RECURING JOBS





        }

        string CronSegundo(int intervaloEmSegundos)
        {
            //"*/5 * * * * *"
            return $"*/{intervaloEmSegundos} * * * * *";

        }
    }
}
