using ApplicationCore.Features.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InterestRepository : IInterestRepository
    {
        private readonly IDictionary<LoanType, decimal> _inmemoryDb = new Dictionary<LoanType, decimal>()
        {
            { LoanType.Housing, 0.035m }
        };

        public async Task<decimal> GetYearlyInterestRate(LoanType loanType)
        {
            if (!_inmemoryDb.TryGetValue(loanType, out decimal interestRate))
            {
                throw new ArgumentException($"Unsupported loan type: {loanType}", nameof(loanType));
            }

            return interestRate;
        }
    }
}
