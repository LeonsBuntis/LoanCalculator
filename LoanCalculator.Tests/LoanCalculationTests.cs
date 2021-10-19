using LoanCalculator.Controllers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoanCalculator.Tests
{
    public class LoanCalculationTests
    {
        [Test]
        public async Task CalculatePayback_ShouldReturnMonthlyPaybackPlan()
        {
            var amount = 10000;
            var years = 5;

            var plan = await LoanCalculationController.CalculatePaybackAsync(amount, years);

            var expectedAmount = 10915m;
            var expectedMonthlyPayment = 182m;

            Assert.AreEqual(expectedAmount, Math.Round(plan.Payments.Sum(), 0));
            Assert.AreEqual(expectedMonthlyPayment, Math.Round(plan.Payments.First(), 0));
        }
    }
}
