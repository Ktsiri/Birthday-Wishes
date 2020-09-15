using IOCO.BirthdayWishes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOCO.BirthdayWishes.Contract.DomainLogic.Rules
{
    public interface IInternalValidator<in T>
    {
        Task<ValidationResponseDto> ValidateWithResultAsync(T objectToValidate,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
