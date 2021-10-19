using LoanCalculator.Controllers;
using NUnit.Framework;
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

            var result = plan.Payments.Sum();

            Assert.AreEqual(amount, result);
        }
    }
}
