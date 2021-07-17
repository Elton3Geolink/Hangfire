using Hangfire.Sqlite.Jobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfire.Sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        

        [HttpPost]
        [Route("v1/recuring-job")]
        public async Task<IActionResult> Post(string jobId, int intervaloEmMinutos)
        {
            RecurringJob.AddOrUpdate(jobId, () => new JobConcorrente().Executar(), Cron.MinuteInterval(intervaloEmMinutos));            
            
            return Ok($"Job '{jobId}' criado com sucesso!!!");
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(string jobId)
        {
            RecurringJob.RemoveIfExists(jobId);

            return Ok($"Job '{jobId}' excluido com sucesso!!!");
        }

        [HttpPost]
        [Route("v1/background-job")]
        public async Task<IActionResult> PostBackGrountJob(int delayEmSegundo)
        {            

            BackgroundJob.Schedule(() => new JobConcorrente().Executar(), TimeSpan.FromSeconds(delayEmSegundo));

            return Ok();
        }

    }
}
