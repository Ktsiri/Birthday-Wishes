using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BirthdayWishes.Common.Services;
using BirthdayWishes.Contract.ApiClient.Requests;
using BirthdayWishes.Contract.DomainLogic.Functions;
using BirthdayWishes.Contract.DomainLogic.UseCases;
using BirthdayWishes.Dto.Enumerations;

namespace BirthdayWishes.DomainLogic.UseCases
{
    [RegisterClassDependency(typeof(ICelebrationDataRetrievalUseCase))]
    sealed class CelebrationDataRetrievalUseCase : ICelebrationDataRetrievalUseCase
    {
        private readonly ICelebrationRequest _celebrationRequest;
        private readonly IAddMessageToQueue _addMessageToQueue;

        public CelebrationDataRetrievalUseCase(ICelebrationRequest celebrationRequest, IAddMessageToQueue addMessageToQueue)
        {
            _celebrationRequest = celebrationRequest;
            _addMessageToQueue = addMessageToQueue;
        }
        public async Task DoEmployeeBirthdayDataRetrievalAsync(CancellationToken cancellationToken = default)
        {
            var getBirthdayForToday = await _celebrationRequest.GetEmployeeBirthdayForToday(DateTime.Today, cancellationToken);

            if(!getBirthdayForToday.Any())
            {
                return;
            }

            await _addMessageToQueue.AddMessage(getBirthdayForToday, MessageStatusEnum.New, 
                MessageTypeEnum.Birthday, cancellationToken);
        }
        public async Task DoEmployeeStartingWorkDataRetrievalAsync(CancellationToken cancellationToken = default)
        {
            var getEmployeeStartingWork = await _celebrationRequest.GetEmployeeStartingWorkToday(DateTime.Today, cancellationToken);

            if (!getEmployeeStartingWork.Any())
            {
                return;
            }

            await _addMessageToQueue.AddMessage(getEmployeeStartingWork, MessageStatusEnum.New,
                MessageTypeEnum.StartingWork, cancellationToken);
        }
    }
}
