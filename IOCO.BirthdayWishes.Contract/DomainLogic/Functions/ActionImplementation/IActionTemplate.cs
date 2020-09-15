using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IOCO.BirthdayWishes.DomainObjects;
using IOCO.BirthdayWishes.Dto;

namespace IOCO.BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation
{
    public interface IActionTemplate
    {
        Task<Tuple<ResponseMessageDto, MessageQueue>> ExecuteActionTemplate(Func<MessageQueue, Task<Tuple<ResponseMessageDto, MessageQueue>>> action,
            MessageQueue messageQueue);
    }
}
