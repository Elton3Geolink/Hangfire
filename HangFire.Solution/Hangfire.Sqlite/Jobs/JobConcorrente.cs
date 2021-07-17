using System;
using System.Threading;

namespace Hangfire.Sqlite.Jobs
{
    public class JobConcorrente
    {
        //[DisableConcurrentExecution(timeoutInSeconds: 25)]
        [AutomaticRetry(Attempts = 4, DelaysInSeconds = new int[] { 5 }, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        //[MaximumConcurrentExecutions(1, 25)]
        public void Executar()
        {
            Guid idExecucao = Guid.NewGuid();

            Console.WriteLine($"------> {idExecucao} --- {DateTime.Now.ToString("HH:mm:ss")} INICIO - JobConcorrente");

            throw new Exception("Teste exception forçada");

            Thread.Sleep(TimeSpan.FromSeconds(10));

            Console.WriteLine($"------> {idExecucao} --- {DateTime.Now.ToString("HH:mm:ss")} FIM - JobConcorrente");
        }
    }
}
