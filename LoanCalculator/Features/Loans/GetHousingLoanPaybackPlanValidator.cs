using FluentValidation;

namespace LoanCalculator.Features.Loans
{
    public class GetHousingLoanPaybackPlanValidator : AbstractValidator<GetHousingLoanPaybackPlan>
    {
        public GetHousingLoanPaybackPlanValidator()
        {
            RuleFor(r => r.LoanLengthInYears)
                .GreaterThan(0)
                .LessThanOrEqualTo(30);

            RuleFor(r => r.LoanAmount)
                .GreaterThan(0)
                .LessThanOrEqualTo(1000000);
        }
    }
}
