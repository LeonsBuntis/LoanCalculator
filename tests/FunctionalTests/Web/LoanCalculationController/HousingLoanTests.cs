using ApplicationCore.Features.Entities;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnitTests.ApplicationCore.Features
{
    public class HousingLoanTests
    {
        private WebAppFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new WebAppFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task CallingLoanCalculation_ReturnsPaybackPlan()
        {
            var amount = 100000;
            var years = 5;

            var response = await _client.GetAsync($"loancalculation?loanAmount={amount}&loanLengthInYears={years}");

            Assert.IsTrue(response.IsSuccessStatusCode);

            var plan = JsonConvert.DeserializeObject<PaybackPlan>(await response.Content.ReadAsStringAsync());

            var expectedMonthlyPayment = 1819.17m;
            var expectedMonths = 60;
            var expectedAmount = 109150.47m;

            Assert.AreEqual(expectedMonthlyPayment, Math.Round(plan.Payments.First(), 2));
            Assert.AreEqual(expectedMonths, plan.Payments.Count());
            Assert.AreEqual(expectedAmount, Math.Round(plan.Payments.Sum(), 2));
        }
    }
}
