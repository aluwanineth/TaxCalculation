using TaxCalculations.Domain.Commons;

namespace TaxCalculations.Domain.Entities;

public class TaxCalculation : AuditableBaseEntity
{
    public string PostalCode { get; set; }
    public double? AnnualIncome { get; set; }
    public double? TaxCalculationAmount { get; set;}
}
