using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BirthdayWishes.Common.Converters;
using BirthdayWishes.Common.Services;
using BirthdayWishes.Contract.DomainLogic.Functions.ActionImplementation;
using BirthdayWishes.Contract.SmtpClient;
using BirthdayWishes.DomainObjects;
using BirthdayWishes.Dto;
using BirthdayWishes.Dto.Enumerations;

namespace BirthdayWishes.DomainLogic.Functions.ActionImplementation.MessageType
{
    [RegisterClassDependency(typeof(IActionImplementation))]
    sealed class StartingWorkMessageTypeAction : IActionImplementation
    {
        private readonly IActionTemplate _actionTemplate;
        private readonly ISendEmailHelper _sendEmailHelper;
        public byte ActionId => (byte)MessageTypeEnum.StartingWork;

        public StartingWorkMessageTypeAction(IActionTemplate actionTemplate, ISendEmailHelper sendEmailHelper)
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
            var message = $"Welcome to iOCO {employee.Name}, {employee.LastName}, we are so proud to have you on board";

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
