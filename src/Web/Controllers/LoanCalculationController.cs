using ApplicationCore.Features.Entities;
using ApplicationCore.Features.HousingLoan;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanCalculationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoanCalculationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<PaybackPlan> GetPaybackPlan(decimal loanAmount, int loanLengthInYears, LoanType loanType)
        {
            var query = CreateQuery(loanAmount, loanLengthInYears, loanType);

            return await _mediator.Send(query);
        }

        public IRequest<PaybackPlan> CreateQuery(decimal loanAmount, int loanLengthInYears, LoanType loanType)
        {
            switch (loanType)
            {
                case LoanType.Housing:
                    return new GetHousingLoanPaybackPlan(loanAmount, loanLengthInYears);
                default:
                    throw new ArgumentException($"Unsupported loan type: {loanType}", nameof(loanType));
            }
        }
    }
}
