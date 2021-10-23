using System.Collections.Generic;

namespace LoanCalculator.Features.Loans
{
    public class PaybackPlan
    {
        public IEnumerable<decimal> Payments { get; init; }
    }
}
