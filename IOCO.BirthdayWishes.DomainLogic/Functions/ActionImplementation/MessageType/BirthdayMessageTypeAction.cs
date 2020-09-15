using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IOCO.BirthdayWishes.Common.Converters;
using IOCO.BirthdayWishes.Common.Services;
using IOCO.BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using IOCO.BirthdayWishes.Contract.SmtpClient;
using IOCO.BirthdayWishes.DomainObjects;
using IOCO.BirthdayWishes.Dto;
using IOCO.BirthdayWishes.Dto.Enumerations;

namespace IOCO.BirthdayWishes.DomainLogic.Functions.ActionImplementation.MessageType
{
    [RegisterClassDependency(typeof(IActionImplementation))]
    sealed class BirthdayMessageTypeAction : IActionImplementation
    {
        private readonly IActionTemplate _actionTemplate;
        private readonly ISendEmailHelper _sendEmailHelper;
        public byte ActionId => (byte)MessageTypeEnum.Birthday;

        public BirthdayMessageTypeAction(IActionTemplate actionTemplate, ISendEmailHelper sendEmailHelper)
        {
            _actionTemplate = actionTemplate;
            _sendEmailHelper = sendEmailHelper;
        }
        public async Task<Tuple<ResponseMessageDto, MessageQueue>> PerformAction(MessageQueue messageQueue)
        {
            return await _actionTemplate.ExecuteActionTemplate(Action, messageQueue);
        }

        private async Task<Tuple<ResponseMessageDto, MessageQueue>> Action(MessageQueue messageQueue)
        {
            var employee = JsonConverter.ConvertFromJson<EmployeeDto>(messageQueue.SourceRawJson);
            // TODO: introduce a template
            var message = $"Happy birthday {employee.Name}, {employee.LastName}";

            var sendEmail = await _sendEmailHelper.SendEmailAsync("khomotso.tsiri@ioco.tech", message,
                "Birthday Wishes", true, false);

            if (sendEmail)
            {
                return new Tuple<ResponseMessageDto, MessageQueue>(new ResponseMessageDto
                {
                    Success = true
                }, messageQueue);
            }
            else
            {
                return new Tuple<ResponseMessageDto, MessageQueue>(new ResponseMessageDto
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "Email could not be sent"
                    }
                }, messageQueue);
            }
        }
    }
}
