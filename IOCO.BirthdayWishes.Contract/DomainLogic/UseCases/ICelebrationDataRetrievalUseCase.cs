using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOCO.BirthdayWishes.Contract.DomainLogic.UseCases
{
    public interface ICelebrationDataRetrievalUseCase
    {
        Task DoEmployeeBirthdayDataRetrievalAsync(CancellationToken cancellationToken = default);
        Task DoEmployeeStartingWorkDataRetrievalAsync(CancellationToken cancellationToken = default);
    }
}
