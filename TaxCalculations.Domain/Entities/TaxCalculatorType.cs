using TaxCalculations.Domain.Commons;

namespace TaxCalculations.Domain.Entities;

public class TaxCalculatorType : AuditableBaseEntity
{
    public string PostalCode { get; set; }
    public string TaxType { get; set; }
    public string TaxTypeDescription { get; set; }
}
