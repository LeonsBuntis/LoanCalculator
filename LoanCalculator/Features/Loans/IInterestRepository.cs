using System.Threading.Tasks;

namespace LoanCalculator.Features.Loans
{
    public interface IInterestRepository
    {
        Task<decimal> GetYearlyInterestRate(LoanType loanType);
    }
}
