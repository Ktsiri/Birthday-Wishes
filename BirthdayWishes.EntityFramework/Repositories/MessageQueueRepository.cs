using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BirthdayWishes.Common.Services;
using BirthdayWishes.Contract.DomainLogic.Retreivers;
using BirthdayWishes.DomainObjects;
using BirthdayWishes.Dto.Enumerations;
using BirthdayWishes.EntityFramework.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BirthdayWishes.EntityFramework.Repositories
{
    [RegisterClassDependency(typeof(IMessageQueueRepository))]
    sealed class MessageQueueRepository : IMessageQueueRepository
    {
        private readonly DomainContext _context;

        public MessageQueueRepository(DomainContext context)
        {
            _context = context;
        }
        public async Task ExecuteTransactions(Func<Task> action, CancellationToken cancellationToken = default)
        {
            // Use of an EF Core resiliency strategy when using multiple DbContexts
            // within an explicit BeginTransaction():
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = _context.Database.BeginTransaction();
                try
                {
                    await action();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            });
        }
        public async Task<IQueryable<MessageQueue>> GetMessagesToProcess(List<byte> messageStatuses, int limit = 30, 
            CancellationToken cancellationToken = default)
        {
            var commaSeparatedMessageStatuses = string.Join(',', messageStatuses);

            FormattableString sqlQuery = $"SELECT TOP ({limit}) * FROM [BirthdayWishes].[dbo].[MessageQueue] bw WITH (XLOCK) WHERE bw.IsBusyProcessing = 0 AND (bw.MessageStatus in ({commaSeparatedMessageStatuses})) ORDER BY bw.MessageType, bw.CreatedDate asc";

            var records = _context.Set<MessageQueue>().FromSqlInterpolated(sqlQuery).ToList();

            if (records.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (var r in records)
                {
                    r.IsBusyProcessing = true;
                }

                await Update(records);
            }

            return records.AsQueryable();
        }

        public async Task<MessageQueue> GetBySystemUniqueId(string systemUniqueId, MessageTypeEnum messageType)
        {
            var query = _context.Set<MessageQueue>();

            return await query.FirstOrDefaultAsync(x => x.SystemUniqueId == systemUniqueId && x.MessageType == messageType);
        }
        public MessageQueue Add(MessageQueue messageQueue)
        {
            _context.Add(messageQueue);
            return messageQueue;
        }


        public async Task<MessageQueue> Update(MessageQueue messageQueue)
        {
            await using var transaction = _context.Database.CurrentTransaction.GetDbTransaction();
            _context.Update(messageQueue);
            await _context.SaveChangesAsync(true);

            return messageQueue;
        }

        public async Task<List<MessageQueue>> Update(List<MessageQueue> messageQueued)
        {
            _context.UpdateRange(messageQueued);
            await _context.SaveChangesAsync(true);

            return messageQueued.ToList();
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int rowsAffected = await _context.SaveChangesAsync(cancellationToken);

            return rowsAffected;
        }
    }
}
