using BirthdayWishes.Contract.DomainLogic.UseCases;
using BirthdayWishes.Dto.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BirthdayWishes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageQueueController : ControllerBase
    {
        [HttpPost]
        [Route("execute-new-messages")]
        public async Task ExecuteNewMessages([FromServices] IMessageQueueUseCase messageQueue, [Required] [FromHeader] int limit, CancellationToken cancellationToken)
        {
            var executingStatuses = new List<byte>
            {
                (byte) MessageStatusEnum.New
            };
            await messageQueue.ExecuteQueue(executingStatuses, limit, cancellationToken).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("execute-sent-messages")]
        public async Task ExecuteSentMessages([FromServices] IMessageQueueUseCase messageQueue, [Required][FromHeader] int limit, CancellationToken cancellationToken)
        {
            var executingStatuses = new List<byte>
            {
                (byte) MessageStatusEnum.Sent
            };
            await messageQueue.ExecuteQueue(executingStatuses, limit, cancellationToken).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("execute-failed-messages")]
        public async Task ExecuteFailedMessages([FromServices] IMessageQueueUseCase messageQueue, [Required][FromHeader] int limit, CancellationToken cancellationToken)
        {
            var executingStatuses = new List<byte>
            {
                (byte) MessageStatusEnum.Failed
            };
            await messageQueue.ExecuteQueue(executingStatuses, limit, cancellationToken).ConfigureAwait(false);
        }
    }
}
