using Hangfire;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HangFire.InMemory.Jobs
{
    public class JobTeste1
    {

        //Attemps = 0 -> Desativar retentativas
        //[AutomaticRetry(Attempts = 0, LogEvents = false, OnAttemptsExceeded = AttemptsExceededAction.Delete)]


        //Attempts: Numero de tentativas. OBS.: FUNCIONA

        //LogEvents: Ativa o desativa geracao de logs sejam eles em console, serilog e etc. OBS.: FUNCIONA

        //DelaysInSeconds: Configurar intervalos entre cada tentativa. Obs.: Nos testes realizado nao funcionou, ou seja, 
        //os intervalos setados nao surtiram efeito. Observei que o intevalo entre cada tentativa demorou certa de 15 segundos em media.
        //[AutomaticRetry(Attempts = 3, DelaysInSeconds = new int[] { 2, 5, 10 }, LogEvents = true)]

        //OnAttemptsExceeded: Atribuir o estado em que o job assumirá depois que todas as tentativas falharem:
        //Os valores possivies sao: Delete e Fail.
        //OBSERVACAO IMPORTANTE: As tarefas com falhas (AttemptsExceededAction.Fail) NÃO EXPIRAM. PORTANTO TAREFAS COM ESTE STATUS DEVERAM SER
        //EXCLUIDAS MANUALMENTE VIA DASHBOARD.
        //DEVE-SE AVALIAR CASO A CASO A NECESSIDADE DE SETAR COM FALHA OU DELETE
        //A VANTAGEM DE SETAR COMO FAIL É QUE É POSSÍVEL RE-EXECUTAR O JOB MANUALMENTE VIA DASHBOARD. NO ENTANTO, CASO 
        //O DASHBOARD NAO SEJA MONITORADO CONSTANTEMENTE SUGIRO SETAR O JOB COMO "DELETE"

        [AutomaticRetry(Attempts = 0, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Executar()
        {
            Console.WriteLine($"--------> {DateTime.Now.ToString("dd/MM/yy HH:mm:ss")} ---       Chamado JobTeste 1 ---       ****************");

            Thread.Sleep(TimeSpan.FromSeconds(10));

            //throw new Exception($"--> Teste exception - {DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}");

            
        }

        [AutomaticRetry(Attempts = 0, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Continue(string jobId)
        {
            Console.WriteLine($"--------> {DateTime.Now.ToString("dd/MM/yy HH:mm:ss")} ---   Continuando Job '{jobId}'      ****************");
        }
    }
}
