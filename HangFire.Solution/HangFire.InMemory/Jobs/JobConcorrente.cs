using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HangFire.InMemory.Jobs
{
    public class JobConcorrente
    {
        [DisableConcurrentExecution(timeoutInSeconds: 25)]
        [AutomaticRetry(Attempts = 0, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        //[MaximumConcurrentExecutions(1, 25)]
        public void Executar()
        {
            Guid idExecucao = Guid.NewGuid();

            Console.WriteLine($"------> {idExecucao} --- {DateTime.Now.ToString("HH:mm:ss")} INICIO - JobConcorrente");
            
            Thread.Sleep(TimeSpan.FromSeconds(20));

            Console.WriteLine($"------> {idExecucao} --- {DateTime.Now.ToString("HH:mm:ss")} FIM - JobConcorrente");
        }
    }
}
