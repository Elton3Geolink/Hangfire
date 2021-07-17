using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace HangFire.InMemory.Jobs
{
    public class JobFactoryContinuation
    {
        [DisableConcurrentExecution(timeoutInSeconds: 60)]
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public void Executar()
        {
            string jobId = BackgroundJob.Schedule(() => JobPai(), TimeSpan.FromSeconds(2));
            string jobFilhoId = BackgroundJob.ContinueWith(jobId, () => jobFilho(), JobContinuationOptions.OnlyOnSucceededState);
            string jobNetoId = BackgroundJob.ContinueWith(jobFilhoId, () => jobNeto(), JobContinuationOptions.OnlyOnSucceededState);

            //EXECUTAR O QUE FOR NECESSARIO
            Thread.Sleep(5);
            
        }

        public void JobPai()
        {
            Console.WriteLine($"*******************     {DateTime.Now.ToString("dd/MM/yy HH:mm")} - JobPai - Inicio");

            //Processar o necessario
            Thread.Sleep(5);
            Console.WriteLine($"*******************     {DateTime.Now.ToString("dd/MM/yy HH:mm")} - JobPai - Fim");
        }

        public void jobFilho()
        {
            Console.WriteLine($"*******************     {DateTime.Now.ToString("dd/MM/yy HH:mm")} - Filho - Inicio");
            
            //Processar o necessario
            Thread.Sleep(2);
            Console.WriteLine($"*******************     {DateTime.Now.ToString("dd/MM/yy HH:mm")} - Filho - Fim");
        }

        public void jobNeto()
        {
            Console.WriteLine($"*******************     {DateTime.Now.ToString("dd/MM/yy HH:mm")} - JobNeto - Inicio");

            //Processar o necessario
            Thread.Sleep(6);
            Console.WriteLine($"*******************     {DateTime.Now.ToString("dd/MM/yy HH:mm")} - JobNeto - Fim");
        }
    }
}
