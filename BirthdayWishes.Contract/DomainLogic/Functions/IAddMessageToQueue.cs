using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BirthdayWishes.DomainObjects;
using BirthdayWishes.Dto;
using BirthdayWishes.Dto.Enumerations;

namespace BirthdayWishes.Contract.DomainLogic.Functions
{
    public interface IAddMessageToQueue
    {
        Task AddMessage(List<EmployeeDto> employeeDtos, MessageStatusEnum messageStatus,
            MessageTypeEnum messageType, CancellationToken cancellationToken = default);
    }
}
