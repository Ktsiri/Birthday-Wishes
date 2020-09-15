using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IOCO.BirthdayWishes.DomainObjects;
using IOCO.BirthdayWishes.Dto.Enumerations;

namespace IOCO.BirthdayWishes.Contract.DomainLogic.Retreivers
{
    public interface IMessageQueueRepository
    {
        Task ExecuteTransactions(Func<Task> action, CancellationToken cancellationToken = default);
        Task<IQueryable<MessageQueue>> GetMessagesToProcess(List<byte> messageStatuses, int limit = 30,
            CancellationToken cancellationToken = default);
        Task<MessageQueue> GetBySystemUniqueId(string systemUniqueId, MessageTypeEnum messageType);
        MessageQueue Add(MessageQueue messageQueue);
        Task<MessageQueue> Update(MessageQueue messageQueue);
        Task<List<MessageQueue>> Update(List<MessageQueue> messageQueued);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
