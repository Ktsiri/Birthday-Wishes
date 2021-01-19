using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BirthdayWishes.Common.Services;
using BirthdayWishes.Contract.ApiClient.Requests;
using BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using BirthdayWishes.Contract.DomainLogic.Retreivers;
using BirthdayWishes.Contract.DomainLogic.UseCases;
using BirthdayWishes.DomainObjects;
using BirthdayWishes.Dto.Enumerations;

namespace BirthdayWishes.DomainLogic.UseCases
{
    [RegisterClassDependency(typeof(IMessageQueueUseCase))]
    sealed class MessageQueueUseCase : IMessageQueueUseCase
    {
        private readonly IMessageQueueRepository _messageQueueRepository;
        private readonly IActionRetriever _actionRetriever;
        private readonly IEmployeeExclusionRequest _employeeExclusionRequest;

        public MessageQueueUseCase(IMessageQueueRepository messageQueueRepository, IActionRetriever actionRetriever,
            IEmployeeExclusionRequest employeeExclusionRequest)
        {
            _messageQueueRepository = messageQueueRepository;
            _actionRetriever = actionRetriever;
            _employeeExclusionRequest = employeeExclusionRequest;
        }

        public async Task ExecuteQueue(List<byte> messageStatuses,
            int limit = 30, CancellationToken cancellationToken = default)
        {
            await _messageQueueRepository.ExecuteTransactions(action: async () =>
            {
                var queued = await _messageQueueRepository.GetMessagesToProcess(messageStatuses, limit, cancellationToken);
                var messageQueues = queued.ToList();
                if (messageQueues.Count == 0)
                {
                    return;
                }

                foreach (var queue in messageQueues)
                {
                    try
                    {
                        if (await _employeeExclusionRequest.IsEmployeeExcludedForCommunication(queue.SystemUniqueId, cancellationToken))
                        {
                            continue;
                        }
                        var action = _actionRetriever.GetAction((byte)queue.MessageStatus, (byte)queue.MessageType);
                        var actionResults = await action.PerformAction(queue);

                        if (actionResults.Item1.Success)
                        {
                            await _messageQueueRepository.Update(actionResults.Item2);
                        }
                        else
                        {
                            actionResults.Item2.RetryCount++;

                            await _messageQueueRepository.Update(actionResults.Item2);
                        }
                    }
                    catch (Exception ex)
                    {
                        // TODO: log an exception
                    }
                    finally
                    {
                        // revert the IsBusyProcessing column back to false - else they won't be picked up again by the next job.
                        queue.IsBusyProcessing = false;
                        queue.UpdatedDate = DateTime.UtcNow;
                        await _messageQueueRepository.Update(queue);
                    }
                }
            }, cancellationToken: cancellationToken);
        }
    }
}
