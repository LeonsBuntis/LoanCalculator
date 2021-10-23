using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoanCalculator.Features.Loans
{
    public class GetHousingLoanPaybackPlanHandler : IRequestHandler<GetHousingLoanPaybackPlan, PaybackPlan>
    {
        private readonly IInterestRepository _interestRepository;

        public GetHousingLoanPaybackPlanHandler(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }

        public async Task<PaybackPlan> Handle(GetHousingLoanPaybackPlan request, CancellationToken cancellationToken)
        {
            decimal yearlyInterestRate = await _interestRepository.GetYearlyInterestRate(request.LoanType);
            var monthlyPayments = CalculateSeriesMonthlyPayments(request.LoanAmount, request.LoanLengthInYears, yearlyInterestRate);

            return new PaybackPlan()
            {
                Payments = monthlyPayments
            };
        }

        public IEnumerable<decimal> CalculateSeriesMonthlyPayments(decimal loanAmount, int loanLengthInYears, decimal yearlyInterestRate)
        {
            double totalMonths = loanLengthInYears * 12;
            decimal monthlyInterestRate = yearlyInterestRate / 12;

            decimal r1 = monthlyInterestRate * (decimal)Math.Pow((double)(1m + monthlyInterestRate), totalMonths);
            decimal r2 = (decimal)Math.Pow((double)(1 + monthlyInterestRate), totalMonths) - 1;
            decimal monthlyPayment = loanAmount * (r1 / r2);

            var rounding = Math.Round((monthlyPayment * 10000), 0) / 10000;

            var payments = new List<decimal>();

            for (int i = 0; i < totalMonths; i++)
            {
                payments.Add(rounding);
            }

            return payments;
        }
    }
}
