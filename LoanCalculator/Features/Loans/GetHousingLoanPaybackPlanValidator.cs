using FluentValidation;

namespace LoanCalculator.Features.Loans
{
    public class GetHousingLoanPaybackPlanValidator : AbstractValidator<GetHousingLoanPaybackPlan>
    {
        public GetHousingLoanPaybackPlanValidator()
        {
            RuleFor(r => r.LoanLengthInYears)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(30);

            RuleFor(r => r.LoanAmount)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(1000000);
        }
    }
}
