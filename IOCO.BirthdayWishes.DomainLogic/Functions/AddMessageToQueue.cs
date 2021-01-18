using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BirthdayWishes.Common.Converters;
using BirthdayWishes.Common.Services;
using BirthdayWishes.Contract.DomainLogic.Functions;
using BirthdayWishes.Contract.DomainLogic.Retreivers;
using BirthdayWishes.DomainObjects;
using BirthdayWishes.Dto;
using BirthdayWishes.Dto.Enumerations;

namespace BirthdayWishes.DomainLogic.Functions
{
    [RegisterClassDependency(typeof(IAddMessageToQueue))]
    sealed class AddMessageToQueue : IAddMessageToQueue
    {
        private readonly IMessageQueueRepository _messageQueueRepository;

        public AddMessageToQueue(IMessageQueueRepository messageQueueRepository)
        {
            _messageQueueRepository = messageQueueRepository;
        }

        public async Task AddMessage(List<EmployeeDto> employeeDtos, MessageStatusEnum messageStatus, 
            MessageTypeEnum messageType, CancellationToken cancellationToken = default)
        {
            if (!employeeDtos.Any())
            {
                return;
            }

            foreach (var employee in employeeDtos)
            {
                var messageExists = await _messageQueueRepository.GetBySystemUniqueId(employee.Id, messageType);
                if (messageExists != null)
                {
                    continue;
                }

                var message = new MessageQueue
                {
                    SystemUniqueId = employee.Id,
                    SourceRawJson = JsonConverter.ConvertToJson(employee),
                    IsBusyProcessing = false,
                    RetryCount = -1,
                    MessageStatus = messageStatus,
                    MessageType = messageType,
                    CreatedDate = DateTime.UtcNow
                };
                _messageQueueRepository.Add(message);
            }

            await _messageQueueRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
