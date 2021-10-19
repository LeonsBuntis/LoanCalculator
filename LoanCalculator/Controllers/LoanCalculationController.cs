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

            var monthlyPayment = Math.Round(amount / totalMonths, 0);

            var payments = new List<decimal>();

            for (int i = 0; i < totalMonths - 1; i++)
            {
                payments.Add(monthlyPayment);
            }

            var lastPayment = amount - (monthlyPayment * (totalMonths - 1));

            payments.Add(lastPayment);

            return new PaybackPlan() { Payments = payments };
        }
    }
}
