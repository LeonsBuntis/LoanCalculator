using System;
using System.Threading.Tasks;

namespace LoanCalculator.Features.Loans
{
    public class InterestRepository : IInterestRepository
    {
        public async Task<decimal> GetYearlyInterestRate(LoanType loanType)
        {
            switch (loanType)
            {
                case LoanType.Housing:
                    return 0.035m;
                default:
                    throw new ArgumentException($"Unsupported loan type: {loanType}", nameof(loanType));
            }
        }
    }
}
