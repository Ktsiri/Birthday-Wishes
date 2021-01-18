using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BirthdayWishes.Contract.DomainLogic.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayWishes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDataRetrievalController : ControllerBase
    {
        [HttpPost]
        [Route("extract-employees-birthday-today")]
        public async Task EmployeeBirthdayDataRetrieval([FromServices] ICelebrationDataRetrievalUseCase celebrationDataRetrievalUseCase, CancellationToken cancellationToken)
        {
            await celebrationDataRetrievalUseCase.DoEmployeeBirthdayDataRetrievalAsync(cancellationToken).ConfigureAwait(false);
        }
        [HttpPost]
        [Route("extract-employees-starting-work-today")]
        public async Task EmployeeStartingWorkDataRetrieval([FromServices] ICelebrationDataRetrievalUseCase celebrationDataRetrievalUseCase, CancellationToken cancellationToken)
        {
            await celebrationDataRetrievalUseCase.DoEmployeeStartingWorkDataRetrievalAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}