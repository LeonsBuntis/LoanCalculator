using LoanCalculator.Features.Loans;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LoanCalculator.Controllers
{
    public interface ILoanPaybackPlanFactory
    {
        IRequest<PaybackPlan> CreateCommand(decimal loanAmount, int loanLengthInYears, LoanType loanType);
    }

    public class LoanPaybackPlanFactory : ILoanPaybackPlanFactory
    {
        public IRequest<PaybackPlan> CreateCommand(decimal loanAmount, int loanLengthInYears, LoanType loanType)
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

    [ApiController]
    [Route("[controller]")]
    public class LoanCalculationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoanPaybackPlanFactory _commandFactory;

        public LoanCalculationController(IMediator mediator, ILoanPaybackPlanFactory commandFactory)
        {
            _mediator = mediator;
            _commandFactory = commandFactory;
        }

        [HttpGet]
        public async Task<PaybackPlan> GetPaybackPlan(decimal loanAmount, int loanLengthInYears, LoanType loanType)
        {
            var command = _commandFactory.CreateCommand(loanAmount, loanLengthInYears, loanType);

            return await _mediator.Send(command);
        }
    }
}
