using ApplicationCore.Features.Entities;
using MediatR;

namespace ApplicationCore.Features.HousingLoan
{
    public class GetHousingLoanPaybackPlan : IRequest<PaybackPlan>
    {
        public LoanType LoanType => LoanType.Housing;

        public int LoanLengthInYears { get; }
        public decimal LoanAmount { get; }

        public GetHousingLoanPaybackPlan(decimal loanAmount, int loanLengthInYears)
        {
            LoanLengthInYears = loanLengthInYears;
            LoanAmount = loanAmount;
        }
    }
}
