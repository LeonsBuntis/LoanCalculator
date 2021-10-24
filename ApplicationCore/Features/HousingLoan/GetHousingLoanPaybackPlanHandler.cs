using ApplicationCore.Features.Entities;
using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Features.HousingLoan
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

        private IEnumerable<decimal> CalculateSeriesMonthlyPayments(decimal loanAmount, int loanLengthInYears, decimal yearlyInterestRate)
        {
            double totalMonths = loanLengthInYears * 12;
            double monthlyInterestRate = (double)yearlyInterestRate / 12;

            double r1 = monthlyInterestRate * Math.Pow((1 + monthlyInterestRate), totalMonths);
            double r2 = Math.Pow((1 + monthlyInterestRate), totalMonths) - 1;
            double monthlyPayment = (double)loanAmount * (r1 / r2);

            decimal rounding = (decimal)Math.Round(monthlyPayment, 4);

            var payments = new List<decimal>();

            for (int i = 0; i < totalMonths; i++)
            {
                payments.Add(rounding);
            }

            return payments;
        }
    }
}
