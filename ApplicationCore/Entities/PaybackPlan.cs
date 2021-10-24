using System.Collections.Generic;

namespace ApplicationCore.Features.Entities
{
    public class PaybackPlan
    {
        public IEnumerable<decimal> Payments { get; init; }
    }
}
