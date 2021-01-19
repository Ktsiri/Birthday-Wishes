using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BirthdayWishes.Contract.DomainLogic.UseCases
{
    public interface IMessageQueueUseCase
    {
        Task ExecuteQueue(List<byte> messageStatuses,
            int limit = 30, CancellationToken cancellationToken = default);
    }
}
