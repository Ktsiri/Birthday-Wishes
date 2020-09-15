using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IOCO.BirthdayWishes.Common.Services;
using IOCO.BirthdayWishes.Contract.ApiClient.Requests;
using IOCO.BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using IOCO.BirthdayWishes.Contract.DomainLogic.Retreivers;
using IOCO.BirthdayWishes.Contract.DomainLogic.UseCases;
using IOCO.BirthdayWishes.DomainObjects;
using IOCO.BirthdayWishes.Dto.Enumerations;

namespace IOCO.BirthdayWishes.DomainLogic.UseCases
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

        public async Task ProcessBaseQueue(List<byte> messageStatuses,
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
                var tasks = messageQueues.Select(message => Task.Run(async () =>
                {
                    while (message.IsBusyProcessing)
                    {
                        try
                        {
                            if (await _employeeExclusionRequest.IsEmployeeExcludedForCommunication(message.SystemUniqueId, cancellationToken))
                            {
                                continue;
                            }
                            var action = _actionRetriever.GetAction((byte)message.MessageStatus);
                            var actionResults = await action.PerformAction(message);

                            if (actionResults.Item1.Success)
                            {
                                await _messageQueueRepository.Update(actionResults.Item2);
                            }
                            else 
                            {
                                message.RetryCount++;
                            }
                        }
                        catch (Exception ex)
                        {
                            // TODO: log an exception
                        }
                        finally
                        {
                            // revert the IsBusyProcessing column back to false - else they won't be picked up again by the next job.
                            message.IsBusyProcessing = false;
                            message.UpdatedDate = DateTime.UtcNow;
                            await _messageQueueRepository.Update(message);
                        }
                    }
                }, cancellationToken))
                    .ToList();
                await Task.WhenAll(tasks);
            }, cancellationToken: cancellationToken);
        }
    }
}
