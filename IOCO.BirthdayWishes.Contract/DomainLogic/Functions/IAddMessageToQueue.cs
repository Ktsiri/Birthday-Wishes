using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IOCO.BirthdayWishes.DomainObjects;
using IOCO.BirthdayWishes.Dto;
using IOCO.BirthdayWishes.Dto.Enumerations;

namespace IOCO.BirthdayWishes.Contract.DomainLogic.Functions
{
    public interface IAddMessageToQueue
    {
        Task AddMessage(List<EmployeeDto> employeeDtos, MessageStatusEnum messageStatus,
            MessageTypeEnum messageType, CancellationToken cancellationToken = default);
    }
}
