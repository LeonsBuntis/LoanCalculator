using ApplicationCore.Features.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IInterestRepository
    {
        Task<decimal> GetYearlyInterestRate(LoanType loanType);
    }
}
