using MediatR;

namespace LoanCalculator.Features.Loans
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
