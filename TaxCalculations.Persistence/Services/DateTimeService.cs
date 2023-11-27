using TaxCalculations.Application.Contracts.Services;

namespace TaxCalculations.Persistence.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime CurrentDateTime => DateTime.Now;
}
