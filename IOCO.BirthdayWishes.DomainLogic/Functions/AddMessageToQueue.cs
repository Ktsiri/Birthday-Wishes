using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IOCO.BirthdayWishes.Common.Converters;
using IOCO.BirthdayWishes.Common.Services;
using IOCO.BirthdayWishes.Contract.DomainLogic.Functions;
using IOCO.BirthdayWishes.Contract.DomainLogic.Retreivers;
using IOCO.BirthdayWishes.DomainObjects;
using IOCO.BirthdayWishes.Dto;
using IOCO.BirthdayWishes.Dto.Enumerations;

namespace IOCO.BirthdayWishes.DomainLogic.Functions
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
