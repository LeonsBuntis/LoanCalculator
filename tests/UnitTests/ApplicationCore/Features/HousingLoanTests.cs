using ApplicationCore.Features.HousingLoan;
using ApplicationCore.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.ApplicationCore.Features
{
    public class HousingLoanTests
    {
        [TestCase(345788, 1, 29364.88, 12, 352378.57)]
        [TestCase(100000, 5, 1819.17, 60, 109150.47)]
        [TestCase(1000000, 30, 4490.45, 360, 1616560.88)]
        public async Task GetHousingLoanPaybackPlan_ShouldReturnMonthlyPaybackPlan(
            decimal amount, 
            int years, 
            decimal expectedMonthlyPayment,
            decimal expectedMonths,
            decimal expectedAmount)
        {
            var query = new GetHousingLoanPaybackPlan(amount, years);

            var interestRepositoryMock = new Mock<IInterestRepository>();
            interestRepositoryMock
                .Setup(r => r.GetYearlyInterestRate(query.LoanType))
                .ReturnsAsync(0.035m);

            var handler = new GetHousingLoanPaybackPlanHandler(interestRepositoryMock.Object);
            var plan = await handler.Handle(query, CancellationToken.None);


            Assert.AreEqual(expectedMonthlyPayment, Math.Round(plan.Payments.First(), 2));
            Assert.AreEqual(expectedMonths, plan.Payments.Count());
            Assert.AreEqual(expectedAmount, Math.Round(plan.Payments.Sum(), 2));
        }

        [TestCase(1, 1)]
        [TestCase(100000, 5)]
        [TestCase(1000000, 30)]
        public async Task GetHousingLoanPaybackPlanValidator_ShouldAllow(
            decimal amount,
            int years)
        {
            var validator = new GetHousingLoanPaybackPlanValidator();

            var query = new GetHousingLoanPaybackPlan(amount, years);
            var result = validator.Validate(query);

            Assert.IsTrue(result.IsValid);
        }

        [TestCase(0, 0)]
        [TestCase(1000001, 31)]
        public async Task GetHousingLoanPaybackPlanValidator_ShouldDeny(
            decimal amount,
            int years)
        {
            var validator = new GetHousingLoanPaybackPlanValidator();

            var query = new GetHousingLoanPaybackPlan(amount, years);
            var result = validator.Validate(query);

            Assert.IsFalse(result.IsValid);
        }
    }
}
