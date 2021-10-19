using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanCalculationController : ControllerBase
    {
        public class PaybackPlan
        {
            public IEnumerable<decimal> Payments { get; init; }
        }

        [HttpGet]
        public async Task<PaybackPlan> GetPaybackPlan(decimal amount, int years)
        {
            return await CalculatePaybackAsync(amount, years);
        }

        public static async Task<PaybackPlan> CalculatePaybackAsync(decimal amount, int years)
        {
            var totalMonths = years * 12;
            double yearlyInterestRate = 0.035;
            double monthlyInterestRate = yearlyInterestRate / 12;

            double r1 = monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalMonths);
            double r2 = Math.Pow(1 + monthlyInterestRate, totalMonths) - 1;
            var monthlyPayment = (double)amount * (r1 / r2);

            var rounding = ((decimal)Math.Round((monthlyPayment * 10000), 0)) / 10000;

            var payments = new List<decimal>();

            for (int i = 0; i < totalMonths; i++)
            {
                payments.Add(rounding);
            }

            return new PaybackPlan() { Payments = payments };
        }
    }
}
